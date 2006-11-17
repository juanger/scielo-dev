//
// TestDFA.cs: Test units for the DFA class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
//

using System;
using NUnit.Framework;
using AutomataLibrary;

namespace AutomataLibrary {

[TestFixture()]
public class TestDFA {

	[Test]
	public void RunMachine1 ()
	{
		string file = "../../Data/dfa-001.xml";
		DFA dfa = new DFA (file, "00100000000000000000000000000");
		Assert.AreEqual (dfa.RunMachine ()? "accepted": "not accepted", "accepted", "D02");
	}
	
	[Test]
	public void RunMachine2 ()
	{
		string file = "../../Data/dfa-002.xml";
		DFA dfa = new DFA (file, "");
		Assert.AreEqual (dfa.RunMachine ()? "accepted": "not accepted", "accepted", "D03");
	}
	
	[Test]
	public void RunMachine3 ()
	{
		string file = "../../Data/dfa-cero-star-uno-cero-plus.xml";
		DFA dfa = new DFA (file, "1000000000000000000000000");
		Assert.AreEqual (dfa.RunMachine ()? "accepted": "not accepted", "accepted", "D04");
	}
	
	[Test]
	public void Description1 ()
	{
		string file = "../../Data/dfa-001.xml";
		DFA dfa = new DFA (file, "10000000000");
		Assert.AreEqual ("Este automata acepta cadenas que tengan como subcadena 001", dfa.Description, "D05");
	}
	
	[Test]
	public void States1 ()
	{
		string [] test = { "q", "q0", "q00", "q001" };
		string file = "../../Data/dfa-001.xml";
		DFA dfa = new DFA (file, "10000000000");
		Assert.AreEqual (test, dfa.States, "D06");
	}
	
	[Test]
	public void Alph1 ()
	{
		string [] test = { "0", "1" };
		string file = "../../Data/dfa-001.xml";
		DFA dfa = new DFA (file, "10000000000");
		Assert.AreEqual (test, dfa.Alph, "D07");
	}
}
}
