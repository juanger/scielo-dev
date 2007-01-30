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
using System.Text;
using System.IO;
using Poppler;


namespace Scielo {
namespace PDF2Text {

public class DocReader {
	private Document doc;
	private Rectangle rec;
	
	private DocReader (Document doc, Rectangle rec)
	{
		this.doc = doc;
		this.rec = rec;
	}

	public static DocReader CreateInstance (Uri uri)
	{
		Document document = null;
		Rectangle rectangle;
		
		#if DEBUG
		Console.WriteLine ("DEBUG: " + uri.ToString ());
		#endif
		
		try {
			document = new Document (uri.ToString (), null);
		} catch (GLib.GException e) {
		
			// TODO: Authentication.
			if (e.Message.StartsWith ("Document is encrypted.")) {
				Console.WriteLine ("FIXME: throw up an authentication dialog here...");
				Environment.Exit (1);
			}
		}

		if (document != null) {
			// Creation of a Poppler.Rectangle that is the size of 
			// the page of the document.
			Page firstpage = document.GetPage (0);
			double width, height;
		
			firstpage.GetSize (out width, out height);
		
			#if DEBUG
			Console.WriteLine ("DEBUG: " + "width = " + width + " height = " + height);
			#endif
			
			rectangle = new Rectangle ();
			rectangle.X1 = 0.0;
			rectangle.X2 = width;
			rectangle.Y1 = 0.0;
			rectangle.Y2 = height;
			
			return new DocReader (document, rectangle);
		} else
			return null;
	}
	
	public string GetText ()
	{
		Page page;
		int capacity, count;
		string pseparator;
		
		capacity = doc.NPages * 4000;
		StringBuilder result = new StringBuilder (capacity);
		for (int i = 0; i < doc.NPages; i++) {
			count = i + 1;
			pseparator = "#!Scielo." + count.ToString () + " ";
                        page = doc.GetPage (i);
                        result.Append (page.GetText (rec));
                        result.Append (pseparator);
                }
                
                return result.ToString ();
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