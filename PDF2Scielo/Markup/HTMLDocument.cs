//
// HTMLDocument: This class implements Document and represents a document that has been replaced
// the internal tags to html's tags.
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
using Scielo.PDF2Text;

namespace Scielo {
namespace Markup {
	
	
public class HTMLDocument : Document {
		
	public HTMLDocument( MarkupHTML mark )
	{	
		text = mark.Text;		
	}
	
	public override string GetText ()
	{	
		return text;
	}
}
}
}
