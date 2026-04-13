using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class GiveCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "give";

		public override string Usage => "/give <itemID> [count]";

		public override string Description => "Видає вам предмет за його Data ID (з необов'язковою кількістю)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Перевіряємо, чи вказано ID
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано ID предмета. Використання: /give <itemID> [count]");
				return;
			}

			// Парсимо ID предмета
			if (!int.TryParse(args[0], out int itemId))
			{
				caller.Reply("Помилка: ID предмета має бути числом.");
				return;
			}

			// Перевіряємо, чи існує такий ID предмета (базові предмети до ItemLoader.ItemCount)
			if (itemId <= 0 || itemId >= ItemLoader.ItemCount)
			{
				caller.Reply($"Помилка: Предмету з ID {itemId} не існує.");
				return;
			}

			// Парсимо кількість (якщо не вказано - видаємо 1 шт.)
			int count = 1;
			if (args.Length >= 2 && !int.TryParse(args[1], out count))
			{
				caller.Reply("Помилка: Кількість має бути числом.");
				return;
			}

			// Видаємо предмет гравцеві. QuickSpawnItem кине предмет прямо в гравця,
			// що гарантує, що він його відразу підбере (якщо є місце в інвентарі).
			caller.Player.QuickSpawnItem(caller.Player.GetSource_FromThis(), itemId, count);

			// Отримуємо локалізовану назву предмета, щоб повідомити гравцю
			string itemName = Lang.GetItemNameValue(itemId);
			caller.Reply($"Успішно видано: {itemName} x{count} (ID: {itemId})");
		}
	}
}
