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
using System.Collections;

namespace Scielo {
namespace PDF2Text {
	
public class AtmNormalizer : INormalizable {

	private string source;
		
	public AtmNormalizer (string source)
	{
		this.source = source;
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public bool RemovePattern (string regexp)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool ReplacePattern (string regexp, string substitute)
	{
		//TODO: To be implemented.
		string newstring;
		Regex regex = new Regex (regexp);
		
		newstring = regex.Replace (source, substitute);

		
		return newstring != null? true: false;
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
	
	public string Text {
		get {
			return source;
		}
	}
}
}
}