//
// TestStringRegexp.cs: Unit tests for the StringRegexp class.
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
using NUnit.Framework;

namespace Scielo.Utils {
	
[TestFixture()]
public class TestStringRegexp {
	
	[Test()]
	public void Unescape ()
	{
		string old = "\\[/author\\]\\n(?&lt;Result&gt;(.|\\n)+?)(\\[author\\]|\\[date\\]|\\[res\\])";
		Assert.AreEqual (@"\[/author\]\n(?&lt;Result&gt;(.|\n)+?)(\[author\]|\[date\]|\[res\])", StringRegexp.Unescape (old));
	}
	
	[Test()]
	public void ReplaceEntities ()
	{
		string old = @"[ ]*(?&lt;Result&gt;(([A-Z]{1,2}\. ([A-Z]{1,2}[.]? )*[-A-Z][-a-zA-Z&amp;;]+( [-a-zA-Z&amp;;]+)?)(, | and )?)+)[\n]+";
		Assert.AreEqual (@"[ ]*(?<Result>(([A-Z]{1,2}\. ([A-Z]{1,2}[.]? )*[-A-Z][-a-zA-Z&;]+( [-a-zA-Z&;]+)?)(, | and )?)+)[\n]+", StringRegexp.ReplaceEntities (old));
	}
}
}
