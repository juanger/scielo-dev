//
// CodesTable: A class that implements the relation between 
// the code original and the code sustitute.
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

namespace PDF2Text
{
	
	
	public class CodesTable
	{
		private byte [] code;
		private byte [] sustitute;
		
		public CodesTable(byte [] code, byte [] sustitute)
		{
			this.code = code;
			this.sustitute = sustitute;
		}
		
	        public byte [] Code {
			get {
				return code;
			}
		}
		
		public byte [] Sustitute {
			get {
				return sustitute;
			}
		}
		
	}
}
