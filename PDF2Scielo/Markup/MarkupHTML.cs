
using System.Text.RegularExpressions;
using System;

namespace Scielo {
namespace Markup {
		
public class MarkupHTML {
	
	private string front;
	private string body;
	private string back;
		
	public MarkupHTML (string front, string body, string back)
	{
		this.body = body;
		this.front = front;
		this.back = back;
	}	
			
	public string CreateDocumentHTML ()
	{
		if (front == "" || body == "" || back == "")
			return null;
			
		MarkFront ();
		string document = HeadDocument () + front + body + back + FootDocument ();
		return document;
	}
	
	public string Front {
		get {
			return front;
		}
	}
	
	public string Body {
		get {
			return body;
		}
	}
	
	public string Back {
		get {
			return back;
		}
	}
	
	private string HeadDocument ()
	{
		string head = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\"> \n";
                       head += "<html>\n";
		       head += "<head>\n";
		       head += "<title>Art&iacute;culo N</title>\n";
                       head += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">\n";
                       head += "</head>\n";
                       head += "<body>";
                return head;
	}
	
	private string FootDocument ()
	{
		string foot = "\n </body> \n";
			foot += "</html>";
		return foot;
	}
	
	private void MarkFront ()
	{
		ReplaceResTag ();
		ReplaceAbsTag ();
		ReplaceKeyTag ();
	}
	
	private void ReplaceResTag ()		
	{
		string startTag = @"\[res\]";
		string endTag = @"\[/res\]";
		string startSustitute = "<p align=\"center\">";
		string endSustitute = "</p> \n <p align=\"justify\">";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}        
	
	private void ReplaceAbsTag ()		
 	{
 		string startTag = @"\[abs\]";
		string endTag = @"\[/abs\]";
		string startSustitute = "</p> \n <br> <p align=\"center\">";
		string endSustitute = "</p> \n <p align=\"justify\">";
 		
		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);
	}
				
	private void ReplaceKeyTag ()
	{
		string tag = "[key]";
		int index = front.IndexOf (tag,0);
		string keywords = front.Substring (index);
		front = front.Remove (index, keywords.Length );
	
		int index2 = keywords.IndexOf (":", 0);
		string cad = keywords.Substring (0,index2+1) +"</b>"+ keywords.Substring (index2+1);
		front += cad;
		
		string startTag = @"\[key\]";
		string endTag = @"\[/key\]";
		string startSustitute = "</p> \n <br><p align=\"justify\"><b>";
		string endSustitute = "</p> \n <br>";
		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);		
	}	
}
}
}
