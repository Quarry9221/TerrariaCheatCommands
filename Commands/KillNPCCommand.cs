using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class KillNPCCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "killNPC";

		public override string Usage => "/killNPC <type>";

		public override string Description => "Вбиває всіх NPC вказаного типу (за Data ID з Wiki)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Перевіряємо чи передано ID
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Ви повинні вказати Data ID NPC. Використання: /killNPC <type>");
				return;
			}

			// Пробуємо перетворити аргумент на число (int)
			if (!int.TryParse(args[0], out int typeId))
			{
				caller.Reply("Помилка: ID повинно бути числом.");
				return;
			}

			int killCount = 0;
			// Перебираємо всіх існуючих NPC у світі (ліміт гри 200)
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC targetNPC = Main.npc[i];
				// Перевіряємо чи NPC живий і його тип збігається з переданим Data ID
				if (targetNPC != null && targetNPC.active && targetNPC.type == typeId)
				{
					// Вбиваємо
					targetNPC.SimpleStrikeNPC(99999, 0, false, 0, null, true, 0, false);
					killCount++;
				}
			}

			if (killCount > 0)
			{
				caller.Reply($"Успішно вбито {killCount} NPC з ID {typeId}.");
			}
			else
			{
				caller.Reply($"Помилка: NPC з Data ID {typeId} не знайдено на карті.");
			}
		}
	}
}
