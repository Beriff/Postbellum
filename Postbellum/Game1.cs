using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Postbellum
{
	public class PostbellumGame : Game
	{
		public const int DefTextureSize = 16;
		public const string GameVersion = "v0.3.0a";
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		Chunk test_chunk = new Chunk(new Vector2(-50, -50));
		GameGrid gg;
		SpriteFont defaultfont;
		bool Debug;
		Actions lastaction;
		Dictionary<Keys, bool> keypresses = new Dictionary<Keys, bool>();
		public static int TileToPixels(int tiles) // TODO: make function inline
		{
			return tiles * DefTextureSize;
		}
		public PostbellumGame(bool debug)
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			Debug = debug;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			keypresses[Keys.D] = true;
			keypresses[Keys.A] = true;
			keypresses[Keys.W] = true;
			keypresses[Keys.S] = true;
			keypresses[Keys.Enter] = true;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Dictionary<string, GridItem> tiles = new Dictionary<string, GridItem>();
			Dictionary<string, Texture2D> entities = new Dictionary<string, Texture2D>();
			tiles["dirt"] = new GridItem(ContentLoader.Load<Texture2D>(this, "dirt"), "dirt");
			tiles["grass"] = new GridItem(ContentLoader.Load<Texture2D>(this, "grass"), "grass");
			tiles["tree"] = new GridItem(ContentLoader.Load<Texture2D>(this, "tree"), "tree");

			defaultfont = ContentLoader.Load<SpriteFont>(this, "defaultfont");

			entities["player_f"] = ContentLoader.Load<Texture2D>(this, "player_front");
			entities["player_s"] = ContentLoader.Load<Texture2D>(this, "player_side");
			entities["player_b"] = ContentLoader.Load<Texture2D>(this, "player_back");

			test_chunk.Fill(tiles["dirt"]);
			test_chunk.Entities.Add(new Entity(tiles["tree"].ItemTexture, 10, new Vector2(5, 5), new Vector2(16, 16), new Vector2(13, 38)));
			Player main_player = new Player(entities["player_f"], entities["player_s"], entities["player_b"], new Vector2(16, 16), test_chunk);
			Dictionary<Actions, Texture2D> player_states = new Dictionary<Actions, Texture2D>();
			player_states[Actions.MoveDown] = entities["player_f"];
			player_states[Actions.MoveUp] = entities["player_b"];
			player_states[Actions.MoveRight] = entities["player_s"];
			player_states[Actions.MoveLeft] = entities["player_s"];
			main_player.States = player_states;
			gg = new GameGrid(new Camera(Vector2.Zero, Vector2.Zero, CameraMode.Default), 
				tiles, 
				main_player);
			gg.AddChunk(test_chunk);
			gg.GameFont = defaultfont;
			gg.ActiveUI = UI.MenuUI;

			// TODO: use this.Content to load your game content here
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || gg.QueueExit)
				Exit();

			gg.FocusedPlayer.Tick(gameTime);

			// TODO: Add your update logic here
			if (Keyboard.GetState().IsKeyDown(Keys.D) && keypresses[Keys.D])
			{
				lastaction = Actions.MoveRight;
				gg.ReceiveAction(Actions.MoveRight);
				keypresses[Keys.D] = false;
			} else if (Keyboard.GetState().IsKeyDown(Keys.A) && keypresses[Keys.A])
			{
				lastaction = Actions.MoveLeft;
				gg.ReceiveAction(Actions.MoveLeft);
				keypresses[Keys.A] = false;
			} else if (Keyboard.GetState().IsKeyDown(Keys.W) && keypresses[Keys.W])
			{
				lastaction = Actions.MoveUp;
				gg.ReceiveAction(Actions.MoveUp);
				keypresses[Keys.W] = false;
			} else if (Keyboard.GetState().IsKeyDown(Keys.S) && keypresses[Keys.S])
			{
				lastaction = Actions.MoveDown;
				gg.ReceiveAction(Actions.MoveDown);
				keypresses[Keys.S] = false;
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && keypresses[Keys.Enter])
			{
				lastaction = Actions.MoveDown;
				gg.ReceiveAction(Actions.Enter);
				keypresses[Keys.Enter] = false;
			}

			if (Keyboard.GetState().IsKeyUp(Keys.D))
			{
				keypresses[Keys.D] = true;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.A))
			{
				keypresses[Keys.A] = true;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.S))
			{
				keypresses[Keys.S] = true;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.W))
			{
				keypresses[Keys.W] = true;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Enter))
			{
				keypresses[Keys.Enter] = true;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// TODO: Add your drawing code here
			//gg.camera.Position = -gg.FocusedPlayer.Position * 32;// * gg.FocusedPlayer.CurrentChunk.ChunkPosition;

			_spriteBatch.Begin();
			gg.Render(_spriteBatch, _graphics);

			if (Debug)
			{
				_spriteBatch.DrawString(defaultfont, string.Format("Build {0}", GameVersion), Vector2.One, Color.White);
				_spriteBatch.DrawString(defaultfont, gg.camera.ToString(), new Vector2(0, 15), Color.White);
				_spriteBatch.DrawString(defaultfont, string.Format("FPS {0}", Math.Round((1 / gameTime.ElapsedGameTime.TotalSeconds))), new Vector2(0, 30), Color.White);
				_spriteBatch.DrawString(defaultfont, string.Format("Hunger {0} {1}", gg.FocusedPlayer.Hunger, Math.Round(gameTime.TotalGameTime.TotalMinutes, 5)), new Vector2(0, 45), Color.White);
			}
				
			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
