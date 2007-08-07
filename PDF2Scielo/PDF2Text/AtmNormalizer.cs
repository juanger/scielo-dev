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
	
	public void Eval (Rule rule)
	{
		string result;
		StringMatchCollection matches;
		if (rule.Block == BlockType.GLOBAL) {
			if (rule.Type == RuleType.STATIC) {
				GlobalReplaceRegex (rule.Regexp, rule.Sustitution);
			} else if (rule.Type == RuleType.FULL) {
				matches = new StringMatchCollection (rule.Regexp, text);
				
				if (rule.UniqueMatch) {
					StringMatch match = matches [0];
					result = match.ApplyModifiers (rule.Modifiers, rule.Type);
					Console.WriteLine ("Test Eval: " + result);
					text = text.Replace (match.FullMatch, result);
				} else {
					foreach (StringMatch m in matches) {
						result = m.ApplyModifiers (rule.Modifiers, rule.Type);
						text.Replace (m.FullMatch, result);
					}
				}
			}
		} else if (rule.Block == BlockType.FRONT) {
			if (rule.Type == RuleType.STATIC) {
			} else if (rule.Type == RuleType.FULL) {
			} else {
				Console.WriteLine ("Test: " + rule.Regexp);
				Console.WriteLine (front);
				matches = new StringMatchCollection (rule.Regexp, front);
				
				if (rule.UniqueMatch) {
					if (matches.Count == 0) {
						Console.WriteLine ("Advertencia: No se encontraron matches con la regla " + rule.Name);
						return;
					} else if (matches.Count > 1) {
						Console.WriteLine ("Advertencia: Se encontró más de un match en la regla " + rule.Name + ", se tomó el primero");
					}
					StringMatch match = matches [0];
					result = match.ApplyModifiers (rule.Modifiers, rule.Type);
					front = front.Replace (match.FullMatch, result);
				} else {
					foreach (StringMatch m in matches) {
						result = m.ApplyModifiers (rule.Modifiers, rule.Type);
						front.Replace (m.FullMatch, result);
					}
				}
			}
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
	
	private void GlobalReplaceRegex (string regexp, string substitute)
	{
		Regex regex = new Regex (regexp);	
		text = regex.Replace (text, substitute);
	}
	
	private void RemoveHeaders ()
	{
		// Remueve encabezados y numeros de pagina.
		#if DEBUG
		StringMatchCollection matches;
		matches = new StringMatchCollection (@"[\n]+[\u000c]+[0-9]+[ ]*[ " + ALET + ASYM + APUC + "]+[\n]+", text);
		Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	foreach (StringMatch match in matches) {
			Console.WriteLine ("MATCH: " + match.FullMatch);
		}
		#endif
		
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/global/rule[1]");
		Rule rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
		
		#if DEBUG
		matches = new StringMatchCollection (@"[\n]+[\u000c]+[ ]*[0-9 " + ALET + ASYM + APUC + "]+[ ]*[\n]*[0-9]*[\n]+", text);
		foreach (StringMatch match in matches) {
			Console.WriteLine ("MATCH: " + match.FullMatch);
		}
		#endif
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[2]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
	}
	
	private void MarkMajorSections ()
	{
		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
		// FIXME: No marca todos los articulos con [ack] y [/ack]. Un ejemplo que encontramos es que el patron debe ser:Acknowledgement
		// Sin embargo también hay casos, que con los ya dados, tampoco cacha. Ejemplo: v17n2a01.pdf
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/global/rule[3]");
		Rule rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[4]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[5]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[6]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
		
		ruleNode = xml_document.SelectSingleNode ("/style/global/rule[7]");
		rule = new Rule (ruleNode, BlockType.GLOBAL);
		Eval (rule);
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
	
	private void RemoveExtras ()
	{
		StringMatchCollection matches;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar texto muerto");
		#endif
		
		matches = new StringMatchCollection (@"\n[ ]*[-0-9.]+\n", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			body = body.Replace (match.FullMatch, "\n");
		}
	}
	
	private void MarkTitle ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/front/rule[1]");
		Rule rule = new Rule (ruleNode, BlockType.FRONT);
		Eval (rule);
	}
	
	private void MarkDate ()
	{
		XmlNode ruleNode = xml_document.SelectSingleNode ("/style/front/rule[2]");
		Rule rule = new Rule (ruleNode, BlockType.FRONT);
		Eval (rule);
//		StringMatchCollection matches;
//		
//		#if DEBUG
//		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar la fecha del articulo.");
//		#endif
//		
//		matches = new StringMatchCollection (@"\n[ ]+(?<Result>Received.*)\n", front);
//		foreach (StringMatch match in matches) {
//			
//			#if DEBUG
//			Console.WriteLine ("MATCH: " + match.FullMatch);
//			#endif
//			
//			string result = "\n[date] " + match.ResultMatch + " [/date]\n";
//			front = front.Replace (match.FullMatch, result);
//		}
	}
	
	private void MarkAuthors ()
	{
		StringMatchCollection 
		matches;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar los autores del articulo.");
		#endif
		
		matches = new StringMatchCollection (@"[ ]*(?<Result>(([A-Z]{1,2}\. ([A-Z]{1,2}[.]? )*[-A-Z][-a-zA-Z&;]+( [-a-zA-Z&;]+)?)(, | and )?)+)[\n]+", front);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "[author] " +  match.ResultMatch + " [/author]\n";
			front = front.Replace (match.FullMatch, result);
		}
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
	
	private void MarkMinorSections ()
	{
		StringMatchCollection matches;
		
		// Etiquetado de las secciones del tipo <num>. <string>
		// FIXME: No se estan agarrando las secciones que son mayores a una linea de texto.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las secciones.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[0-9]+[.][ ].*\n", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "\n[sec] " + match.FullMatch.Trim () + " [/sec]\n";
			body = body.Replace (match.FullMatch, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>[.] <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[ ]*[0-9][.][0-9]+[.]*[ ].*\n", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "\n[subsec] " + match.FullMatch.Trim () + " [/subsec]\n";
			body = body.Replace (match.FullMatch, result);
		}
		
		// Etiquetado de las secciones del tipo <letter>) <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones alternas.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[a-z]\) .*\n", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "\n[subsec] " + match.FullMatch.Trim () + " [/subsec]\n";
			body = body.Replace (match.FullMatch, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>.<num> <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsubsecciones.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[0-9][.][0-9]+[.][0-9][.]*.*\n", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "\n[subsubsec] " + match.FullMatch.Trim () + " [/subsubsec]\n";
			body = body.Replace (match.FullMatch, result);
		}
	}
	
	private void MarkParagraphs ()
	{
		StringMatchCollection matches;
		
		// Etiquetado de los parrafos.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar los parrafos.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[ ]{3,5}(?<Result>[A-Zi].*)", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "\n[para] " + match.ResultMatch;
			body = body.Replace (match.FullMatch, result);
		}
		
		matches = new StringMatchCollection (@"(\[sec\]|\[subsec\]|\[subsubsec\]).*\n", body); 
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = "[para] " + match.FullMatch;
			body = body.Replace (match.FullMatch, result);
		}
		
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
		
		matches = new StringMatchCollection (@"\[cit\][^[]*", back);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + match.FullMatch);
			#endif
			
			string result = match.FullMatch.TrimEnd () + " [/cit]\n";
			back = back.Replace (match.FullMatch, result);
		}
	}
	
	private void MarkFootFigure ()
	{
		StringMatchCollection matches;
		
		// Etiquetado de los pies de figura.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para los pies de figura.");
		#endif
		
		matches = new StringMatchCollection (@"[\n]+[ ]*(?<Result>Fig[.][ ]?[0-9]+[.] .*)", body);
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH figura: " + match.FullMatch);
			#endif
			
			string result = "\n[fig] " + match.ResultMatch;
			
			body = body.Replace (match.FullMatch, result);
		}
		
		matches = new StringMatchCollection (@"(?<Result>\[fig\] Fig[.][ ]?[0-9]+[.] [-a-zA-Z0-9.,:;´&#()/ \n\u00f6]*?[.])\n", body);		
		foreach (StringMatch match in matches) {
			
			#if DEBUG
			Console.WriteLine ("MATCH figura2: " + match.FullMatch);
			#endif
			
			string result = match.ResultMatch + " [/fig]\n";
			
			body = body.Replace (match.FullMatch, result);
		}
	}
}
}