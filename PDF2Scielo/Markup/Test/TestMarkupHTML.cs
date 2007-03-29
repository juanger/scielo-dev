//
// TestConfiguration.cs: Unit tests for the Configuration class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using NUnit.Framework;

namespace Scielo {
namespace Markup {
	
	
[TestFixture()]
public class TestMarkupHTML
{
		
	[Test]
	public void MarkupHTMLCorrect()
	{
		string front = "[res]Res[/res]Texto[abs]Abs[/abs]Texto[key]Keywords:el1,el2.[/key]";
		string body = "Body";
		string back = "References";
		string frontResult = "[res]Res[/res]Texto[abs]Abs[/abs]Texto[key]Keywords:el1,el2.[/key]";
		MarkupHTML mark = new MarkupHTML(front, body, back);
		mark.MarkFront();
		Console.WriteLine("Resultado:::::::::::::\n"+ mark.Front);
	}
	
	[Test]
	public void CreateInstanceWithEmptyValues ()
	{
		MarkupHTML mark = new MarkupHTML ("","","");
		Assert.IsNull (mark.CreateDocumentHTML(),"CIWEV");
	}
	
}
}
}