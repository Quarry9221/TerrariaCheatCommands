using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class SetBlockCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "setblock";
		public override string Usage => "/setblock <tileID> [xOffset] [yOffset]";
		public override string Description => "Створює/розміщує блок (тайл) за вказаним ID";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0 || !ushort.TryParse(args[0], out ushort tileId))
			{
				caller.Reply("Помилка: Використовуйте /setblock <tileID> [xOffset] [yOffset]");
				return;
			}

			// За замовчуванням координати будуть під ногами гравця
			int xOffset = 0;
			int yOffset = 2; 

			if (args.Length >= 2) int.TryParse(args[1], out xOffset);
			if (args.Length >= 3) int.TryParse(args[2], out yOffset);

			// Переводимо піксельні координати гравця у координати блоків та додаємо зсув
			int x = (int)(caller.Player.Center.X / 16f) + xOffset;
			int y = (int)(caller.Player.Center.Y / 16f) + yOffset;

			// Знищуємо старий блок, який там стояв (якщо був) і не видаємо за нього лут
			WorldGen.KillTile(x, y, noItem: true);
			
			// Форсовано ставимо наш блок
			bool success = WorldGen.PlaceTile(x, y, tileId, mute: false, forced: true);

			if (success)
			{
				// Оновлюємо картинку екрану для мультиплеєра
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
				caller.Reply($"Блок (Tile ID: {tileId}) успішно заспавнено!");
			}
			else
			{
				caller.Reply($"Помилка: Не вдалося розмістити блок {tileId}. Перевірте чи валідний ID.");
			}
		}
	}
}
