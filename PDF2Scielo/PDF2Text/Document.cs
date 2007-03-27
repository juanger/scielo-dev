//
// AtmNormalizer: A class that implements a the interface INormalizer for
// Atmosfera.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;

namespace Scielo {
namespace PDF2Text {

public abstract class Document {

	protected string text;
	protected string front;
	protected string body;
	protected string back;
	
	public Document ()
	{
		text = null;
	}
	
	public string Text {
		get {
			text = front + body + back;
			return text;
		}
	}
	
	public override string ToString ()
	{
		return Text;	
	}
}
}
}