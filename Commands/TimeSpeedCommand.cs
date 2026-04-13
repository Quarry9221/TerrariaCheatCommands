using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class TimeSpeedCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "timespeed";

		public override string Usage => "/timespeed <multiplier>";

		public override string Description => "Змінює швидкість плину ігрового часу (хмар, сонця, росту рослин)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано коефіцієнт швидкості. Використання: /timespeed <множник>");
				return;
			}

			// Парсимо введене дробове число
			if (double.TryParse(args[0], out double multiplier))
			{
				if (multiplier < 0)
				{
					caller.Reply("Помилка: Швидкість часу не може бути від'ємною (гра не підтримує рух часу назад без багів).");
					return;
				}

				// Присвоюємо нову швидкість нашій системі світу
				CheatSystem.TimeMultiplier = multiplier;
				caller.Reply($"Швидкість плину часу встановлена на: x{multiplier}");
			}
			else
			{
				caller.Reply("Помилка: Множник має бути числом (наприклад: 5 або 0.5).");
			}
		}
	}
}
