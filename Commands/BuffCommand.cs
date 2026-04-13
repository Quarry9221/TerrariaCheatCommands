using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class BuffCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "buff";

		public override string Usage => "/buff <buffID> [timeInSeconds]";

		public override string Description => "Накладає баф або дебаф (з таблиці ID) на вашого персонажа";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано ID бафа. Використання: /buff <buffID> [timeInSeconds]");
				return;
			}

			if (!int.TryParse(args[0], out int buffId))
			{
				caller.Reply("Помилка: ID бафа має бути числом.");
				return;
			}

			// Час за замовчуванням - 60 секунд
			int timeInSeconds = 60;
			if (args.Length >= 2 && !int.TryParse(args[1], out timeInSeconds))
			{
				caller.Reply("Помилка: Час має бути числом.");
				return;
			}

			// В Terraria 1 секунда = 60 тікам
			int timeInTicks = timeInSeconds * 60;

			// Накладаємо баф. 
			caller.Player.AddBuff(buffId, timeInTicks);
			
			caller.Reply($"Успішно накладено ефект (ID: {buffId}) на {timeInSeconds} секунд.");
		}
	}
}
