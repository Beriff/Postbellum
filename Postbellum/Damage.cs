using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class Damage
	{
		public readonly BodyParts Aimed;
		public readonly int Precision;
		public readonly int DamageScalar;

		public Damage(BodyParts aim, int precision, int damage)
		{
			Aimed = aim;
			Precision = precision;
			DamageScalar = damage;
		}

		public int GetDamage()
		{
			return new Random().Next(DamageScalar/Precision, DamageScalar*Precision);
		}
	}
}
