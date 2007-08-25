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
	private Rule [] rules;
		
	public Normalizer (string source, string format)
	{
		// Construimos el XML reader para obtener las regexp.
		StyleReader style = new StyleReader (format);
		rules = style.GetRules ();
		
		StringEncoding encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
	}
	
	// TODO: Hay que hacer general esta clase tal que usando el string format
	// que define el formato del documento se elija el archivo XML con las ER
	// correspondientes.
	public Normalizer (RawDocument document)
	{
		// Construimos el XML reader para obtener las regexp.
		StyleReader style = new StyleReader (document.Format);
		rules = style.GetRules ();
		
		StringEncoding encoder = new StringEncoding (document.GetText ());
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
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
		
		Logger.Log (Level.INFO, "Aplicando regla: {0}", rule.Name);
		
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
					//Console.WriteLine ("Advertencia: No se encontraron matches con la regla " + rule.Name);
					Logger.Log (Level.WARNING, "No se encontraron matches con la regla {0}", rule.Name);
					break;
				}
				
				return;
			} else if (matches.Count > 1) {
				Logger.Log (Level.WARNING, "Se encontró m{as de un match en la regla {0}, se tomó el primero", rule.Name);
				//Console.WriteLine ("Advertencia: Se encontró más de un match en la regla " + rule.Name + ", se tomó el primero");
			}
			StringMatch match = matches [0];
			
			if (rule.Type == RuleType.STATIC) {
				result = rule.Sustitution;
				source = source.Replace (match.FullMatch, result);
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
			}
		} else {
			//Console.WriteLine ("Test matches: " + matches.Count);
			Logger.Log (Level.INFO, "Matches: {0}", matches.Count );
			foreach (StringMatch m in matches) {
				if (rule.Type == RuleType.STATIC)
					result = rule.Sustitution;
				else
					result = m.ApplyModifiers (rule.Modifiers, rule.Type);
				source = source.Replace (m.FullMatch, result);
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
		return new NormDocument (front, body, back);
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
			text = Front + Body + Back;
			return text;
		}
	}
}
}