using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Postbellum
{
	public class GridItem
	{
		public Texture2D ItemTexture { get; set; }
		public string Name { get; set; }
		public Vector2 GridPosition { get; set; }
		public const float TextureScalar = 1.5f;
		public bool Collision = false;

		public int GlobalUIIndex = 0;

		public GridItem(Texture2D texture, string name)
		{
			GridPosition = Vector2.One;
			Name = name;
			ItemTexture = texture;
		}
		public GridItem(Texture2D texture)
		{
			GridPosition = Vector2.One;
			Name = texture.Name;
			ItemTexture = texture;
		}
		public GridItem(Texture2D texture, string name, Vector2 pos)
		{
			GridPosition = pos;
			Name = name;
			ItemTexture = texture;
		}
	}
}
