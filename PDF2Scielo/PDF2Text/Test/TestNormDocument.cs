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
	private string [] styles;
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
		styles = new string [temp_docs.Count];
		
		int count = 0;
		foreach (string[] array in temp_docs) {
			styles [count] = array [0];
			Uri uri = new Uri(array[1]);
			test_docs.Add (new PDFPoppler (uri));
			count++;
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
			NormDocument ndoc0 = new NormDocument (null, null, null, null);
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
			NormDocument ndoc0 = new NormDocument ("", "", "", "");
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
			ndoc = rdoc.Normalize (styles [count]);
			normtext = ndoc.GetText ();
			Assert.AreEqual (norm_docs[count], normtext, "GT" + count);
			count += 1;
		}
	}
}
}
}
