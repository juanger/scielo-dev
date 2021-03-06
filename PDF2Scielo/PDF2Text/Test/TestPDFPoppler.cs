//
// TestPDFPoppler.cs: Unit tests for the PDFpoppler class.
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
using System.Collections;
using System.IO;
using NUnit.Framework;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestPDFPoppler {
	
	private ArrayList test_docs;
	private ArrayList raw_docs;
	
	[SetUp]
	public void Initialize ()
	{
		ArrayList temp_docs;
		test_docs = new ArrayList ();
		raw_docs = new ArrayList ();
		
		temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.PDF);
		foreach (string[] array in temp_docs) {
			Uri uri = new Uri(array[1]);
			test_docs.Add (new PDFPoppler (uri));
		}
		
		temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.RAW);
		foreach (string[] array in temp_docs) {
			raw_docs.Add (Test.ReadFile (array [1]));
		}
	}
	
	
	[Test]
	public void CreateInstanceWithInvalidUri ()
	{
		Uri uri = new Uri ("/foo/v17n01a02.pdf");
		
		try {
			PDFPoppler doc = new PDFPoppler (uri);
			Type etype = Type.GetType ("Scielo.PDF2Text.PDFPoppler");
			Assert.IsNotInstanceOfType (etype, doc, "CI01");
			Assert.IsNull (doc, "CI02");
		} catch (FileNotFoundException) {
			Console.WriteLine ("Error: El archivo no existe.");
		}
	}
	
	[Test]
	public void CreateInstanceWithValidUri ()
	{		
		int count = 0;
		Type etype = Type.GetType ("Scielo.PDF2Text.PDFPoppler");
		
		foreach (PDFPoppler doc in test_docs) {
			Assert.IsInstanceOfType (etype, doc, "CI" + count);
			count += 1;
		}
	}
	
	[Test]
	public void CreateRawDocument ()
	{
		int count = 0;
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		foreach (PDFPoppler doc in test_docs) {
			RawDocument raw = doc.CreateRawDocument ();
			Assert.IsInstanceOfType (etype, raw, "CR" + count);
			count += 1;
		}
	}
		
	[Test]
	public void GetRawText ()
	{
		string pdftext;
		int count = 0;
		
		foreach (PDFPoppler doc in test_docs) {
			pdftext = doc.GetRawText ();
			Assert.AreEqual (raw_docs[count], pdftext, "GR" + count);
			count += 1;
		}
	}
	

}
}
}
