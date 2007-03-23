//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Alejandro Rosendo Robles. (rosendo69@hotmail.com)
//   Virginia Teodosio Procopio. (ainigriv_t@hotmail.com)
// Copyright (C) 2007 UNAM DGB
//
using System.Text.RegularExpressions;
using System;

namespace Scielo {
namespace Markup {
		
public class MarkupHTML {
	private string text;
		
	public MarkupHTML (string txt)
	{
		text = txt;
	}
		
	public string Text {
		get {
			return text;
		}
	}
	
	public void ReplaceAbsTag ()		
	{
		string absTag = @"\[abs\]";
		string absTagEnd = @"\[/abs\]";
		string htmlTag = "<b>";
		string htmlTagEnd = "</b>";

		text = Regex.Replace(text, absTag, htmlTag);
		text = Regex.Replace(text, absTagEnd, htmlTagEnd);			
	}
			
}
}
}
