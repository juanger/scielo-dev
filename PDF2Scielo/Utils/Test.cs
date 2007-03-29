//
// StringEncoding: A class that implements the coders and decoders
// (a restrict list) of a string. 
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
using System.IO;

namespace Scielo {
namespace Utils {

public class Test {
			
	public static string PathOfTest ()
	{
		// FIXME: Este es un hack para correr los casos que depende de la
		// locacion cuando se corre el test.
		string path;
		path = Environment.CurrentDirectory;
		path = path.Replace ("bin" + Path.DirectorySeparatorChar + "Debug", String.Empty);
		path = path.Replace ("bin" + Path.DirectorySeparatorChar + "Release", String.Empty);
		path = Path.Combine (path, "Test");
		
		return path;
	}
	
	public static string ReadFile (string filepath)
	{
		string result;
		
		FileStream filestream = null;
		using (filestream = File.Open (filepath, FileMode.Open)) {
			StreamReader reader = new StreamReader (filestream);
			result = reader.ReadToEnd ();
			reader.Close ();
		}
		
		return result;
	}
}
}
}