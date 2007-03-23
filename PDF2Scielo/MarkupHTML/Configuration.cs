//
// Configuration.cs: Class that implements the configuration of state, state of stack and input of
// pushdown automata.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;

namespace Scielo {
namespace Markup {

public class Configuration {
	private string state;
	private string input;
	private string stack;
	
	public Configuration (string state, string input, string stack) {
		this.state = state;
		this.input = input;
		this.stack = stack;
	}
	
	public override string ToString ()
	{
		string result = "State: " + state + ", Input: " + input + ", Stack: " + stack;
		
		return result;
	}
	
	public override bool Equals (Object obj)
	{
		if (obj == null || GetType () != obj.GetType ())
			return false;
			
		Configuration o = (Configuration) obj;
		return (state == o.state) && (input == o.input) && (stack == o.stack);
	}
	
	public override int GetHashCode ()
	{
		return state.GetHashCode () ^ input.GetHashCode () ^ stack.GetHashCode ();
	}
	
	public static bool operator== (Configuration lhs, Configuration rhs)
	{
		return lhs.Equals (rhs);
	}
	
	public static bool operator!= (Configuration lhs, Configuration rhs)
	{
		return !lhs.Equals (rhs);
	}

}
}
}