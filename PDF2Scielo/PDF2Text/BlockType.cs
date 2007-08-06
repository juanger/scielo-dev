//
// AtmNormalizer: A class that implements values for block types.
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

namespace Scielo.PDF2Text {
	
	public enum BlockType :int
	{
		GLOBAL = 0,
		FRONT = 1,
		BODY = 2,
		BACK = 3,
	}
}
