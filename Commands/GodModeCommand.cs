using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class GodModeCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "godmode";

		public override string Usage => "/godmode";

		public override string Description => "Увімкнути/вимкнути Режим Бога (Безсмертя та Безкінечна Мана)";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Отримуємо доступ до нашого кастомного гравця
			CheatPlayer cheatPlayer = caller.Player.GetModPlayer<CheatPlayer>();

			// Перемикаємо значення
			cheatPlayer.godMode = !cheatPlayer.godMode;

			if (cheatPlayer.godMode)
			{
				// Додатково лікуємо гравця при включенні
				caller.Player.statLife = caller.Player.statLifeMax2;
				caller.Player.statMana = caller.Player.statManaMax2;
				caller.Reply("Режим Бога УВІМКНЕНО! Тепер ви безсмертні і маєте безкінечну ману.");
			}
			else
			{
				caller.Reply("Режим Бога ВИМКНЕНО! Будьте обережні.");
			}
		}
	}
}
