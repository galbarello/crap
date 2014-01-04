using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace crap
{

	public class Nothing
	{
		private Nothing() { }
		public static readonly Nothing Value = new Nothing();
		public override string ToString()
		{
			return "Nothing";
		}
	}

}
