// project created on 1/22/2007 at 4:46 PM
using Gtk;
using System;
using System.IO;
using Poppler;


namespace Scielo {
namespace PDF2Text {

public class Article {
	private Document doc;
	private Rectangle rec;
	
	public Article (string file)
	{
		Uri uri = new Uri (file);
		Console.WriteLine ("1:" + uri.ToString ());

//		// Mainloop necesario para inicializar variables para uso de poppler.
//		MainLoop main = new MainLoop ();
//		main.Run ();

		
		try {
			doc = new Document (uri.ToString (), null);
		 } catch (GLib.GException e) {

			//TODO: Authentication.
                        if (e.Message.StartsWith ("Document is encrypted.")) {
                                Console.WriteLine ("FIXME: throw up an authentication dialog here...");
                                Environment.Exit (1);
                        }
                  }
                  
                  rec = new Rectangle ();
                  rec.X1 = 0.0;
                  rec.X2 = 1000.0;
                  rec.Y1 = 0.0;
                  rec.Y2 = 800.0;
	}
	
	public string GetText ()
	{
		Page page;
		string result = "";
		
                for (int i = 0; i < doc.NPages; i++) {
                        page = doc.GetPage (i);
                        result += page.GetText (rec);
                }
                
                return result;
	}
	
	public void CreateFile ()
	{
		FileStream filestream = null;

                using (filestream = File.Create ("/home/hector/text.txt")) {
                	StreamWriter writer = new StreamWriter (filestream);
                	writer.Write (GetText ());
                	writer.Flush ();
                	writer.Close ();
                }
	}
		
}
}
}