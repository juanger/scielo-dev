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
using System.Collections;

namespace Scielo {
namespace Utils {

public class Test {
	
	public enum DocTypes :int 
	{
		PDF = 0,
		RAW = 1,
		NORM = 2,
		HTML = 3,
	}
	
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

	// Regresa un ArrayList de arreglos de dos cadenas, la primera es el
	// estilo y la segunda es el path al archivo
	public static ArrayList GetAllFilesByType (int type)
	{
		string line, style, path, testPath, stylePath, source;
		ArrayList docs = new ArrayList ();
		
		testPath = PathOfTest ();
		source = Path.Combine (testPath, "unit-test.sources");
		
		FileStream mainReader = new FileStream (source, FileMode.Open);
		StreamReader smainReader = new StreamReader (mainReader);
		
		while (smainReader.Peek () > -1) {
			style = smainReader.ReadLine ();
			stylePath = Path.Combine (testPath, style);
			source = Path.Combine (stylePath, style + ".sources");
			
			FileStream sourceReader = new FileStream (source, FileMode.Open);
			StreamReader styleReader = new StreamReader (sourceReader);
			
			while (styleReader.Peek () > -1) {
				string[] array = new string [2];
				line = styleReader.ReadLine ();
			
				path = Path.Combine (stylePath, Test.GetFileNameByType (line, type));
				array [0] = style;
				array [1] = path;
				docs.Add (array);
			}
			styleReader.Close ();
		}
		smainReader.Close ();
		
		return docs;
	}

	// regresa el nombre del archivo correspondiente aal tipo dado	
	public static string GetFileNameByType (string line,int type)
	{
		string [] array = line.Split (':');
		return array[type];
	}
}
}
}