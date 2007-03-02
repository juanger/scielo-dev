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
using System.Text.RegularExpressions;
using System.IO;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

public class PDFPoppler : IExtractable {

	private string doc_path;
	private string file_name;
	private static string temp_dir;
	private INormalizable norm;
		
	private PDFPoppler (string fullpath)
	{
		doc_path = fullpath;
		file_name = Path.GetFileNameWithoutExtension (fullpath);
	}
	
	public static PDFPoppler CreateInstance (Uri uri)
	{
		string docpath, temp, user, dir;
		docpath = uri.LocalPath;
		
		if (!File.Exists (docpath))
			return null;
		
		user = Environment.UserName;
		dir = "Poppler-" + user;
		temp = Path.GetTempPath ();
		temp_dir = Path.Combine (temp, dir);
		
		if (!Directory.Exists (temp_dir))
			Directory.CreateDirectory (temp_dir);
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + "Ruta del archivo: " + uri.LocalPath);
		#endif
		
		return new PDFPoppler (docpath);
	}
	
	public string GetNormText (string encoding)
	{	
		norm = new AtmNormalizer (ExtractText ());
		//norm.ReplaceChars (StringEncoding.CharactersDefault);
		norm.MarkText ();
		
		return norm.Text;
	}
	
	public string GetRawText ()
	{
		return ExtractText ();
	}
	
	public Queue GetNonText ()
	{
		ExtractImages ();
		
		return null;
	}
	
	public void CreateHTMLFile (string filepath, string filename)
	{
		string fullpath, name;
		name = filename + ".htm";
		fullpath = Path.Combine (filepath, name);
		
		FileStream filestream = null;
                using (filestream = File.Create (fullpath)) {
                	StreamWriter writer = new StreamWriter (filestream);
                	writer.Write (GetNormText ("utf8"));
                	writer.Flush ();
                	writer.Close ();
                }
	}
	
	private string ExtractText ()
	{
		string dir, filepath, result;
		result = null;
		
		dir = Path.Combine (temp_dir, file_name);
		filepath = Path.Combine (dir, file_name + ".txt");
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + "Ruta del directorio temporal: " + dir);
		#endif
		
		if (Directory.Exists (dir)) {
			Directory.Delete (dir, true);
		}
		
		Directory.CreateDirectory (dir);
		Process proc = Process.Start ("pdftotext", " -layout " + doc_path + " " + filepath);
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
		
		docdir = Path.Combine (temp_dir, file_name);
		imgdir = Path.Combine (docdir, "Images");
		
		if (Directory.Exists (imgdir)) {
			Directory.Delete (imgdir, true);
		}
		
		oworkdir = Environment.CurrentDirectory;
		Directory.CreateDirectory (imgdir);
		Environment.CurrentDirectory = imgdir;

		Process proc = Process.Start ("pdfimages", " -j " + doc_path + " Images");
		proc.WaitForExit ();
		
		Environment.CurrentDirectory = oworkdir;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: "+ "Ruta del directorio de trabajo: " + Environment.CurrentDirectory);
		#endif
	}
}
}
}