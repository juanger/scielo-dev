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
	public void GetRawText ()
	{
		string path, path0, path1, path2, text0, text1, text2;
		string pdftext0, pdftext1, pdftext2;

		path = PathOfTest ();

		// Rutas a los PDF a usar en el test unit.
		path0 = Path.Combine (path, "v17n01a02.pdf");
		path1 = Path.Combine (path, "v17n4a03.pdf");
		path2 = Path.Combine (path, "v18n4a02.pdf");
		
		Uri uri0 = new Uri (path0);
		Uri uri1 = new Uri (path1);
		Uri uri2 = new Uri (path2);
		
		PDFPoppler doc0 = PDFPoppler.CreateInstance (uri0);
		PDFPoppler doc1 = PDFPoppler.CreateInstance (uri1);
		PDFPoppler doc2 = PDFPoppler.CreateInstance (uri2);
		
		path0 = Path.Combine (path, "v17n01a02.txt");
		path1 = Path.Combine (path, "v17n4a03.txt");
		path2 = Path.Combine (path, "v18n4a02.txt");
		
		text0 = ReadFile (path0);
		text1 = ReadFile (path1);
		text2 = ReadFile (path2);
		
		pdftext0 = doc0.GetRawText ();
		pdftext1 = doc1.GetRawText ();
		pdftext2 = doc2.GetRawText ();
		
		Assert.AreEqual (text0, pdftext0);
		Assert.AreEqual (text1, pdftext1);
		Assert.AreEqual (text2, pdftext2);
	}
	
	[Test]
	public void CreateFile ()
	{
		string path, path0, path1, path2, text0, text1, text2;
		string pdftext0, pdftext1, pdftext2;
		
		path = PathOfTest ();
		
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
		
		path0 = Path.Combine (path, "v17n01a02.txt");
		path1 = Path.Combine (path, "v17n4a03.txt");
		path2 = Path.Combine (path, "v18n4a02.txt");
		
		// Cadena con el contenido del archivo final tal como queremos
		// que PDFPoppler nos entregue.
		text0 = ReadFile (path0);
		text1 = ReadFile (path1);
		text2 = ReadFile (path2);
		
		// Usamos CreateFile y depositamos los resultados en archivos
		// temporales.
		doc0.CreateFile (path, "v17n01a02f");
		doc1.CreateFile (path, "v17n4a03f");
		doc2.CreateFile (path, "v18n4a02f");
		
		// Se lee el contenido de los archivos creados por CreateFile.
		pdftext0 = ReadFile (Path.Combine (path, "v17n01a02f.txt"));
		pdftext1 = ReadFile (Path.Combine (path, "v17n4a03f.txt"));
		pdftext2 = ReadFile (Path.Combine (path, "v18n4a02f.txt"));
		
		Assert.AreEqual (text0, pdftext0, "CF01");
		Assert.AreEqual (text1, pdftext1, "CF02");
		Assert.AreEqual (text2, pdftext2, "CF03");
	}
	
	private string ReadFile (string filepath)
	{
		string result;
		
		FileStream filestream = null;
		using (filestream = File.Open (filepath, FileMode.Open)) {
			StreamReader reader = new StreamReader (filestream);
			result = reader.ReadToEnd ();
			reader.Close ();
		}
		
		return result;
	}
	
	private string PathOfTest ()
	{
		// FIXME: Este es un hack para correr los casos depende de la
		// locacion cuando se corre el test.
		string path;
		path = Environment.CurrentDirectory;
		path = path.Replace ("bin" + Path.DirectorySeparatorChar + "Debug", String.Empty);
		path = Path.Combine (path, "Test");
		
		return path;
	}
}
}
}
