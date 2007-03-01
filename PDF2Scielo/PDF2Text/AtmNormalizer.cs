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
		
	public AtmNormalizer (string source)
	{
		StringEncoding encoder = new StringEncoding (source);
		text = encoder.GetStringUnicode ();
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public string RemoveHeaders ()
	{
		// Remueve encabezados y numeros de pagina.
		Match [] matches;
		matches = GetMatches (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+");
		ReplacePattern (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+", "\n");
		
	 	#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		

		
		matches = GetMatches (@"[\n]+[\u000c]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]*[0-9]+[\n]+");
		ReplacePattern (@"[\n]+[\u000c]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]*[0-9]+[\n]+", "\n");
		
	 	#if DEBUG
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif		

	 	return text;
	}
	
	public string RemovePattern (string regexp)
	{
		return ReplacePattern (regexp, String.Empty);
	}
	
	public string MarkText ()
	{
		MarkMajorSections ();
		MarkMinorSections ();
		MarkParagraphs ();
		
		#if DEBUG
		// Aqui se busca posibles alteraciones en el texto ie que se eliminen espacios.
		Match [] matches;
		matches = GetMatches (@"[ \n]+[a-z]+[A-Z]+[a-zA-Z]*[ \n]+");
		foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		
		return text;
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public string ReplacePattern (string regexp, string substitute)
	{
		Regex regex = new Regex (regexp);	
		text = regex.Replace (text, substitute);
		
		return text;
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
	
	public Match [] GetMatches (string regexp)
	{
		Match [] result;
		Regex regex = new Regex (regexp);
		MatchCollection matches = regex.Matches (text);
		
		result = new Match [matches.Count];
		matches.CopyTo (result, 0);
		
		return result;
	}
	
	
	public string Text {
		get {
			return text;
		}
	}
     	
     	private void MarkMajorSections ()
     	{
     		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
		ReplacePattern (@"[\n]+[ ]+RESUMEN[\n]+", "\n[res] Resumen [/res]\n");
		ReplacePattern (@"[\n]+[ ]+ABSTRACT[\n]+", "\n[abs] Abstract [/abs]\n");
		ReplacePattern (@"[\n]+References\n", "\n[ref] References [/ref]\n");
		ReplacePattern (@"[\n]+Acknowledgements\n", "\n[ack] Acknowledgements [/ack]\n");
		
		//Etiquetado de KEYWORD.		
		Match [] matches;
		matches = GetMatches (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z,;.&\u002d ]+");
		
		string result, old;
		old = matches [0].Value;
		result = old.Trim ();
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar la seccion Keyword.");
		Console.WriteLine ("MATCH: " + result);
		#endif
		
		result = "\n[key] " + result + " [/key].\n";
		ReplacePattern (old, result);
     	}
     	
     	private void MarkMinorSections ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de las secciones del tipo <num>. <string>
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las secciones.");
		#endif
     		
     		matches = GetMatches (@"[\n]+[0-9][.][ ].*\n");
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[sec] " + result + " [/sec]\n";
			ReplacePattern (old, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>[.] <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[0-9][.][0-9]+[.]*[ ].*\n");
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[subsec] " + result + " [/subsec]\n";
			ReplacePattern (old, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>.<num> <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsubsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[0-9][.][0-9]+[.][0-9][.]*.*\n");
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[subsubsec] " + result + " [/subsubsec]\n";
			ReplacePattern (old, result);
		}
     	}
     	
     	private void MarkParagraphs ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de los parrafos.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar los parrafos.");
		#endif
     		
     		matches = GetMatches (@"[.][\n]*[ ]{3,4}[A-Z].*");
		foreach (Match m in matches) {
			string result, old;
			old = m.Value;
			result = old.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
//			result = "\n[para] " + result + " [/para]\n";
//			ReplacePattern (old, result);
		}
     	}	
}
}
}