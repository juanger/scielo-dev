//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Alejandro Rosendo Robles. (rosendo69@hotmail.com)
//   Virginia Teodosio Procopio. (ainigriv_t@hotmail.com)
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Xml;
using System.Collections;

using System.IO;

namespace Scielo{
namespace MarkupHTML{

/* This class implements the read from an XML file which one contents the formal definition of an specific NPDA 
 and divide and organized in classes all the formal elements of NPDA */ 
public class XReaderNPDA {

	public XmlNodeList deltaElements;
	public XmlNode [] deltaTransitions;
	
	/**
	* This constructor reads the name file that contains the definition of the NPDA.
	*/
	public XReaderNPDA( string namefile )
	{
		XmlDocument document = new XmlDocument ();
		document.Load (namefile);
		XmlNode root = document.DocumentElement;
		deltaElements = root.SelectNodes ("/pda/delta/trans");
		Console.WriteLine ("en la lista=="+deltaElements.Count);
	}
	
	/* This method gets the full Delta Relation defined in the XML document in the form of list of nodes
	*/
	public void getDeltaRelation ()
	{
		int count = 1;
		foreach (XmlNode alph2 in deltaElements) {
			XmlNodeList listTmp = alph2.ChildNodes;
			Console.WriteLine ("elemento=="+count);
			
			foreach (XmlNode alph3 in listTmp) {
				Console.WriteLine ("::"+alph3.FirstChild.Value);
			}
		count ++;
		}
	}
	
	public static void Main ()
	{
		XReaderNPDA rXN = new XReaderNPDA ("npdaAtm.xml");
		rXN.getDeltaRelation ();
	}
}
}
}