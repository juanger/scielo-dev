//
// TestPDFPoppler.cs: Unit tests for the PDFpoppler class.
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
using NUnit.Framework;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestPDFPoppler {
		
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
		string path = Test.PathOfTest ();

		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0);
		PDFPoppler doc1 = new PDFPoppler (uri1);
		PDFPoppler doc2 = new PDFPoppler (uri2);
		
		Type etype = Type.GetType ("Scielo.PDF2Text.PDFPoppler");
		Assert.IsInstanceOfType (etype, doc0, "CI01");
		Assert.IsInstanceOfType (etype, doc1, "CI02");
		Assert.IsInstanceOfType (etype, doc2, "CI03");
	}
	
	[Test]
	public void CreateRawDocument ()
	{
		string path = Test.PathOfTest ();

		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0);
		PDFPoppler doc1 = new PDFPoppler (uri1);
		PDFPoppler doc2 = new PDFPoppler (uri2);
		
		RawDocument raw0 = doc0.CreateRawDocument ();
		RawDocument raw1 = doc1.CreateRawDocument ();
		RawDocument raw2 = doc2.CreateRawDocument ();
		
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Assert.IsInstanceOfType (etype, raw0, "CI01");
		Assert.IsInstanceOfType (etype, raw1, "CI02");
		Assert.IsInstanceOfType (etype, raw2, "CI03");	
	}
		
	[Test]
	public void GetRawText ()
	{
		string path = Test.PathOfTest ();

		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v17n2a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0);
		PDFPoppler doc1 = new PDFPoppler (uri1);
		PDFPoppler doc2 = new PDFPoppler (uri2);
		
		path0 = Path.Combine (path, "v17n01a02-raw.txt");
		path1 = Path.Combine (path, "v17n4a03-raw.txt");
		path2 = Path.Combine (path, "v17n2a02-raw.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		string pdftext0 = doc0.GetRawText ();
		string pdftext1 = doc1.GetRawText ();
		string pdftext2 = doc2.GetRawText ();
		
		Assert.AreEqual (text0, pdftext0, "GR01");
		Assert.AreEqual (text1, pdftext1, "GR02");
		Assert.AreEqual (text2, pdftext2, "GR03");
	}
}
}
}
