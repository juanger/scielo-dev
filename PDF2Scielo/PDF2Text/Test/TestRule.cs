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
	public void TestStaticAttributes()
	{
		XmlNode ruleNode = document.SelectSingleNode ("/def:style/def:global/def:rule[1]", manager);
		Rule newRule = new Rule (ruleNode, manager, BlockType.GLOBAL);
		Assert.AreEqual ("RemoveHeaders0", newRule.Name, "TSA0");
		Assert.AreEqual (@"[\n]+[\u000c]+[0-9]+[ ]*[ \w\p{S}\p{P}]+[\n]+", newRule.Regexp, "TSA1");
		Assert.AreEqual ("\n", newRule.Sustitution, "TSA2");
		Assert.AreEqual (false, newRule.UniqueMatch, "TSA3");
		Assert.AreEqual (RuleType.STATIC, newRule.Type, "TSA4");
		Assert.AreEqual (BlockType.GLOBAL, newRule.Block, "TSA5");
		Assert.IsNull (newRule.Modifiers, "TSA6");
	}
	
	[Test()]
	public void TestFullAttributes()
	{
		XmlNode ruleNode = document.SelectSingleNode ("/def:style/def:global/def:rule[last()]", manager);
		Rule newRule = new Rule (ruleNode, manager, BlockType.GLOBAL);
		Assert.AreEqual ("MarkKeyword", newRule.Name, "TFA0");
		Assert.AreEqual (@"[\n]+(Key words|Keywords|Keyword|Key word):[ ]+[ \w\p{P}\n]+?[\n]+", newRule.Regexp, "TFA1");
		Assert.AreEqual ("#{Result}", newRule.Sustitution, "TFA2");
		Assert.AreEqual (true, newRule.UniqueMatch, "TFA3");
		Assert.AreEqual (RuleType.FULL, newRule.Type, "TFA4");
		Assert.AreEqual (BlockType.GLOBAL, newRule.Block, "TFA5");
		Assert.AreEqual ("Trim", newRule.Modifiers [0].Name, "TFA6");
		Assert.AreEqual ("Concat", newRule.Modifiers [1].Name);
		Assert.AreEqual ("\n[key] ", newRule.Modifiers [1].Parameters ["prefix"]);
		Assert.AreEqual (" [/key]\n\n", newRule.Modifiers [1].Parameters ["postfix"]);
	}
	
	[Test()]
	public void TestResultAttributes()
	{
		XmlNode ruleNode = document.SelectSingleNode ("/def:style/def:front/def:rule[1]", manager);
		Rule newRule = new Rule (ruleNode, manager, BlockType.FRONT);
		Assert.AreEqual ("MarkTitle", newRule.Name, "TRA0");
		Assert.AreEqual (@"^Atm.*[\n ]+(?<Result>[^|]+?)\n[\n]+", newRule.Regexp, "TRA1");
		Assert.AreEqual (@"#{Result}", newRule.Sustitution, "TRA2");
		Assert.AreEqual (true, newRule.UniqueMatch, "TRA3");
		Assert.AreEqual (RuleType.RESULT, newRule.Type, "TRA4");
		Assert.AreEqual (BlockType.FRONT, newRule.Block, "TRA5");
		Assert.AreEqual (1, newRule.Modifiers.Length);
		Assert.AreEqual ("Concat", newRule.Modifiers [0].Name);
		Assert.AreEqual ("[title] ", newRule.Modifiers [0].Parameters ["prefix"]);
		Assert.AreEqual (" [/title]\n", newRule.Modifiers [0].Parameters ["postfix"]);
	}
}
}
