//
// MarkerWindow.cs: Main Window of application.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using Gtk;

namespace Scielo.PDF2Scielo {
public partial class MarkerWindow: Gtk.Window {
	
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
	}
}
}