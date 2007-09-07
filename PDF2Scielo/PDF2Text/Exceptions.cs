//
// Exceptions.cs: Contiene las excepciones usadas por PDF2Text.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;

namespace Scielo.PDF2Text {
public class StyleException : System.ApplicationException {
	public StyleException(string message) : base (message)
	{
	}
}

public class NormalizerException : System.ApplicationException {
	public NormalizerException(string message) : base (message)
	{
	}
}
}
