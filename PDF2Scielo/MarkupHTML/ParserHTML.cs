//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Alejandro Rosendo Robles. (rosendo69@hotmail.com)
//   Virginia Teodosio Procopio. (ainigriv_t@hotmail.com)
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Collections;

namespace Scielo{
namespace MarkupHTML{

public class ParserHTML : IEnumerable
{
	private string [] elements;
	
	public ParserHTML (string source, char [] delimiters)
	{
		// Parse the string into tokens:
		elements = source.Split (delimiters);
	}
	
	// IEnumerable Interface Implementation:
	//   Declaration of the GetEnumerator() method
	//   required by IEnumerable
	public IEnumerator GetEnumerator ()
	{
		return new TokenEnumerator (this);
	}
	
	// Inner class implements IEnumerator interface:
	private class TokenEnumerator : IEnumerator {
		private int position = -1;
		private ParserHTML t;
		
		public TokenEnumerator (ParserHTML t)
		{
			this.t = t;
		}
		
		// Declare the MoveNext method required by IEnumerator:
		public bool MoveNext ()
		{
			if (position < t.elements.Length - 1) {
				position++;
				return true;
			} else 
				return false;
		}
		
		// Declare the Reset method required by IEnumerator:
		public void Reset ()
		{
			position = -1;
		}
		
		// Declare the Current property required by IEnumerator:
		public object Current {
			get {
				return t.elements [position];
			}
		}
	}
   
}
}
}