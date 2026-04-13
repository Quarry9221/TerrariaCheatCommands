using System;
using Terraria;
using Terraria.ModLoader;

using Terraria.ModLoader.IO;

namespace terrariacheat
{
	// Цей клас дозволяє нам додавати власні змінні та логіку до гравця
	public class CheatPlayer : ModPlayer
	{
		// Змінна, що зберігає стан Режиму Бога для цього конкретного гравця
		public bool godMode = false;
		
		// Змінні для збереження зміненого здоров'я та мани
		public int bonusLife = 0;
		public int bonusMana = 0;

		// Зберігання змінних після закриття гри
		public override void SaveData(TagCompound tag)
		{
			tag["bonusLife"] = bonusLife;
			tag["bonusMana"] = bonusMana;
		}

		// Відновлення значень при вході у світ
		public override void LoadData(TagCompound tag)
		{
			bonusLife = tag.GetInt("bonusLife");
			bonusMana = tag.GetInt("bonusMana");
		}

		// Спеціальний метод tModLoader 1.4.4 для втручання у статистику!
		public override void ModifyMaxStats(out StatModifier health, out StatModifier mana)
		{
			health = StatModifier.Default;
			mana = StatModifier.Default;

			health.Base += bonusLife;
			mana.Base += bonusMana;
		}

		// PostUpdateEquips викликається кожен кадр після того, 
		// як розраховані всі бонуси від броні та аксесуарів
		public override void PostUpdateEquips()
		{
			if (godMode)
			{
				// Тримаємо здоров'я та ману завжди на максимумі
				Player.statLife = Player.statLifeMax2;
				Player.statMana = Player.statManaMax2;
				
				// Робимо гравця повністю невразливим (вороги проходитимуть крізь нього без шкоди)
				Player.immune = true;
				Player.immuneTime = Math.Max(Player.immuneTime, 60);
			}
		}
	}
}
