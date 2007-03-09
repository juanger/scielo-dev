//
// TestCodesTable.cs: Test for CodesTable class
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
using NUnit.Framework;

namespace Scielo {
namespace Utils {

[TestFixture()]
public class TestCodesTable
{
	[Test]
	public void CodeAndSustitute()
	{
		byte [] code = new byte[] {5,8,3,9};
		byte [] sustitute = new byte [] {2,6,3};
		CodesTable obj = new CodesTable (code, sustitute);
                Assert.AreEqual (code, obj.Code, "Code equal");
                Assert.AreEqual (sustitute, obj.Sustitute, "sustitute equal"); 
	}
	
	[Test]
	public void ElementsNull()
	{
		byte [] code = new byte[] {5,8,3,9};
		byte [] sustitute = new byte [] {2,6,3};
		CodesTable obj = new CodesTable (code, sustitute);
		CodesTable obj2 = new CodesTable (null, null);
		Assert.IsNotNull (obj, "Element 1");
		Assert.IsNotNull (obj2, "Element 2");
	}
	
}
}
}