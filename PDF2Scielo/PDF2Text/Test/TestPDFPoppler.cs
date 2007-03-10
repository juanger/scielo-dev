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
		PDFPoppler doc = PDFPoppler.CreateInstance (uri);
		
		Type etype = Type.GetType ("Scielo.PDF2Text.PDFPoppler");
		Assert.IsNotInstanceOfType (etype, doc, "CI01");
		Assert.IsNull (doc, "CI02");
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
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		Type etype = Type.GetType ("Scielo.PDF2Text.PDFPoppler");
		Assert.IsInstanceOfType (etype, doc0, "CI01");
		Assert.IsInstanceOfType (etype, doc1, "CI02");
		Assert.IsInstanceOfType (etype, doc2, "CI03");
	}
	
	[Test]
	public void GetNormText ()
	{
		string path = Test.PathOfTest ();

		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		path0 = Path.Combine (path, "v17n01a02-norm.txt");
		path1 = Path.Combine (path, "v17n4a03-norm.txt");
		path2 = Path.Combine (path, "v18n4a02-norm.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		string pdftext0 = doc0.GetNormText ("utf8");
		string pdftext1 = doc1.GetNormText ("utf8");
		string pdftext2 = doc2.GetNormText ("utf8");
		
		Assert.AreEqual (text0, pdftext0);
		Assert.AreEqual (text1, pdftext1);
		//Assert.AreEqual (text2, pdftext2);
	}
	
	[Test]
	public void GetRawText ()
	{
		string path = Test.PathOfTest ();

		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		path0 = Path.Combine (path, "v17n01a02-raw.txt");
		path1 = Path.Combine (path, "v17n4a03-raw.txt");
		path2 = Path.Combine (path, "v18n4a02-raw.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		string pdftext0 = doc0.GetRawText ();
		string pdftext1 = doc1.GetRawText ();
		string pdftext2 = doc2.GetRawText ();
		
		Assert.AreEqual (text0, pdftext0);
		Assert.AreEqual (text1, pdftext1);
		Assert.AreEqual (text2, pdftext2);
	}
	
	[Test]
	public void CreateHTMLFile ()
	{	
		string path = Test.PathOfTest ();
		string path0, path1, path2, text0, text1, text2;
		string pdftext0, pdftext1, pdftext2;
		
		// Rutas a los PDF a usar en el test unit.
		path0 = Path.Combine (path, "v17n01a02.pdf");
		path1 = Path.Combine (path, "v17n4a03.pdf");
		path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		// Se crean los lectores para cada PDF.
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		path0 = Path.Combine (path, "v17n01a02.htm");
		path1 = Path.Combine (path, "v17n4a03.htm");
		path2 = Path.Combine (path, "v18n4a02.htm");
		
		// Cadena con el contenido del archivo final tal como queremos
		// que PDFPoppler nos entregue.
		text0 = Test.ReadFile (path0);
		text1 = Test.ReadFile (path1);
		text2 = Test.ReadFile (path2);
		
		// Usamos CreateFile y depositamos los resultados en archivos
		// temporales.
		doc0.CreateHTMLFile (path, "v17n01a02f");
		doc1.CreateHTMLFile (path, "v17n4a03f");
		doc2.CreateHTMLFile (path, "v18n4a02f");
		
		// Se lee el contenido de los archivos creados por CreateFile.
		pdftext0 = Test.ReadFile (Path.Combine (path, "v17n01a02f.htm"));
		pdftext1 = Test.ReadFile (Path.Combine (path, "v17n4a03f.htm"));
		pdftext2 = Test.ReadFile (Path.Combine (path, "v18n4a02f.htm"));
		
		Assert.AreEqual (text0, pdftext0, "CF01");
		Assert.AreEqual (text1, pdftext1, "CF02");
		Assert.AreEqual (text2, pdftext2, "CF03");
	}
}
}
}
