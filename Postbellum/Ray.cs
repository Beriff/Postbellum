using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class Ray
	{

		private static void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		protected Vector2 Start;
		protected Vector2 Destination;
		protected GameGrid Space;
		private uint step = 0;
		public List<Vector2> coords;
		public int Length { get; private set; }

		public Ray(Vector2 start, Vector2 dest, GameGrid gg)
		{
			Start = start;
			Destination = dest;
			Space = gg;

			int x1 = (int)Start.X;
			int y1 = (int)Start.Y;

			int x2 = (int)Destination.X;
			int y2 = (int)Destination.Y;

			int dx = x2 - x1;

			int dy = y2 - y1;

			bool issteep = Math.Abs(dy) > Math.Abs(dx);

			if (issteep)
			{
				Swap(ref x1, ref x2);
				Swap(ref y1, ref y2);
			}

			bool swapped = false;
			if (x1 > x2)
			{
				Swap(ref x1, ref x2);
				Swap(ref y1, ref y2);
				swapped = true;
			}

			dx = x2 - x1;

			dy = y2 - y1;

			int error = (int)(dx / 2.0);

			int ystep = y1 < y2 ? 1 : -1;

			int y = y1;

			List<Vector2> points = new List<Vector2>();
			for (int x = x1; x < x2; ++x)
			{
				Vector2 coords = issteep ? new Vector2(y, x) : new Vector2(x, y);
				points.Add(coords);

				error -= Math.Abs(dy);
				if (error < 0)
				{
					y += ystep;
					error += dx;
				}
			}

			if (swapped)
				points.Reverse();

			coords = points;

		}
		
	}
}
