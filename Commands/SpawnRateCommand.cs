using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class SpawnRateCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "spawnrate";

		public override string Usage => "/spawnrate <multiplier>";

		public override string Description => "Змінює швидкість та кількість природного спавну ворогів";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано множник. Використання: /spawnrate <множник>");
				return;
			}

			if (double.TryParse(args[0], out double multiplier))
			{
				if (multiplier < 0)
				{
					caller.Reply("Помилка: Спавн-рейт не може бути від'ємним.");
					return;
				}

				// Передаємо множник у наш GlobalNPC
				CheatGlobalNPC.SpawnRateMultiplier = multiplier;

				if (multiplier == 0)
				{
					caller.Reply("Спавн монстрів ПОВНІСТЮ ВИМКНЕНО!");
				}
				else if (multiplier == 1)
				{
					caller.Reply("Швидкість спавну монстрів повернуто до Ванільної (x1).");
				}
				else
				{
					caller.Reply($"Мобів тепер з'являється у {multiplier} разів більше/швидше!");
				}
			}
			else
			{
				caller.Reply("Помилка: Множник має бути числом (наприклад: 30 або 0.5).");
			}
		}
	}
}
