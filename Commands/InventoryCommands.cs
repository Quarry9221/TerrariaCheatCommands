using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	// Команда для очищення інвентарю
	public class ClearInvCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "clearinv";
		public override string Usage => "/clearinv";
		public override string Description => "Очищає основний рюкзак (зберігає вашу броню та аксесуари)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Основні слоти рюкзака — це індекси від 0 до 49 (5 рядів по 10 слотів)
			for (int i = 0; i < 50; i++)
			{
				// Безпечно перетворюємо предмет на порожнечу (повітря)
				caller.Player.inventory[i].TurnToAir();
			}
			caller.Reply("Ваш рюкзак було повністю очищено! Броня та екіпірування залишились на місці.");
		}
	}

	// Команда для перемішування інвентарю (суто Crowd Control ефект)
	public class ShuffleInvCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "shuffleinv";
		public override string Usage => "/shuffleinv";
		public override string Description => "Хаотично перемішує всі предмети у вашому рюкзаку";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Для перемішування використовуємо класичний алгоритм Фішера-Єтса 
			for (int i = 49; i > 0; i--)
			{
				int j = Main.rand.Next(0, i + 1);
				
				// Обмінюємо місцями
				Item temp = caller.Player.inventory[i].Clone();
				caller.Player.inventory[i] = caller.Player.inventory[j].Clone();
				caller.Player.inventory[j] = temp;
			}
			
			caller.Reply("Бац! Всі ваші речі в рюкзаку тепер перемішані. Успіхів з їх сортуванням!");
		}
	}
}
