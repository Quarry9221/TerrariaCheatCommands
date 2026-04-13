using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace terrariacheat.Commands
{
	public class RandomTPCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "randomtp";
		public override string Usage => "/randomtp";
		public override string Description => "Телепортує вас у випадкову безпечну точку світу";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			Player player = caller.Player;
			int maxTries = 100;

			// Спробуємо знайти безпечний блок (не всередині стіни)
			for (int i = 0; i < maxTries; i++)
			{
				// Вибираємо випадковий X (уникаємо самих країв карти)
				int randX = Main.rand.Next(100, Main.maxTilesX - 100);
				
				// Вибираємо Y від поверхні до середини підземелля
				int minHeigth = (int)Main.worldSurface;
				int randY = Main.rand.Next(100, Main.maxTilesY - 200);

				// Знаходимо першу вільну "безпечну" комірку
				if (Collision.SolidCollision(new Vector2(randX * 16, randY * 16), player.width, player.height) == false)
				{
					// Перевіряємо, чи є під нами твердий блок, щоб не телепортнутися в пустоту високо в небі
					bool foundGround = false;
					for (int ySearch = randY; ySearch < randY + 50; ySearch++)
					{
						if (WorldGen.SolidTile(randX, ySearch))
						{
							randY = ySearch - 2; // Стаємо на блок
							foundGround = true;
							break;
						}
					}

					if (foundGround)
					{
						Vector2 targetPos = new Vector2(randX * 16, randY * 16);
						player.Teleport(targetPos, 1, 0); // 1 = магічний синій стиль
						caller.Reply($"Миттєвий випадковий телепорт! Координати: {randX}, {randY}");
						return;
					}
				}
			}

			// Якщо за 100 спроб не вдалося знайти ідеальну безпечну точку (що малоймовірно), 
			// ми просто кидаємо гравця у випадкову точку високо в небі.
			int fallbackX = Main.rand.Next(100, Main.maxTilesX - 100);
			int fallbackY = 100;
			player.Teleport(new Vector2(fallbackX * 16, fallbackY * 16), 1, 0);
			caller.Reply($"Екстремальний телепорт у небо! X: {fallbackX}");
		}
	}
}
