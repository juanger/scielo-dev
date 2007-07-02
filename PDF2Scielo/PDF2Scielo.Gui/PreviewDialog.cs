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
		try {
			string filename = "cachedDoc.html";
			string file_path = System.IO.Path.Combine (tmp_path, filename);
			using (FileStream file = new FileStream (file_path, FileMode.Create)) {
				StreamWriter sw = new StreamWriter (file);
				sw.Write (data);
				sw.Close ();
			}
		
			preview.LoadUrl (file_path);
			File.Delete (file_path);
		}catch(IOException except){
			Console.WriteLine ("ERROR: "+ except.Message);
		}
	}
}
}
