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

namespace Scielo {
namespace PDF2Text {

[TestFixture()]
public class TestPDFPoppler {
	
	[Test]
	public void CreateInstance ()
	{
		Uri uri = new Uri ("/foo/v17n01a02.pdf");
		PDFPoppler doc = PDFPoppler.CreateInstance (uri);
		Assert.IsNull (doc, "CI01");	
	}
	
	[Test]
	public void CreateInstance2 ()
	{
		string path, path0, path1, path2; 
		path = Environment.CurrentDirectory;
		path = path.Replace ("bin/Debug", String.Empty);
		path0 = Path.Combine (path, "Test/v17n01a02.pdf");
		path1 = Path.Combine (path, "Test/v17n4a03.pdf");
		path2 = Path.Combine (path, "Test/v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		Assert.IsNotNull (doc0, "CI02:1");
		Assert.IsNotNull (doc1, "CI02:2");
		Assert.IsNotNull (doc2, "CI02:3");
	}
	
	[Test]
	public void GetText ()
	{
		string path, path0, path1, path2; 
		path = Environment.CurrentDirectory;
		path = path.Replace ("bin/Debug", String.Empty);
		path0 = Path.Combine (path, "Test/v17n01a02.pdf");
		path1 = Path.Combine (path, "Test/v17n4a03.pdf");
		path2 = Path.Combine (path, "Test/v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
	}
}
}
}
