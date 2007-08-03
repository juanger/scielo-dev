//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Xml;

namespace Scielo.PDF2Text {
public class Rule {
	string name;
	RuleType type;
	int expected_matches;
	string [] depends;
	string regexp;
	string sust;
	
	public enum RuleType :int
	{
		STATIC = 0,
		FULL = 1,
		RESULT = 2,
	}
	
	public Rule(XmlNode root)
	{
		if (root == null)
			throw new ArgumentNullException ("Danger danger Mr. Robinson");
		Console.WriteLine (root.OuterXml);
		XmlNode node = root.SelectSingleNode ("@name");
		name = node.Value;
		Console.WriteLine (node.OuterXml);
		System.Console.WriteLine("DEBUG: Rule name: {0}", name);
		regexp = root.SelectSingleNode ("regexp").FirstChild.Value;
		Console.WriteLine (regexp);
		sust = root.SelectSingleNode ("sust").FirstChild.Value;
		Console.WriteLine (sust);
		//expected_matches = int.root.SelectSingleNode ("@expectedMatches").Value;
		//Console.WriteLine (expected_matches);
	}
	
	public string Name {
		get {
			return name;
		}
	}
	
	public RuleType Type {
		get {
			return type;
		}
	}
	
	public int ExpectedMatches {
		get {
			return expected_matches;
		}
	}
	
	public string [] Depends {
		get {
			return depends;
		}
	}
	
	public string Regexp {
		get {
			return regexp;
		}
	}
		
	public string Sustitution {
		get {
			return sust;
		}
	}
}
}
