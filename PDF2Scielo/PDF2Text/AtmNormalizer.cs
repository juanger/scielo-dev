//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
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

using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace Scielo {
namespace PDF2Text {
	
public class AtmNormalizer : INormalizable {

	private string text;
	private ArrayList data_byte;
		
	public AtmNormalizer (string source)
	{
		data_byte = ConverterBytesUTF8 (source);
		ReplaceCodesTable();
		text = GetStringUnicode ();
	}
	
	public void SetEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public bool RemovePattern (string regexp)
	{
		return ReplacePattern (regexp, String.Empty);
	}
	
	public bool InsertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool ReplacePattern (string regexp, string substitute)
	{
		Regex regex = new Regex (regexp);
		
		text = regex.Replace (text, substitute);
		return text != null? true: false;
	}
	
	public bool ReplaceFootNotes (string regexp)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool ReplaceChars (ArrayList rechar)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public string Text {
		get {
			return text;
		}
	}
	
	private ArrayList ConverterBytesUTF8 (string data)
	{
		UTF8Encoding utf8 = new UTF8Encoding();
	        ArrayList bytes = new ArrayList( utf8.GetBytes(data) );
	        return bytes;
    	}
    	
	private bool ReplaceBytes (byte[] code, byte[] substitute)
	{
        	for (int i=0; i < data_byte.Count; i++) {
           		if ( data_byte[i].Equals(code[0]) ){
             			if ( CompareBytes (code, i) ){
                			SubstituteBytes (i, code, substitute);
                			i = i + substitute.Length;
             			}
           		}
        	}
        	return true;
    	}

    	private bool CompareBytes (byte[] code, int position)
    	{
          	int index = position;
          	
           	for (int i=0; i < code.Length; i++){
               		if (index >= data_byte.Count)
                	  	return false;
                	  	
               		if (code[i].Equals (data_byte [index]))
                    		index++;                    
                	else
                  		return false; 
           	}
	        return true;  
    	}

    	private void SubstituteBytes (int position, byte[] code, byte[] substitute)
    	{
        	DeleteCodeBytes (position, code);
        	InsertCodeBytes (position, substitute);
    	}
     
     	private void DeleteCodeBytes (int position, byte[] code)
     	{
	     	for (int i = 0; i < code.Length; i++ )
        	    data_byte.RemoveAt (position);
     	}

	private void InsertCodeBytes (int position, byte[] substitute)
	{
        	for (int i = 0; i < substitute.Length; i++){
        		data_byte.Insert (position, substitute[i]);
        		position++;    
        	}
     	}
     	
      	private String GetStringUnicode ()
      	{
        	UTF8Encoding utf8 = new UTF8Encoding();
        	Encoding unicode = Encoding.Unicode;
        	byte [] sustituteByte = new byte [data_byte.Count];
        	
           	for (int i = 0; i < data_byte.Count; i++)
              		sustituteByte[i] = (byte)data_byte [i];

           	byte [] unicodeBytes = Encoding.Convert (utf8, unicode, sustituteByte); 
           	char [] data = new char[ unicode.GetCharCount (unicodeBytes, 0, unicodeBytes.Length) ];     
           	unicode.GetChars( unicodeBytes, 0, unicodeBytes.Length, data, 0 );
           	String utfString = new String( data );
           	return utfString;	
     	} 
     	
	public ArrayList DataByte {
		get {
			return data_byte;
		}
	}
	
	private ArrayList CodesList ()
	{
		ArrayList table = new ArrayList();
		//" (start)
		table.Add( new CodesTable (new byte[] {194,147},
		                           new byte[] {38, 35, 49, 52, 55, 59}));
		//" (end)                           
		table.Add( new CodesTable (new byte[] {194,148},
		                           new byte[] {38, 35, 49, 52, 56, 59}));		
		//(start ')
                table.Add (new CodesTable (new byte [] {194,145},
                                           new byte [] {38, 35, 49, 52, 53, 59}));
                //(end ')                           
                table.Add (new CodesTable (new byte [] {194,146},
                        		   new byte [] {38, 35, 49, 52, 54, 59}));
                //(long -)                           
                table.Add (new CodesTable (new byte [] {194,150},
                        		   new byte [] {38, 35, 49, 53, 48, 59}));
                //(&aacute;)                           
                table.Add (new CodesTable (new byte [] {195,161},
                        		   new byte [] {38, 97, 97, 99, 117, 116, 101, 59}));
		//(&eacute;)                           
                table.Add (new CodesTable (new byte [] {195,169},
                        		   new byte [] {38, 101, 97, 99, 117, 116, 101, 59}));     
		//(&iacute;)                           
                table.Add (new CodesTable (new byte [] {195,173},
                        		   new byte [] {38, 105, 97, 99, 117, 116, 101, 59}));     
		//(&oacute;)                           
                table.Add (new CodesTable (new byte [] {195,179},
                        		   new byte [] {38, 111, 97, 99, 117, 116, 101, 59}));     
		//(&uacute;)                           
                table.Add (new CodesTable (new byte [] {195,186},
                        		   new byte [] {38, 117, 97, 99, 117, 116, 101, 59}));    
                //(&Aacute;)                           
                table.Add (new CodesTable (new byte [] {195,129},
                        		   new byte [] {38, 65, 97, 99, 117, 116, 101, 59}));
                //(&Eacute;)
                table.Add (new CodesTable (new byte [] {195,137},
                        		   new byte [] {38, 69, 97, 99, 117, 116, 101, 59}));        
                //(&Iacute;)                           
                table.Add (new CodesTable (new byte [] {195,141},
                        		   new byte [] {38, 73, 97, 99, 117, 116, 101, 59}));
                //(&Oacute;)                           
                table.Add (new CodesTable (new byte [] {195,147},
                        		   new byte [] {38, 79, 97, 99, 117, 116, 101, 59}));        		   
                //(&Uacute;)                           
                table.Add (new CodesTable (new byte [] {195,154},
                        		   new byte [] {38, 85, 97, 99, 117, 116, 101, 59}));                       		   
                //(&ntilde;)                           
                table.Add (new CodesTable (new byte [] {195,177},
                        		   new byte [] {38, 110, 116, 105, 108, 100, 101, 59}));
                //(&Ntilde;)                           
                table.Add (new CodesTable (new byte [] {195,145},
                        		   new byte [] {38, 78, 116, 105, 108, 100, 101, 59}));
                        		
		return table;                      
	}
	
	private void ReplaceCodesTable( ){
	       	ArrayList table = CodesList();
	       	for (int i = 0; i < table.Count; i++){
	       	        CodesTable codT = (CodesTable) table[i];
			ReplaceBytes( codT.Code, codT.Sustitute );  				
	       	}
	}
	
}
}
}