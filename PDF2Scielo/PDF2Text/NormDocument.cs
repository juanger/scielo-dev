//
// NormDocument: This class implements Document and represents a document that has been normalized
// and is marked with out internal definition.
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

public class NormDocument : Document {
	
	protected string front;
	protected string body;
	protected string back;

	public NormDocument (string front, string body, string back, string format)
	{
		if (front == null | body == null | back == null)
			throw new ArgumentNullException ();

		this.front = front;
		this.body = body;
		this.back = back;
		this.format = format;
	}
	
	public NormDocument (INormalizable normalizer)
	{
		front = normalizer.Front;
		body = normalizer.Body;
		back = normalizer.Back;
		format = normalizer.Format;
	}
	
	public string Front {
		get {
			return front;
		}
	}
	
	public string Body {
		get {
			return body;
		}
	}
	
	public string Back {
		get {
			return back;
		}
	}

	public override string GetText ()
	{	
		text = Front + Body + Back;
		return text;
	}
}
}
}