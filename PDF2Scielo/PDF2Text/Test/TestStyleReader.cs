//
// StyleReader.cs: Pruebas de unidad para la clase StyleReader.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.IO;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo.PDF2Text {
[TestFixture()]
public class TestStyleReader {
	[Test()]
	public void ValidStyle ()
	{
		StyleReader reader = new StyleReader ("atm");
		Type etype = Type.GetType ("Scielo.PDF2Text.StyleReader");
		Assert.IsInstanceOfType (etype, reader, "VS");
	}
	
	[Test ()]
	[ExpectedException(typeof (Exception))]
	public void InvalidStyle ()
	{
		string source = Path.Combine (Test.PathOfTest (), "test-invalid.xml");
		StyleReader reader = new StyleReader (new Uri (source));
		Type etype = Type.GetType ("Scielo.PDF2Text.StyleReader");
		Assert.IsInstanceOfType (etype, reader, "IS");
	}
	
	[Test ()]
	public void GetStyleList ()
	{
		string [] list = StyleReader.GetStyleList();
		Assert.AreEqual (3, list.Length, "GSL01");
		Assert.AreEqual ("arcame", list[0], "GDL02");
		Assert.AreEqual ("atm", list[1], "GDL03");
		Assert.AreEqual ("eng-agr", list[2], "GDL04");
	}
}
}
