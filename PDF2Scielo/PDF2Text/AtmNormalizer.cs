//
// AssemblyInfo.cs: Assembly Information.
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
using System.Collections;

namespace Scielo {
namespace PDF2Text {
	
public class AtmNormalizer : INormalizable {
		
	public AtmNormalizer()
	{
	
	}
	
	public void setEncoding (string encoding)
	{
		//TODO: To be implemented.
	}
	
	public bool removePattern (string regexp)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool insertNonText ()
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool replacePattern (string regexp, string substitute)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool replaceFootNotes (string regexp)
	{
		//TODO: To be implemented.
		return false;
	}
	
	public bool replaceChars (ArrayList rechar)
	{
		//TODO: To be implemented.
		return false;
	}
}
}
}