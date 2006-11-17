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
using System.Collections;

namespace AutomataLibrary {
public class DFAReader : FAReader {
	
	public DFAReader(string path) : base (path)
	{		
	}
	
	public override Hashtable GetDelta ()
	{
		string [][] trans = GetDeltaAux ();
		Hashtable hash = new Hashtable ();
		
		foreach (string [] str in trans) {
			hash.Add (new Context (str [0], str [1]), str [2]);
		}
		
		return hash;
	}
	
	private string [][] GetDeltaAux ()
	{
		string [][] result;
		int tcount = 0;
		
		XmlNode root = document.DocumentElement;
		XmlNodeList trans = root.SelectNodes ("/dfa/delta/*");
		result = new string [trans.Count][];
		
		foreach (XmlNode transnode in trans) {
			int icount = 0;
			result [tcount] = new string [3];
			XmlNodeList nodes = transnode.ChildNodes;

			foreach (XmlNode node in nodes) 	
				result [tcount] [icount++] = node.FirstChild.Value;
			
			tcount++;
		}
		
		return result;
	}
	
	public override string [] GetIStates ()
	{
		XmlNode root = document.DocumentElement;
		XmlNode node = root.SelectSingleNode ("/dfa/state");
		string [] result = new string [1];
		result [0] = node.FirstChild.Value;
		
		return result;
	}
}
}
