//
// INormalizable.cs: Interface que define los metodos basicos para darle formato
// a un stream de texto.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Alejandro Rosendo Robles. (rosendo69@hotmail.com)
//   Virginia Teodosio Procopio. (ainigriv_t@hotmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace Scielo {
namespace PDF2Text {
	
public interface INormalizable {
	
	void SetEncoding (string encoding);
	
	string RemoveHeaders ();
	
	string RemovePattern (string regexp);
	
	string MarkSections ();
	
	bool InsertNonText ();
	
	string ReplacePattern (string regexp, string substitute);
	
	bool ReplaceFootNotes (string regexp);
	
	bool ReplaceChars (ArrayList rechar);
	
	Match [] GetMatches (string regexp);
	
	string Text { get; } 
}
}
}