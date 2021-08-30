using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postbellum
{
	class AnimatedTexture
	{
		private float Interval;
		private Texture2D[] Frames;
		private int FrameIndex = 0;
		public AnimatedTexture(Texture2D[] frames, float interval)
		{
			Frames = frames;
			Interval = interval;
		}

		private void LoopFrameIndex()
		{
			int l = Frames.Length;
			if (FrameIndex + 1 > l)
			{
				FrameIndex = 0;
			}
			else
			{
				FrameIndex++;
			}
		}

		public Texture2D GetTexture(GameTime gt)
		{
			if (gt.ElapsedGameTime.TotalSeconds % Interval == 0 && gt.ElapsedGameTime.TotalSeconds != 1)
			{
				LoopFrameIndex(); 
			}
			return Frames[FrameIndex];
		}
	}
}
