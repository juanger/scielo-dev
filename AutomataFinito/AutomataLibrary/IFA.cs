//
// IFA.cs: Interface that defines the basic methods and properties of a Finite automata.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using System.Collections;

namespace AutomataLibrary {
public interface IFA {

	string Description { get; }
	string [] States { get; }
	string [] Alph { get; }
	Hashtable Delta { get; }
	string [] IStates { get; }
	string [] FState { get; }
	string IString { get; }
	
	bool RunMachine ();
}
}
