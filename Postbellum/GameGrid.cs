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
		public UI ActiveUI;
		public SpriteFont GameFont;
		public int global_index = 0;
		public bool IsActiveUI = true;
		public bool QueueExit = false;
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
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.MoveDown, this);
						break;
					}
					MoveFocused(new Vector2(0, 1));
					break;
				case Actions.MoveUp:
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.MoveUp, this);
						break;
					}
					MoveFocused(new Vector2(0, -1));
					break;
				case Actions.MoveLeft:
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.MoveLeft, this);
						break;
					} 
					else
					{
						MoveFocused(new Vector2(-1, 0));
					}
					
					break;
				case Actions.MoveRight:
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.MoveRight, this);
						break;
					}
					else
					{
						MoveFocused(new Vector2(1, 0));
					}
					break;
				case Actions.Enter:
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.Enter, this);
						break;
					}
					break;
				case Actions.Escape:
					if (IsActiveUI)
					{
						ActiveUI.PassAction(Actions.Escape, this);
						break;
					} else
					{
						IsActiveUI = true;
						UI.MenuUI.Active = true;
						ActiveUI = UI.MenuUI;
						break;
					}
					break;
			}
		}
		public void MoveFocused(Vector2 pos)
		{
			Vector2 presm_pos = FocusedPlayer.Position + pos;
			Vector2 move = Vector2.Zero;

			if (presm_pos.X > Chunk.ChunkSizeX - 1)
			{	
				presm_pos.X = 0;
				FocusedPlayer.CurrentChunk.ChunkPosition += new Vector2(1, 0);
				move += new Vector2(1, 0);
			} else if (presm_pos.X < 0)
			{
				presm_pos.X = Chunk.ChunkSizeX - 1;
				FocusedPlayer.CurrentChunk.ChunkPosition -= new Vector2(1, 0);
				move -= new Vector2(1, 0);
			}

			if (presm_pos.Y > Chunk.ChunkSizeY - 1)
			{
				presm_pos.Y = 0;
				FocusedPlayer.CurrentChunk.ChunkPosition -= new Vector2(0, 1);
				move -= new Vector2(0, 1);
			} else if (presm_pos.Y < 0)
			{
				presm_pos.Y = Chunk.ChunkSizeY - 1;
				FocusedPlayer.CurrentChunk.ChunkPosition += new Vector2(0, 1);
				move += new Vector2(0, 1);
			}

			FocusedPlayer.Position = presm_pos;
			TryGenerateChunk(FocusedPlayer.CurrentChunk.ChunkPosition);

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
			
		}
		public void AddChunk(Chunk c)
		{
			gmap[c.ChunkPosition] = c;
		}

		public GridItem GetAtGlobalCoords(int x, int y)
		{
			Vector2 chunk_coords = new Vector2(
				(float)Math.Floor((double)(x / Chunk.ChunkSizeX)), 
				(float)Math.Floor((double)(y / Chunk.ChunkSizeY))
				);

			Vector2 local_coords = new Vector2(x % Chunk.ChunkSizeX, y % Chunk.ChunkSizeY);

			return gmap[chunk_coords].GetAt(local_coords);
		}

		public void TryGenerateChunk(Vector2 chunk_coords)
		{
			if (!gmap.ContainsKey(chunk_coords))
			{
				gmap[chunk_coords] = GenerateChunk(ChunkTypes.Temperate, chunk_coords);
			}
		}

		public GridItem GetAtGlobalCoords(Vector2 vec)
		{
			return GetAtGlobalCoords((int)vec.X, (int)vec.Y);
		}

		public Chunk GenerateChunk(ChunkTypes type, Vector2 chunk_position)
		{
			Chunk target = new Chunk(chunk_position);
			if (type == ChunkTypes.Temperate)
			{
				target.Fill(tiles["dirt"]);
				for (int x = 0; x <= Chunk.ChunkSizeX - 1; x++)
				{
					for (int y = 0; y <= Chunk.ChunkSizeY - 1; y++)
					{
						target.grid[x, y] = new GridItem[] { tiles["dirt"], tiles["grass"] }[new Random().Next(0, 2)];
					}
				}
			}
			return target;
		}

		public void Render(SpriteBatch sb, GraphicsDeviceManager gdm)
		{
			int w = FocusedPlayer.EntityTexture.Width;
			int h = FocusedPlayer.EntityTexture.Height;
			foreach (KeyValuePair<Vector2, Chunk> entry in gmap)
			{
				entry.Value.Render(sb, camera.Position);
			}
			sb.Draw(FocusedPlayer.EntityTexture, new Vector2(FocusedPlayer.CurrentChunk.ChunkPosition.X + FocusedPlayer.Position.X * w * GridItem.TextureScalar, FocusedPlayer.CurrentChunk.ChunkPosition.Y + FocusedPlayer.Position.Y * h * GridItem.TextureScalar) + camera.Position,
						null, Color.White, 0f, new Vector2(0, 0), GridItem.TextureScalar, SpriteEffects.None, 1f);
			if (ActiveUI.Active && IsActiveUI)
			{
				ActiveUI.Render(sb, this, gdm);
			}
		}

		
	}
}
