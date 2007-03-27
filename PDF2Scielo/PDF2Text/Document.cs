//
// Document.cs: An abstract class that represents a generic type of document.
// Atmosfera.
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
namespace PDF2Text {

public abstract class Document {

	protected string text;
	
	public Document ()
	{
		text = null;
	}
	
	public abstract string GetText ();

	public void WriteFile (string filepath, string filename, string extension)
	{
		string fullpath, name;
		name = filename + "." + extension;
		fullpath = Path.Combine (filepath, name);
		
		FileStream filestream = null;
                using (filestream = File.Create (fullpath)) {
                	StreamWriter writer = new StreamWriter (filestream);
                	writer.Write (GetText ());
                	writer.Flush ();
                	writer.Close ();
                }
        }
}
}
}