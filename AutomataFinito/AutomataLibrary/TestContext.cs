//
// TestContext.cs: Test units for the Context class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using NUnit.Framework;

namespace AutomataLibrary {
	
[TestFixture()]
public class TestContext {
	
	[Test]
	public void Case1 ()
	{
		Context test1 = new Context ("q", "a");
		Context test2 = new Context ("q", "a");
		Assert.AreEqual (test1, test2, "C01");
	}

	[Test]
	public void Case2 ()
	{
		Context test1 = new Context ("q", "b");
		Context test2 = new Context ("q", "b");
		Assert.IsTrue (test1 == test2, "C02");
	}

	[Test]
	public void Case3 ()
	{
		Context test1 = new Context ("q", "a");
		Context test2 = new Context ("", "");
		Assert.IsNotNull (test1, "C03-A");
		Assert.IsNotNull (test2, "C03-B");
	}
	
	[Test]
	public void Case4 ()
	{
		int code1, code2;
		Context test1 = new Context ("q", "a");
		Context test2 = new Context ("q", "a");
		code1 = test1.GetHashCode ();
		code2 = test2.GetHashCode ();
		Console.WriteLine ("test1 object has HashCode: {0}", code1);
		Console.WriteLine ("test2 object has HashCode: {0}", code2);
		Assert.AreEqual (code1, code2, "C04");
	}
}
}
