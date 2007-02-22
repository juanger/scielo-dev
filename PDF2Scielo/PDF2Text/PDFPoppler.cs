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
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace Scielo {
namespace PDF2Text {

public class PDFPoppler : IExtractable {

	private string docpath;
	private string filename;
	private static string tempdir;
		
	private PDFPoppler (string fullpath)
	{
		docpath = fullpath;
		filename = Path.GetFileNameWithoutExtension (fullpath);
	}
	
	public static PDFPoppler CreateInstance (Uri uri)
	{
		string docpath, temp;
		docpath = uri.LocalPath;
		
		if (!File.Exists (docpath))
			return null;
		
		temp = Path.GetTempPath ();
		tempdir = Path.Combine (temp, "Poppler");
		
		if (!Directory.Exists (tempdir))
			Directory.CreateDirectory (tempdir);
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + "Ruta del archivo: " + uri.LocalPath);
		#endif
		
		return new PDFPoppler (docpath);
	}
	
	public String GetText (string encoding)
	{	
		//FIXME: Remover encabezados y numeros de pagina usando Replace.
		AtmNormalizer norm = new AtmNormalizer (ExtractText ());
		norm.ReplacePattern (@"[\n]+[\u000c]+[0-9]+[ ]*[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()]+[\n]+", "\n");
		//norm.ReplacePattern (@"[\n]+[\u000c]+[a-zA-Z. \u00f1\u002f\u0050-\u00ff-’,()]+[\n]+[0-9]+[\n]+", " ");
		
		//FIXME: Etiquetado de RESUMEN, ABSTRACT y REFERENCES.
		norm.ReplacePattern (@"[\n]+RESUMEN\n", "\n[res] Resumen [/res]\n");
		norm.ReplacePattern (@"[\n]+ABSTRACT\n", "\n[abs] Abstract [/abs]\n");
		norm.ReplacePattern (@"[\n]+References\n", "\n[ref] References [/ref]\n");

		//FIXME: Etiquetado de Keyword.		
		Match [] matches;
		matches = norm.GetMatches (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z, ]+");
		
		foreach (Match m in matches) {
			string result;
			result = m.Value.Trim (); 
			Console.WriteLine ("MATCH: " + result);
			result = "\n[key] " + result + " [/key].\n";
			norm.ReplacePattern (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[a-zA-Z, ]+\n",
				result);
		}
		
		matches = norm.GetMatches (@"[\n]+[0-9][.][ ].*\n");
		foreach (Match m in matches) {
			string result, mid;
			mid = m.Value;
			result = mid.Trim ();
			Console.WriteLine ("MATCH: " + result);
			result = "\n[sec] " + result + " [/sec]\n";
			norm.ReplacePattern (mid, result);
		}
		
		return norm.Text;
	}
	
	public Queue GetNonText ()
	{
		//TODO: To be implemented.
		ExtractImages ();
		
		return null;
	}
	
	public void CreateFile (string filepath, string filename)
	{
		string fullpath, name;
		name = filename + ".txt";
		fullpath = Path.Combine (filepath, name);
		FileStream filestream = null;

                using (filestream = File.Create (fullpath)) {
                	StreamWriter writer = new StreamWriter (filestream);
                	writer.Write (GetText ("utf8"));
                	writer.Flush ();
                	writer.Close ();
                }
	}
	
	private string ExtractText ()
	{
		string dir, filepath, result;
		result = null;
		
		dir = Path.Combine (tempdir, filename);
		filepath = Path.Combine (dir, filename + ".txt");
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + "Ruta del directorio temporal: " + dir);
		#endif
		
		if (Directory.Exists (dir)) {
			Directory.Delete (dir, true);
		}
		
		Directory.CreateDirectory (dir);
		Process proc = Process.Start ("pdftotext", " -raw " + docpath + " " + filepath);
		proc.WaitForExit ();
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + "Ruta del archivo temporal: " + filepath);
		#endif
		
		FileStream filestream = null;
		using (filestream = File.Open (filepath, FileMode.Open)) {
			StreamReader reader = new StreamReader (filestream);
			result = reader.ReadToEnd ();
			reader.Close ();
		}
		
		return result;
	}
	
	private void ExtractImages ()
	{
		string docdir, imgdir, oworkdir;
		
		docdir = Path.Combine (tempdir, filename);
		imgdir = Path.Combine (docdir, "Images");
		
		if (Directory.Exists (imgdir)) {
			Directory.Delete (imgdir, true);
		}
		
		oworkdir = Environment.CurrentDirectory;
		Directory.CreateDirectory (imgdir);
		Environment.CurrentDirectory = imgdir;

		Process proc = Process.Start ("pdfimages", " -j " + docpath + " Images");
		proc.WaitForExit ();
		
		Environment.CurrentDirectory = oworkdir;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: "+ "Ruta del directorio de trabajo: " + Environment.CurrentDirectory);
		#endif
	}
}
}
}