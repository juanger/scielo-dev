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
using Scielo.Utils;
using System.Xml;

namespace Scielo.PDF2Text {
public class Rule {
	string name;
	RuleType type;
	bool unique_match;
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
		
		// Aqui se obtiene el nombre de la regla.
		XmlNode node = root.SelectSingleNode ("@name");
		name = node.Value;
		
		// Aqui se obtiene la expresion regular de la regla.
		regexp = root.SelectSingleNode ("regexp").FirstChild.Value;
		regexp = StringRegexp.Unescape (regexp);
		regexp = StringRegexp.ReplaceEntities (regexp);
		
		// Aqui se obtiene la expresion de sustitucion.
		sust = root.SelectSingleNode ("sust").FirstChild.Value;
		sust = StringRegexp.Unescape (sust);
		
		// Aquí se obtiene el número de matches esperados.
		XmlNode matchNode = root.SelectSingleNode ("@expectedMatches");
		unique_match = true;
		
		if (matchNode != null)
			unique_match = !matchNode.Value.Equals ("unbounded");
		
		// Aquí se obtiene el tipo de la regla.
		if (regexp.IndexOf ("(?<Result>") != -1)
			type = RuleType.RESULT;
		else if (sust.IndexOf ("#{Result}") == -1)
			type = RuleType.STATIC;
		else
			type = RuleType.FULL;
		
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
	
	public bool UniqueMatch {
		get {
			return unique_match;
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
