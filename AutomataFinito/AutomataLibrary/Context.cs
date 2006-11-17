//
// Context.cs: A class that represent a given context in the automata
// ie. a given current state and a given input.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;

namespace AutomataLibrary {
public class Context {
	private string state;
	private string input;
	
	public Context (string state, string input) {
		this.state = state;
		this.input = input;
	}
	
	public override string ToString ()
	{
		string result = "State: " + state + ", Input: " + input;
		
		return result;
	}
	
	public override bool Equals (Object obj)
	{
		if (obj == null || GetType () != obj.GetType ())
			return false;
			
		Context o = (Context) obj;
		return (state == o.state) && (input == o.input);
	}
	
	public override int GetHashCode ()
	{
		return state.GetHashCode () ^ input.GetHashCode ();
	}
	
	public static bool operator== (Context lhs, Context rhs)
	{
		return lhs.Equals (rhs);
	}
	
	public static bool operator!= (Context lhs, Context rhs)
	{
		return !lhs.Equals (rhs);
	}
}
}
