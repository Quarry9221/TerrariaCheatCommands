using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Для Vector2, якщо знадобиться

namespace terrariacheat.Commands
{
	public class SpawnCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "spawn";

		public override string Usage => "/spawn <type> [count] [xOffset] [yOffset]";

		public override string Description => "Спавнить NPC. Можна вказати кількість та відступ від гравця в пікселях.";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано Data ID. Використання: /spawn <type> [count] [xOffset] [yOffset]");
				return;
			}

			if (!int.TryParse(args[0], out int typeId))
			{
				caller.Reply("Помилка: Data ID повинно бути числом.");
				return;
			}

			// Парсимо кількість, якщо не вказано — 1
			int count = 1;
			if (args.Length >= 2 && !int.TryParse(args[1], out count))
			{
				caller.Reply("Помилка: Кількість повинна бути числом.");
				return;
			}

			// Парсимо відступи. За замовчуванням: X = 0, Y = -50 (трохи над гравцем)
			int xOffset = 0;
			int yOffset = -50;
			if (args.Length >= 3) int.TryParse(args[2], out xOffset);
			if (args.Length >= 4) int.TryParse(args[3], out yOffset);

			int spawnCount = 0;

			for (int i = 0; i < count; i++)
			{
				// Якщо спавнимо багато за раз — додаємо невеличкий розкид, щоб вони не зілипалися в одного
				int randomSpreadX = (count > 1) ? Main.rand.Next(-30, 30) : 0;
				int randomSpreadY = (count > 1) ? Main.rand.Next(-30, 30) : 0;

				int spawnX = (int)caller.Player.Center.X + xOffset + randomSpreadX;
				int spawnY = (int)caller.Player.Center.Y + yOffset + randomSpreadY;

				int npcIndex = NPC.NewNPC(
					caller.Player.GetSource_FromThis(), 
					spawnX, 
					spawnY, 
					typeId
				);

				if (npcIndex < Main.maxNPCs)
				{
					spawnCount++;
				}
			}

			if (spawnCount > 0)
			{
				caller.Reply($"Успішно заспавлено {spawnCount} шт. NPC (Data ID: {typeId}).");
			}
			else
			{
				caller.Reply($"Помилка: Не вдалося заспавнити NPC (ID {typeId}).");
			}
		}
	}
}
