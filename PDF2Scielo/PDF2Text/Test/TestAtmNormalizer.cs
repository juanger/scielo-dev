//
// TestAtmNormalizer.cs: Test for AtmNormalizer class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using NUnit.Framework;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestAtmNormalizer {
	
	public void Constructor ()
	{
		string data = "sofisticadas, el Análisis de";
		AtmNormalizer atmN = new AtmNormalizer (data, "atm");
		
		Type etype = Type.GetType ("Scielo.PDF2Text.AtmNormalizer");
		Assert.IsInstanceOfType (etype, atmN, "CI01");
		Assert.IsNotNull (atmN, "CI02");
	}
	
	[Test]
	public void GetMatchesNamed ()
	{
		string source = "Foo and Bar. Foo blahFoo blah. oFooFoo. BARFoo\n Foo";
		string [] results = AtmNormalizer.NewGetMatches (@"\b(?<Result>Foo)\b", source);
		Assert.AreEqual(3,results.Length, "GSM1");
	}
	
	[Test]
	public void GetMatchesUnamed ()
	{
		string source = "\n\nAlgo: Bar Bar Bar \n\nAlgo: Bar Bar \n\n";
		string [] results = AtmNormalizer.NewGetMatches (@"[\n]+Algo:[\p{L} ]*?[\n]+", source);
		Assert.AreEqual(1, results.Length, "GSM2");
	}
}
}
}