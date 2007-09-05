//
// TestNormalizer.cs: Test for Normalizer class.
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
using System.Xml;
using System.Collections;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo.PDF2Text {

[TestFixture()]
public class TestNormalizer {
	private string [] style;
	private ArrayList raw_docs;
	private XmlDocument document;
	private XmlNamespaceManager manager;
	private string path;
	
	[SetUp()]
	public void Initialize ()
	{
		document = new XmlDocument ();
		path = Test.PathOfTest ();
		string source = Path.Combine (path, "normalizer-rules.xml");
		XmlTextReader reader = new XmlTextReader (source);
		document.Load (reader);
		manager = new XmlNamespaceManager (document.NameTable);
		manager.AddNamespace ("def", "http://www.scielo.org.mx");
		
		ArrayList test_docs = new ArrayList ();
		ArrayList temp_docs = Test.GetAllFilesByType ((int) Test.DocTypes.PDF);
		style = new string [temp_docs.Count];
		
		int count = 0;
		foreach (string[] array in temp_docs) {
			style [count] = array [0];
			Uri uri = new Uri(array[1]);
			test_docs.Add (new PDFPoppler (uri));
			count++;
		}
		
		raw_docs = new ArrayList ();
		foreach (PDFPoppler pdf in test_docs) {
			raw_docs.Add (new RawDocument (pdf));
		}
		
		#if DEBUG
		Logger.ActivateDebug ();
		#endif
	}
	
	[Test()]
	public void ConstructorRawDocument ()
	{
		Type etype = Type.GetType ("Scielo.PDF2Text.Normalizer");
		
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw, style [count]);
			Assert.IsInstanceOfType (etype, norm, "CRD" + count);
			count++;
		}
	}
	
	[Test()]
	public void ConstructorString ()
	{
		Type etype = Type.GetType ("Scielo.PDF2Text.Normalizer");
		
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw.GetText (), style [count]);
			Assert.IsInstanceOfType (etype, norm, "CS" + count);
			count++;
		}
	}
	
	[Test()]
	[ExpectedException(typeof (FileNotFoundException))]
	public void ConstructorBadStyle ()
	{
		Type etype = Type.GetType ("Scielo.PDF2Text.Normalizer");
		int count = 0;
		foreach (RawDocument raw in raw_docs) {
			Normalizer norm = new Normalizer (raw.GetText (), "atn");
			Assert.IsInstanceOfType (etype, norm, "CBS" + count);
			count++;
		}
	}
	
	[Test()]
	public void ApplyRule ()
	{
		XmlNodeList globalNodes = document.SelectNodes ("/def:style/def:global/def:rule", manager);
		Rule[] rules = new Rule [globalNodes.Count];
		
		int count = 0;
		foreach (XmlNode node in globalNodes) {
			rules [count] = new Rule (node, manager, BlockType.GLOBAL);
			count++;
		}
		
		string filepath = Path.Combine (path, "normalizer.txt");
		Normalizer norm = new Normalizer (Test.ReadFile (filepath), rules);
		norm.ApplyRule (rules [0]);
		norm.ApplyRule (rules [1]);
		filepath = Path.Combine (path, "normalizer-step1.txt");
		Assert.AreEqual (Test.ReadFile (filepath), norm.Text, "AR");
	}
}
}