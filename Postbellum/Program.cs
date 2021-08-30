using System;

namespace Postbellum
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new PostbellumGame(true))
				game.Run();
		}
	}
}
