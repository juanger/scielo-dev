//
// GStarSXQ.cs: 
// 
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
namespace MarkupHTML {

public class GStarSXQ {

	protected string [] gammaStar;
	protected string state;

	public GStarSXQ (string [] symbols, string st)
	{
		gammaStar = symbols;
		state = st;
	}

	public string [] GammaStar {
		get {
			return gammaStar;
		}
	}

	public string State {
		get {
			return state;
		}
	}

	public override string ToString()
	{
		ArrayList elements = new ArrayList (gammaStar);
		return ("symbols::" + elements.ToString () + "::state::" + state);
	}

	public override bool Equals (Object obj)
	{
		if (obj == null || GetType () != obj.GetType ())
			return false;

		GStarSXQ o = (GStarSXQ) obj;
		return (state == o.state) && (gammaStar == o.gammaStar);
	}

	public override int GetHashCode ()
	{
		return state.GetHashCode () ^ gammaStar.GetHashCode ();
	}	
}
}
}