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
using System.IO;
using Scielo.PDF2Text;
using Scielo.Markup;

namespace Scielo.PDF2Scielo {
public partial class MarkerWindow: Gtk.Window {
	private RawDocument rdocument;
	private NormDocument ndocument;
	private HTMLDocument html_document;
	
	public MarkerWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		rdocument = null;
		ndocument = null;
		html_document = null;
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
			Normalize.Sensitive = true;
		}
		
		dialog.Destroy ();
	}
	
	private void OnMarkupActivated (object sender, System.EventArgs e)
	{
		if (ndocument == null)
			ndocument = rdocument.Normalize ();	
		
		MarkupHTML marker = new MarkupHTML (ndocument);
		html_document = marker.CreateHTMLDocument ();
		text_view.Buffer.Text = html_document.GetText ();
		Markup.Sensitive = false;
		Preview.Sensitive = true;
	}

	private void OnNormalizeActivated (object sender, System.EventArgs e)
	{
		ndocument = rdocument.Normalize ();
		text_view.Buffer.Text = ndocument.GetText ();
		Normalize.Sensitive = false;
	}

	private void OnPreviewActivated (object sender, System.EventArgs e)
	{
//		System.IO.StreamWriter html = File.CreateText();
		Preview preview = new Preview (html_document.GetText ());
	}
}
}