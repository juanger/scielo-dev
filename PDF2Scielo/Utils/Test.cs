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
	
	public static string GetPDFFileName (string line)
	{
		int index = line.IndexOf ("\t");
		return line.Substring (0, index);
	}
	
	public static string GetRawFileName (string line)
	{
		int sindex = line.IndexOf ("\t") + 1;
		int length = line.IndexOf ("\t", sindex) - sindex;
		return line.Substring (sindex, length);
	}
	
	public static string GetNormFileName (string line)
	{
		int sindex = line.IndexOf ("\t")  + 1;
		sindex = line.IndexOf ("\t", sindex) + 1;
		int length = line.IndexOf ("\t", sindex) - sindex;
		return line.Substring (sindex, length);
	}
	
	public static string GetHTMLFileName (string line)
	{
		int sindex = line.IndexOf ("\t")  + 1;
		sindex = line.IndexOf ("\t", sindex) + 1;
		sindex = line.IndexOf ("\t", sindex) + 1;
		return line.Substring (sindex);
	}
}
}
}