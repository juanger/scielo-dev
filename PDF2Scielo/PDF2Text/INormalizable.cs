//
// AssemblyInfo.cs: Assembly Information.
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
using System.Collections;

namespace Scielo {
namespace PDF2Text {
	
public interface INormalizable {
	
	void setEncoding (string encoding);
	
	bool removePattern (string regexp);
	
	bool insertNonText ();
	
	bool replacePattern (string regexp, string substitute);
	
	bool replaceFootNotes (string regexp);
	
	bool replaceChars (ArrayList rechar);
}
}
}