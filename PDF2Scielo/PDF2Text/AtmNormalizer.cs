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
	
	public bool RemovePattern (string regexp)
	{
		return ReplacePattern (regexp, String.Empty);
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool ReplacePattern (string regexp, string substitute)
	{
		Regex regex = new Regex (regexp);
		
		text = regex.Replace (text, substitute);
		return text != null? true: false;
	}
	
	public bool ReplaceFootNotes (string regexp)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool ReplaceChars (ArrayList rechar)
	{
		//TODO: To be implemented.
		return false;
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