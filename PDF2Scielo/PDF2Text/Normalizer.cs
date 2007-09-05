//
// Normalizer: Una clase que implementa la interface INormalizable, usa documentos de estilo
// para obtener las expresiones regulares a ejecutar y asi obtener un NormDocument.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using Scielo.Utils;
using System.Xml;
using System.IO;

namespace Scielo.PDF2Text {
public class Normalizer : INormalizable {
	private string text;
	private string front;
	private string body;
	private string back;
	private string format;
	private Rule [] rules;
		
	public Normalizer (string source, string format)
	{
		// Construimos un StyleReader para obtener las regexp.
		StyleReader style = new StyleReader (format);
		rules = style.GetRules ();
		
		EncodeText (source);
	}
	
	public Normalizer (string source, Rule[] rules)
	{
		// No se construye un StyleReader puesto que se recibe las regexp a usar
		// mediante el arreglo rules, por ende no puede ser nulo.
		if (rules == null)
			throw new ArgumentNullException ("Error: El arreglo rules no puede ser null.");
		
		this.rules = rules;
		EncodeText (source);
	}
	
	public Normalizer (RawDocument document, string format)
	{
		// Construimos un StyleReader para obtener las regexp.
		StyleReader style = new StyleReader (format);
		this.format = format;
		rules = style.GetRules ();
		
		// Si el estilo tiene mas de una columna se rompe y se convierte a una
		// sola columna.
		if (style.GetNumColumns () > 1)
			document.BreakColumns ();
		
		EncodeText (document.GetText ());
	}
	
	private void EncodeText (string source)
	{
		StringEncoding encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		this.text = encoder.GetStringUnicode ();
	}
	
	public void ApplyRule (Rule rule)
	{
		string result, source = "";
		StringMatchCollection matches;
		
		switch (rule.Block) {
		case BlockType.GLOBAL:
			source = text;
			break;
		case BlockType.FRONT:
			source = front;
			break;
		case BlockType.BODY:
			source = body;
			break;
		case BlockType.BACK:
			source = back;
			break;
		}
		
		Logger.Log (Level.INFO, "Aplicando regla: {0} en bloque {1}", rule.Name, rule.Block);
		
		matches = new StringMatchCollection (rule.Regexp, source);
		
		if (rule.UniqueMatch) {
			if (matches.Count == 0) {
				switch (rule.Sustitution) {
				case "#{Front}":
				case "#{Body}":
				case "#{Back}":
					Logger.Log (Level.ERROR, "La regla necesaria {0} no obtuvo resultados", rule.Name);
					throw new Exception ("La regla necesaria " + rule.Name + 
						" no obtuvo resultados. "+
						"La normalización se ha cancelado");
				default:
					Logger.Log (Level.WARNING, "No se encontraron matches con la regla {0}", rule.Name);
					break;
				}
				
				return;
			} else if (matches.Count > 1) {
				Logger.Log (Level.WARNING, "Se encontró mas de un match en la regla {0}, se tomó el primero", rule.Name);
			}
			
			StringMatch match = matches [0];
			Logger.Log (Level.DEBUG, "Match: {0}", match.FullMatch);
			
			if (rule.Type == RuleType.STATIC) {
				result = rule.Sustitution;
				source = source.Replace (match.FullMatch, result);
				
				Logger.Log (Level.DEBUG, "Result: {0}", result);
			} else {
				result = match.ApplyModifiers (rule.Modifiers, rule.Type);
				switch (rule.Sustitution) {
				case "#{Front}":
					front = result;
					break;
				case "#{Body}":
					body = result;
					break;
				case "#{Back}":
					back = result;
					break;
				case "#{Result}":
					source = source.Replace (match.FullMatch, result);
					break;
				}
				
				Logger.Log (Level.DEBUG, "Result: {0}", result);
			}
		} else {
			Logger.Log (Level.INFO, "Total Matches: {0}", matches.Count );
			foreach (StringMatch m in matches) {
				Logger.Log (Level.DEBUG, "Match: {0}", m.FullMatch);
				
				if (rule.Type == RuleType.STATIC)
					result = rule.Sustitution;
				else
					result = m.ApplyModifiers (rule.Modifiers, rule.Type);
				
				source = source.Replace (m.FullMatch, result);
				Logger.Log (Level.DEBUG, "Result: {0}", result);
			}
		}
		
		switch (rule.Block) {
		case BlockType.GLOBAL:
			text = source;
			break;
		case BlockType.FRONT:
			front = source;
			break;
		case BlockType.BODY:
			body = source;
			break;
		case BlockType.BACK:
			back = source;
			break;
		}
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public string MarkText ()
	{
		foreach (Rule rule in rules) 
			ApplyRule (rule);
		
		return Text;
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public NormDocument CreateNormDocument ()
	{
		MarkText ();
		return new NormDocument (front, body, back, format);
	}
	
	public string Front {
		get {
			return front;
		}
	}
	
	public string Body {
		get {
			return body;
		}
	}
	
	public string Back {
		get {
			return back;
		}
	}
		
	public string Text {
		get {
			if (Front != null && Body != null && Back != null)
				text = Front + Body + Back;
			
			return text;
		}
	}
	
	public string Format {
		get {
			return format;
		}
	}
}
}