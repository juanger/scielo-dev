//
// TestDocReader.cs: Unit tests for the DocReader class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using NUnit.Framework;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestDocReader {

	[Test]	
	public void Case1 ()
	{
		Uri uri = new Uri ("/foo/foo/foo.pdf");
		DocReader reader = DocReader.CreateInstance (uri);
		Assert.IsNull (reader, "DR01");
	}
	
	public void Case2 ()
	{
		
	}
}
}
}
