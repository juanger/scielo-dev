
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
