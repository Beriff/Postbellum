using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	public class Entity
	{
		public Texture2D EntityTexture { get; protected set; }
		protected int MaxHP;
		protected int HP;
		public Vector2 Position = new Vector2(1, 1);
		public Vector2 BoundingBox = new Vector2(16, 16);
		public Vector2 Offset = new Vector2(0, 0);
		public delegate void Tick(GameTime gt);
		public bool Collision = true;

		public Entity(Texture2D texture, int hp, Vector2 position)
		{
			MaxHP = HP = hp;
			EntityTexture = texture;
			Position = position;
		}

		public Entity(Texture2D texture, int hp, Vector2 position, Vector2 hitbox, Vector2 offset)
		{
			MaxHP = HP = hp;
			EntityTexture = texture;
			Position = position;
			BoundingBox = hitbox;
			Offset = offset;
		}
	}
}
