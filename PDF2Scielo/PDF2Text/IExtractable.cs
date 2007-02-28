//
// IExtractable: Interface que define los metodos basicos para extraer informacion
// de un documento.
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
	
public interface IExtractable {

	string GetNormText (string encoding);
	
	string GetRawText ();
	
	void CreateHTMLFile (string filepath, string filename);
	
	Queue GetNonText ();
}
}
}