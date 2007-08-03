//
// TestStringMatch.cs: Unit tests for the StringMatch class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//


using System;
using NUnit.Framework;

namespace Scielo.PDF2Text {
	
[TestFixture()]
public class TestStringMatch {
	
	[Test]
	public void HasResultMatch ()
	{
		StringMatch fullMatch = new StringMatch ("Foo", "");
		StringMatch resultMatch = new StringMatch ("Foo", "Bar");
		Assert.AreEqual(false, fullMatch.HasResultMatch (), "HRM01");
		Assert.AreEqual(true, resultMatch.HasResultMatch (), "HRM02");
	}
}
}
