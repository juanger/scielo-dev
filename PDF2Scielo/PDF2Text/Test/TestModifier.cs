// /home/hector/Projects/PDF2Scielo/PDF2Text/Test/TestModifier.cs created with MonoDevelop
// User: hector at 6:02 PM 8/6/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Xml;
using System.IO;
using System.Collections;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo.PDF2Text {
	
[TestFixture()]
public class TestModifier {
	private XmlDocument document;
	private XmlNamespaceManager manager;
	
	[SetUp()]
	public void Initialize ()
	{
		document = new XmlDocument ();
		string source = Path.Combine (Test.PathOfTest (), "test-schema.xml");
		XmlTextReader reader = new XmlTextReader (source);
		document.Load (reader);
		manager = new XmlNamespaceManager (document.NameTable);
		manager.AddNamespace ("def", "http://www.scielo.org.mx");
	}
	
	[Test()]
	public void TestModifierWithoutParamsAttributes ()
	{
		XmlNode node = document.SelectSingleNode ("/def:style/def:global/def:rule[last()]/def:modifiers/def:modifier[1]", manager);
		Modifier mod = new Modifier (node, manager);
		Assert.AreEqual ("Trim", mod.Name, "TMWOPA1");
		Assert.IsNull (mod.Parameters, "TMWOPA2");
	}
	
	[Test()]
	public void TestModifierWithParamsAttributes ()
	{
		XmlNode node = document.SelectSingleNode ("/def:style/def:global/def:rule[last()]/def:modifiers/def:modifier[2]", manager);
		Modifier mod = new Modifier (node, manager);
		Assert.AreEqual ("Concat", mod.Name, "TMWPA1");
		Assert.AreEqual ("\n[key] ", mod.Parameters ["prefix"], "TMWPA2");
		Assert.AreEqual (" [/key]\n\n", mod.Parameters ["postfix"], "TMWPA3");
	}
}
}
