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
	public void ConstructorString ()
	{
		RawDocument rdoc0 = new RawDocument ("");
		RawDocument rdoc1 = new RawDocument ("Hola Mundo");
		RawDocument rdoc2 = new RawDocument ("            ad        ");
		
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Assert.IsInstanceOfType (etype, rdoc0, "CI01");
		Assert.IsInstanceOfType (etype, rdoc1, "CI01");
		Assert.IsInstanceOfType (etype, rdoc2, "CI01");	
	}
}
}
}
