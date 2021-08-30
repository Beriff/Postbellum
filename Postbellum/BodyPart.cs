using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	enum BodyParts
	{
		Legs,
		Hands,
		Eyes,
		Torso,
		Head
	}
	class BodyPart
	{
		public int Health;
		public int Functioning;

		public bool IsFunctional()
		{
			return Health >= Functioning;
		}
	}
}
