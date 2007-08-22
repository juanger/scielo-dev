//
// Foo.cs: A class that implements
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using Mono.GetOptions;

namespace Scielo.PDF2Scielo {
public class AppOptions : Options {
	[Option("Formato de estilo a usar", 'f', "format")]
	public bool Format = false;
	
	[Option("Numero de columnas", 'n', "columns")]
	public bool numColumns = false;
	
	[Option("output version information and exit", "version")]
	public override WhatToDoNext DoAbout()
	{
		return base.DoAbout();
	}
	
	[Option("display this help and exit", "help")]
	public override WhatToDoNext DoHelp()
	{
		return base.DoHelp();
	}
	
	[KillOption]
	public override WhatToDoNext DoHelp2() { return WhatToDoNext.GoAhead; }
	
	[KillOption]
	public override WhatToDoNext DoUsage() { return WhatToDoNext.GoAhead; }
	
	public AppOptions(string[] args) : base(args) {}
	
	protected override void InitializeOtherDefaults() 
	{
		ParsingMode = OptionsParsingMode.Both | OptionsParsingMode.GNU_DoubleDash;
//		BreakSingleDashManyLettersIntoManyOptions = true; 
	}
}
}
