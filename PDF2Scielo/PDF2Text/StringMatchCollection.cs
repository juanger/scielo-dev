//
// StringMatchCollection: A class that implements a collection
// of StringMatches from the same regular expression.
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
using System.Collections;
using System.Text.RegularExpressions;

namespace Scielo.PDF2Text {
public class StringMatchCollection {
	public ArrayList matches;
	
	public StringMatchCollection (string regexp, string source)
	{
		matches = GetMatches (regexp, source);
	}
	
	private ArrayList GetMatches (string regexp, string source)
	{
		ArrayList result;
		Regex regex = new Regex (regexp);
		MatchCollection matches = regex.Matches (source);
		
		result = new  ArrayList(matches.Count);
		foreach (Match match in matches)
		{
			string all = match.Groups[0].Value;
			string named = match.Groups["Result"].Value;
			result.Add(new StringMatch(all, named));
		}
		return result;
	}
	
	public StringMatch Item (int index)
	{
		return (StringMatch) matches [index];
	}
	
	public StringMatch this [int index] {
		get {
			return (StringMatch) matches [index];
		}
	}
}
}
