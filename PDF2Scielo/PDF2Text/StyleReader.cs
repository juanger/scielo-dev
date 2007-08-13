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
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;

namespace Scielo {
namespace PDF2Text {
	
public class StyleReader {
	private XmlDocument document;
	private string styles_path;
	private bool val_success = true;

	public StyleReader(string format)
	{
//		XmlTextReader reader = new XmlTextReader (GetStyleFile (format));
//		ValidateWithXSD (reader);
		document = new XmlDocument ();
		document.Load (new XmlTextReader (GetStyleFile (format)));
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
	
	public string GetStyleFile (string format)
	{	
		Assembly assem = Assembly.GetExecutingAssembly ();
		Console.WriteLine ("Test: {0}", assem.Location);
		Regex regexp = new Regex (@"/PDF2Text/bin/[^/]*/PDF2Text.dll");
		string path = regexp.Replace (assem.Location, String.Empty);
//		if (path.Equals (assem.Location))
//			throw new DirectoryNotFoundException ();
		
		styles_path = Path.Combine (path, "Styles");
		
		return Path.Combine (styles_path, format + ".xml");
	}
	
	public Rule [] GetRules ()
	{
		XmlNodeList fullList = document.SelectNodes ("//rule");
		Console.WriteLine ("Numero de reglas: ", fullList.Count);
		Rule [] result = new Rule [fullList.Count];
		
		int counter = 0;
		XmlNodeList globalList = document.SelectNodes ("/style/global/*");
		foreach (XmlNode node in globalList) {
			result [counter] = new Rule (node, BlockType.GLOBAL);
			Console.WriteLine ("Nombre regla: ", result [counter].Name);
			counter++;
		}
		
		XmlNodeList frontList = document.SelectNodes ("/style/front/*");
		foreach (XmlNode node in frontList) {
			result [counter] = new Rule (node, BlockType.FRONT);
			counter++;
		}
		
		XmlNodeList bodyList = document.SelectNodes ("/style/body/*");
		foreach (XmlNode node in bodyList) {
			result [counter] = new Rule (node, BlockType.BODY);
			counter++;
		}
		
		XmlNodeList backList = document.SelectNodes ("/style/back/*");
		foreach (XmlNode node in backList) {
			result [counter] = new Rule (node, BlockType.BACK);
			counter++;
		}
		
		return result;
	}
}
}
}