//
// StringEncoding: A class that implements the coders and decoders
// (a restrict list) of a string. 
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
using System.Text;
using System.Collections;

namespace Scielo {
namespace Utils {
	
public class StringEncoding {
	
	private Encoding encoder;
	public ArrayList data_byte;
        private static ArrayList characters_default = StringEncoding.CodesList();  
 
	public StringEncoding (string data)
	{
		encoder = new UTF8Encoding ();
		data_byte = ConverterDataInBytes (data);
	}
	
	public StringEncoding (string data, int codePage)
	{
		encoder = Encoding.GetEncoding (codePage);
		data_byte = ConverterDataInBytes (data);
	}
		
	public ArrayList ConverterDataInBytes (string data)
	{
	       	ArrayList bytes = new ArrayList (encoder.GetBytes(data));
	        return bytes;
    	}    	
    	
     	public String GetStringUnicode ()
      	{
        	Encoding unicode = Encoding.Unicode;
        	byte [] sustituteByte = new byte [data_byte.Count];
        	
           	for (int i = 0; i < data_byte.Count; i++)
              		sustituteByte[i] = (byte)data_byte [i];

           	byte [] unicodeBytes = Encoding.Convert (encoder, unicode, sustituteByte); 
           	char [] data = new char[ unicode.GetCharCount (unicodeBytes, 0, unicodeBytes.Length) ];     
           	unicode.GetChars( unicodeBytes, 0, unicodeBytes.Length, data, 0 );
           	String utfString = new String( data );
           	return utfString;
     	} 
     	
     	public void ReplaceCodesTable (ArrayList table){
	       	for (int i = 0; i < table.Count; i++){
	       	        CodesTable codT = (CodesTable) table[i];
			ReplaceBytes( codT.Code, codT.Sustitute );
	       	}
	}
	
	public static ArrayList CharactersDefault{
		get {
			return characters_default;
		}
	}
	
	public ArrayList DataByte{
		get {
			return data_byte;
		}
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
    	
    	private static ArrayList CodesList ()
	{
		ArrayList table = new ArrayList();
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
                //----------------------------symbols
                //° ---> &#176; ---> &deg;
		
		//• ---> &#149; ---> &bull;
		//« ---> &#171; ---> &lt;&lt;
		//» ---> &#187; ---> &gt;&gt;
		//± ---> &#177; ---> &plusmn;
		//™ ---> &#153; ---> <sup>TM</sup>
		//® ---> &#174; ---> &reg;
		//© ---> &#169; ---> &copy;
		//ª ---> &#170; ---> <sup>a</sup>
		//ä ---> &#288; ---> &auml;
		//â ---> &#226; ---> &acirc;
		//à ---> &#224; ---> &agrave;
		//å ---> &#229; ---> &aring;
		//ô ---> &#244; ---> &ocirc;
		//ã ---> &#227; ---> &atilde;
		//è ---> &#232; ---> &egrave;
		//ê ---> &#234; ---> &ecirc;
		//ç ---> &#231; ---> &ccedil;
		//î ---> &#238; ---> &icirc;
		//ì ---> &#236; ---> &igrave;
		//a ---> ???? ---> &alpha;
		//ß ---> &#223; ---> &beta;
		//µ ---> &#181; ---> &micro;

		//(start ')
                table.Add (new CodesTable (new byte [] {194,145},
                                           new byte [] {38, 35, 49, 52, 53, 59}));
                //(end ')                           
                table.Add (new CodesTable (new byte [] {194,146},
                        		   new byte [] {38, 35, 49, 52, 54, 59}));
		//" (start)
		table.Add( new CodesTable (new byte [] {194,147},
		                           new byte [] {38, 35, 49, 52, 55, 59}));
		//" (end)                           
		table.Add( new CodesTable (new byte [] {194,148},
		                           new byte [] {38, 35, 49, 52, 56, 59}));		
                //(long -)                           
                table.Add (new CodesTable (new byte [] {194,150},
                        		   new byte [] {38, 35, 49, 53, 48, 59}));
                //(&iexcl; )                           
                table.Add (new CodesTable (new byte [] {194,161},
                        		   new byte [] {38, 35, 49, 54, 49, 59}));
                //(Cent sign - &#162;)                           
                table.Add (new CodesTable (new byte [] {194,162},
                        		   new byte [] {38, 35, 49, 54, 50, 59}));
                //(&deg; degree sign)                           
                table.Add (new CodesTable (new byte [] {194,176},
                        		   new byte [] {38, 35, 49, 55, 54, 59}));	   
                //(&iquest; )                           
                table.Add (new CodesTable (new byte [] {194,191},
                        		   new byte [] {38, 35, 49, 57, 49, 59}));
                //(En Dash - – &#8211;)                           
                table.Add (new CodesTable (new byte [] {226,128,147},
                        		   new byte [] {38, 35, 56, 50, 49, 49, 59}));
                //(Right Single Quotation Mark - ’ &#8217; )                           
                table.Add (new CodesTable (new byte [] {226,128,153},
                        		   new byte [] {38, 35, 56, 50, 49, 55, 59}));     
                 //(Dagger † - &#134;)                           
                table.Add (new CodesTable (new byte [] {226,128,160},
                        		   new byte [] {38, 35, 49, 51, 52, 59}));       		   
		return table;                      
		}
	
}
}
}
