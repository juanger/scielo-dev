//
// StringMatch: A class that implements objects with matches to regular expressions.
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

namespace Scielo.PDF2Text {
public class StringMatch {
	
	string all;
	string named;
	
	public StringMatch (string all, string named)
	{
		if (all == null || named == null)
			throw new ArgumentNullException ("Error: no match for one regular expression.");
		
		this.all = all;
		this.named = named;
	}
	
	public string FullMatch {
		get {
			return all;
		}
	}
	
	public string ResultMatch {
		get {
			return named;
		}
	}
	
	public bool HasResultMatch ()
	{
		return !named.Equals (String.Empty);
	}
}
}
