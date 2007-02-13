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
	
	         //ParserHTML.DisplayString(source);
	         ParserHTML.FormatString(source);
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
	
	//*********************************
	// quitar esto
	public static string[] LowNames = {
        "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", 
    	"BS", "HT", "LF", "VT", "FF", "CR", "SO", "SI",
    	"DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB",
    	"CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US"};

    	public static void DisplayString (string text)
    	{

    		Console.WriteLine ("String length: {0}", text.Length);
    		foreach (char c in text) {
        		if (c < 32)
            			Console.WriteLine ("<{0}> U+{1:x4}", ParserHTML.LowNames[c], (int)c);
        		else if (c > 127)
            			Console.WriteLine ("(Possibly non-printable) U+{0:x4}", (int)c);
            		else
            			Console.WriteLine ("{0} U+{1:x4}", c, (int)c);
    		}
	}
	
	public static void FormatString(string text)
	{
		int line = (int) '\n';

		Console.Write ("-----------------------------------el entero es::" + line);
	}
	
	//**********************************
	public string [] GetElements ()
	{
		return elements;
	}
   
}
}
}