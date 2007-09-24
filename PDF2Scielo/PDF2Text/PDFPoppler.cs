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
using System.IO;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

public class PDFPoppler : IExtractable {

	private string doc_path;
	private string file_name;
	private static string temp_dir;
	
	public PDFPoppler (Uri uri)
	{
		string docpath;
		docpath = uri.LocalPath;
		
		if (!File.Exists (docpath))
			throw new FileNotFoundException ();
		
		CreateWorkDir();
		
		Logger.Debug ("Ruta del archivo: {0}", uri.LocalPath);
		
		doc_path = docpath;
		file_name = Path.GetFileNameWithoutExtension (docpath);
	}
	
	public string GetRawText ()
	{
		return ExtractText ();
	}
	
	// TODO: Falta implementar una clase que encapsule a las imagenes y demas 
	// elementos de un documento y los vaya poniendo en un queue.
	public Queue GetNonText ()
	{
		ExtractImages ();
		
		return null;
	}
	
	// TODO: Se necesita conocer el formato del archivo para que el Normalizador
	// elija el archivo XML con las ER correspondientes.
	public RawDocument CreateRawDocument ()
	{
		return new RawDocument (this);
	}
	
	private void CreateWorkDir ()
	{
		string user, dir, temp;
		user = Environment.UserName;
		dir = "Poppler-" + user;
		temp = Path.GetTempPath ();
		temp_dir = Path.Combine (temp, dir);
		
		if (!Directory.Exists (temp_dir))
			Directory.CreateDirectory (temp_dir);	
	}
	
	private string ExtractText ()
	{
		string dir, filepath, result;
		result = null;
		
		dir = Path.Combine (temp_dir, file_name);
		filepath = Path.Combine (dir, file_name + ".txt");
		
		Logger.Debug ("Ruta del directorio temporal: {0}", dir);
		
		if (Directory.Exists (dir)) {
			Directory.Delete (dir, true);
		}
		
		Directory.CreateDirectory (dir);
		Process proc = Process.Start ("pdftotext", " -layout " + doc_path + " " + filepath);
		proc.WaitForExit ();
		
		Logger.Debug ("Ruta del archivo temporal: {0}", filepath);
		
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
		
		Logger.Debug ("Ruta del directorio de trabajo: {0}", Environment.CurrentDirectory);
	}
}
}
}