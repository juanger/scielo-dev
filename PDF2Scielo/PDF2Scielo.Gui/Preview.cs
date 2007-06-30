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
using Gecko;

namespace Scielo.PDF2Scielo {
public class Preview : Gtk.Window {
	private WebControl moz;
	
	public Preview(string data) : base (Gtk.WindowType.Toplevel)
	{
		this.SetDefaultSize (800, 800);
		VBox vbox = new VBox (false, 1);
		this.Add (vbox);
		
		moz = new WebControl ("/tmp/csharp", "GeckoTest");
		//moz.LoadUrl ("www.google.com");

//		moz.RenderData ("<html>Hola</html>", "file://home/hector/test.html", "text/html");
		vbox.PackStart(moz, true, true, 1);
		
		this.ShowAll ();
	}
}
}
