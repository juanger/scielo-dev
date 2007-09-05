//
// Foo.cs: A class that implements
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using Scielo.PDF2Text;

namespace Scielo.PDF2Scielo {
public partial class StyleSelectDialog : Gtk.Dialog {
	private ComboBox box;
	
	public StyleSelectDialog ()
	{
		this.Build();
		box = new ComboBox (StyleReader.GetStyleList ());
		Gtk.Table table = new Table (2, 2, false);
		this.VBox.Add (table);
		table.Attach (new Label ("Style: "), 0, 1, 0, 1, AttachOptions.Shrink, AttachOptions.Shrink, 0, 0);
		table.Attach (box, 1, 2, 0, 1, AttachOptions.Fill, AttachOptions.Expand, 0, 0);
		table.Attach (new Gtk.HSeparator (), 0, 2, 1, 2, AttachOptions.Expand | AttachOptions.Fill, AttachOptions.Shrink, 0, 5);
		this.ShowAll ();
	}
	
	public ComboBox Box {
		get {
			return box;
		}
	}
}
}
