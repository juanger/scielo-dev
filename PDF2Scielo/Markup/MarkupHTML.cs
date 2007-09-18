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
	protected string text;
		
	public MarkupHTML (NormDocument document)
	{	
		front = document.Front;
		body = document.Body;
		back = document.Back;
	}	
	
	public MarkupHTML (string front, string body, string back)
	{
		if (front == null | body == null | back == null)
			throw new ArgumentNullException ();
		this.front = front;
		this.body = body;
		this.back = back;
	}
	
	public HTMLDocument CreateHTMLDocument ()
	{
		return new HTMLDocument (this);
	}
	
	public void MarkDocument ()
	{
		if (front == "" || body == "" || back == "")
			return;
			
		MarkFront ();
		MarkBody ();
		MarkBack ();
		text = HeadDocument () + front + body + back + FootDocument ();
	}
	
	public string Text {
		get {
			MarkDocument();
			return text;
		}
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
                       head += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\n";
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
		ReplaceTitleTag ();
		ReplaceAuthorTag ();
		ReplaceAffTag ();
		ReplaceDateTag ();
		ReplaceResTag ();
		ReplaceAbsTag ();
		ReplaceKeyTag ();
	}
	
	public void MarkBody ()
	{
		ReplaceParaTag ();
		ReplaceSecTag ();
		ReplaceSubsecTag ();
		ReplaceAckTag ();
	}
	
	public void MarkBack ()
	{
		ReplaceRefTag ();
		ReplaceCitTag ();
	}
	
	private void ReplaceTitleTag ()		
	{	
		string startTag = @"\[title\]";
		string endTag = @"\[/title\]";
		string startSustitute = "<p align=\"center\"><font face=\"verdana\" size=\"4\"><b>";
		string endSustitute = "</b></font></p>\n<p align=\"center\"><font face=\"verdana\" size=\"2\">&nbsp;</font></p>";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}		
		
	private void ReplaceAuthorTag ()		
	{	
		string startTag = @"\[author\]";
		string endTag = @"\[/author\]";
		string startSustitute = "<p align=\"center\"><font face=\"verdana\" size=\"2\">";
		string endSustitute = "<i><br>";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}		
		
	private void ReplaceAffTag ()		
	{	
		string startTag = @"\[aff\]";
		string endTag = @"\[/aff\]";
		string startSustitute = "";
		string endSustitute = "</i></font></p>";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}	
	
	private void ReplaceDateTag ()		
	{	
		string startTag = @"\[date\]";
		string endTag = @"\[/date\]";
		string startSustitute = "<font face=\"verdana\" size=\"2\">&nbsp;</font></p><p align=\"justify\"><font face=\"verdana\" size=\"2\">";
		string endSustitute = "</font></p><p align=\"justify\"><font face=\"verdana\" size=\"2\">&nbsp;</font></p>";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}		
			
	private void ReplaceResTag ()		
	{
		string label = @"\[res\] Resumen \[/res\]";
		string replace = @"\[res\] RESUMEN \[/res\]";
		front = Regex.Replace(front, label, replace);
		
		string startTag = @"\[res\]";
		string endTag = @"\[/res\]";
		string startSustitute = "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>";
		string endSustitute = "</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">";

		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);			
	}        
	
	private void ReplaceAbsTag ()		
 	{
 		string label = @"\[abs\] Abstract \[/abs\]";
		string replace = @"\[abs\] ABSTRACT \[/abs\]";
		front = Regex.Replace(front, label, replace);
		
 		string startTag = @"\[abs\]";
		string endTag = @"\[/abs\]";
		string startSustitute = "</font></p>\n<br><p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>";
		string endSustitute = "</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">";
 		
		front = Regex.Replace (front, startTag, startSustitute);
		front = Regex.Replace (front, endTag, endSustitute);
	}
				
	private void ReplaceKeyTag ()
	{
		string tag = "[key]";
		int index = front.IndexOf (tag,0);
		if (index == -1 )
			return;
		string end_tag = "[/key]";
		int end_index = front.IndexOf (end_tag, index);
				
		string keywords = front.Substring (index, end_index - index);
		front = front.Remove (index, keywords.Length );

		int index2 = keywords.IndexOf (":", 0);
		if (index2 == -1 )
			return;

		string cad = keywords.Substring (0,index2+1) +"</b>"+ keywords.Substring (index2+1);
		//front += cad;
		front = front.Insert (index, cad);
		
		Regex startTag = new Regex (@"\[key\]");
		Regex endTag = new Regex (@"\[/key\]");
		string startSustitute = "</font></p>\n<br><p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>";
		string endSustitute = "</font></p>\n<br>";
		front = startTag.Replace (front, startSustitute, 1);
		front = endTag.Replace (front, endSustitute, 1);
		ReplaceKeyTag ();
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
		string startSustitute = "<p align=\"justify\"><font face=\"verdana\" size=\"2\">";
		string endSustitute = "</font></p>";
		body = Regex.Replace (body, startTag, startSustitute);
		body = Regex.Replace (body, endTag, endSustitute);
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

	private void ReplaceAckTag ()
	{
		string startTag = @"\[ack\]";
		string endTag = @"\[/ack\]";
		string startSustitute = "<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>";
		string endSustitute = "</b></font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\">";
		body = Regex.Replace (body, startTag, startSustitute);
		body = Regex.Replace (body, endTag, endSustitute);
	}
	
	private void ReplaceRefTag ()
	{
		string startTag = @"\[ref\]";
		string endTag = @"\[/ref\]";
		string startSustitute = "</font></p>\n<p align=\"justify\"><font face=\"verdana\" size=\"2\"><b>";
		string endSustitute = "</b></font></p>";
		back = Regex.Replace (back, startTag, startSustitute);
		back = Regex.Replace (back, endTag, endSustitute);
	}
	
	private void ReplaceCitTag ()
	{
		string startTag = @"\[cit\]";
		string endTag = @"\[/cit\]";
		string startSustitute = "<p align=\"justify\"><font face=\"verdana\" size=\"2\">";
		string endSustitute = "</font></p>";
		back = Regex.Replace (back, startTag, startSustitute);
		back = Regex.Replace (back, endTag, endSustitute);
	}
}
}
}