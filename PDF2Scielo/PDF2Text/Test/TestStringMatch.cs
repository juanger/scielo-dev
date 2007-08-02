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
public class TestStringMatch {
	
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
		
		full = new StringMatchCollection("(sub)?tropical", source);
		result = new StringMatchCollection(" (?<Result>tropical)[ \n]", source);
		
	}
	
	[Test]
	public void FullMatch ()
	{
		Assert.AreEqual(8, full.Count);
	}
	
	[Test]
	public void ResultMatch ()
	{
		Assert.AreEqual(6, result.Count);
	}
}
}
