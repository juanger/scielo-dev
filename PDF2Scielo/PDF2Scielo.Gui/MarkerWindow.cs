//
// MarkerWindow.cs: Main Window of application.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using Scielo.PDF2Text;

namespace Scielo.PDF2Scielo {
public partial class MarkerWindow: Gtk.Window {
	private RawDocument rdocument;
	
	public MarkerWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	private void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	private void OnQuitActivated (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}

	private void OnOpenActivated (object sender, System.EventArgs e)
	{
		OpenPDFDialog dialog = new OpenPDFDialog ();
		if (dialog.Run () == (int) ResponseType.Ok) {
			Uri uri = new Uri (dialog.Document);
			PDFPoppler reader = new PDFPoppler (uri, "atm");
			rdocument = reader.CreateRawDocument ();
			text_view.Buffer.Text = rdocument.GetText ();
			Markup.Sensitive = true;
		}
		
		dialog.Destroy ();
	}

	private void OnMarkupActivated (object sender, System.EventArgs e)
	{
		
	}
}
}