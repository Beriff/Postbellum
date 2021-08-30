using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	static class ContentLoader
	{
		public static T Load<T>(Game g, string name)
		{
			return g.Content.Load<T>(name);
		}

		public static List<T> LoadArray<T>(Game g, string[] names)
		{
			List<T> ls = new List<T>();
			foreach(string name in names)
			{
				ls.Add(g.Content.Load<T>(name));
			}
			return ls;
		}
	}
}
