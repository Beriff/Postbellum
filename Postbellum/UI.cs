﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class UI
	{
		public Action<SpriteBatch, GameGrid, GraphicsDeviceManager> Render;
		public Action<Actions, GameGrid> PassAction;
		public bool Active = true;

		public UI(Action<SpriteBatch, GameGrid, GraphicsDeviceManager> render, Action<Actions, GameGrid> pass)
		{
			Render = render;
			PassAction = pass;
		}

		public static UI MenuUI = new UI((SpriteBatch sb, GameGrid gg, GraphicsDeviceManager gdm) => {
			int half_h = (int)(gdm.PreferredBackBufferHeight * .5);
			int half_w = (int)(gdm.PreferredBackBufferWidth * .5);
			Texture2D rect = new Texture2D(gdm.GraphicsDevice, half_w, half_h);

			Color[] data = new Color[half_w * half_h];
			for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
			rect.SetData(data);

			sb.Draw(rect, new Vector2(half_w / 2, half_h / 2), Color.Black);
			sb.DrawString(gg.GameFont, "Game Menu", new Vector2(half_w / 2, half_h / 2), Color.White);

			string[] selections = new string[] { "Options", "Exit" };
			for(int i = 0; i < selections.Length; i++)
			{
				string sel = gg.global_index == i ? ">>" : "";
				sb.DrawString(gg.GameFont, sel + selections[i], new Vector2(half_w / 2, half_h / 2 + (i+2)*15), Color.White);
			}
		}, (Actions a, GameGrid gg) => { 
			if (a == Actions.MoveDown)
			{
				if (gg.global_index + 1 >= 2)
				{
					gg.global_index = 0;
				} else
				{
					gg.global_index++;
				}
			} else if (a == Actions.MoveUp)
			{
				if (gg.global_index - 1 < 0)
				{
					gg.global_index = 1;
				}
				else
				{
					gg.global_index--;
				}
			} else if (a == Actions.Enter)
			{
				if (gg.global_index == 1)
				{
					gg.QueueExit = true;
				}
			}
		});
	}
}
