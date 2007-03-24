//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Alejandro Rosendo Robles. (rosendo69@hotmail.com)
//   Virginia Teodosio Procopio. (ainigriv_t@hotmail.com)
// Copyright (C) 2007 UNAM DGB
//
using System.Text.RegularExpressions;
using System;

namespace Scielo {
namespace Markup {
		
public class MarkupHTML {
	private string text;
		
	public MarkupHTML (string txt)
	{
		text = txt;
	}
		
	public string Text {
		get {
			return text;
		}
	}
	
	public string HeadDocument()
	{
		string head = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\"> \n";
                       head += "<html>\n";
		       head += "<head>\n";
		       head += "<title>Art&iacute;culo N</title>\n";
                       head += "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">\n";
                       head += "</head>\n";
		       Console.WriteLine(head);
                return head;
	}
	/*
	private string Front(){
	}
	
	private string Body(){
	   ReplaceAbsTag();			
	}
			
	private string Back(){
	}
	
    */
	public string CreateDocumentHTML(){
	    string document = HeadDocument() + text;
	    return document;
	}
	
	public void ReplaceAbsTag ()		
 	{
 		string startTag = @"\[abs\]";
		string endTag = @"\[/abs\]";
		string startSustitute = "<p align=\"center\">";
		string endSustitute = "</p> \n <p align=\"justify\">";
 		
		text = Regex.Replace(text, startTag, startSustitute);
		text = Regex.Replace(text, endTag, endSustitute);
	}
	
	public void ReplaceResTag ()		
	{
		string startTag = @"\[res\]";
		string endTag = @"\[/res\]";
		string startSustitute = "</p> \n <br><p align=\"center\">";
		string endSustitute = "</p> \n <p align=\"justify\">";

		text = Regex.Replace(text, startTag, startSustitute);
		text = Regex.Replace(text, endTag, endSustitute);			
	}        
		
}
}
}
