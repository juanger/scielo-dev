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
		XmlNode ruleNode = document.SelectSingleNode ("/style/global/rule[1]");
		Rule newRule = new Rule (ruleNode);
		Assert.AreEqual ("RemoveHeaders0", newRule.Name, "TSA0");
		Assert.AreEqual (@"[\n]+[\u000c]+[0-9]+[ ]*[ #{ALET}#{ASYM}#{APUC}]+[\n]+", newRule.Regexp, "TSA1");
		Assert.AreEqual (@"\n", newRule.Sustitution, "TSA2");
		Assert.AreEqual (false, newRule.UniqueMatch, "TSA3");
		Assert.AreEqual (Rule.RuleType.STATIC, newRule.Type, "TSA4");
	}
	
	[Test()]
	public void TestFullAttributes()
	{
		XmlNode ruleNode = document.SelectSingleNode ("/style/global/rule[last()]");
		Rule newRule = new Rule (ruleNode);
		Assert.AreEqual ("MarkKeyword", newRule.Name, "TFA0");
		Assert.AreEqual (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[ #{ALET}#{APUC}\n]+?[\n]+", newRule.Regexp, "TFA1");
		Assert.AreEqual (@"#{Result}", newRule.Sustitution, "TFA2");
		Assert.AreEqual (true, newRule.UniqueMatch, "TFA3");
		Assert.AreEqual (Rule.RuleType.FULL, newRule.Type, "TFA4");
	}
	
	[Test()]
	public void TestResultAttributes()
	{
		XmlNode ruleNode = document.SelectSingleNode ("/style/front/rule[1]");
		Rule newRule = new Rule (ruleNode);
		Assert.AreEqual ("MarkTitle", newRule.Name, "TRA0");
		Assert.AreEqual (@"^Atm.*[\n ]+(?<Result>[^|]+?)\n[\n]+", newRule.Regexp, "TRA1");
		Assert.AreEqual (@"#{Result}", newRule.Sustitution, "TRA2");
		Assert.AreEqual (true, newRule.UniqueMatch, "TRA3");
		Assert.AreEqual (Rule.RuleType.RESULT, newRule.Type, "TRA4");
	}
}
}
