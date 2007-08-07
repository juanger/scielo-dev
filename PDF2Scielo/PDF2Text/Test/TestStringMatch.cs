//
// TestStringMatch.cs: Unit tests for the StringMatch class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//


using System;
using System.Collections;
using NUnit.Framework;

namespace Scielo.PDF2Text {
	
[TestFixture()]
public class TestStringMatch {
	
	[Test]
	public void HasResultMatch ()
	{
		StringMatch fullMatch = new StringMatch ("Foo", "");
		StringMatch resultMatch = new StringMatch ("Foo", "Bar");
		Assert.AreEqual(false, fullMatch.HasResultMatch (), "HRM01");
		Assert.AreEqual(true, resultMatch.HasResultMatch (), "HRM02");
	}
	
	[Test]
	public void ApplyModifierFullTrim ()
	{
		StringMatch fullMatch = new StringMatch ("\nFoo    ", "");
		Modifier trim = new Modifier ("Trim", null);
		Assert.AreEqual ("Foo", fullMatch.ApplyModifier (fullMatch.FullMatch, trim));
	}
	
	[Test]
	public void ApplyModifierResultTrim ()
	{
		StringMatch resultMatch = new StringMatch ("\nFoo    ", "\n\n    Bar    \n");
		Modifier trim = new Modifier ("Trim", null);
		Assert.AreEqual ("Bar", resultMatch.ApplyModifier (resultMatch.ResultMatch, trim));
	}
	
	[Test]
	public void ApplyModifierFullConcat ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Hector";
		parameters ["postfix"] = "Morales";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual ("Hector Gomez Morales", fullMatch.ApplyModifier (fullMatch.FullMatch, concat));
	}
	
	[Test] 
	public void ApplyModifierResultConcat ()
	{
		StringMatch resultMatch = new StringMatch (" Gomez ", " German ");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Juan";
		parameters ["postfix"] = "Castañeda";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual ("Juan German Castañeda", resultMatch.ApplyModifier (resultMatch.ResultMatch, concat));
	}
	
	[Test]
	public void ApplyModifiersFull ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Hector";
		parameters ["postfix"] = "Morales";
		Modifier trim = new Modifier ("Trim", null);
		Modifier concat = new Modifier ("Concat", parameters);
		Modifier [] modifiers = {trim, concat};
		Assert.AreEqual ("HectorGomezMorales", fullMatch.ApplyModifiers (modifiers, RuleType.FULL));
	}
	
	[Test]
	public void ApplyModifiersResult ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "\n German \n");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Juan";
		parameters ["postfix"] = "Castañeda";
		Modifier trim = new Modifier ("Trim", null);
		Modifier concat = new Modifier ("Concat", parameters);
		Modifier [] modifiers = {trim, concat};
		Assert.AreEqual ("JuanGermanCastañeda", fullMatch.ApplyModifiers (modifiers, RuleType.RESULT));
	}
	
	[Test]
	public void ApplyModifiersFull2 ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "\n\nHector";
		parameters ["postfix"] = "Morales    ";
		Modifier trim = new Modifier ("Trim", null);
		Modifier concat = new Modifier ("Concat", parameters);
		Modifier [] modifiers = {concat, trim};
		Assert.AreEqual ("Hector Gomez Morales", fullMatch.ApplyModifiers (modifiers, RuleType.FULL));
	}
	
	[Test]
	public void ApplyModifiersResult2 ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", " German ");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "         Juan";
		parameters ["postfix"] = "Castañeda\n  \n";
		Modifier trim = new Modifier ("Trim", null);
		Modifier concat = new Modifier ("Concat", parameters);
		Modifier [] modifiers = {concat, trim};
		Assert.AreEqual ("Juan German Castañeda", fullMatch.ApplyModifiers (modifiers, RuleType.RESULT));
	}
	
	[Test]
	public void ApplyModifiersFullConcatPrefix ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Hector";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual ("Hector Gomez ", fullMatch.ApplyModifier (fullMatch.FullMatch, concat));
	}
	
	[Test]
	public void ApplyModifierResultConcatPrefix ()
	{
		StringMatch resultMatch = new StringMatch (" Gomez ", " German ");
		Hashtable parameters = new Hashtable ();
		parameters ["prefix"] = "Juan";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual ("Juan German ", resultMatch.ApplyModifier (resultMatch.ResultMatch, concat));
	}
	
	[Test]
	public void ApplyModifierFullConcatPostfix ()
	{
		StringMatch fullMatch = new StringMatch (" Gomez ", "");
		Hashtable parameters = new Hashtable ();
		parameters ["postfix"] = "Morales";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual (" Gomez Morales", fullMatch.ApplyModifier (fullMatch.FullMatch, concat));
	}
	
	[Test] 
	public void ApplyModifierResultConcatPostfix ()
	{
		StringMatch resultMatch = new StringMatch (" Gomez ", " German ");
		Hashtable parameters = new Hashtable ();
		parameters ["postfix"] = "Castañeda";
		Modifier concat = new Modifier ("Concat", parameters);
		Assert.AreEqual (" German Castañeda", resultMatch.ApplyModifier (resultMatch.ResultMatch, concat));
	}
}
}
