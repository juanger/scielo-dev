//
// TestNormDocument.cs: Unit tests for the NormDocument class.
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
using System.IO;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestNormDocument {
	
	[Test]
	public void ConstructorInvalid ()
	{
		try {
			NormDocument ndoc0 = new NormDocument (null, null, null);
			Type etype = Type.GetType ("Scielo.PDF2Text.NormDocument");
			Assert.IsInstanceOfType (etype, ndoc0, "CI01");
		} catch (ArgumentNullException) {
			Console.WriteLine ("Error: NormDocument no acepta como parametro(s) a null.");
		}	
	}
}
}
}
