using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace terrariacheat.Commands
{
	public class TeleportCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "tp"; // Дамо коротку назву команди для зручності

		public override string Usage => "/tp <x> <y> або /tp spawn";

		public override string Description => "Телепортує гравця за координатами (у блоках) або на спавн";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано координати. Використання: /tp <x> <y> або /tp spawn");
				return;
			}

			// Якщо гравець написав /tp spawn
			if (args.Length == 1 && args[0].ToLower() == "spawn")
			{
				// Координати спавну гравця зберігаються у блоках, множимо на 16 (1 блок = 16 пікселів)
				Vector2 spawnPos = new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16);
				caller.Player.Teleport(spawnPos, 1, 0); // 1 = стиль телепорту (магічний синій пил)
				caller.Reply("Вас було телепортовано на точку спавну.");
				return;
			}

			// Якщо гравець ввів дві координати
			if (args.Length >= 2)
			{
				if (!int.TryParse(args[0], out int x) || !int.TryParse(args[1], out int y))
				{
					caller.Reply("Помилка: Координати мають бути числами.");
					return;
				}

				// Множимо X та Y на 16, щоб гравець міг вводити зручні координати у блоках
				Vector2 targetPos = new Vector2(x * 16, y * 16);
				caller.Player.Teleport(targetPos, 1, 0);
				caller.Reply($"Вас було телепортовано за координатами X: {x}, Y: {y} (блоки).");
			}
		}
	}
}
