//
// NFAReader.cs: A Class that implements a reader for xml files
// that use the ndfa.dtd.
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
using System.Text;

namespace AutomataLibrary {
public class NFAReader : FAReader {
	
	public NFAReader(string path) : base (path)
	{
	}
	
	public override Hashtable GetDelta ()
	{
		ArrayList [] trans = GetDeltaAux ();
		Hashtable hash = new Hashtable ();
		
		foreach (ArrayList list in trans) {
			Context con = new Context ((string) list[0], (string) list [1]);
			
			string [] tstates = new string [list.Count - 2];
			list.CopyTo (2, tstates, 0, tstates.Length);
			
			StringBuilder fstates = new StringBuilder ();
			
			int count = 1;
			foreach (string state in tstates)
				if (count++ != tstates.Length)
					fstates.AppendFormat ("{0}:", state);
				else
					fstates.AppendFormat ("{0}", state);
			
			hash.Add (con, fstates.ToString ());
		}
		
		return hash;
	}
	
	private ArrayList [] GetDeltaAux ()
	{
		int tcount = 0;
		ArrayList [] result;
		
		XmlNode root = document.DocumentElement;
		XmlNodeList trans = root.SelectNodes ("/ndfa/delta/*");
		result = new ArrayList [trans.Count];
		
		foreach (XmlNode transnode in trans) {
			XmlNodeList tnodes = transnode.ChildNodes;
			
			result [tcount] = new ArrayList ();
			
			result [tcount].Add (tnodes [0].FirstChild.Value);
			result [tcount].Add (tnodes [1].FirstChild.Value);
			
			XmlNodeList snodes = tnodes[2].ChildNodes;
			
			foreach (XmlNode node in snodes)
				result [tcount].Add (node.FirstChild.Value);
			
			
			tcount++;
		}
		
		return result;
	}
	
	public override string [] GetIStates ()
	{
		XmlNode root = document.DocumentElement;
		XmlNodeList nodes = root.SelectNodes ("/ndfa/stateSet[2]/*");
		string [] result = new string [nodes.Count];
		
		int count = 0;
		foreach (XmlNode node in nodes)
			result [count++] = node.FirstChild.Value;
		
		return result;
	}
}
}
