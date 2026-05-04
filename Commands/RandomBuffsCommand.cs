using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace terrariacheat.Commands
{
	public class BuffsCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "buffs";
		public override string Usage => "/buffs <count>";
		public override string Description => "Видає випадкову кількість позитивних бафів на 5 хвилин";

		// Масив усіх позитивних бафів
		private readonly int[] positiveBuffs = new int[] 
		{
			112, 16, 13, 107, 343, 106, 123, 111, 114, 8, 121, 109, 4, 18, 105, 17, 116, 10, 5, 113, 257, 7, 6, 104, 12, 1, 206, 115, 2, 11, 122, 9, 110, 3, 14, 108, 376, 124, 15, 117, 26, 207, 78, 73, 74, 75, 76, 77, 79, 71
		};

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0 || !int.TryParse(args[0], out int count) || count <= 0)
			{
				caller.Reply("Помилка: вкажіть додатне число бафів (наприклад: /buffs 5)");
				return;
			}

			if (count > positiveBuffs.Length) count = positiveBuffs.Length;

            // Перемішуємо масив і беремо перші [count] елементів 
			var chosenBuffs = positiveBuffs.OrderBy(x => Main.rand.Next()).Take(count).ToList();

			foreach (var buffId in chosenBuffs)
			{
				caller.Player.AddBuff(buffId, 3600 * 5); // 3600 тиків = 1 хвилина. Робимо на 5 хвилин.
			}

			caller.Reply($"Ви отримали {count} випадкових позитивних бафів!");
		}
	}

	public class DebuffsCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "debuffs";
		public override string Usage => "/debuffs <count>";
		public override string Description => "Застосовує випадкову кількість негативних дебафів до вас на 1 хвилину";

		// Масив усіх негативних бафів та проклять
		private readonly int[] negativeBuffs = new int[] 
		{
			30, 20, 24, 70, 22, 80, 35, 23, 31, 32, 197, 33, 36, 195, 196, 37, 38, 39, 69, 44, 46, 47, 149, 156, 164, 163, 144, 148, 145, 94, 21, 88, 68, 67, 25, 119, 120, 86, 350, 194, 199, 332, 333, 334, 321, 353, 72, 204, 103, 137, 320, 153, 203, 169, 189, 183, 186, 344
		};

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0 || !int.TryParse(args[0], out int count) || count <= 0)
			{
				caller.Reply("Помилка: введіть додатне число дебафів (наприклад: /debuffs 5)");
				return;
			}

			if (count > negativeBuffs.Length) count = negativeBuffs.Length;

            // Перемішуємо масив випадковим чином
			var chosenDebuffs = negativeBuffs.OrderBy(x => Main.rand.Next()).Take(count).ToList();

			foreach (var buffId in chosenDebuffs)
			{
				caller.Player.AddBuff(buffId, 3600 * 1); // Дебафи робимо на 1 хвилину (щоб не страждати вічно)
			}

			caller.Reply($"Ой лишенько! Ви були прокляті {count} випадковими дебафами!");
		}
	}
}
