//
// DFA.cs: Class that represents a deterministic finite automata.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using System.Text;
using System.Collections;

namespace AutomataLibrary {
public class DFA : IFA {

	protected string description;
	protected string [] states;
	protected string [] alph;
	protected Hashtable delta;
	protected string [] i_state;
	protected string [] f_states;
	protected string i_string;
	protected string current_state;
	
	public DFA (string filePath, string input)
	{
		DFAReader reader = new DFAReader (filePath);
		description = reader.GetDescription ();
		states = reader.GetStates ();
		alph = reader.GetAlph ();
		delta = reader.GetDelta ();
		i_state = reader.GetIStates ();
		f_states = reader.GetFStates ();
		
		i_string = input;
		
		if (i_state == null || i_state.Length > 1) {
			Console.WriteLine ("Error: DFA can only have one initial state");
			Environment.Exit (1);
		}
		
		current_state = i_state [0];
	}
	
	public DFA (NFA nfa)
	{	
		description = nfa.Description;
		alph = nfa.Alph;
		i_string = nfa.IString;
		
		int count = 1;
		string istr = "";
		foreach (string sub in nfa.IStates) {
			if (count++ != nfa.IStates.Length)
				istr += sub + ":";
			else
				istr +=  sub;
		}
		
		string [] istate = new string [1];
		istate[0] = istr;
		
		i_state = istate;
		current_state = istr;
		GetDeltaFinal (nfa);
	}
	
	private void GetDeltaFinal (NFA nfa) 
	{
		Queue newstates = new Queue ();
		Queue queue = new Queue ();
		Hashtable odelta = nfa.Delta;
		Hashtable ndelta = new Hashtable ();
		newstates.Enqueue (current_state);
		queue.Enqueue (current_state);
		
		while (queue.Count != 0) {
			string substate = (string) queue.Dequeue ();
			string [] states = substate.Split (':');
			
			foreach (string sym in alph) {
				
				int icount = 0;
				string [] nstates = new string [states.Length];
				foreach (string sub in states) {
					nstates [icount++] = (string) odelta [new Context (sub, sym)];
				}
				
				string union = Union (nstates);
				if (union == null)
					union = "ERROR";
				
				if (!newstates.Contains (union)) {
					newstates.Enqueue (union);
					queue.Enqueue (union);
				}
				
				ndelta.Add (new Context (substate, sym), union);
			}
		}
		
		this.delta = ndelta;
		
		int qcount = 0;
		string [] qstates = new string [newstates.Count];
		foreach (object o in newstates)
			qstates [qcount++] = (string) o;
			
		this.states = qstates;
		string [] ofstates = nfa.FState;
		ArrayList list = new ArrayList ();
		
		foreach (string sub in states)
			foreach (string asub in ofstates)
				if (sub.IndexOf (asub) != -1) {
					list.Add (sub);
					break;
				}
		
		string [] farray = new string [list.Count];
		list.CopyTo (farray);
		f_states = farray;
	}
	
	private string Union (string [] nstates)
	{	
		string result;
		if (nstates.Length > 0)
			result = nstates [0];
		else
			return null;
		
		for (int i = 1; i < nstates.Length; i++) {
			result = Union (result, nstates [i]);
		}
		
		return result;
	}
	
	private string Union (string lhs, string rhs)
	{
		if (lhs == null && rhs == null)
			return null;
			
		if (lhs == null)
			return rhs;
		else if (rhs == null)
			return lhs;
			
		StringBuilder result = new StringBuilder ();
		result.Append (lhs);
		
		string [] lsplit = lhs.Split (':');
		string [] rsplit = rhs.Split (':');
		
		
		foreach (string lsub in lsplit)
			for (int i = 0; i < rsplit.Length; i++)
				if (lsub == rsplit [i])
					rsplit [i] = "NULL";
		
		foreach (string rsub in rsplit)
			if (rsub != "NULL")
				result.AppendFormat (":{0}", rsub);
		
		string res = result.ToString ();
		
		return res;
	}
	
	public bool RunMachine ()
	{	
		foreach (char input in i_string) {
			if (!DoTransition (input))
				return false;
		}

		foreach (string final in f_states) {
			if (current_state == final)
				return true;
		}
		
		return false;
	}
	
	public bool DoTransition (char cInput)
	{
		Context con = new Context (current_state, cInput.ToString ());
		current_state = (string) delta[con];
		
		return (current_state != null);
	}
	
	public void Reset ()
	{
		current_state = i_state[0];
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
	
	public string CurrentState { 
		get {
			return current_state;
		}
	}
}
}
