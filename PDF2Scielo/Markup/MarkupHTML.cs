//
// MarkupHTML.cs: Class that mark the text with html's tags.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//
using System.Text.RegularExpressions;
using System;
using Scielo.PDF2Text;

namespace Scielo {
namespace Markup {
		
public class MarkupHTML {
	
	private string front;
	private string body;
	private string back;
		
	public MarkupHTML (NormDocument document)
	{
		front = document.Front;
		body = document.Body;
		back = document.Back;
	}	
	
	public MarkupHTML (string front, string body, string back)
	{
		this.front = front;
		this.body = body;
		this.back = back;
	}
			
	public string CreateDocumentHTML ()
	{
		if (front == "" || body == "" || back == "")
			return null;
			
		MarkFront ();
		MarkBody ();
		MarkBack ();
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
	
	public void MarkFront ()
	{
		ReplaceResTag ();
		ReplaceAbsTag ();
		ReplaceKeyTag ();
	}
	
	public void MarkBody ()
	{
		ReplaceParaTag ();
		ReplaceSecTag ();
		ReplaceSubsecTag ();
	}
	
	public void MarkBack ()
	{
		ReplaceRefTag ();
	}
	
	private void ReplaceResTag ()		
	{
		string startTag = @"\[res\]";
		string endTag = @"\[/res\]";
		string startSustitute = "<p align=\"center\">";
		string endSustitute = "</p>\n<p align=\"justify\">";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}        
	
	private void ReplaceAbsTag ()		
 	{
 		string startTag = @"\[abs\]";
		string endTag = @"\[/abs\]";
		string startSustitute = "</p>\n<br><p align=\"center\">";
		string endSustitute = "</p>\n<p align=\"justify\">";
 		
		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);
	}
				
	private void ReplaceKeyTag ()
	{
		string tag = "[key]";
		int index = front.IndexOf (tag,0);
		if (index == -1 )
		{
			Console.WriteLine("fuera de rango");
			return;
		}
		string keywords = front.Substring (index);
		front = front.Remove (index, keywords.Length );
	
		int index2 = keywords.IndexOf (":", 0);
		if (index2 == -1 )
		{
			Console.WriteLine("fuera de rango");
			return;
		}
		string cad = keywords.Substring (0,index2+1) +"</b>"+ keywords.Substring (index2+1);
		front += cad;
		
		string startTag = @"\[key\]";
		string endTag = @"\[/key\]";
		string startSustitute = "</p>\n<br><p align=\"justify\"><b>";
		string endSustitute = "</p>\n<br>";
		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);		
	}
	
	private void ReplaceSecTag ()
	{
		string startTag = @"\[sec\]";
		string endTag = @"\[/sec\]";
		string startSustitute = "<b>";
		string endSustitute = "</b><br>";
		body = Regex.Replace (body, startTag, startSustitute);
		body = Regex.Replace (body, endTag, endSustitute);
	}
	
	private void ReplaceParaTag ()
	{
		string startTag = @"\[para\]";
		string endTag = @"\[/para\]";
		string startSustitute = "<p align=\"justify\">";
		string endSustitute = "</p>";
		body = Regex.Replace (body, startTag, startSustitute);
		body = Regex.Replace (body, endTag, endSustitute);
	}
	
	private void ReplaceRefTag ()
	{
		string startTag = @"\[ref\]";
		string endTag = @"\[/ref\]";
		string startSustitute = "<p align=\"left\"><b>";
		string endSustitute = "</b></p>";
		back = Regex.Replace (back, startTag, startSustitute);
		back = Regex.Replace (back, endTag, endSustitute);
	}
	
	private void ReplaceSubsecTag ()
	{
		string startTag = @"\[subsec\]";
		string endTag = @"\[/subsec\]";
		string startSustitute = "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><i>";
		string endSustitute = "</i></font></p>";
		body = Regex.Replace (body, startTag, startSustitute);
		body = Regex.Replace (body, endTag, endSustitute);
	}
}
}
}