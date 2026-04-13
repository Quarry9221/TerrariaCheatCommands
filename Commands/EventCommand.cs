using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat.Commands
{
	public class EventCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;

		public override string Command => "event";

		public override string Usage => "/event <bloodmoon|eclipse|goblin|pirate|martian|slime|frost>";

		public override string Description => "Запускає обрану подію або вторгнення у світі";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Помилка. Використання: /event <bloodmoon|eclipse|goblin|pirate|martian|slime|frost>");
				return;
			}

			string eventName = args[0].ToLower();

			switch (eventName)
			{
				case "bloodmoon":
					// Для кривавого місяця примусово робимо ніч
					Main.dayTime = false;
					Main.time = 0;
					Main.bloodMoon = true;
					caller.Reply("Увага: Почався Кривавий Місяць!");
					break;
				case "eclipse":
					// Для затемнення примусово робимо ранок
					Main.dayTime = true;
					Main.time = 0;
					Main.eclipse = true;
					caller.Reply("Увага: Почалося Сонячне Затемнення!");
					break;
				case "goblin":
					Main.StartInvasion(1); // 1 = Армія Гоблінів
					caller.Reply("Увага: Наближається Армія Гоблінів!");
					break;
				case "frost":
					Main.StartInvasion(2); // 2 = Морозний Легіон Сніговиків
					caller.Reply("Увага: Наближається Морозний Легіон!");
					break;
				case "pirate":
					Main.StartInvasion(3); // 3 = Піратське вторгнення
					caller.Reply("Увага: Наближаються Пірати!");
					break;
				case "martian":
					Main.StartInvasion(4); // 4 = Марсіанське Божевілля
					caller.Reply("Увага: Марсіани атакують!");
					break;
				case "slime":
					Main.StartSlimeRain(); // Слизневий дощ
					caller.Reply("Увага: З неба падає слизь (Слизневий Дощ)!");
					break;
				default:
					caller.Reply("Помилка: Невідома подія. Доступні: bloodmoon, eclipse, goblin, pirate, martian, slime, frost.");
					return; // Завершуємо достроково, щоб уникнути зайвої синхронізації
			}

			// Синхронізуємо дані світу у мультиплеєрі
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}
}
