//
// Driver.cs: Entry Point of the CLI frontend of pdf2scielo.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.IO;
using Scielo.PDF2Text;
using Scielo.Markup;

namespace Scielo {
namespace PDF2Scielo {

public class Driver {

	private static void Header ()
	{
		Console.WriteLine (new AssemblyInfo ().ToString ());
		Environment.Exit (0);
	}
	
	private static void Help (int exitcode)
	{
		Console.WriteLine ("pdf2scielo <ARCHIVO>");
		Console.WriteLine ("\tDonde <ARCHIVO> es la ruta a un documento PDF ap convertir a HTML SciELO.\n");
		Console.WriteLine ("pdf2scielo --help");
		Console.WriteLine ("\tMuestra la ayuda de esta herramienta.\n");
		Console.WriteLine ("pdf2scielo --version");
		Console.WriteLine ("\tMuestra informacion sobre esta herramienta.\n");
		Environment.Exit (exitcode);
	}
	
	private static Uri ParsePath (string filepath)
	{
		Uri uri;
		string ext, filename, filext;
		bool pdf;
		
		// Se checa que la extension no sea vacia ni otra que no sea
		// .pdf o .PDF
		ext = ".pdf.PDF";
		filename = Path.GetFileName (filepath);
		filext = Path.GetExtension (filepath);
		pdf = (filename != String.Empty) && 
			(filext != String.Empty) && (ext.IndexOf (filext) != -1);

		#if DEBUG		
		Console.WriteLine ("DEBUG: " + "La extension del archivo: " + filext);
		#endif
		
		if (pdf) {		
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
		Uri uri;
		PDFPoppler reader;
		string filepath;
		
		if (args.Length == 1) {
			switch (args [0]) {
			case "--help":
				Help (0);
				break;
			case "--version":
				Header ();
				break;
			}
		} else
			Help (1);

		filepath = args [0];
		uri = ParsePath (filepath);
		
		if (uri != null && File.Exists (filepath)) {
			//Application.Init ();
			reader = new PDFPoppler (uri);
			
			if (reader != null) {
				Console.WriteLine ("Transformando PDF ... ");
				reader.CreateHTMLFile (Environment.CurrentDirectory,
					Path.GetFileNameWithoutExtension (filepath));
				reader.GetNonText ();
				Console.WriteLine ("Finalizando\n");
			} else {
				Console.WriteLine ("Error: El archivo {0} no es un archivo PDF", args [0]);
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