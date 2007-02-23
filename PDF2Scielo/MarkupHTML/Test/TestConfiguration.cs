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
namespace MarkupHTML {
	
[TestFixture()]
public class TestConfiguration {
		
	[Test]
	public void Equals ()
	{
		Configuration test1 = new Configuration ("q", "a", "x");
		Configuration test2 = new Configuration ("q", "a", "x");
		Assert.AreEqual (test1, test2, "C01");
	}

	[Test]
	public void OperatorOverloading ()
	{
		Configuration test1 = new Configuration ("q", "b", "y");
		Configuration test2 = new Configuration ("q", "b", "y");
		Assert.IsTrue (test1 == test2, "C02");
	}

	[Test]
	public void Constructor ()
	{
		Configuration test1 = new Configuration ("q", "a", "z");
		Configuration test2 = new Configuration ("", "", "");
		Assert.IsNotNull (test1, "C03-A");
		Assert.IsNotNull (test2, "C03-B");
	}
	
	[Test]
	public void HashCode ()
	{
		int code1, code2;
		Configuration test1 = new Configuration ("q", "a", "w");
		Configuration test2 = new Configuration ("q", "a", "w");
		code1 = test1.GetHashCode ();
		code2 = test2.GetHashCode ();
		Console.WriteLine ("test1 object has HashCode: {0}", code1);
		Console.WriteLine ("test2 object has HashCode: {0}", code2);
		Assert.AreEqual (code1, code2, "C04");
	}
}
}
}
