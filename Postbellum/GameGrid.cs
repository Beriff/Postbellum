using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class GameGrid
	{
		private Dictionary<Vector2, Chunk> gmap;
		private readonly Dictionary<string, GridItem> tiles;
		public Camera camera;
		public Player FocusedPlayer;
		public GameGrid(Camera cam, Dictionary<string, GridItem> arr, Player p)
		{
			gmap = new Dictionary<Vector2, Chunk>();
			tiles = arr;
			camera = cam;
			FocusedPlayer = p;
		}
		public void ReceiveAction(Actions a)
		{
			switch(a)
			{
				case Actions.MoveDown:
					MoveFocused(new Vector2(0, 1));
					break;
				case Actions.MoveUp:
					MoveFocused(new Vector2(0, -1));
					break;
				case Actions.MoveLeft:
					MoveFocused(new Vector2(-1, 0));
					break;
				case Actions.MoveRight:
					MoveFocused(new Vector2(1, 0));
					break;
			}
		}
		public void MoveFocused(Vector2 pos)
		{
			Vector2 presm_pos = FocusedPlayer.Position + pos;
			Vector2 move = Vector2.Zero;

			if (presm_pos.X > Chunk.ChunkSizeX)
			{	
				presm_pos.X = 0;
				FocusedPlayer.CurrentChunk.ChunkPosition += new Vector2(1, 0);
				move += new Vector2(1, 0);
			} else if (presm_pos.X < 0)
			{
				presm_pos.X = Chunk.ChunkSizeX;
				FocusedPlayer.CurrentChunk.ChunkPosition -= new Vector2(1, 0);
				move -= new Vector2(1, 0);
			}

			if (presm_pos.Y > Chunk.ChunkSizeY)
			{
				presm_pos.Y = 0;
				FocusedPlayer.CurrentChunk.ChunkPosition -= new Vector2(0, 1);
				move -= new Vector2(0, 1);
			} else if (presm_pos.Y < 0)
			{
				presm_pos.Y = Chunk.ChunkSizeY;
				FocusedPlayer.CurrentChunk.ChunkPosition += new Vector2(0, 1);
				move += new Vector2(0, 1);
			}

			if ((FocusedPlayer.CurrentChunk.IsEntityAt(presm_pos) && FocusedPlayer.CurrentChunk.GetEntityAt(presm_pos).Collision) || FocusedPlayer.CurrentChunk.GetAt(presm_pos).Collision)
			{
				MoveFocused(-move);

				return;
			}

			if (((int)pos.X) == 1)
			{
				camera.Position.X -= 24;
			} else if (((int)pos.X) == -1)
			{
				camera.Position.X += 24;
			}

			if (((int)pos.Y) == 1)
			{
				camera.Position.Y -= 24;
			} else if (((int)pos.Y) == -1)
			{
				camera.Position.Y += 24;
			}
			FocusedPlayer.Position = presm_pos;
		}
		public void AddChunk(Chunk c)
		{
			gmap[c.ChunkPosition] = c;
		}

		public Chunk GenerateChunk(ChunkTypes type, Vector2 chunk_position)
		{
			Chunk target = new Chunk(chunk_position);
			if (type == ChunkTypes.Temperate)
			{
				target.Fill(tiles["dirt"]);
			}
			return target;
		}

		public void Render(SpriteBatch sb)
		{
			int w = FocusedPlayer.EntityTexture.Width;
			int h = FocusedPlayer.EntityTexture.Height;
			foreach (KeyValuePair<Vector2, Chunk> entry in gmap)
			{
				entry.Value.Render(sb, camera.Position);
			}
			sb.Draw(FocusedPlayer.EntityTexture, new Vector2(FocusedPlayer.CurrentChunk.ChunkPosition.X + FocusedPlayer.Position.X * w * GridItem.TextureScalar, FocusedPlayer.CurrentChunk.ChunkPosition.Y + FocusedPlayer.Position.Y * h * GridItem.TextureScalar) + camera.Position,
						null, Color.White, 0f, new Vector2(0, 0), GridItem.TextureScalar, SpriteEffects.None, 1f);
		}

		
	}
}
