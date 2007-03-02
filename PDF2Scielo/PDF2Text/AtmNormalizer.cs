//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {
	
public class AtmNormalizer : INormalizable {

	private string text;
	private string front;
	private string body;
	private string back;
		
	public AtmNormalizer (string source)
	{
		StringEncoding encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
		
		RemoveHeaders ();
		MarkMajorSections ();
		GetBlocks ();
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	

	
	public string RemovePattern (string regexp, string source)
	{
		return ReplacePattern (regexp, String.Empty, source);
	}
	
	public string MarkText ()
	{
		//MarkMajorSections ();
		MarkMinorSections ();
		MarkParagraphs ();
		
		return Text;
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public string ReplacePattern (string regexp, string substitute, string source)
	{
		Regex regex = new Regex (regexp);	
		return regex.Replace (source, substitute);
	}
	
	public string ReplaceFootNotes (string regexp)
	{
		//TODO: To be implemented.
		return null;
	}
	
	public string ReplaceChars (ArrayList rechar)
	{
		StringEncoding encoder = new StringEncoding (text);
		encoder.ReplaceCodesTable (rechar);
		text = encoder.GetStringUnicode ();
		
		return text;
	}
	
	public Match [] GetMatches (string regexp, string source)
	{
		Match [] result;
		Regex regex = new Regex (regexp);
		MatchCollection matches = regex.Matches (source);
		
		result = new Match [matches.Count];
		matches.CopyTo (result, 0);
		
		return result;
	}
	
	
	public string Text {
		get {
			text = front + body + back;
			return text;
		}
	}
	
	private void GlobalReplacePattern (string regexp, string substitute)
	{
		Regex regex = new Regex (regexp);	
		text = regex.Replace (text, substitute);
	}
	
	private void RemoveHeaders ()
	{
		Match [] matches;
		
		// Remueve encabezados y numeros de pagina.
		matches = GetMatches (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+", text);
		GlobalReplacePattern (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+", "\n");
		
	 	#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		

		
		matches = GetMatches (@"[\n]+[\u000c]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]*[0-9]+[\n]+", text);
		GlobalReplacePattern (@"[\n]+[\u000c]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]*[0-9]+[\n]+", "\n");
		
	 	#if DEBUG
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
	}
	
	private void MarkMajorSections ()
     	{
     		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
		GlobalReplacePattern (@"[\n]+[ ]+RESUMEN[\n]+", "\n[res] Resumen [/res]\n");
		GlobalReplacePattern (@"[\n]+[ ]+ABSTRACT[\n]+", "\n[abs] Abstract [/abs]\n");
		GlobalReplacePattern (@"[\n]+References\n", "\n[ref] References [/ref]\n");
		GlobalReplacePattern (@"[\n]+Acknowledgements\n", "\n[ack] Acknowledgements [/ack]\n");
		
		//Etiquetado de KEYWORD.		
		Match [] matches;
		matches = GetMatches (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z,;.&\u002d ]+", text);
		
		string result, old;
		old = matches [0].Value;
		result = old.Trim ();
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar la seccion Keyword.");
		Console.WriteLine ("MATCH: " + result);
		#endif
		
		result = "\n[key] " + result + " [/key].\n";
		GlobalReplacePattern (old, result);
     	}
	
	private void GetBlocks ()
	{
		string temp;
		Match [] matches;
		matches = GetMatches (@"Atm(.|\s)* \[/key\][.]\n", text);
		front = matches [0].Value;
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
		Console.WriteLine ("MATCH: " + front);
		#endif
		
		matches = GetMatches (@"\[/key\][.](.|\s)*\[ref\]", text);
		temp = matches [0].Value;
		body = temp.Substring (8, temp.Length - 13);
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	Console.WriteLine ("MATCH: " + body);
		#endif
		
		matches = GetMatches (@"\[ref\](.|\s)*", text);
		back = matches [0].Value;
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	Console.WriteLine ("MATCH: " + back);
		#endif
	}
     	

     	
     	private void MarkMinorSections ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de las secciones del tipo <num>. <string>
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las secciones.");
		#endif
     		
     		matches = GetMatches (@"[\n]+[0-9][.][ ].*\n", body);
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[sec] " + result + " [/sec]\n";
			body = ReplacePattern (old, result, body);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>[.] <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[0-9][.][0-9]+[.]*[ ].*\n", body);
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[subsec] " + result + " [/subsec]\n";
			body = ReplacePattern (old, result, body);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>.<num> <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsubsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[0-9][.][0-9]+[.][0-9][.]*.*\n", body);
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[subsubsec] " + result + " [/subsubsec]\n";
			body = ReplacePattern (old, result, body);
		}
     	}
     	
     	private void MarkParagraphs ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de los parrafos.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar los parrafos.");
		#endif
     		
     		matches = GetMatches (@"[.][\n]*[ ]{3,4}[A-Z].*", body);
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
//			result = "\n[para] " + result + " [/para]\n";
//			body = ReplacePattern (old, result, body);
		}
     	}	
}
}
}