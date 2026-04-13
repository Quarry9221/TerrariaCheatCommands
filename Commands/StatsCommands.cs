using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class MaxHealthCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "maxhealth";
		public override string Usage => "/maxhealth <+|->";
		public override string Description => "Додає або забирає 1 серце (20 ХП) від вашого максимального здоров'я";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Використовуйте /maxhealth + (щоб додати серце) або /maxhealth - (щоб забрати).");
				return;
			}

			CheatPlayer cp = caller.Player.GetModPlayer<CheatPlayer>();

			if (args[0] == "+")
			{
				cp.bonusLife += 20; // 1 серце = 20 HP
				caller.Player.statLife += 20;
				caller.Reply($"❤️ Максимальне здоров'я ЗБІЛЬШЕНО! (+1 Серце)");
			}
			else if (args[0] == "-")
			{
				// statLifeMax2 - це остаточне ХП, яке ви зараз маєте з усіма бонусами
				if (caller.Player.statLifeMax2 > 20)
				{
					cp.bonusLife -= 20;
					
					if (caller.Player.statLife > caller.Player.statLifeMax2 - 20)
						caller.Player.statLife = caller.Player.statLifeMax2 - 20;
						
					caller.Reply($"💔 Максимальне здоров'я ЗМЕНШЕНО! (-1 Серце)");
				}
				else
				{
					caller.Reply("У вас залишилося всього одне серце! Зменшувати далі смертельно.");
				}
			}
			else
			{
				caller.Reply("Помилка: Використовуйте /maxhealth + або /maxhealth -");
			}
		}
	}

	public class MaxManaCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "maxmana";
		public override string Usage => "/maxmana <+|->";
		public override string Description => "Додає або забирає 1 зірку (20 Мани) від вашої максимальної мани";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Використовуйте /maxmana + (щоб додати зірку) або /maxmana - (щоб забрати).");
				return;
			}

			CheatPlayer cp = caller.Player.GetModPlayer<CheatPlayer>();

			if (args[0] == "+")
			{
				cp.bonusMana += 20; // 1 зірка = 20 Мани
				caller.Player.statMana += 20;
				caller.Reply($"✨ Максимальна мана ЗБІЛЬШЕНА! (+1 Зірка)");
			}
			else if (args[0] == "-")
			{
				if (caller.Player.statManaMax2 >= 20)
				{
					cp.bonusMana -= 20;
					
					if (caller.Player.statMana > caller.Player.statManaMax2 - 20)
						caller.Player.statMana = caller.Player.statManaMax2 - 20;
						
					caller.Reply($"❌ Максимальна мана ЗМЕНШЕНА! (-1 Зірка)");
				}
				else
				{
					caller.Player.statMana = 0;
					caller.Reply("Мана повністю на нулі!");
				}
			}
			else
			{
				caller.Reply("Помилка: Використовуйте /maxmana + або /maxmana -");
			}
		}
	}
}
