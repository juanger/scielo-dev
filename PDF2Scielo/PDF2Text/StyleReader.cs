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
	
	public StyleReader(string format)
	{
		XmlTextReader reader = new XmlTextReader (GetStyleFile (format));
		ValidateWithXSD (reader);
	}
	
  	public void ValidationCallBack (object sender, ValidationEventArgs args)
  	{
  		val_success = false;
  		Console.WriteLine("\r\n\tValidation error: " + args.Message );
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
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Validando estilo. La validacion fue {0}", 
			(val_success == true ? "exitosa!" : "no exitosa."));
		#endif
		
		//Close the reader.
		val_reader.Close();
		
		return val_success;
	}
	
	private string GetStyleFile (string format)
	{	
		string stylepath = Assembly.GetExecutingAssembly ().Location.Replace ("PDF2Scielo/bin/Debug/PDF2Text.dll", "Styles");
		return Path.Combine (stylepath, format + ".xml");
	}
}
}
}