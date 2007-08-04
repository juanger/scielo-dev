//
// TestRawDocument.cs: Unit tests for the RawDocument class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//

using System;
using System.Xml;
using System.IO;
using Scielo.Utils;
using NUnit.Framework;

namespace Scielo.PDF2Text {
[TestFixture()]
public class TestRule {
	private XmlDocument document;
	
	[SetUp()]
	public void Initialize ()
	{
		document = new XmlDocument ();
		string source = Path.Combine (Test.PathOfTest (), "test-schema.xml");
		Console.WriteLine (source);
		XmlTextReader reader = new XmlTextReader (source);
		document.Load (reader);
	}
	
	[Test()]
	public void TestStaticAttributes()
	{
		XmlNode root = document.DocumentElement;
		//Console.WriteLine (root.OuterXml);
		XmlNode ruleNode = document.SelectSingleNode ("/style/global/rule[1]");
		Console.WriteLine (ruleNode.OuterXml);
		Rule newRule = new Rule (ruleNode);
		Assert.AreEqual ("RemoveHeaders0", newRule.Name, "TA0");
		Assert.AreEqual (@"[\n]+[\u000c]+[0-9]+[ ]*[ #{ALET}#{ASYM}#{APUC}]+[\n]+", newRule.Regexp, "TA1");
		Assert.AreEqual (@"\n", newRule.Sustitution, "TA2");
		Assert.AreEqual (false, newRule.UniqueMatch, "TA3");
		Assert.AreEqual (Rule.RuleType.STATIC, newRule.Type, "TA4");
	}
}
}
