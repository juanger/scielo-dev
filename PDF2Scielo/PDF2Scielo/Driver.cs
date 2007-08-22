//
// Driver.cs: Entry Point of the CLI frontend of pdf2scielo.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;
using Scielo.PDF2Text;
using Scielo.Markup;

namespace Scielo {
namespace PDF2Scielo {

public class Driver {
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
		RawDocument rdoc;
		NormDocument ndoc;
		MarkupHTML marker;
		HTMLDocument htmldoc;
		string filepath, format;
		
		AppOptions options = new AppOptions (args);
		
		if (options.GotNoArguments) {
			Application.Init ();
			MarkerWindow win = new MarkerWindow ();
			win.Show ();
			Application.Run ();
		} else {
			if (!options.Format) {
				options.DoHelp ();
				Environment.Exit (0);
			} else if (!options.GotNoArguments) {
				format = options.FirstArgument;
				filepath = options.SecondArgument;
				uri = ParsePath (filepath);
				
				if (uri != null) {
					try {
						reader = new PDFPoppler (uri, format);
						
						Console.WriteLine ("Transformando PDF ... ");
						
						rdoc = reader.CreateRawDocument ();
						ndoc = rdoc.Normalize ();
						ndoc.WriteDocument (Environment.CurrentDirectory, 
							Path.GetFileNameWithoutExtension (filepath), "norm");
						marker = new MarkupHTML (ndoc);
						htmldoc = marker.CreateHTMLDocument ();
						htmldoc.WriteDocument (Environment.CurrentDirectory, 
							Path.GetFileNameWithoutExtension (filepath), "htm");
						reader.GetNonText ();
						
						Console.WriteLine ("Finalizando\n");
					} catch (FileNotFoundException) {
						Console.WriteLine ("Error: El archivo {0} no existe.", filepath);
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
}
}