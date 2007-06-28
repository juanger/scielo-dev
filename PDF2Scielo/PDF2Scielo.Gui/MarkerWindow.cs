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
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
}