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
using System.Reflection;

namespace Scielo {
namespace PDF2Text {
	
public class StyleReader {
	
	XmlValidatingReader val_reader;
	XmlSchema schema;
	bool val_success = true;
	
	public StyleReader()
	{
		XmlTextReader reader = new XmlTextReader ("/home/hector/Projects/PDF2Scielo/PDF2Text/Test/test.xml");
		ValidateWithDTD (reader);
		reader = new XmlTextReader ("/home/hector/Projects/PDF2Scielo/PDF2Text/Test/test-schema.xml");
		ValidateWithXSD (reader);
	}
	
	//Display the validation error.
  	public void ValidationCallBack (object sender, ValidationEventArgs args)
  	{
  		val_success = false;
  		Console.WriteLine("\r\n\tValidation error: " + args.Message );
  	}
  	
  	private class StyleDTDResolver : XmlUrlResolver {
  		public override object GetEntity (Uri absoluteUri, string role, Type ofObjectToReturn)
  		{
			return Assembly.GetExecutingAssembly ().GetManifestResourceStream ("style.dtd");
  		}
	}
	
	private bool ValidateWithDTD (XmlReader reader) 
	{
		val_reader = new XmlValidatingReader (reader);
		val_reader.XmlResolver = new StyleDTDResolver ();
		val_reader.ValidationType = ValidationType.DTD;
		
		// Set the validation event handler
		val_reader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);
		
		while (val_reader.Read()){}
		
		Console.WriteLine ("Validation finished. Validation {0}", 
			(val_success == true ? "successful!" : "failed."));
		
		//Close the reader.
		val_reader.Close();
		
		return val_success;
	}
	
	private bool ValidateWithXSD (XmlReader reader)
	{
		val_reader = new XmlValidatingReader (reader);
		val_reader.ValidationType = ValidationType.Schema;
		
		// Set the validation event handler
		val_reader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);
		
		Stream xsd = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("style.xsd");
		schema = XmlSchema.Read (xsd, null);
		schema.Compile (null);
		
		val_reader.Schemas.Add (schema);
		while (val_reader.Read()){}
		
		Console.WriteLine ("Validation finished. Validation {0}", 
			(val_success == true ? "successful!" : "failed."));
		
		//Close the reader.
		val_reader.Close();
		
		return val_success;
	}
}
}
}