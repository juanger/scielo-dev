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
	}
}
}
