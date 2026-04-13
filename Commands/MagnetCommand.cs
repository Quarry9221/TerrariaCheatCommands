using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class MagnetCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "magnet";
		public override string Usage => "/magnet";
		public override string Description => "Миттєво притягує абсолютно всі предмети та лут з карти до ваших ніг";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			int count = 0;

			// Проходимо по масиву всіх предметів, що лежать на землі
			for (int i = 0; i < Main.maxItems; i++)
			{
				Item item = Main.item[i];
				
				// Якщо предмет існує і це не порожнеча
				if (item.active && item.type > 0)
				{
					// Телепортуємо предмет до центру гравця
					item.position = caller.Player.Center;
					count++;

					// Якщо граємо в мультиплеєрі, кажемо всім іншим клієнтам, що предмет змінив позицію
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.SyncItem, -1, -1, null, i);
					}
				}
			}

			if (count > 0)
			{
				caller.Reply($"🧲 Магніт спрацював! Зі всього світу до вас прилетіло {count} предметів.");
			}
			else
			{
				caller.Reply("На карті зараз немає жодного покинутого луту.");
			}
		}
	}
}
