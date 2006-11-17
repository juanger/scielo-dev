//
// RegGram.cs: A class that represent a regular grammar.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;

namespace AutomataLibrary {
public class RegGram {
	private string description;
	private string [] non_terminals;
	private string [] terminals;
	private string [][] propositions;
	private string i_sym;
	private string i_string;
		
	public RegGram(string filePath , string input)
	{
		GReader reader = new GReader (filePath);
		description = reader.GetDescription ();
		non_terminals = reader.NonTerminals ();
		terminals = reader.Terminals ();
		propositions = reader.GetProductions ();
		i_sym = reader.GetISym ();
		i_string = input;
	}
	
	public bool RunMachine () {
		NFA nfa = new NFA (this);
		return nfa.RunMachine ();
	}
	
	public string Description { 
		get {
			return description;
		}
	}
	
	public string [] Nonterminals { 
		get {
			return non_terminals;
		}
	}
	
	public string [] Terminals {
		get {
			return terminals;
		}
	}
	
	public string [][] Propositions {
		get {
			return propositions;
		}
	}
	
	public string ISym {
		get {
			return i_sym;
		}
	}
	
	public string IString {
		get {
			return i_string;
		}
	}
}
}
