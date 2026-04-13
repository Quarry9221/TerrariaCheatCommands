using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class ClearBuffsCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "clearbuffs";

		public override string Usage => "/clearbuffs";

		public override string Description => "Очищає всі активні бафи та дебафи з вашого персонажа";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Йдемо з кінця масиву, оскільки DelBuff зсуває елементи.
			for (int i = Player.MaxBuffs - 1; i >= 0; i--)
			{
				if (caller.Player.buffType[i] > 0)
				{
					caller.Player.DelBuff(i);
				}
			}

			caller.Reply("Всі ефекти було успішно видалено.");
		}
	}
}
