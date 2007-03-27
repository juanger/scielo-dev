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
	public void TestCase()
	{
		MarkupHTML mark = new MarkupHTML("[res]Resumen[/res]Texto del Resumen [abs]Abstract[/abs]Texto Abstract [key]Keywords:element1,element2[/key]","","");
		Console.WriteLine("Resultado:::::::::::::\n"+ mark.CreateDocumentHTML());
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