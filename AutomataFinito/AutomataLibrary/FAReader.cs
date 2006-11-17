//
// FAReader.cs: An abstract class that represents a reader of xml files.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using System.IO;
using System.Xml;
using System.Collections;

namespace AutomataLibrary {
public abstract class FAReader {
	protected XmlDocument document;
	
	public FAReader(string path)
	{
		document = new XmlDocument ();
		
		try {
			XmlTextReader textreader = new XmlTextReader (path);
			XmlValidatingReader vreader = new XmlValidatingReader (textreader);
			document.Load (textreader);
			textreader.Close ();
		} catch (FileNotFoundException e) {
			Console.WriteLine ("Error: {0} not found.", e.FileName);
			Environment.Exit (1);
		} catch (DirectoryNotFoundException) {
			Console.WriteLine ("Error: {0} not found.", path);
			Environment.Exit (1);
		}
	}
	
	public string GetDescription ()
	{
		XmlNode root = document.DocumentElement;
		XmlNode node = root.SelectSingleNode ("/*/description");
		
		if (node == null)
			return null;
		
		return node.FirstChild.Value;
	}

	public string [] GetStates ()
	{
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/*/stateSet[1]/*");
		string [] result = new string [nodes.Count];
		
		int count = 0;
		foreach (XmlNode node in nodes)
			result[count++] = node.FirstChild.Value;
		
		return result;
	}
	
	public string [] GetAlph ()
	{
		string [] result;
		int count = 0;
	
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/*/alph/*");
		result = new string [nodes.Count];
		
		foreach (XmlNode node in nodes)
			result[count++] = node.FirstChild.Value;
			
		return result;
	}

	abstract public Hashtable GetDelta ();
		
	abstract public string [] GetIStates ();
	
	public string [] GetFStates ()
	{
		string [] result;
		int count = 0;
	
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/*/stateSet[last()]/*");
		result = new string [nodes.Count];
		
		foreach (XmlNode node in nodes)
			result[count++] = node.FirstChild.Value;
			
		return result;
	}
}
}
