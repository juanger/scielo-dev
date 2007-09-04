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
using Gecko;

namespace Scielo.PDF2Scielo {
public partial class MarkerWindow: Gtk.Window {
	private RawDocument rdocument;
	private NormDocument ndocument;
	private HTMLDocument html_document;
	private PreviewDialog preview = null;
	
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
		Normalize.Sensitive = false;
		Preview.Sensitive = true;
	}

	private void OnNormalizeActivated (object sender, System.EventArgs e)
	{
		StyleSelectDialog dialog = new StyleSelectDialog ();
		
		if (dialog.Run () == (int) ResponseType.Ok) {
			try {
				ndocument = rdocument.Normalize ();
				text_view.Buffer.Text = ndocument.GetText ();
				Normalize.Sensitive = false;
			} catch (Exception exception){
				MessageDialog md = new MessageDialog (this,
					DialogFlags.DestroyWithParent, 
					MessageType.Error, 
					ButtonsType.Ok, 
					exception.Message);
				md.Run ();
				md.Destroy();
			}
		}
		
		dialog.Destroy ();
	}

	private void OnPreviewActivated (object sender, System.EventArgs e)
	{
		if (preview == null)
			preview = new PreviewDialog (html_document.GetText ());
		else
			preview.Render (html_document.GetText ());
		
		preview.Run ();
		preview.Hide ();
	}

	private void OnAboutActivated (object sender, System.EventArgs e)
	{
		AboutDialog dialog = new AboutDialog ();
		dialog.Run ();
	}
}
}