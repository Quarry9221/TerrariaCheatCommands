using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class KillPlayerCommand : ModCommand
	{
		// Визначаємо тип команди - працює тільки в чаті
		public override CommandType Type => CommandType.Chat;

		// Сама команда, яка буде вводитись в чаті (без /)
		public override string Command => "killplayer";

		// Опис використання команди
		public override string Usage => "/killplayer";

		// Опис команди для допомоги
		public override string Description => "Вбиває вашого персонажа";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Причина смерті (від звичайних "містичних обставин" до кастомної)
			PlayerDeathReason reason = PlayerDeathReason.ByCustomReason(Terraria.Localization.NetworkText.FromLiteral(caller.Player.name + " вирішив завершити все."));

			// Вбиваємо гравця, завдавши велику кількість шкоди (наприклад 9999)
			caller.Player.KillMe(reason, 9999.0, 0, false);
			
			// Відправляємо повідомлення про виконання в чат
			caller.Reply("Вас було вбито.");
		}
	}
}
