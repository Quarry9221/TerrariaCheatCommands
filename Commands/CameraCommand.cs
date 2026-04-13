using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class CameraCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "zoom";
		public override string Usage => "/zoom <множник>";
		public override string Description => "Змінює наближення ігрової камери (віддалення/наближення). Щоб скинути, введіть 1.";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Використання: /zoom <множник>. Наприклад: /zoom 2 (наблизить у 2 рази).");
				return;
			}

			if (float.TryParse(args[0], out float zoomValue))
			{
				if (zoomValue <= 0f)
				{
					caller.Reply("Значення зуму має бути більшим за нуль!");
					return;
				}

				// Змінюємо глобальну ціль зуму камери. 
				// 1.0f - стандартна гра. 2.0f - максимальне стандартне наближення.
				Main.GameZoomTarget = zoomValue;

				caller.Reply($"Камеру встановлено на зум: x{zoomValue}");
			}
			else
			{
				caller.Reply("Помилка: потрібно ввести число (наприклад 1.5, 2 або 0.5).");
			}
		}
	}
}
