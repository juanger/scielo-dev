//
// TestRawDocument.cs: Unit tests for the RawDocument class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.IO;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo.PDF2Text {
[TestFixture()]
public class TestStringMatchCollection {
	StringMatchCollection full, result;
	string source;
	
	[SetUp]
	public void Initialize ()
	{
		string path = Path.Combine (Test.PathOfTest (), "TestBack.txt" );
		
		using (FileStream mainReader = new FileStream(path, FileMode.Open)) {
			using (StreamReader sreader = new StreamReader(mainReader))
				source = sreader.ReadToEnd ();
		}
	}
	
	[Test]
	public void FullMatch ()
	{
		full = new StringMatchCollection ("(sub)?tropical", source);
		Assert.AreEqual(8, full.Count);
	}
	
	[Test]
	public void ResultMatch ()
	{
		result = new StringMatchCollection (" (?<Result>tropical)[ \n]", source);
		Assert.AreEqual(6, result.Count);
	}
	
	[Test]
	public void Count ()
	{
		result = new StringMatchCollection ("Gandu, A. W.", source);
		Assert.AreEqual(2, result.Count);
	}
}
}
