//
// Driver.cs: Entry Point of the GUI frontend of pdf2scielo.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using Gtk;

namespace Scielo.PDF2Scielo {
class MainClass {
	public static void Main (string[] args)
	{
		Application.Init ();
		MarkerWindow win = new MarkerWindow ();
		win.Show ();
		Application.Run ();
	}
}
}