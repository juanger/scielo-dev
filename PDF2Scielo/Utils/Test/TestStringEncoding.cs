//
// TestStringEncoding.cs: Test for StringEncoding class
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
namespace Utils {
		
[TestFixture()]
public class TestStringEncoding{

        [Test]
	public void Constructor1 ()
	{
		string data = "sofisticadas, el Análisis de";
	        Console.WriteLine("Prueba Descripcion:Reemplazo de: '' por '&#147;' ");
	        Console.WriteLine ("La cadena antes de entrar::::" + data);
		StringEncoding converter = new StringEncoding (data);
		converter.ReplaceCodesTable(StringEncoding.CharactersDefault);
		Console.WriteLine ("La cadena al salir::::" + converter.GetStringUnicode() );		
	}

	[Test]
	public void Constructor2Caso1 ()
	{
		string data = "\u307b,\u308b,\u305a,\u3042,\u306d";
                Console.WriteLine ("La cadena antes de entrar::::"+data);
	        StringEncoding converter = new StringEncoding (data, 932);
	        Console.WriteLine ("La cadena al salir::::" + converter.GetStringUnicode ());
	}
	
	[Test]
	public void Constructor2Caso2 ()
	{
		string data = "This string contains the unicode character Pi(\u03a0)";
                Console.WriteLine ("La cadena antes de entrar::::"+data);
	        StringEncoding converter = new StringEncoding (data, 1255);
	        Console.WriteLine ("La cadena al salir::::" + converter.GetStringUnicode ());
	}	
}
}
}
