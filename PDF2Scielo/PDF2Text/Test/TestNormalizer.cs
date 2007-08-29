//
// TestNormalizer.cs: Test for Normalizer class.
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
using Scielo.Utils;
using NUnit.Framework;
using System.IO;

namespace Scielo.PDF2Text {

[TestFixture()]
public class TestNormalizer {
	private ArrayList raw_docs;
	
	[SetUp()]
	public void Initialize ()
	{
		ArrayList test_docs = new ArrayList ();
		ArrayList temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.PDF);
		
		foreach (string[] array in temp_docs) {
			Uri uri = new Uri(array[1]);
			test_docs.Add (new PDFPoppler (uri, array [0]));
		}
		
		raw_docs = new ArrayList ();
		foreach (PDFPoppler pdf in test_docs) {
			raw_docs.Add (new RawDocument (pdf));
		}
	}
	
	[Test()]
	public void ConstructorRawDocument ()
	{
		Type etype = Type.GetType ("Scielo.PDF2Text.Normalizer");
		
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw);
			Assert.IsInstanceOfType (etype, norm, "CRD" + count);
			count++;
		}
	}
	
	[Test()]
	public void ConstructorString ()
	{
		Type etype = Type.GetType ("Scielo.PDF2Text.Normalizer");
		
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw.GetText (), "atm");
			Assert.IsInstanceOfType (etype, norm, "CS" + count);
			count++;
		}
	}
	
	[Test()]
	[ExpectedException(typeof (FileNotFoundException))]
	public void ConstructorBadStyle ()
	{
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw.GetText (), "atn");
			count++;
		}
	}
}
}