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
using NUnit.Framework;
using System.IO;
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestRawDocument {
	
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
		string path = Test.PathOfTest ();
		
		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v17n2a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0, "atm");
		PDFPoppler doc1 = new PDFPoppler (uri1, "atm");
		PDFPoppler doc2 = new PDFPoppler (uri2, "atm");
		
		RawDocument rdoc0 = new RawDocument (doc0);
		RawDocument rdoc1 = new RawDocument (doc1);
		RawDocument rdoc2 = new RawDocument (doc2);
		
		Type etype = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Assert.IsInstanceOfType (etype, rdoc0, "CI01");
		Assert.IsInstanceOfType (etype, rdoc1, "CI01");
		Assert.IsInstanceOfType (etype, rdoc2, "CI01");
	}
	
	[Test]
	public void GetText () 
	{
		string path = Test.PathOfTest ();
		
		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v17n2a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0, "atm");
		PDFPoppler doc1 = new PDFPoppler (uri1, "atm");
		PDFPoppler doc2 = new PDFPoppler (uri2, "atm");
		
		RawDocument rdoc0 = new RawDocument (doc0);
		RawDocument rdoc1 = new RawDocument (doc1);
		RawDocument rdoc2 = new RawDocument (doc2);
		
		string rtext0 = rdoc0.GetText ();
		string rtext1 = rdoc1.GetText ();
		string rtext2 = rdoc2.GetText ();
		
		path0 = Path.Combine (path, "v17n01a02-raw.txt");
		path1 = Path.Combine (path, "v17n4a03-raw.txt");
		path2 = Path.Combine (path, "v17n2a02-raw.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		Assert.AreEqual (text0, rtext0, "GR01");
		Assert.AreEqual (text1, rtext1, "GR02");
		Assert.AreEqual (text2, rtext2, "GR03");
	}
	
	[Test]
	public void Normalize () 
	{
		string path = Test.PathOfTest ();
		
		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v17n2a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = new PDFPoppler (uri0, "atm");
		PDFPoppler doc1 = new PDFPoppler (uri1, "atm");
		PDFPoppler doc2 = new PDFPoppler (uri2, "atm");
		
		RawDocument rdoc0 = new RawDocument (doc0);
		RawDocument rdoc1 = new RawDocument (doc1);
		RawDocument rdoc2 = new RawDocument (doc2);
		
		NormDocument ndoc0 = rdoc0.Normalize ();
		NormDocument ndoc1 = rdoc1.Normalize ();
		NormDocument ndoc2 = rdoc2.Normalize ();
		
		Type etype0 = Type.GetType ("Scielo.PDF2Text.RawDocument");
		Assert.IsInstanceOfType (etype0, rdoc0, "CI01");
		Assert.IsInstanceOfType (etype0, rdoc1, "CI01");
		Assert.IsInstanceOfType (etype0, rdoc2, "CI01");
		
		Type etype1 = Type.GetType ("Scielo.PDF2Text.NormDocument");
		Assert.IsInstanceOfType (etype1, ndoc0, "CI01");
		Assert.IsInstanceOfType (etype1, ndoc1, "CI01");
		Assert.IsInstanceOfType (etype1, ndoc2, "CI01");
	}
	
	[Test]
	public void WriteDocument ()
	{	
		string path = Test.PathOfTest ();
		
		// Rutas a los PDF a usar en el test unit.
		string path0 = Path.Combine (path, "v17n01a02.pdf");
		string path1 = Path.Combine (path, "v17n4a03.pdf");
		string path2 = Path.Combine (path, "v17n2a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		// Se crean los lectores para cada PDF.
		PDFPoppler doc0 = new PDFPoppler (uri0, "atm");
		PDFPoppler doc1 = new PDFPoppler (uri1, "atm");
		PDFPoppler doc2 = new PDFPoppler (uri2, "atm");
		
		RawDocument rdoc0 = new RawDocument (doc0);
		RawDocument rdoc1 = new RawDocument (doc1);
		RawDocument rdoc2 = new RawDocument (doc2);
		
		path0 = Path.Combine (path, "v17n01a02-raw.txt");
		path1 = Path.Combine (path, "v17n4a03-raw.txt");
		path2 = Path.Combine (path, "v17n2a02-raw.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		// Usamos CreateFile y depositamos los resultados en archivos
		// temporales.
		rdoc0.WriteDocument (path, "v17n01a02-rawf", "txt");
		rdoc1.WriteDocument (path, "v17n4a03-rawf", "txt");
		rdoc2.WriteDocument (path, "v17n2a02-rawf", "txt");
		
		// Se lee el contenido de los archivos creados por CreateFile.
		string rtext0 = Test.ReadFile (Path.Combine (path, "v17n01a02-rawf.txt"));
		string rtext1 = Test.ReadFile (Path.Combine (path, "v17n4a03-rawf.txt"));
		string rtext2 = Test.ReadFile (Path.Combine (path, "v17n2a02-rawf.txt"));
		
		Assert.AreEqual (text0, rtext0, "GR01");
		Assert.AreEqual (text1, rtext1, "GR02");
		Assert.AreEqual (text2, rtext2, "GR03");
	}
}
}
}
