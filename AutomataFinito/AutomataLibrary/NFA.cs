//
// NFA.cs: Class that represents a nondeterministic finite automata.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using System.Collections;

namespace AutomataLibrary {
	
public class NFA : IFA {
	
	protected string description;
	protected string [] states;
	protected string [] alph;
	protected Hashtable delta;
	protected string [] i_state;
	protected string [] f_states;
	protected string i_string;
	protected string [] current_state;
	
	public NFA (string filePath, string input)
	{
		NFAReader reader = new NFAReader (filePath);
		description = reader.GetDescription ();
		states = reader.GetStates ();
		alph = reader.GetAlph ();
		delta = reader.GetDelta ();
		i_state = reader.GetIStates ();
		f_states = reader.GetFStates ();
		
		i_string = input;
		
		if (i_state == null) {
			Console.WriteLine ("Error: NFA doesn't have a initial state");
			Environment.Exit (1);
		}
		
		current_state = i_state;
	}

	public NFA (RegGram gram) 
	{	
		description = gram.Description;
		alph = gram.Terminals;
		
		int length = gram.Nonterminals.Length;
		string [] nterminals = new string [length + 2];
		gram.Nonterminals.CopyTo (nterminals, 0);
		nterminals [length] = "ZFinal";
		nterminals [length + 1] = "ERROR";
		states = nterminals;
		
		string [] nistate = new string [1];
		nistate [0] = gram.ISym;
		i_state = nistate;
		
		GetDeltaFinal (gram);
		
		i_string = gram.IString;
	}
	
	private void GetDeltaFinal (RegGram gram)
	{
		Hashtable hash = new Hashtable ();
		bool acepta = false;
		string [][] propositions = gram.Propositions;
		
		foreach (string [] prop in propositions) {
			string left = prop [0];
			string right = prop [1];
			
			if (right == "epsilon")
				acepta = true;
			
			if (right.IndexOf (":") == -1) {
				Context con = new Context (left, right);
				if (hash [con] == null) {
					hash.Add (con, "ZFinal");
				} else {
					string temp = (string) hash [con];
					hash.Remove (con);
					hash.Add (con, temp + ":ZFinal");
				}
			} else {
				string [] sub = right.Split (':');
				string subleft = sub [0];
				string subright = sub [1];
				Context con = new Context (left, subleft);
				
				if (hash [con] == null) {
					hash.Add (con, subright);
				} else {
					string temp = (string) hash [con];
					hash.Remove (con);
					hash.Add (con, temp + ":" + subright);
				}
			}
		}
		
		foreach (string sym in alph) {
			hash.Add (new Context ("ZFinal", sym), "ERROR");
			hash.Add (new Context ("ERROR", sym), "ERROR");
		}
		
		delta = hash;
		
		string [] nfstates;
		if (acepta) {
			nfstates = new string [2];
			nfstates [0] = "ZFinal";
			nfstates [1] = gram.ISym;
		} else {
			nfstates = new string [1];
			nfstates [0] = "ZFinal";
		}
		
		f_states = nfstates;
	}
	
	public bool RunMachine ()
	{	
		DFA dfa = new DFA (this);
		return dfa.RunMachine ();
	}
	
	public string Description { 
		get {
			return description;
		}
	}
	
	public string [] States { 
		get {
			return states;
		}
	}
	
	public string [] Alph { 
		get {
			return alph;
		}
	}
	
	public Hashtable Delta { 
		get {
			return delta;
		}
	}
	
	public string [] IStates { 
		get {
			return i_state;
		}
	}
	
	public string [] FState { 
		get {
			return f_states;
		}
	}
	
	public string IString {
		get {
			return i_string;
		}
	}
	
	public string [] CurrentState { 
		get {
			return current_state;
		}
	}
}
}
