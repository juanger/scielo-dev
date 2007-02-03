//
// XReaderNPDA.cs: This class implements the read from an XML file which one contents the 
// formal definition of an specific NPDA and divide and organized in classes all the formal 
// elements of NPDA.
//
// Author:
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Xml;
using System.Collections;

using System.IO;

namespace Scielo {
namespace MarkupHTML {

public class XReaderNPDA {

	private XmlNode root;

	protected string [] sigma;
	protected string [] gamma;
	protected string symbolStack;
	protected string initialState;

	/**
	* This constructor reads the name file that contains the definition of the NPDA. 
	*/
	public XReaderNPDA( string namefile )
	{
		XmlDocument document = new XmlDocument ();
		document.Load (namefile);

		root = document.DocumentElement;

		GetAlphabets ();
		symbolStack = SetSymbolStack ();
		initialState = SetInitialState ();
	}

	/* This method gets the full Delta Relation defined in the XML document in the form of list of nodes
	*/
	public Hashtable GetDelta()
	{
		XmlNodeList deltaElements = root.SelectNodes ("/pda/delta/trans");	
		Hashtable hash = new Hashtable (); 

		Console.WriteLine ("en la lista==" + deltaElements.Count); 

		for (int i = 0; i < deltaElements.Count; i++) {

			XmlNodeList listTmp = deltaElements.Item (i).ChildNodes;

			//the two nodes of GammaStarSetXQ
			XmlNode gammaStar = listTmp [3].ChildNodes.Item(0);
			XmlNodeList gammaElements = gammaStar.ChildNodes;

			//the str tag
			XmlNodeList symbols = gammaElements [0].ChildNodes;
			string [] elements = new string [symbols.Count];
			elements [0] = symbols [0].FirstChild.Value;

			if (symbols [1] != null) 
				elements [1] = symbols [1].FirstChild.Value; 

			string state = gammaElements[1].FirstChild.Value;
			
			hash.Add (new Configuration (listTmp [0].FirstChild.Value,
				listTmp [1].FirstChild.Value,
				listTmp [2].FirstChild.Value),
				new GStarSXQ (elements, state));                
		}
		
		return hash;
	}

	/* 
	*/
	protected void GetAlphabets ()
	{
		XmlNodeList alphabets = root.SelectNodes ("/pda/alph");	
		XmlNode sigmaNode = alphabets.Item (0);
		XmlNode gammaNode = alphabets.Item (1);

		sigma = SetSymbols (sigmaNode);
		gamma = SetSymbols (gammaNode);
	}


	private string [] SetSymbols (XmlNode node)
	{
		XmlNodeList listSymbols = node.ChildNodes;
		string [] symbols = new string [listSymbols.Count];
		
		for(int i=0; i<listSymbols.Count; i++) {
			symbols [i] = listSymbols [i].FirstChild.Value;
		}
		
		return symbols;
	}

	public string [] GetSigma ()
	{
		return sigma;
	} 

	public string [] GetGamma ()
	{
		return gamma;
	} 


	public string GetSymbolStack ()
	{
		return symbolStack;
	}

	public string SetSymbolStack ()
	{
		XmlNodeList symbols = root.SelectNodes ("/pda/sym");	
		XmlNode symbolNode = symbols.Item (0);
		string value = symbolNode.FirstChild.Value;  
		return value;
	}

	public string GetInitialState ()
	{
		return initialState;
	}


	public string SetInitialState ()
	{
		XmlNodeList states = root.SelectNodes ("/pda/state");	
		XmlNode stateNode = states.Item (0);
		string value = stateNode.FirstChild.Value;  
		return value;
	}


	public void PrintKeysAndValues (Hashtable myList)
	{
		IDictionaryEnumerator myEnumerator = myList.GetEnumerator ();
		Console.WriteLine ("\t-KEY-\t-VALUE-");
		while (myEnumerator.MoveNext ())
			Console.WriteLine ("\t{0}:\t{1}", myEnumerator.Key, myEnumerator.Value);
		Console.WriteLine ();

		Console.WriteLine ("alfabetos:::Sigma");
		PrintValues (sigma);
		Console.WriteLine ("alfabetos:::Gamma");
		PrintValues (gamma);
		Console.WriteLine ("symbolStack::" + symbolStack);  
		Console.WriteLine ("initialState::" + initialState);  
	}

	public static void PrintValues (IEnumerable myList)
	{
		System.Collections.IEnumerator myEnumerator = myList.GetEnumerator ();
		while (myEnumerator.MoveNext ())
			Console.Write ("\t{0}", myEnumerator.Current);
		Console.WriteLine ();
	}
}
}
}
