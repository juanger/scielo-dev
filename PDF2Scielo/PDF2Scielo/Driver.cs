//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;
using Scielo.PDF2Text;

namespace Scielo {
namespace PDF2Scielo {

public class Driver {

	private static void Header ()
	{
		Console.WriteLine (new AssemblyInfo ().ToString ());
	}
	
	private static void Help (int exitcode)
	{
		Console.WriteLine ("pdf2scielo <ARCHIVO>");
		Console.WriteLine ("\tdonde  <ARCHIVO> es la ruta a un documento PDF a convertir a HTML SciELO.\n");
		Console.WriteLine ("pdf2scielo --help");
		Console.WriteLine ("\tMuestra la ayuda de esta herramienta.\n");
		Environment.Exit (exitcode);
	}
	
	private static Uri ParsePath (string filepath)
	{
		Uri uri;
		
		// Se checa que la extension no sea ni vacia ni otra que no sea
		// .pdf o .PDF
		string ext = ".pdf.PDF";
		string filename = Path.GetFileName (filepath);
		string filext = Path.GetExtension (filepath);
		bool pdf = (ext.IndexOf (filext) != -1) && (filext != String.Empty);
		
		Console.WriteLine ("DEBUG: " + filext);
		if (filename != String.Empty && pdf) {
			Console.WriteLine ("DEBUG: Inside if in ParserPath" );
		
			if (Path.IsPathRooted (filepath))
				uri = new Uri (filepath);
			else
				uri = new Uri (Path.GetFullPath (filepath));
				
			return uri;
		} else 
			return null;
	}
	
	public static void Main(string[] args)
	{
		bool help = false;
		Header ();
			
		if (args.Length == 1) {
			switch (args [0]) {
			case "--help":
				help = true;
				break;
			}
		} else {
			Help (1);
		}
		
		if (help)
			Help (0);

		Uri path = ParsePath (args [0]);
		Application.Init ();
		
		if (path != null) {
			DocReader doc = new DocReader (path);
			
			if (doc != null)
				doc.CreateFile (Environment.CurrentDirectory,
					Path.GetFileNameWithoutExtension (args [0]));
			else {
				Console.WriteLine ("Error: El archivo {0} no es un archivo PDF o no existe", args [0]);
				Environment.Exit (1);
			}
		} else {
			Console.WriteLine ("Error: Solo se acepta la ruta a un documento PDF.");
			Environment.Exit (1);
		}
	}
}
}
}