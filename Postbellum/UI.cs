using Microsoft.Xna.Framework;
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

		private static Texture2D GetRect(GraphicsDeviceManager gdm, int w, int h)
		{
			Texture2D rect = new Texture2D(gdm.GraphicsDevice, w, h);
			Color[] data = new Color[w * h];
			for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
			rect.SetData(data);

			return rect;
		}

		public UI(Action<SpriteBatch, GameGrid, GraphicsDeviceManager> render, Action<Actions, GameGrid> pass)
		{
			Render = render;
			PassAction = pass;
		}

		public static UI SettingsUI = new UI((SpriteBatch sb, GameGrid gg, GraphicsDeviceManager gdm) =>
		{
			int half_h = (int)(gdm.PreferredBackBufferHeight * .5);
			int half_w = (int)(gdm.PreferredBackBufferWidth * .5);
			Texture2D rect = UI.GetRect(gdm, half_w, half_h);

			sb.Draw(rect, new Vector2(half_w / 2, half_h / 2), Color.Black);
			sb.DrawString(gg.GameFont, "Settings", new Vector2(half_w - gg.GameFont.MeasureString("Settings").X / 2, half_h / 2), Color.White);
			string[] selections = new string[] { "  | Back", "  | Autosave", "  | Volume", "  | Keybindings" };
			string[] values = new string[] { "", PostbellumGame.autosave.ToString(), PostbellumGame.volume.ToString(), "" };

			for (int i = 0; i < selections.Length; i++)
			{
				sb.DrawString(gg.GameFont, selections[i], new Vector2(half_w / 2 + gg.GameFont.MeasureString(">>").X, half_h / 2 + (i + 2) * 15), Color.White);
				sb.DrawString(gg.GameFont, values[i], new Vector2(half_w, half_h / 2 + (i + 2) * 15), Color.White);
				if (i == gg.global_index)
					sb.DrawString(gg.GameFont, ">>", new Vector2(half_w / 2, half_h / 2 + (i + 2) * 15), Color.Yellow);
			}

		}, (Actions a, GameGrid gg) => {
			if (a == Actions.MoveDown)
			{
				if (gg.global_index + 1 >= 4)
				{
					gg.global_index = 0;
				}
				else
				{
					gg.global_index++;
				}
			}
			else if (a == Actions.MoveUp)
			{
				if (gg.global_index - 1 < 0)
				{
					gg.global_index = 3;
				}
				else
				{
					gg.global_index--;
				}
			}
			else if (a == Actions.Enter)
			{
				if (gg.global_index == 0)
				{
					gg.ActiveUI = MenuUI;
				}
				else if (gg.global_index == 2)
				{
					//uhhhh
				}
			}
			else if (a == Actions.MoveRight)
			{
				if (gg.global_index == 1)
				{
					PostbellumGame.autosave = !PostbellumGame.autosave;
				}
				else if (gg.global_index == 2 && gg.global_index <= 99)
				{
					PostbellumGame.volume++;
				}
			}
			else if (a == Actions.MoveLeft)
			{
				if (gg.global_index == 1)
				{
					PostbellumGame.autosave = !PostbellumGame.autosave;
				}
				else if (gg.global_index == 2 && gg.global_index > 0)
				{
					PostbellumGame.volume--;
				}
			}
			else if (a == Actions.Escape)
			{
				gg.ActiveUI = MenuUI;
			}
		});

		public static UI MenuUI = new UI((SpriteBatch sb, GameGrid gg, GraphicsDeviceManager gdm) => {
			int half_h = (int)(gdm.PreferredBackBufferHeight * .5);
			int half_w = (int)(gdm.PreferredBackBufferWidth * .5);
			Texture2D rect = UI.GetRect(gdm, half_w, half_h);

			sb.Draw(rect, new Vector2(half_w / 2, half_h / 2), Color.Black);
			sb.DrawString(gg.GameFont, "Game Menu", new Vector2(half_w - gg.GameFont.MeasureString("Game Menu").X / 2, half_h / 2), Color.White);

			string[] selections = new string[] { "  | Return", "  | Options", "  | Exit" };
			for(int i = 0; i < selections.Length; i++)
			{
				sb.DrawString(gg.GameFont, selections[i], new Vector2(half_w / 2 + gg.GameFont.MeasureString(">>").X, half_h / 2 + (i+2)*15), Color.White);
				if (i == gg.global_index)
					sb.DrawString(gg.GameFont, ">>", new Vector2(half_w / 2, half_h / 2 + (i + 2) * 15), Color.Yellow);
			}
		}, (Actions a, GameGrid gg) => { 
			if (a == Actions.MoveDown)
			{
				if (gg.global_index + 1 >= 3)
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
					gg.global_index = 2;
				}
				else
				{
					gg.global_index--;
				}
			} else if (a == Actions.Enter)
			{
				if (gg.global_index == 2)
				{
					gg.QueueExit = true;
				} else if (gg.global_index == 0)
				{
					gg.ActiveUI.Active = false;
					gg.IsActiveUI = false;
				}
				else if (gg.global_index == 1)
				{
					gg.ActiveUI = SettingsUI;
				}
			} else if (a == Actions.Escape)
			{
				gg.ActiveUI.Active = false;
				gg.IsActiveUI = false;
			}
		});
	}
}
