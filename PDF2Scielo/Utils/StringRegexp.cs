//
// StringRegexp: A class that implements regular expression handling
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
//
// Copyright (C) 2007 UNAM DGB
//

using System;

namespace Scielo.Utils {
public class StringRegexp {
	
	public static string Unescape (string regexp)
	{
		return regexp.Replace (@"\\", @"\");
	}
	
	public static string ReplaceEntities (string regexp)
	{
		string [][] entities = {new string [] {"&gt;", ">"}, 
					new string [] {"&lt;", "<"},
					new string [] {"&amp;", "&"}};
		
		string result = regexp;
		foreach (string [] entity in entities)
		{
			result = result.Replace (entity [0], entity [1]);
		}
		
		return result;
	}
}
}
