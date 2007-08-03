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
public class StringMatchCollection : IEnumerable {
	private ArrayList matches;
	
	public StringMatchCollection (string regexp, string source)
	{
		matches = GetMatches (regexp, source);
	}
	
	// Implementacion privada de StringMatchCollectionEnumerator que implementa IEnumerator.
	private class StringMatchCollectionEnumerator : IEnumerator {
		private StringMatchCollection match_collection;
		private int index;
		
		public StringMatchCollectionEnumerator (StringMatchCollection collection)
		{
			match_collection = collection;
			index = -1;
		}
		
		public bool MoveNext ()
		{
			index++;
			if (index >= match_collection.matches.Count)
				return false;
			else
				return true;
		}
		
		public void Reset ()
		{
			index = -1;
		}
		
		public object Current {
			get {
				return match_collection [index];
			}
		}
	}
	
	// NOTE: Para implementar la interfase IEnumerable es necesario implementar
	// el metodo GetEnumerator ().
	public IEnumerator GetEnumerator ()
	{
		return (IEnumerator) new StringMatchCollectionEnumerator (this);
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
	
	public int Count {
		get {
			return matches.Count;
		}
	}
	
	public StringMatch this [int index] {
		get {
			return (StringMatch) matches [index];
		}
	}
}
}
