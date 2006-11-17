//
// Main.cs: Point of entry of the AutomataFinito application.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2006 Hector Gomez (cantera.homelinux.org)
// 

using System;
using AutomataLibrary;

namespace AutomataFinito {
public class MainClass {
	
	static private void Header ()
	{
		Console.WriteLine (new AssemblyInfo ().ToString ());
	}
	
	static private void Help ()
	{
		Console.WriteLine ("AutomataFinito --dfa | --nfa | --gra xml-file string");
		Console.WriteLine ("\tWhere xml-file is a XML representation of a DA and string is the input to be tested.\n");
		Console.WriteLine ("AutomataFinito --dfa xml-file string");
		Console.WriteLine ("\tOutputs true if the given string is accepted otherwise returns false.\n");
		Console.WriteLine ("AutomataFinito --nfa xml-file string");
		Console.WriteLine ("\tOutputs true if the given string is accepted otherwise returns false.\n");
		Console.WriteLine ("AutomataFinito --gra xml-file string");
		Console.WriteLine ("\tOutputs true if the given string is accepted by the regular grammar in the xml file.");
		Console.WriteLine ("AutomataFinito -h");
		Console.WriteLine ("AutomataFinito --help");
		Console.WriteLine ("\tShow help about this tool.");
	}
		
	public static void Main(string[] args)
	{
		bool help = false;
		bool adfa = false;
		bool anfa = false;
		bool gram = false;
		string filename = null;
		string input = null;
		
		Header ();
		
		try {
			for (int i = 0; i < args.Length - 2; i++ ) {
				switch (args [i]) {
				case "--dfa":
					adfa = true;
					filename = args [i+1];
					input = args [i+2];
					break;
				case "--nfa":
					anfa = true;
					filename = args [i+1];
					input = args [i+2];
					break;
				case "--gra":
					gram = true;
					filename = args [i+1];
					input = args [i+2];
					break;
				case "-h":
				case "--help":
					help = true;
					break;
				}
			}
		} catch (Exception e) {
			Console.WriteLine ("Error: " + e.ToString ());
		}
			
		if (help)
			Help ();
			
		if (adfa) {
			IFA fa = new DFA (filename, input);
			Console.WriteLine ("Input: {0} was {1} by the DFA",
				input == ""? "epsilon" : input,
				fa.RunMachine ()? "accepted" : "not accepted");
		} else if (anfa) {

			IFA fa = new NFA (filename, input);
			Console.WriteLine ("Input: {0} was {1} by the NFA",
				input == ""? "epsilon" : input,
				fa.RunMachine ()? "accepted" : "not accepted");
		} else if (gram) {
			RegGram rg = new RegGram (filename, input);
			Console.WriteLine ("Input: {0} was {1} by the grammar",
				input == ""? "epsilon" : input,
				rg.RunMachine ()? "accepted" : "not accepted");
		} else
			Help ();			
	}
}
}