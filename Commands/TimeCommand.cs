using Terraria;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class TimeCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "time";

		public override string Usage => "/time <day|noon|night|midnight>";

		public override string Description => "Змінює час доби у світі";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка: Не вказано час. Використання: /time <day|noon|night|midnight>");
				return;
			}

			string timeTarget = args[0].ToLower();

			switch (timeTarget)
			{
				case "day":
					Main.dayTime = true;
					Main.time = 0.0;
					caller.Reply("Встановлено час: Ранок (04:30)");
					break;
				case "noon":
					Main.dayTime = true;
					Main.time = 27000.0;
					caller.Reply("Встановлено час: Полудень (12:00)");
					break;
				case "night":
					Main.dayTime = false;
					Main.time = 0.0;
					caller.Reply("Встановлено час: Ніч (19:30)");
					break;
				case "midnight":
					Main.dayTime = false;
					Main.time = 16200.0;
					caller.Reply("Встановлено час: Північ (00:00)");
					break;
				default:
					caller.Reply("Помилка: Невідомий час. Допустимі значення: day, noon, night, midnight.");
					break;
			}

			// Якщо ми знаходимось на мультиплеєр-сервері, синхронізуємо час з іншими гравцями
			if (Main.netMode == Terraria.ID.NetmodeID.Server)
			{
				Terraria.NetMessage.SendData(Terraria.ID.MessageID.WorldData);
			}
		}
	}
}
