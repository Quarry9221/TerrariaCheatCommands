using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class HealCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "heal";

		public override string Usage => "/heal";

		public override string Description => "Повністю відновлює здоров'я та ману";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// statLifeMax2 та statManaMax2 — це максимальні значення з урахуванням усіх бонусів та броні.
			int healAmount = caller.Player.statLifeMax2 - caller.Player.statLife;
			int manaAmount = caller.Player.statManaMax2 - caller.Player.statMana;

			// Відновлюємо
			caller.Player.statLife = caller.Player.statLifeMax2;
			caller.Player.statMana = caller.Player.statManaMax2;

			// Створюємо візуальні зелені/сині циферки над гравцем (як при звичайному хілі)
			if (healAmount > 0)
			{
				caller.Player.HealEffect(healAmount, true);
			}
			
			if (manaAmount > 0)
			{
				caller.Player.ManaEffect(manaAmount);
			}

			caller.Reply("Здоров'я та мана повністю відновлені!");
		}
	}
}
