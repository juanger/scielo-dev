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
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {
	
public class StyleReader {
	private XmlDocument document;
	private string styles_path;
	private bool val_success = true;
	private XmlNamespaceManager manager;
	
	public StyleReader(string format)
	{
		// FIXME: Revisar posible bug en Mono cuando se tienen atributos
		// en el elemento raiz del XML.
		XmlTextReader reader = new XmlTextReader (GetStyleFile (format));
		ValidateWithXSD (reader);
		
		if (val_success) {
			document = new XmlDocument ();
			document.Load (GetStyleFile (format));
			manager = new XmlNamespaceManager (document.NameTable);
			manager.AddNamespace ("def", "http://www.scielo.org.mx");
		} else {
			Logger.Log (Level.ERROR, "Estilo de documento {0} no es válido", format);
			throw new Exception ("Estilo de documento " + format + " no es valido.");
		}
	}
	
	public StyleReader (Uri uri)
	{
		XmlTextReader reader = new XmlTextReader (uri.LocalPath);
		ValidateWithXSD (reader);
		
		if (val_success) {
			document = new XmlDocument ();
			document.Load (uri.LocalPath);
			manager = new XmlNamespaceManager (document.NameTable);
			manager.AddNamespace ("def", "http://www.scielo.org.mx");
		} else {
			Logger.Log (Level.ERROR, "Estilo de documento {0} no es válido", uri.LocalPath);
			throw new Exception ("Estilo de documento " + uri.LocalPath + " no es valido.");
		}
	}
	
	private void ValidationCallBack (object sender, ValidationEventArgs args)
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
		
		Logger.Log (Level.DEBUG, "Validando estilo. La validacion fue {0}", 
			(val_success == true ? "exitosa!" : "no exitosa."));
		
		valReader.Close();
		
		return val_success;
	}
	
	public string GetStyleFile (string format)
	{	
		Assembly assem = Assembly.GetExecutingAssembly ();
		Logger.Log (Level.INFO, "Test: {0}", assem.Location);
		Regex regexp = new Regex (@"/(PDF2Text|PDF2Scielo.Gui|PDF2Scielo)/bin/[^/]*/PDF2Text.dll");
		string path = regexp.Replace (assem.Location, String.Empty);
//		if (path.Equals (assem.Location))
//			throw new DirectoryNotFoundException ();
		
		styles_path = Path.Combine (path, "style");
		
		return Path.Combine (styles_path, format + ".xml");
	}
	
	public Rule [] GetRules ()
	{
		XmlNodeList fullList = document.SelectNodes ("//def:rule", manager);
		Logger.Log (Level.INFO, "Número de reglas: {0}", fullList.Count);
		Rule [] result = new Rule [fullList.Count];
		
		int counter = 0;
		XmlNodeList globalList = document.SelectNodes ("/def:style/def:global/*", manager);
		Logger.Log (Level.INFO, "Numero de reglas en global: {0}", globalList.Count);
		foreach (XmlNode node in globalList) {
			result [counter] = new Rule (node, manager, BlockType.GLOBAL);
			counter++;
		}
		
		XmlNodeList frontList = document.SelectNodes ("/def:style/def:front/*", manager);
		foreach (XmlNode node in frontList) {
			result [counter] = new Rule (node, manager, BlockType.FRONT);
			counter++;
		}
		
		XmlNodeList bodyList = document.SelectNodes ("/def:style/def:body/*", manager);
		foreach (XmlNode node in bodyList) {
			result [counter] = new Rule (node, manager, BlockType.BODY);
			counter++;
		}
		
		XmlNodeList backList = document.SelectNodes ("/def:style/def:back/*", manager);
		foreach (XmlNode node in backList) {
			result [counter] = new Rule (node, manager, BlockType.BACK);
			counter++;
		}
		
		return result;
	}
}
}
}