//
// AssemblyInfo.cs: Assembly Information.
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
	
	[Test]
	public void ReplacePattern ()
	{
		
	}
	
	[Test]
	public void ReplaceBytes ()
	{       
	        string data = "sofisticadas, el Análisis de";
	        Console.WriteLine("Prueba Descripcion:Reemplazo de: '' por '&#147;' ");
	        Console.WriteLine ("La cadena antes de entrar::::" + data);
		AtmNormalizer converter = new AtmNormalizer (data);
		Console.WriteLine ("La cadena al salir::::" + converter.Text);
		
    	}
}
}
}