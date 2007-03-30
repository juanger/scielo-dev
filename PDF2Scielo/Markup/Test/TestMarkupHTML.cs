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
	public void MarkupFrontCorrect()
	{
		string front  = "[res]Res[/res]Texto";
		       front += "[abs]Abs[/abs]Texto";
		       front += "[key]Keywords:el1,el2.[/key]";
		string body = "Body";
		string back = "References";
		string result  = "<p align=\"center\">Res</p>\n<p align=\"justify\">Texto";
		       result += "</p>\n<br><p align=\"center\">Abs</p>\n<p align=\"justify\">Texto";
		       result += "</p>\n<br><p align=\"justify\"><b>Keywords:</b>el1,el2.</p>\n<br>";
		MarkupHTML mark = new MarkupHTML (front, body, back);
		mark.MarkFront ();		
		Assert.AreEqual (mark.Front, result, "MUFC");
	}
	
	[Test]
	public void MarkupBodyCorrect()
	{
		string front = "Front";
		string body  = "[para][sec]1. Titulo[/sec]Texto[/para]";
		       body += "[para]Texto[/para]";
		       body += "[para]";
		       body += "[subsec]1.2 Titulo[/subsec]";
		       body += "Texto[/para]";
		string back = "References";
		string result  = "<p align=\"justify\"><b>1. Titulo</b><br>Texto</p>";
		       result += "<p align=\"justify\">Texto</p>";
		       result += "<p align=\"justify\">";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><i>1.2 Titulo</i></font></p>";
		       result += "Texto</p>";
		MarkupHTML mark = new MarkupHTML (front, body, back);
		mark.MarkBody ();		
		Assert.AreEqual (mark.Body, result, "MUBC");
	}
	
	[Test]
	public void MarkupBackCorrect()
	{
		string front = "Front";
		string body  = "Body";
		string back  = "[ref]Referencias[/ref]Texto";
		string result  = "<p align=\"left\"><b>Referencias</b></p>Texto";
		MarkupHTML mark = new MarkupHTML (front, body, back);
		mark.MarkBack ();		
		Assert.AreEqual (mark.Back, result, "MUBKC");
	}
	
	[Test]
	public void CreateInstanceWithEmptyValues ()
	{
		MarkupHTML mark = new MarkupHTML ("","","");
		Assert.IsNull (mark.CreateDocumentHTML(),"CIWEV");
	}
	
	/*[Test]
	public void CreateEmptyParts()
	{
		MarkupHTML mark = new MarkupHTML ("Front","Body","Back");
		mark.CreateDocumentHTML();
	}*/
}
}
}