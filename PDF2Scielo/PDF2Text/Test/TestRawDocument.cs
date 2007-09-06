//
// TestRawDocument.cs: Unit tests for the RawDocument class.
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
using System.IO;
using System.Collections;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestRawDocument {
	
	private ArrayList test_docs;
	private ArrayList raw_docs;
	private string [] styles;
	
	[SetUp]
	public void Initialize ()
	{
		test_docs = new ArrayList ();
		raw_docs = new ArrayList ();
		
		ArrayList temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.PDF);
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
	}
	
	[Test]
	public void ConstructorString ()
	{
		RawDocument rdoc0 = new RawDocument ("", "atm");
		RawDocument rdoc1 = new RawDocument ("Hola Mundo", "atm");
		RawDocument rdoc2 = new RawDocument ("            ad        ", "atm");
		
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Assert.IsInstanceOfType (etype, rdoc0, "CI01");
		Assert.IsInstanceOfType (etype, rdoc1, "CI01");
		Assert.IsInstanceOfType (etype, rdoc2, "CI01");	
	}
	
	[Test]
	public void ConstructorInterfase ()
	{
		int count = 0;
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		
		foreach (PDFPoppler doc in test_docs) {
			RawDocument rdoc = new RawDocument (doc);
			Assert.IsInstanceOfType (etype, rdoc, "CI" + count);
			count++;
		}
	}
	
	[Test]
	public void GetText () 
	{
		string rawtext;
		int count = 0;
		
		foreach (PDFPoppler doc in test_docs) {
			RawDocument rdoc = new RawDocument (doc);
			rawtext = rdoc.GetText ();
			Assert.AreEqual (raw_docs[count], rawtext, "GT" + count);
			count += 1;
		}
	}
	
	[Test]
	public void Normalize () 
	{
		NormDocument ndoc;
		int count = 0;
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Type etype1 = Type.GetType ("Scielo.PDF2Text.NormDocument");
		
		foreach (PDFPoppler doc in test_docs) {
			RawDocument rdoc = new RawDocument (doc);
			ndoc= rdoc.Normalize (styles[count]);
			Assert.IsInstanceOfType (etype, rdoc, "NM" + count);
			Assert.IsInstanceOfType (etype1, ndoc, "NM" + count);
			count++;
		}
	}
	
	[Test]
	public void WriteDocument ()
	{	
		string result, temp_dir;
		int count = 0;
		
		foreach (PDFPoppler doc in test_docs) {
			RawDocument rdoc = new RawDocument (doc);
			
			temp_dir = Path.GetTempPath ();
			
			rdoc.WriteDocument (temp_dir, "temp01", "txt");
			result = Test.ReadFile (Path.Combine (temp_dir, "temp01.txt"));
			
			Assert.AreEqual (raw_docs[count], result, "WD" + count);
			count++;
		}
	}
}
}
}
