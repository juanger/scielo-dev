//
// Modifier: A class that implements modifiers for rules.
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
using System.Xml
;
using System.Collections;
using Scielo.Utils;

namespace Scielo.PDF2Text {
	
public class Modifier {
	private string name;
	private static Hashtable valid_modifiers;
	private Hashtable parameters;
	
	public Modifier (XmlNode node, XmlNamespaceManager manager)
	{
		string tempName = node.SelectSingleNode ("@name", manager).Value;
		
		if (ValidModifier (tempName))
			name = tempName;
		else
			throw new ArgumentException ("El nombre del modificador es inválido");
		
		if (node.ChildNodes.Count != 0)
			parameters = new Hashtable ();
		
		foreach (XmlNode newNode in node.ChildNodes) {
			parameters.Add (newNode.Attributes ["name"].Value, StringRegexp.Unescape (newNode.Attributes ["value"].Value));
		}
	}
	
	public Modifier (string name, Hashtable parameters)
	{
		this.name = name;
		this.parameters = parameters;
	}
	
	public string Name {
		get {
			return name;
		}
	}
	
	public Hashtable Parameters {
		get {
			return parameters;
		}
	}
	
	private static void InitializeValid ()
	{
		valid_modifiers = new Hashtable ();
		valid_modifiers ["Trim"] = true;
		valid_modifiers ["Concat"] = true;
		valid_modifiers ["TrimEnd"] = true;
	}
	
	public static bool ValidModifier (string name)
	{
		if (valid_modifiers == null)
			InitializeValid ();
		return (valid_modifiers [name] != null);
	}
}
}
