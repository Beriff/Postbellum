using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	enum Actions
	{
		MoveDown,
		MoveRight,
		MoveUp,
		MoveLeft,
		Idle
	}
	class Player : Entity
	{
		public BodyPart Legs = new BodyPart();
		public BodyPart Hands = new BodyPart();
		public BodyPart Head = new BodyPart();
		public BodyPart Torso = new BodyPart();
		public BodyPart Eyes = new BodyPart();

		public Dictionary<Actions, Texture2D> States = new Dictionary<Actions, Texture2D>();
		public Chunk CurrentChunk;
		public Player(Texture2D texture_front, Texture2D texture_side, Texture2D texture_back, Vector2 position, Chunk c) : base(texture_front, 100, position)
		{
			CurrentChunk = c;
		}

		public void StateUpdate(Actions action)
		{
			EntityTexture = States[action];
		}

		public bool IsDead()
		{
			return HP <= 0;
		}

		public bool TakeDamage(Damage dmg)
		{
			HP -= dmg.GetDamage() / 2;

			switch(dmg.Aimed)
			{
				case BodyParts.Eyes:
					Eyes.Health -= dmg.GetDamage() / 2;
					break;

				case BodyParts.Hands:
					Hands.Health -= dmg.GetDamage() / 2;
					break;

				case BodyParts.Head:
					Head.Health -= dmg.GetDamage() / 2;
					break;

				case BodyParts.Legs:
					Legs.Health -= dmg.GetDamage() / 2;
					break;

				case BodyParts.Torso:
					Torso.Health -= dmg.GetDamage() / 2;
					break;
			}

			return IsDead();
		}
	}
}
