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
	
	private string styles_path;
	private bool val_success = true;

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
		XmlValidatingReader valReader;
		XmlSchema schema;
		Stream xsdStream;
		
		valReader = new XmlValidatingReader (reader);
		valReader.ValidationType = ValidationType.Schema;
		
		// Set the validation event handler
		valReader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);
		
		xsdStream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("style.xsd");
		schema = XmlSchema.Read (xsdStream, null);
		schema.Compile (null);
		
		valReader.Schemas.Add (schema);
		while (valReader.Read()){}
		
		#if DEBUG
		Console.WriteLine ("DEBUG: Validando estilo. La validacion fue {0}", 
			(val_success == true ? "exitosa!" : "no exitosa."));
		#endif
		
		valReader.Close();
		
		return val_success;
	}
	
	private string GetStyleFile (string format)
	{	
		Assembly assem = Assembly.GetExecutingAssembly ();
		string path = assem.Location.Replace ("/bin/Debug/PDF2Text.dll", String.Empty);
		path = Path.GetDirectoryName (path);
		styles_path = Path.Combine (path, "Styles");
		
		return Path.Combine (styles_path, format + ".xml");
	}
}
}
}