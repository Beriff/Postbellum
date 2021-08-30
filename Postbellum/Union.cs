using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class Union<A, B>
	{
		private A a;
		private B b;

		public Union(A val)
		{
			a = val;
		}

		public Union(B val)
		{
			b = val;
		}

	}
}
