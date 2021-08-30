using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	enum CameraMode
	{
		Default,
		Flip
	}
	class Camera
	{
		public Vector2 Position = new Vector2(PostbellumGame.TileToPixels(16), PostbellumGame.TileToPixels(10));
		public Vector2 View = new Vector2(PostbellumGame.TileToPixels(32), PostbellumGame.TileToPixels(20));
		public CameraMode Mode = CameraMode.Default;

		public Camera(Vector2 pos, Vector2 view, CameraMode cm)
		{
			Position = new Vector2(PostbellumGame.TileToPixels((int)pos.X), PostbellumGame.TileToPixels((int)pos.Y));
			View = new Vector2(PostbellumGame.TileToPixels((int)view.X), PostbellumGame.TileToPixels((int)view.Y));
			Mode = cm;
		}
		public Camera()
		{

		}

		public override string ToString()
		{
			return string.Format("Camera[{0}x{1}]: <{2}, {3}>", View.X, View.Y, Position.X, Position.Y);
		}

		public bool Interpolate(Vector2 node)
		{
			if (Position.X == node.X && Position.Y == node.Y)
				return true;
			if (Position.X < node.X)
			{
				Position.X += 1;
			} else if (Position.X > node.X)
			{
				Position.X -= 1;
			}

			if (Position.Y < node.Y)
			{
				Position.X += 1;
			}
			else if (Position.Y > node.Y)
			{
				Position.Y -= 1;
			}


			return false;
		}
	}

}
