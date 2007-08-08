//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
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
public class AtmNormalizer : INormalizable {
	private XmlDocument xml_document;
	private string text;
	private string front;
	private string body;
	private string back;
	private const string ALET = @"\w";
	private const string ASYM = @"\p{S}";
	private const string APUC = @"\p{P}";
		
	public AtmNormalizer (string source, string format)
	{
		// Construimos el XML reader para obtener las regexp.
		//StyleReader style = new StyleReader (format);
		
		StringEncoding encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
		
		xml_document = new XmlDocument ();
		string style_source = Path.Combine (Test.PathOfTest (), "test-schema.xml");
		XmlTextReader reader = new XmlTextReader (style_source);
		xml_document.Load (reader);
		
		RemoveHeaders ();
		MarkMajorSections ();
		GetBlocks ();
		RemoveExtras ();
	}
	
	// TODO: Hay que hacer general esta clase tal que usando el string format
	// que define el formato del documento se elija el archivo XML con las ER
	// correspondientes.
	public AtmNormalizer (RawDocument document)
	{
		// Construimos el XML reader para obtener las regexp.
		//StyleReader style = new StyleReader (document.Format);
		
		StringEncoding encoder = new StringEncoding (document.GetText ());
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
		
		xml_document = new XmlDocument ();
		string style_source = Path.Combine (Test.PathOfTest (), "test-schema.xml");
		XmlTextReader reader = new XmlTextReader (style_source);
		xml_document.Load (reader);
		
		RemoveHeaders ();
		MarkMajorSections ();
		GetBlocks ();
		RemoveExtras ();
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
		
		matches = new StringMatchCollection (rule.Regexp, source);
		
		if (rule.UniqueMatch) {
			if (matches.Count == 0) {
				Console.WriteLine ("Advertencia: No se encontraron matches con la regla " + rule.Name);
				return;
			} else if (matches.Count > 1) {
				Console.WriteLine ("Advertencia: Se encontró más de un match en la regla " + rule.Name + ", se tomó el primero");
			}
			StringMatch match = matches [0];
			if (rule.Type == RuleType.STATIC)
				result = rule.Sustitution;
			else
				result = match.ApplyModifiers (rule.Modifiers, rule.Type);
			source = source.Replace (match.FullMatch, result);
		} else {
			Console.WriteLine ("Test matches: " + matches.Count);
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
		MarkTitle ();
		MarkDate ();
		MarkAuthors ();
		MarkAff ();
		MarkMinorSections ();
		MarkFootFigure ();
		MarkParagraphs ();
		MarkCitations ();
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
	
	private void RemoveHeaders ()
	{
		// Remueve encabezados y numeros de pagina.
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/global/rule[1]");
		Rule rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[2]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
	}
	
	private void MarkMajorSections ()
	{
		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
		// FIXME: No marca todos los articulos con [ack] y [/ack]. Un ejemplo que encontramos es que el patron debe ser:Acknowledgement
		// Sin embargo también hay casos, que con los ya dados, tampoco cacha. Ejemplo: v17n2a01.pdf
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/global/rule[3]");
		Rule rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[4]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[5]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[6]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[7]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		ApplyRule (rule);
	}
	
	private void GetBlocks ()
	{
		StringMatchCollection matches;
		matches = new StringMatchCollection (@"^(.|\s)* \[/key\]\n", text);
		front = matches [0].FullMatch;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el front"); 	
		Console.WriteLine ("MATCH: " + front);
		#endif
		
		// NOTE: "?:" Sirve para que no tome un grupo como un backreference
		matches = new StringMatchCollection (@"\[/key\](?<Result>(?:.|\s)*)\[ref\]", text);
		body = matches [0].ResultMatch;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el body"); 	
		Console.WriteLine ("MATCH: " + body);
		#endif
		
		matches = new StringMatchCollection (@"\[ref\](.|\s)*", text);
		back = matches [0].FullMatch;
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el back"); 	
	 	Console.WriteLine ("MATCH: " + back);
		#endif
	}
	
	private void MarkTitle ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/front/rule[1]");
		Rule rule = new Rule (ruleNode, BlockType.FRONT);
		ApplyRule (rule);
	}
	
	private void MarkDate ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/front/rule[2]");
		Rule rule = new Rule (ruleNode, BlockType.FRONT);
		ApplyRule (rule);
	}
	
	private void MarkAuthors ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/front/rule[3]");
		Rule rule = new Rule (ruleNode, BlockType.FRONT);
		ApplyRule (rule);
	}
	
	private void MarkAff ()
	{
		StringMatchCollection matches;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar las afiliaciones de los autores del articulo.");
		#endif
		
		matches = new StringMatchCollection (@"\[/author\]\n(?<Result>(.|\n)+?)(\[author\]|\[date\]|\[res\])", front);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			int index = match.FullMatch.LastIndexOf ("[");
			string tag = match.FullMatch.Substring (index);
			string result= "[/author]\n[aff] " + match.ResultMatch.Trim () + " [/aff]\n" + tag;
			front = front.Replace (match.FullMatch, result);
		}
	}
	
	private void RemoveExtras ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/body/rule[1]");
		Rule rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
	}
	
	private void MarkMinorSections ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/body/rule[2]");
		Rule rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[3]");
		rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[4]");
		rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[5]");
		rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
	}
	
	private void MarkParagraphs ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/body/rule[6]");
		Rule rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[7]");
		rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
//		
//		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[8]");
//		rule = new Rule (ruleNode, BlockType.BODY);
//		ApplyRule (rule);
		StringMatchCollection matches;
		
		matches = new StringMatchCollection (@"\n(\[para\]|\[sec\]|\[subsec\]|\[subsubsec\]|\[ack\]).*", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			// FIXME: Caso para no poner un [/para] extra antes de la primera seccion.
			if (match.FullMatch.StartsWith ("\n[para] [sec] 1. "))
				continue;
			
			string result = " [/para]" + match.FullMatch;
			body = body.Replace (match.FullMatch, result);
		}
	}
	
	private void MarkFootFigure ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/body/rule[9]");
		Rule rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/body/rule[10]");
		rule = new Rule (ruleNode, BlockType.BODY);
		ApplyRule (rule);
	}
	
	private void MarkCitations ()
	{
		StringMatchCollection matches;
		
		// Etiquetado de las citas.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para las citas.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+(?<Result>[A-Z].*)", back);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.ResultMatch);
			#endif
			
			string result = "[cit] " + match.ResultMatch;
			back = back.Replace (match.ResultMatch, result);
		}
		
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/back/rule[2]");
		Rule rule = new Rule (ruleNode, BlockType.BACK);
		ApplyRule (rule);
	}
}
}