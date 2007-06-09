//
// TestNormDocument.cs: Unit tests for the NormDocument class.
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
using NUnit.Framework;
using System.IO;
using System.Collections;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestNormDocument {
	
	private ArrayList test_docs;
	private ArrayList raw_docs;
	private ArrayList norm_docs;
	
	[SetUp]
	public void Initialize ()
	{
		ArrayList temp_docs;
		test_docs = new ArrayList ();
		raw_docs = new ArrayList ();
		norm_docs = new ArrayList ();

		temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.PDF);
		foreach (string[] array in temp_docs) {
			Uri uri = new Uri(array[1]);
			test_docs.Add (new PDFPoppler (uri, array [0]));
		}
		
		temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.RAW);
		foreach (string[] array in temp_docs) {
			raw_docs.Add (Test.ReadFile (array [1]));
		}
		
		temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.NORM);
		foreach (string[] array in temp_docs) {
			norm_docs.Add (Test.ReadFile (array [1]));
		}
	}
	
	[Test]
	public void ConstructorInvalid ()
	{
		try {
			NormDocument ndoc0 = new NormDocument (null, null, null);
			Type etype = Type.GetType ("Scielo.PDF2Text.NormDocument");
			Assert.IsInstanceOfType (etype, ndoc0, "CI01");
		} catch (ArgumentNullException) {
			Console.WriteLine ("Error: NormDocument no acepta como parametro(s) a null.");
		}	
	}
	
	[Test]
	public void ConstructorValid ()
	{
		try {
			NormDocument ndoc0 = new NormDocument ("", "", "");
			Type etype = Type.GetType ("Scielo.PDF2Text.NormDocument");
			Assert.IsInstanceOfType (etype, ndoc0, "CI01");
		} catch (ArgumentNullException) {
			Console.WriteLine ("Error: NormDocument no acepta como parametro(s) a null.");
		}
	}
	
	[Test]
	public void GetText () 
	{	
		string normtext;
		int count = 0;
		NormDocument ndoc;
		
		foreach (PDFPoppler doc in test_docs) {
			RawDocument rdoc = new RawDocument (doc);
			ndoc = rdoc.Normalize ();
			normtext = ndoc.GetText ();
			Assert.AreEqual (norm_docs[count], normtext, "GT" + count);
			count += 1;
		}
		
//		NormDocument ndoc0 = rdoc0.Normalize ();
//		NormDocument ndoc1 = rdoc1.Normalize ();
//		NormDocument ndoc2 = rdoc2.Normalize ();
//		
//		string ntext0 = ndoc0.GetText ();
//		string ntext1 = ndoc1.GetText ();
//		string ntext2 = ndoc2.GetText ();
//		
//		path0 = Path.Combine (path, "v17n01a02-norm.txt");
//		path1 = Path.Combine (path, "v17n4a03-norm.txt");
//		path2 = Path.Combine (path, "v17n2a02-norm.txt");
//		
//		string text0 = Test.ReadFile (path0);
//		string text1 = Test.ReadFile (path1);
//		string text2 = Test.ReadFile (path2);
//		
//		Assert.AreEqual (text0, ntext0, "GT01");
//		Assert.AreEqual (text1, ntext1, "GT02");
//		Assert.AreEqual (text2, ntext2, "GT03");
	}
}
}
}
