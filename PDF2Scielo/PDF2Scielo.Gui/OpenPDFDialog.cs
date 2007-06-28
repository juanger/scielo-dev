	//
// OpenPDFDialog: Dialog that uses a file chooser to open a PDF document.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;

namespace Scielo.PDF2Scielo{
public partial class OpenPDFDialog : Gtk.Dialog {
	private string filename = String.Empty;
	
	public OpenPDFDialog()
	{
		this.Build();
		FileFilter allFiles = new FileFilter ();
		FileFilter pdfFiles = new FileFilter ();
		
		allFiles.AddPattern ("*.*");
		allFiles.Name = "All Files";
		
		pdfFiles.AddPattern ("*.PDF");
		pdfFiles.AddPattern ("*.pdf");
		pdfFiles.Name = "PDF Files";
		
		open_dialog.AddFilter (pdfFiles);
		open_dialog.AddFilter (allFiles);
		
		string homePath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
		open_dialog.SetCurrentFolder (homePath);
	}
	
	private void OnButtonOkClicked (object sender, System.EventArgs e)
	{
		filename = open_dialog.Filename;
		
		#if DEBUG
		Console.WriteLine ("Filename: {0}", filename);
		#endif
		
		if (Directory.Exists (filename))
			open_dialog.SetCurrentFolder (filename);
		else
			Respond (ResponseType.Ok);
	}
	
	private void OnOpenDialogFileActivated (object sender, System.EventArgs e)
	{
		filename = open_dialog.Filename;
		Respond (ResponseType.Ok);
		
		#if DEBUG
		Console.WriteLine ("Filename: {0}", filename);
		#endif
	}
	
	public string Document {
		get {
			return filename;
		}
	}
	
	
}
}