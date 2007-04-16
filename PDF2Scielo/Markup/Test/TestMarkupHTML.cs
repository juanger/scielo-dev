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
		string result  = "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>Res</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">Texto";
		       result += "</font></p>\n<br><p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>Abs</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">Texto";
		       result += "</font></p>\n<br><p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>Keywords:</b>el1,el2.</font></p>\n<br>";
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
		       body += "[ack]Acknowledgements[/ack]Texto";
		string back = "References";
		string result  = "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>1. Titulo</b><br>Texto</font></p>";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\">Texto</font></p>";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\">";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><i>1.2 Titulo</i></font></p>";
		       result += "Texto</font></p>";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>Acknowledgements";
		       result += "</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">Texto";
		MarkupHTML mark = new MarkupHTML (front, body, back);
		mark.MarkBody ();		
		Assert.AreEqual (mark.Body, result, "MUBC");
	}
	
	[Test]
	public void MarkupBackCorrect()
	{
		string front = "Front";
		string body  = "Body";
		string back  = "[ref]Referencias[/ref]";
		       back  += "[cit]Texto[/cit]";
		string result = "</font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>Referencias</b></font></p>";
		       result += "<p align=\"justify\"><font face=\"verdana\" size=\"2\">Texto</font></p>";
		MarkupHTML mark = new MarkupHTML (front, body, back);
		mark.MarkBack ();		
		Assert.AreEqual (mark.Back, result, "MUBKC");
	}
	
	[Test]
	public void CreateInstanceWithEmptyValues ()
	{
		MarkupHTML mark = new MarkupHTML ("","","");
		Assert.IsNull (mark.Text,"CIWEV");
	}
	
	[Test]
	public void CreatePartsWithoutTags()
	{
		MarkupHTML mark = new MarkupHTML ("Front","Body","Back");
		Assert.IsNotNull (mark, "CPWT");
	}
}
}
}