using Terraria.ModLoader;

namespace terrariacheat
{
	// ModSystem використовується для глобальних налаштувань світу, 
	// на відміну від ModPlayer, який працює для окремого гравця.
	public class CheatSystem : ModSystem
	{
		// Глобальний мультиплікатор швидкості часу
		public static double TimeMultiplier = 1.0;

		// Скидаємо значення на нормальне при кожному завантаженні або виході зі світу
		public override void OnWorldLoad()
		{
			TimeMultiplier = 1.0;
		}

		public override void OnWorldUnload()
		{
			TimeMultiplier = 1.0;
		}

		// Вбудований метод tModLoader для втручання у плин часу
		public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
		{
			if (TimeMultiplier != 1.0)
			{
				// Збільшуємо швидкість дня і ночі (рух сонця і місяця)
				timeRate *= TimeMultiplier;
				
				// Збільшуємо швидкість оновлення тайлів (наприклад, ріст дерев чи трави) 
				// та подій, щоб світ адаптувався під нову швидкість часу.
				tileUpdateRate *= TimeMultiplier;
				eventUpdateRate *= TimeMultiplier;
			}
		}
	}
}
