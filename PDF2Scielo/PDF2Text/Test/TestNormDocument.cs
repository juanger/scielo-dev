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
using Scielo.Utils;

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestNormDocument {
	
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
		
		string ntext0 = ndoc0.GetText ();
		string ntext1 = ndoc1.GetText ();
		string ntext2 = ndoc2.GetText ();
		
		path0 = Path.Combine (path, "v17n01a02-norm.txt");
		path1 = Path.Combine (path, "v17n4a03-norm.txt");
		path2 = Path.Combine (path, "v17n2a02-norm.txt");
		
		string text0 = Test.ReadFile (path0);
		string text1 = Test.ReadFile (path1);
		string text2 = Test.ReadFile (path2);
		
		Assert.AreEqual (text0, ntext0, "GT01");
		Assert.AreEqual (text1, ntext1, "GT02");
		Assert.AreEqual (text2, ntext2, "GT03");
	}
}
}
}
