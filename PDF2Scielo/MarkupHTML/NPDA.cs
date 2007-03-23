//
// NPDA.cs:
//
// Author:
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
// Copyright (C) 2007 UNAM DGB
// 

using System;
using System.Xml;
using System.Collections;

namespace Scielo {
namespace Markup {

public class NPDA {

	/*
	description;
	stateSet;
	finalStates; 
	*/

	// bloks BF, BB and BBk

	protected Hashtable deltaTransitions;

	protected string currentState;
	protected ArrayList inputString;
	protected Stack stack;
	protected int head = 0;
	protected GStarSXQ nextConfiguration; 
	protected string currentToken;
	protected string [] sigmaAlphabet;
	protected string [] gammaAlphabet;
	protected string symbolStack;
	protected string initialState;


	public NPDA (string filename, string [] input)
	{
		XReaderNPDA xr = new XReaderNPDA (filename);
		deltaTransitions = xr.GetDelta ();
		sigmaAlphabet = xr.GetSigma ();
		gammaAlphabet = xr.GetGamma ();
		symbolStack = xr.GetSymbolStack ();     
		initialState = xr.GetInitialState ();
		inputString  = new ArrayList (input); 
		currentState = initialState;
		currentToken = (string)inputString [head];
		stack = new Stack ();

	}

	public void PrepareInput ()
	{
		string end = "EOF";
		string end2 = "epsilon"; 
		inputString.Add (end); 
		inputString.Add (end2); 
		inputString.Add (end2); 
	}

	public void RunMachine ()
	{
		Console.WriteLine ("entro");
		stack.Push (symbolStack);
		MatchSymbol();
		Configuration tmp = new Configuration (initialState, currentToken, (string)stack.Peek () );

		Console.WriteLine ("*********************state:" + initialState + "" );
		nextConfiguration = (GStarSXQ) deltaTransitions [tmp];

		//string currentToken = (string) inputString[head];
		MatchSymbol();

		while (DoTransition (currentToken)) {
			Console.WriteLine ("hiiiiiiiiiiiiiii ");
			Console.WriteLine ("*************(configuration::state::"
				+ currentState + "::in::" + currentToken + "::glance::"
				+ stack.Peek () + ")::entrada::" + inputString [head]);
			head++;
			UpdateStack ();
			currentToken = (string) inputString [head];   
			MatchSymbol ();
		}

		if (head == inputString.Count + 1 && (string)stack.Peek () == symbolStack && currentState == "10")
			Console.WriteLine("uffffffffffffffff ............ por fin!!! ");
	}

	public void UpdateStack ()
	{
		ArrayList symbols = new ArrayList (nextConfiguration.GammaStar);
		stack.Pop ();      
		if (symbols.Count == 2) {
			stack.Push(symbols [1]);        
			stack.Push(symbols [0]);        
		} else if ((string) symbols [0] != "epsilon")
			stack.Push(symbols [0]);
	}

	public bool DoTransition (string cInput )
	{
		string glance = (string) stack.Peek ();    

		Configuration config = new Configuration (currentState, cInput.ToString (), glance);
		nextConfiguration = (GStarSXQ) deltaTransitions [config];

		if (nextConfiguration == null)
			Console.WriteLine ("*****************aqui esta**********");

		if (currentState == null)
			Console.WriteLine ("*****************aqui esta state**********");

		currentState = nextConfiguration.State;
			   
		if (nextConfiguration == null)
			return false;

		return true;
	}

	// This method checks the currentToken belongs to Sigma Alphabet.
	public bool MatchSymbol ()
	{
		ArrayList alphabetS = new ArrayList (sigmaAlphabet);
		if (!alphabetS.Contains (currentToken)) {
			currentToken = "asterisc";
			return false;
		}
		
		return true;
	}
}
}
}