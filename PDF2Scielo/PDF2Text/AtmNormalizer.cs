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
		
	public AtmNormalizer (string source, string format)
	{
		// Construimos el XML reader para obtener las regexp.
		StyleReader style = new StyleReader (format);
		
		StringEncoding encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
		
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
		StyleReader style = new StyleReader (document.Format);
		
		StringEncoding encoder = new StringEncoding (document.GetText ());
		encoder.ReplaceCodesTable (StringEncoding.CharactersDefault);
		text = encoder.GetStringUnicode ();
		
		RemoveHeaders ();
		MarkMajorSections ();
		GetBlocks ();
		RemoveExtras ();
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
		Match [] matches;
		
		// Remueve encabezados y numeros de pagina.
	 	#if DEBUG
	 	matches = GetMatches (@"[\n]+[\u000c]+[0-9]+[ ]*[-a-zA-Z. \u00f1\u002f\u0050-\u00ff’,()&;]+[\n]+", text);
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar los encabezados y numeros de pagina"); 	
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		
		GlobalReplaceRegex (@"[\n]+[\u000c]+[0-9]+[ ]*[-a-zA-Z. \u00f1\u002f\u0050-\u00ff’,()&;]+[\n]+", "\n");
		
		// Remueve texto muerto (ie. ejes de graficas) dejados al extraer el texto.
	 	#if DEBUG
	 	matches = GetMatches (@"[\n]+[\u000c]+[ ]*[-a-zA-Z0-9. \u00f1\u002f\u0050-\u00ff#’,()&;]+", text);
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		
		GlobalReplaceRegex (@"[\n]+[\u000c]+[ ]*[-a-zA-Z0-9. \u00f1\u002f\u0050-\u00ff#’,()&;]+[ ]*[\n]*[0-9]*[\n]+", "\n");
	}
	
	private void MarkMajorSections ()
     	{
     		string smatch, result;
     		Match match;
     		Match [] matches;
     		
     		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
     		// FIXME: No marca todos los articulos con [ack] y [/ack]. Un ejemplo que encontramos es que el patron debe ser:Acknowledgement
     		// Sin embargo también hay casos, que con los ya dados, tampoco cacha. Ejemplo: v17n2a01.pdf	
		GlobalReplaceRegex (@"[\n]+[ ]+RESUMEN[\n]+", "\n[res] Resumen [/res]\n");
		GlobalReplaceRegex (@"[\n]+[ ]+ABSTRACT[\n]+", "\n[abs] Abstract [/abs]\n");
		GlobalReplaceRegex (@"[\n]+References\n", "\n[ref] References [/ref]\n");
		GlobalReplaceRegex (@"[\n]+[ ]*(Acknowledgement|Acknowledgment)[s]?\n", "\n[ack] Acknowledgements [/ack]\n");
		
		//Etiquetado de KEYWORD.		
		matches = GetMatches (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z,;.&\u002d \n]+[\n]+", text);
		match = matches [0];
		smatch = match.Value;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar la seccion Keyword.");
		Console.WriteLine ("MATCH: " + smatch);
		#endif
		
		result = "\n[key] " + smatch.Trim () + " [/key]\n\n";
		text = text.Replace (smatch, result);
     	}
	
	private void GetBlocks ()
	{
		string temp;
		Match [] matches;
		matches = GetMatches (@"Atm(.|\s)* \[/key\]\n", text);
		front = matches [0].Value;
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el front"); 	
		Console.WriteLine ("MATCH: " + front);
		#endif
		
		matches = GetMatches (@"\[/key\](.|\s)*\[ref\]", text);
		temp = matches [0].Value;
		
		// FIXME: Documentar o hacer mejor la extracion de body.
		body = temp.Substring (7, temp.Length - 12);
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el body"); 	
	 	Console.WriteLine ("MATCH: " + body);
		#endif
		
		matches = GetMatches (@"\[ref\](.|\s)*", text);
		back = matches [0].Value;
		
		#if DEBUG
	 	Console.WriteLine ("DEBUG: Resultados obtenidos para obtener el back"); 	
	 	Console.WriteLine ("MATCH: " + back);
		#endif
	}
	
	private void RemoveExtras ()
	{
		Match [] matches;
		
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para eliminar texto muerto");
		#endif
		
		matches = GetMatches (@"\n[ ]*[-0-9.]+\n", body);
		foreach (Match m in matches) {
			string smatch;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			body = body.Replace (smatch, "\n");
		}
	}
	
	private void MarkTitle ()
	{
		Match [] matches;
		
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar el titulo del articulo.");
		#endif
		
		matches = GetMatches (@"^Atm.*[\n]+[^|]+?\n[\n]+", front);
		foreach (Match m in matches) {
			int index;
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			index = smatch.IndexOf ("\n");
			result = "[title] " +  smatch.Substring (index).Trim () + " [/title]\n";
			front = front.Replace (smatch, result);
		}
	}

	private void MarkDate ()
	{
		Match [] matches;
		
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar la fecha del articulo.");
		#endif
		
		matches = GetMatches (@"\n[ ]+Received.*\n", front);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "\n[date] " +  smatch.Trim () + " [/date]\n";
			front = front.Replace (smatch, result);
		}
	}
	
	private void MarkAuthors ()
	{
		Match [] matches;
		
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar los autores del articulo.");
		#endif
		
		matches = GetMatches (@"[ ]*(([A-Z]{1,2}\. ([A-Z]{1,2}[.]? )*[-A-Z][-a-zA-Z&;]+( [-a-zA-Z&;]+)?)(, | and )?)+[\n]+", front);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif

			result = "[author] " +  smatch.Trim () + " [/author]\n";
			front = front.Replace (smatch, result);
		}
	}
	
	private void MarkAff ()
	{
		Match [] matches;
		
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para marcar las afiliaciones de los autores del articulo.");
		#endif
		
		matches = GetMatches (@"\[/author\]\n(.|\n)+?(\[author\]|\[date\]|\[res\])", front);
		foreach (Match m in matches) {
			int index;
			string smatch, tag, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif

			index = smatch.IndexOf ("\n");
			result = smatch.Substring (index);
			index = result.LastIndexOf ("[");
			tag = result.Substring (index);
			result= "[/author]\n[aff] " + result.Substring (0, index).Trim () + " [/aff]\n" + tag;
			front = front.Replace (smatch, result);
		}
	}
	
     	private void MarkMinorSections ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de las secciones del tipo <num>. <string>
     		// FIXME: No se estan agarrando las secciones que son mayores a una linea de texto.
     		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las secciones.");
		#endif
     		
     		matches = GetMatches (@"[\n]+[0-9]+[.][ ].*\n", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "\n[sec] " + smatch.Trim () + " [/sec]\n";
			body = body.Replace (smatch, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>[.] <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[ ]*[0-9][.][0-9]+[.]*[ ].*\n", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "\n[subsec] " + smatch.Trim () + " [/subsec]\n";
			body = body.Replace (smatch, result);
		}
		
		// Etiquetado de las secciones del tipo <letter>) <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsecciones alternas.");
		#endif
		
		matches = GetMatches (@"[\n]+[a-z]\) .*\n", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "\n[subsec] " + smatch.Trim () + " [/subsec]\n";
			body = body.Replace (smatch, result);
		}
		
		// Etiquetado de las secciones del tipo <num>.<num>.<num> <string>
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar las subsubsecciones.");
		#endif
		
		matches = GetMatches (@"[\n]+[0-9][.][0-9]+[.][0-9][.]*.*\n", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "\n[subsubsec] " + smatch.Trim () + " [/subsubsec]\n";
			body = body.Replace (smatch, result);
		}
     	}
     	
     	private void MarkParagraphs ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de los parrafos.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para capturar los parrafos.");
		#endif
     		
     		matches = GetMatches (@"[\n]+[ ]{3,5}[A-Zi].*", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = smatch.TrimStart ();
			result = "\n[para] " + result;

			body = body.Replace (smatch, result);
		}
		
		
		matches = GetMatches (@"(\[sec\]|\[subsec\]|\[subsubsec\]).*\n", body); 
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "[para] " + smatch.TrimStart ();
			body = body.Replace (smatch, result);
		}
		
 		
		matches = GetMatches (@"\n(\[para\]|\[sec\]|\[subsec\]|\[subsubsec\]|\[ack\]).*", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			// FIXME: Caso para no poner un [/para] extra antes de la primera seccion.
			if (smatch.StartsWith ("\n[para] [sec] 1. "))
				continue;
			
			result = " [/para]" + smatch;
			body = body.Replace (smatch, result);
		}
     	}
     	
     	private void MarkCitations ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de las citas.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para las citas.");
		#endif
		
		matches = GetMatches (@"[\n]+[A-Z].*", back);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value.TrimStart ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = "[cit] " + smatch ;
			back = back.Replace (smatch, result);
		}
		
		matches = GetMatches (@"\[cit\][^[]*", back);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + smatch);
			#endif
			
			result = smatch.TrimEnd ();
			result = result + " [/cit]\n";
			back = back.Replace (smatch, result);
		}
     	}
     	
     	private void MarkFootFigure ()
     	{
     		Match [] matches;
     		
     		// Etiquetado de los pies de figura.
		#if DEBUG
		Console.WriteLine ("DEBUG: Resultados obtenidos para los pies de figura.");
		#endif
     		
     		matches = GetMatches (@"[\n]+[ ]*Fig[.][ ]?[0-9]+[.] .*", body);
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH figura: " + smatch);
			#endif
			
			result = smatch.TrimStart ();
			result = "\n[fig] " + result;
			
			body = body.Replace (smatch, result);
		}
		
		matches = GetMatches (@"\[fig\] Fig[.][ ]?[0-9]+[.] [-a-zA-Z0-9.,:;´&#()/ \n\u00f6]*?[.]\n", body);		
		foreach (Match m in matches) {
			string smatch, result;
			smatch = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH figura2: " + smatch);
			#endif
			
			result = smatch.TrimEnd ();
			result = result + " [/fig]\n";
			
			body = body.Replace (smatch, result);
		}
     	}
}
}
}