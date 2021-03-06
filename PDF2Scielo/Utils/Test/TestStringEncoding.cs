//
// TestStringEncoding.cs: Test for StringEncoding class.
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
using System.Collections;
using NUnit.Framework;

namespace Scielo.Utils {
[TestFixture()]
public class TestStringEncoding{
	[Test]
	public void Constructor1 ()
	{
		string data = "sofisticadas, el Análisis de";
		StringEncoding converter = new StringEncoding (data);
		converter.ReplaceCodesTable(StringEncoding.CharactersDefault);
		Assert.AreEqual ("sofisticadas, el &#147;An&aacute;lisis de", converter.GetStringUnicode (), "C01");
	}
	
	[Test]
	public void Constructor2Caso1 ()
	{
		string data = "\u307b,\u308b,\u305a,\u3042,\u306d";
		StringEncoding converter = new StringEncoding (data, 932);
		Assert.AreEqual (data, converter.GetStringUnicode(), "CO2 C1");
	}
	
	[Test]
	public void Constructor2Caso2 ()
	{
		string text = "";
		StringEncoding converter = new StringEncoding (text, 1255);
		Assert.AreEqual (text, converter.GetStringUnicode(), "CO2 C2");
	}
	
	[Test]
	public void Constructor2Caso3 ()
	{
		StringEncoding converter = new StringEncoding ("");
		Assert.IsNotNull (converter, "CO2 C3");
	}
	
	[Test]
	public void Prueba ()
	{
		string data = "a°a";
		StringEncoding converter = new StringEncoding (data);
		Console.WriteLine("El codigo::");
		Console.WriteLine("tamaño!::"+converter.data_byte.Count);
		for (int i=0; i < converter.data_byte.Count; i++){
			Console.WriteLine("::"+converter.data_byte[i]);
		}
		Console.WriteLine("::aca termina");
		converter.ReplaceCodesTable (StringEncoding.CharactersDefault);
		Console.WriteLine ("::"+converter.GetStringUnicode ()+"::");
		StringEncoding.CodesCharacters();
	}
}
}
