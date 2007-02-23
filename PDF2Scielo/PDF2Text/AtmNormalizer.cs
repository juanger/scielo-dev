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
	private StringEncoding encoder;
		
	public AtmNormalizer (string source)
	{
		encoder = new StringEncoding (source);
		encoder.ReplaceCodesTable();
		text = encoder.GetStringUnicode ();
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public string RemoveHeaders ()
	{
		#if DEBUG
		Match [] matches;
		matches = GetMatches (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+");
	 		
	 	foreach (Match m in matches) {
			Console.WriteLine ("MATCH: " + m.Value);
		}
		#endif
		
		// Remueve encabezados y numeros de pagina usando ReplacePattern.
	 	return ReplacePattern (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()&;]+[\n]+", "\n");
	}
	
	public string RemovePattern (string regexp)
	{
		return ReplacePattern (regexp, String.Empty);
	}
	
	public string MarkSections ()
	{
		// Etiquetado de RESUMEN, ABSTRACT, REFERENCES y ACKNOWLEDGEMENTS.
		ReplacePattern (@"[\n]+RESUMEN\n", "\n[res] Resumen [/res]\n");
		ReplacePattern (@"[\n]+ABSTRACT\n", "\n[abs] Abstract [/abs]\n");
		ReplacePattern (@"[\n]+References\n", "\n[ref] References [/ref]\n");
		ReplacePattern (@"[\n]+Acknowledgements\n", "\n[ack] Acknowledgements [/ack]\n");
		
		//Etiquetado de Keyword.		
		Match [] matches;
		matches = GetMatches (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z,;.&\u002d ]+");
		
		foreach (Match m in matches) {
			string result;
			result = m.Value.Trim ();
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
			
			result = "\n[key] " + result + " [/key].\n";
			ReplacePattern (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z,;.&\u002d ]+\n",
				result);
		}
		
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
		
		matches = GetMatches (@"[ ]+[a-z]+[A-Z]+[a-z]*[ ]+");
		foreach (Match m in matches) {
			string result;
			result = m.Value;
			
			#if DEBUG
			Console.WriteLine ("MATCH: " + result);
			#endif
		}
		
		return this.Text;
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
		//TODO: To be implemented.
		return null;
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
     	
      		
}
}
}