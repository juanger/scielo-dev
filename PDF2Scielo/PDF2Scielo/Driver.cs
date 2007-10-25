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
using Scielo.Utils;

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
		
		Logger.Debug ("La extensión del archivo: {0}",  filext);
		
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
		string filepath, format, num;
		
		AppOptions options = new AppOptions (args);
		
		#if DEBUG
		Logger.ActivateDebug ();
		#endif
		
		if (options.GotNoArguments) {
			Application.Init ();
			MarkerWindow win = new MarkerWindow ();
			win.Show ();
			Application.Run ();
		} else {
			if (!options.Format && !options.numColumns) {
				options.DoHelp ();
				Environment.Exit (0);
			} else if (!options.GotNoArguments && options.Format) {
				format = options.FirstArgument;
				filepath = options.SecondArgument;
				uri = ParsePath (filepath);
				
				if (uri != null) {
					try {
						reader = new PDFPoppler (uri);
						
						Logger.Debug ("Transformando PDF", "");
						
						rdoc = reader.CreateRawDocument ();
						ndoc = rdoc.Normalize (format);
						ndoc.WriteDocument (Environment.CurrentDirectory, 
							Path.GetFileNameWithoutExtension (filepath), "norm");
						marker = new MarkupHTML (ndoc);
						htmldoc = marker.CreateHTMLDocument ();
						htmldoc.WriteDocument (Environment.CurrentDirectory, 
							Path.GetFileNameWithoutExtension (filepath), "htm");
						reader.GetNonText ();
						
						Logger.Debug ("Finalizando", "");
					} catch (FileNotFoundException) {
						Logger.Error ("El archivo {0} no existe", filepath);
						Environment.Exit (1);
					}
				} else {
					Logger.Error ("Solo se acepta la ruta a un documento PDF", "");
					Environment.Exit (1);
				}
			} else if (!options.GotNoArguments && options.numColumns) {
				num = options.FirstArgument;
				filepath = options.SecondArgument;
				format = options.ThirdArgument;
				uri = ParsePath (filepath);
				
				if (uri != null) {
					try {
						Console.WriteLine ("En opcion de columnas");
						reader = new PDFPoppler (uri);
						
						Console.WriteLine ("Transformando PDF ... ");
						
						rdoc = reader.CreateRawDocument ();
						
						Console.WriteLine ("Buscando las {0} columnas.", num);
						rdoc.BreakColumns();
						Console.WriteLine ("Rompio las {0} columnas here......", rdoc.GetText());
						rdoc.WriteDocument (Environment.CurrentDirectory, 
						Path.GetFileNameWithoutExtension (filepath), "column");
						Console.WriteLine ("Finalizando\n");
					} catch (FileNotFoundException) {
						Console.WriteLine ("Error: El archivo {0} no existe.", filepath);
						Environment.Exit (1);
					}
				}
			}
		}
	}
}
}
}