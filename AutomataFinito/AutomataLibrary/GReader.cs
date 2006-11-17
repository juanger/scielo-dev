//
// DFAReader.cs: A Class that implements a reader for xml files
// that use the dfa.dtd.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Collections;

namespace AutomataLibrary {
public class GReader {
	private bool m_success = true;
	private XmlDocument document;
		
	public GReader(string path)
	{
		document = new XmlDocument ();
		
		try {
			XmlTextReader textreader = new XmlTextReader (path);
			XmlValidatingReader vreader = new XmlValidatingReader (textreader);
			
			// Set the validation event handler
      			vreader.ValidationEventHandler += new ValidationEventHandler (ValidationCallBack);

			// Load the XML to Document node.
			document.Load (textreader);
			Console.WriteLine ("Validation finished. Validation {0}", (m_success==true ? "successful!" : "failed."));
			
			//Close the reader.
			vreader.Close();

		} catch (FileNotFoundException e) {
			Console.WriteLine ("Error: {0} not found.", e.FileName);
			Environment.Exit (1);
		} catch (DirectoryNotFoundException) {
			Console.WriteLine ("Error: {0} not found.", path);
			Environment.Exit (1);
		} catch (XmlException) {
			Console.WriteLine ("Error: {0} is not well-formed xml.", path);
			Environment.Exit (1);
		}
	}
	
	public string GetDescription ()
	{
		XmlNode root = document.DocumentElement;
		XmlNode node = root.SelectSingleNode ("/gram/description");
		
		if (node == null)
			return null;
		
		return node.FirstChild.Value;
	}
	
	public string [] NonTerminals ()
	{
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/gram/alph[1]/*");
		string [] result = new string [nodes.Count];
		
		int count = 0;
		foreach (XmlNode node in nodes)
			result [count++] = node.FirstChild.Value;
			
		return result;
	}
	
	public string [] Terminals ()
	{
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/gram/alph[last()]/*");
		string [] result = new string [nodes.Count];
		
		int count = 0;
		foreach (XmlNode node in nodes)
			result [count++] = node.FirstChild.Value;
			
		return result;
	}
	
	public string [][] GetProductions ()
	{	
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/gram/productionSet/*");
		string [][] result = new string [nodes.Count][];
		
		int pcount = 0;
		foreach (XmlNode pnode in nodes) {
			result [pcount] = new string [2];
			
			XmlNodeList pnodes = pnode.ChildNodes; 
			XmlNode left  = pnodes [0];
			XmlNode right = pnodes [1];
		
			result [pcount] [0] = left.InnerText;
			if (right.FirstChild.InnerXml == "<epsilon />") {
				result [pcount] [1] = "epsilon";
				pcount++;
				continue;
			}
			
			int icount = 1;
			XmlNodeList rmnodes = right.ChildNodes;
			XmlNodeList rnodes = rmnodes[0].ChildNodes;
			StringBuilder sresult = new StringBuilder ();
			foreach (XmlNode rnode in rnodes) {
				
				if (rnodes.Count != icount)
					sresult.AppendFormat ("{0}:", rnode.FirstChild.Value);
				else
					sresult.Append (rnode.FirstChild.Value);
						
				icount++;
			}
			result [pcount] [1] = sresult.ToString ();
			pcount++;
		}
		
		return result;
	}
	
	public string GetISym ()
	{
		XmlNode root = document.DocumentElement;
		XmlNode node = root.SelectSingleNode ("/gram/sym");
		string result = node.FirstChild.Value;
		
		return result;
	}
	
	//Display the validation error.
  	private void ValidationCallBack (object sender, ValidationEventArgs args)
  	{
     		m_success = false;
     		Console.WriteLine("\r\n\tValidation error: " + args.Message );
  	}
}
}
