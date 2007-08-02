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
		
		FileStream mainReader = new FileStream(path, FileMode.Open);
		StreamReader sreader = new StreamReader(mainReader);
		
		source = sreader.ReadToEnd ();
		full = new StringMatchCollection("(sub)?tropical", source);
		result = new StringMatchCollection("\b(?<Result>tropical)", source);
		
	}
	
	[Test]
	public void FullMatch ()
	{
		int counter = 0;
		foreach (StringMatch match in full) 
			counter++;
		Assert.AreEqual(8, counter);
	}
	
	[Test]
	public void ResultMatch ()
	{
		int counter = 0;
		foreach (StringMatch match in result) 
			counter++;
		Assert.AreEqual(6, counter);
	}
}
}
