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
using Scielo.Utils;

namespace Scielo.PDF2Scielo {
public partial class PreviewDialog : Gtk.Dialog {
	private WebControl preview;
	private string tmp_path;
	
	public PreviewDialog (string data)
	{
		this.Build();
		tmp_path = System.IO.Path.GetTempPath ();
		tmp_path = System.IO.Path.Combine (tmp_path, "editor");
		preview = new WebControl (tmp_path, "EditorGecko");
		preview.NetStop += OnComplete;
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
		}catch(IOException except){
			Logger.Log (Level.ERROR, "{0}", except.Message);
		}
	}
	
	public void OnComplete (object o, EventArgs e)
	{
		try {
			File.Delete ("cachedDoc.html");
		} catch(IOException except){
			Logger.Log (Level.ERROR, "{0}", except.Message);
		}
	}
}
}
