//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Scielo {
namespace PDF2Text {
	
public class StyleReader {
	
	XmlValidatingReader val_reader;
	bool m_success = true;
	
	public StyleReader()
	{
		XmlTextReader reader = new XmlTextReader ("/home/hector/Projects/PDF2Scielo/PDF2Text/Test/test.xml");
		val_reader = new XmlValidatingReader (reader);
		
		//
		val_reader.XmlResolver = new MyDTDResolver ();
		
		//
		val_reader.ValidationType = ValidationType.DTD;
		
		// Set the validation event handler
		val_reader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);
		
		// Read XML data
		while (val_reader.Read()){}
		Console.WriteLine ("Validation finished. Validation {0}", (m_success==true ? "successful!" : "failed."));
		
		//Close the reader.
		val_reader.Close();
	}
	
	//Display the validation error.
  	public void ValidationCallBack (object sender, ValidationEventArgs args)
  	{
  		m_success = false;
  		Console.WriteLine("\r\n\tValidation error: " + args.Message );
  	}
  	
  	private class MyDTDResolver : XmlUrlResolver {
  		public override object GetEntity (Uri absoluteUri, string role, Type ofObjectToReturn)
  		{
			FileStream filestream = File.Open ("/home/hector/Projects/PDF2Scielo/PDF2Text/Data/style.dtd", FileMode.Open);
  		
  			return filestream;
  		}
	}
}
}
}