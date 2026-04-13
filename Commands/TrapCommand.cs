using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class TrapCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "trap";
		public override string Usage => "/trap <lava|water|honey|cobweb>";
		public override string Description => "Генерує обрану смертельну пастку прямо над вашою головою!";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Використання: /trap <lava|water|honey|cobweb>");
				return;
			}

			// Координати тайлу, які знаходяться на 3 блоки вище голови гравця
			int x = (int)(caller.Player.Center.X / 16);
			int y = (int)(caller.Player.Center.Y / 16) - 3;

			string trapType = args[0].ToLower();

			switch (trapType)
			{
				case "lava":
				{
					Tile tile = Main.tile[x, y];
					tile.LiquidType = LiquidID.Lava;
					tile.LiquidAmount = 255;
					WorldGen.SquareTileFrame(x, y, false);
					caller.Reply("Обережно, гаряче! Лавова пастка активована.");
					break;
				}
				case "water":
				{
					Tile tile = Main.tile[x, y];
					tile.LiquidType = LiquidID.Water;
					tile.LiquidAmount = 255;
					WorldGen.SquareTileFrame(x, y, false);
					caller.Reply("Водяна пастка відкрита.");
					break;
				}
				case "honey":
				{
					Tile tile = Main.tile[x, y];
					tile.LiquidType = LiquidID.Honey;
					tile.LiquidAmount = 255;
					WorldGen.SquareTileFrame(x, y, false);
					caller.Reply("Липка медова пастка готова.");
					break;
				}
				case "cobweb":
					// Для павутиння ми розміщуємо блоки 3х3
					for (int i = x - 1; i <= x + 1; i++) {
						for (int j = y; j <= y + 2; j++) {
							WorldGen.PlaceTile(i, j, TileID.Cobweb, true, true);
						}
					}
					caller.Reply("Павутиння накинуто! Ви застрягли.");
					break;
				default:
					caller.Reply("Невідомий тип. Оберіть lava, water, honey або cobweb.");
					return;
			}

			// Оновлюємо квадрат тайлів для мультиплеєру
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, x, y, 3);
			}
		}
	}
}
