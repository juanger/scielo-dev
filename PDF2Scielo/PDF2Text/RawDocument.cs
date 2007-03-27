//
// RawDocument: This class implements Document and represents a document that is in a raw state.
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

public class RawDocument : Document {

	public RawDocument (string text) 
	{
		this.text = text;
	}

	public RawDocument (IExtractable extractor)
	{
		text = extractor.GetRawText ();
	}
	
	public override string GetText ()
	{
		return text;
	}
	
	public NormDocument Normalize ()
	{
		AtmNormalizer norm = new AtmNormalizer (text);
		norm.MarkText ();
		
		return new NormDocument (norm.Front, norm.Body, norm.Back);
	}
}
}
}