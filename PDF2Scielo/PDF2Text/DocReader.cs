//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;
using Poppler;


namespace Scielo {
namespace PDF2Text {

public class DocReader {
	private Document doc;
	private Rectangle rec;
	
	public DocReader (Uri uri)
	{
		Console.WriteLine ("DEBUG:" + " FILE = " + uri.ToString ());

		// Mainloop needed to initialize the use of poppler.
		//MainLoop main = new MainLoop ();
		//main.Run ();

		try {
			doc = new Document (uri.ToString (), null);
		} catch (GLib.GException e) {
		
			//TODO: Authentication.
			if (e.Message.StartsWith ("Document is encrypted.")) {
				Console.WriteLine ("FIXME: throw up an authentication dialog here...");
				Environment.Exit (1);
			}
		}

		if (doc != null) {
		
			// Creation of a Poppler.Rectangle that is the size of the page of the document.
			Page firstpage = doc.GetPage (0);
			double width, height;
		
			firstpage.GetSize (out width, out height);
		
			Console.WriteLine ("DEBUG: " + "width = " + width + " height = " + height);
			rec = new Rectangle ();
			rec.X1 = 0.0;
			rec.X2 = width;
			rec.Y1 = 0.0;
			rec.Y2 = height;
		} else
			rec = null;
	}
	
	public string GetText ()
	{
		Page page;
		string result = "";
		
                for (int i = 0; i < doc.NPages; i++) {
                	int temp = i + 1;
                        page = doc.GetPage (i);
                        result += page.GetText (rec);
                        result += "#!Scielo." + temp.ToString () + " ";
                }
                
                return result;
	}
	
	public void CreateFile (string filepath, string filename)
	{
		string name = filename + ".txt";
		FileStream filestream = null;

                using (filestream = File.Create (filepath + Path.DirectorySeparatorChar + name)) {
                	StreamWriter writer = new StreamWriter (filestream);
                	writer.Write (GetText ());
                	writer.Flush ();
                	writer.Close ();
                }
	}
		
}
}
}