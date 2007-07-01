//
// PreviewDialog: Dialog that show a preview of a HTMLDocument.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;
using Gecko;

namespace Scielo.PDF2Scielo {
public partial class PreviewDialog : Gtk.Dialog {
	private WebControl preview;
	private string tmp_path;
	private static int tmp_file = 0;
	
	public PreviewDialog (string data)
	{
		this.Build();
		tmp_path = System.IO.Path.GetTempPath ();
		tmp_path = System.IO.Path.Combine (tmp_path, "editor");
		this.preview = new WebControl (tmp_path, "EditorGecko");
		Render (data);
		this.VBox.Add (preview);
		preview.Show ();
	}
	
	public void Render (string data) 
	{
		string filename = (tmp_file++) + ".html";
		string file_path = System.IO.Path.Combine (tmp_path, filename);
		using (FileStream file = new FileStream (file_path, FileMode.Create)) {
			StreamWriter sw = new StreamWriter (file);
			sw.Write (data);
			sw.Close ();
		}
		
		preview.LoadUrl (file_path);
	}
}
}
