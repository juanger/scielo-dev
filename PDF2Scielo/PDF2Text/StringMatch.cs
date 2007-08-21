//
// StringMatch: A class that implements objects with matches to regular expressions.
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
using Scielo.Utils;

namespace Scielo.PDF2Text {
public class StringMatch {
	
	string full_match;
	string result_match;
	
	public StringMatch (string fullMatch, string resultMatch)
	{
		if (fullMatch == null || resultMatch == null)
			throw new ArgumentNullException ("Error: no match for one regular expression.");
		
		full_match = fullMatch;
		result_match = resultMatch;
	}
	
	public string ApplyModifiers (Modifier [] listModifiers, RuleType ruleType)
	{
		if (listModifiers == null) {
			switch (ruleType){
			case RuleType.RESULT:
				return result_match;
			case RuleType.FULL:
				return full_match;
			}
		}
		
		string result;
		if (ruleType == RuleType.FULL) {
			result = full_match;
			if (listModifiers == null)
				Console.WriteLine ("Foo");
			foreach (Modifier modifier in listModifiers) {
				result = ApplyModifier (result, modifier);
			}
		} else {
			result = result_match;
			foreach (Modifier modifier in listModifiers) {
				result = ApplyModifier (result, modifier);
			}
		}
		return result;
	}
	
	public string ApplyModifier (string text,Modifier modifier)
	{
		string result = String.Empty;
		switch (modifier.Name) {
		case "Trim":
			result = text.Trim ();
			break;
		case "Concat":
			result = StringRegexp.Unescape ((string) modifier.Parameters ["prefix"]) + text + StringRegexp.Unescape ((string) modifier.Parameters ["postfix"]);
			break;
		case "TrimEnd":
			result = text.TrimEnd ();
			break;
		}
		
		return result;
	}
	
	public string FullMatch {
		get {
			return full_match;
		}
	}
	
	public string ResultMatch {
		get {
			return result_match;
		}
	}
	
	public bool HasResultMatch ()
	{
		return !result_match.Equals (String.Empty);
	}
}
}
