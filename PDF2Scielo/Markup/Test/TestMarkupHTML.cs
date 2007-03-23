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
		
	[Test()]
	public void TestCase()
	{
		MarkupHTML obj = new MarkupHTML ("mejora.[abs] Abstract [/abs] y continua [res] Resumen [/res] ");
		obj.HeadDocument();
		obj.ReplaceAbsTag();
		obj.ReplaceResTag();
 		Console.WriteLine("Resultado:    "+obj.Text);
	}
}
}
}