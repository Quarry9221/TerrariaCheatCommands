using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class ClearItemsCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "clearitems";

		public override string Usage => "/clearitems";

		public override string Description => "Видаляє всі викинуті предмети на землі (корисно для зменшення лагів)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			int clearedCount = 0;

			// Проходимось по всіх існуючих предметах в ігровому світі (ліміт зазвичай 400)
			for (int i = 0; i < Main.maxItems; i++)
			{
				Item item = Main.item[i];
				if (item != null && item.active)
				{
					item.active = false; // Вимикаємо предмет, після чого гра його очистить
					clearedCount++;

					// Якщо граємо в мультиплеєрі, кажемо всім іншим клієнтам, що предмет зник
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i);
					}
				}
			}

			caller.Reply($"Світ успішно очищено! З землі видалено {clearedCount} предметів.");
		}
	}
}
