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
using System.Collections;
using System.Text.RegularExpressions;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {
	
public class StyleReader {
	private XmlDocument document;
	private static bool val_success = true;
	private XmlNamespaceManager manager;
	
	public StyleReader(string format)
	{
		try {
			XmlTextReader reader = new XmlTextReader (GetStyleFile (format));
			ValidateWithXSD (reader);
			
			if (val_success) {
				document = new XmlDocument ();
				document.Load (GetStyleFile (format));
				manager = new XmlNamespaceManager (document.NameTable);
				manager.AddNamespace ("def", "http://www.scielo.org.mx");
			} else {
				Logger.Error ("Estilo de documento {0} no es válido.", format);
				throw new StyleException ("Estilo de documento " + format + " no es valido.");
			}
		} catch (IOException) {
			Logger.Error ("Estilo de documento {0} no existe.", format);
			throw new StyleException ("Estilo de documento " + format + " no existe.");
		}
	}
	
	public StyleReader (Uri uri)
	{
		try {
			XmlTextReader reader = new XmlTextReader (uri.LocalPath);
			ValidateWithXSD (reader);
			
			if (val_success) {
				document = new XmlDocument ();
				document.Load (uri.LocalPath);
				manager = new XmlNamespaceManager (document.NameTable);
				manager.AddNamespace ("def", "http://www.scielo.org.mx");
			} else {
				Logger.Error ("Estilo de documento {0} no es válido", uri.LocalPath);
				throw new StyleException ("Estilo de documento " + uri.LocalPath + " no es valido.");
			}
		} catch (IOException) {
			Logger.Error ("Estilo de documento {0} no existe.", uri.LocalPath);
			throw new StyleException ("Estilo de documento " + uri.LocalPath + " no existe.");
		}
	}
	
	public static String [] GetStyleList ()
	{
		string path = GetStylePath ();
		
		ArrayList list = new ArrayList ();
		foreach (String file in Directory.GetFiles (path)) {
			if (!ValidStyle (file))
				continue;
			
			Logger.Debug ("Style: {0}", file);
			list.Add (Path.GetFileNameWithoutExtension (file));
		}
		
		return (string []) list.ToArray (Type.GetType ("System.String"));
	}
	
	private static bool ValidStyle (string file)
	{
		if (!file.EndsWith(".xml"))
			return false;
		XmlTextReader reader = new XmlTextReader (file);
		return ValidateWithXSD (reader);
	}
	
	private static void ValidationCallBack (object sender, ValidationEventArgs args)
	{
		val_success = false;
		Console.WriteLine("\r\n\tValidation error: " + args.Message );
	}
	
	private static bool ValidateWithXSD (XmlReader reader)
	{
		XmlValidatingReader valReader;
		XmlSchema schema;
		Stream xsdStream;
		
		val_success = true;
		valReader = new XmlValidatingReader (reader);
		valReader.ValidationType = ValidationType.Schema;
		
		// Set the validation event handler
		valReader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);
		
		xsdStream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("style.xsd");
		schema = XmlSchema.Read (xsdStream, null);
		schema.Compile (null);
		
		valReader.Schemas.Add (schema);
		while (valReader.Read()){}
		valReader.Close();
		
		return val_success;
	}
	
	private static string GetStylePath ()
	{
		// TODO: Gran hack para obtener la ruta del directorio "style".
		Assembly assem = Assembly.GetExecutingAssembly ();
		Regex regexp = new Regex (@"/(PDF2Text|PDF2Scielo.Gui|PDF2Scielo)/bin/[^/]*/PDF2Text.dll");
		string path = regexp.Replace (assem.Location, String.Empty);
		
		return Path.Combine (path, "style");
	}
	
	public string GetStyleFile (string format)
	{
		return Path.Combine (GetStylePath (), format + ".xml");
	}
	
	public Rule [] GetRules ()
	{
		XmlNodeList fullList = document.SelectNodes ("//def:rule", manager);
		Logger.Info ("Número total de reglas: {0}", fullList.Count);
		Rule [] result = new Rule [fullList.Count];
		
		int counter = 0;
		XmlNodeList globalList = document.SelectNodes ("/def:style/def:global/*", manager);
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
	
	public int GetNumColumns ()
	{
		XmlNode node = document.SelectSingleNode ("/def:style/@ncolumns", manager);
		Logger.Info ("Número total de columnas: {0}", node.Value);
		
		return Int32.Parse (node.Value);
	}
}
}
}