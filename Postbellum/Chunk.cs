using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	public class Chunk
	{
		public const int ChunkSizeX = 32;
		public const int ChunkSizeY = 32;

		public Vector2 ChunkPosition { get; set; } = new Vector2(0, 0);

		public GridItem[,] grid;
		public List<Entity> Entities = new List<Entity>();
		public Chunk()
		{
			grid = new GridItem[ChunkSizeX, ChunkSizeY];
		}
		public Chunk(GridItem[,] items)
		{
			grid = items;
		}
		public Chunk(Vector2 position)
		{
			grid = new GridItem[ChunkSizeX, ChunkSizeY];
			ChunkPosition = position;
		}
		public GridItem GetAt(Vector2 pos)
		{
			if ((pos.X >= 0 && pos.X <= ChunkSizeX-1) && (pos.Y >= 0 && pos.Y <= ChunkSizeY-1))
				return grid[(int)pos.X, (int)pos.Y];
			throw new Exception("Position out of range: " + pos.ToString());
		}
		public void Render(SpriteBatch sb, Vector2 offset)
		{
			for (int x = 0; x < ChunkSizeX; x++)
			{
				for (int y = 0; y < ChunkSizeY; y++)
				{
					Texture2D texture = grid[x, y].ItemTexture;
					int w = texture.Width;
					int h = texture.Height;
					sb.Draw(texture, new Vector2(ChunkPosition.X*32 + x*w*GridItem.TextureScalar, ChunkPosition.Y*32 + y*h*GridItem.TextureScalar) + offset, 
						null, Color.White, 0f, new Vector2(0,0), GridItem.TextureScalar, SpriteEffects.None, 0f);
				}
			}

			foreach (Entity ent in Entities)
			{
				Texture2D texture = ent.EntityTexture;
				int w = (int)ent.BoundingBox.X;
				int h = (int)ent.BoundingBox.Y;
				sb.Draw(texture, 
						new Vector2(ChunkPosition.X + ent.Position.X * w * GridItem.TextureScalar - ent.Offset.X, 
									ChunkPosition.Y + ent.Position.Y * h * GridItem.TextureScalar - ent.Offset.Y) + offset,
						null, Color.White, 0f, new Vector2(0, 0), GridItem.TextureScalar, SpriteEffects.None, 0f);
			}
		}
		public Chunk Fill(GridItem dev)
		{
			grid = new GridItem[ChunkSizeX, ChunkSizeY];
			for (int x = 0; x < ChunkSizeX; x++ )
			{
				for (int y = 0; y < ChunkSizeY; y++)
				{
					grid[x, y] = dev;
				}
			}
			return this;
		}

		public bool IsEntityAt(Vector2 pos)
		{
			foreach(Entity ent in Entities)
			{
				if (ent.Position == pos)
					return true;
			}
			return false;
		}
		public Entity GetEntityAt(Vector2 pos)
		{
			foreach (Entity ent in Entities)
			{
				if (ent.Position == pos)
					return ent;
			}
			throw new Exception("No entity at " + pos.ToString() + ". Please use chunk.IsEntityAt() before.");
		}

		public Vector2 LocalCoordsToGlbal(Vector2 local)
		{
			return new Vector2(
				PostbellumGame.TileToPixels((int)(ChunkPosition.X * 16 + local.X)),
				PostbellumGame.TileToPixels((int)(ChunkPosition.Y * 16 + local.Y))
				);
		}
	}
}
