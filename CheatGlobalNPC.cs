using Terraria;
using Terraria.ModLoader;

namespace terrariacheat
{
	// GlobalNPC використовується для глобальних змін, що стосуються ВСІХ істот у грі
	public class CheatGlobalNPC : GlobalNPC
	{
		// Множник швидкості появи ворогів (1.0 = стандартна швидкість)
		public static double SpawnRateMultiplier = 1.0;

		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			// Якщо множник 0 - повністю зупиняємо природний спавн монстрів
			if (SpawnRateMultiplier == 0)
			{
				maxSpawns = 0; 
				return;
			}

			// Якщо множник відмінний від стандартного
			if (SpawnRateMultiplier != 1.0)
			{
				// Що МЕНШЕ значення spawnRate, то ЧАСТІШЕ гра намагається спавнити мобів.
				// Тому ми ділимо стандартний шанс на наш множник.
				spawnRate = (int)(spawnRate / SpawnRateMultiplier);
				
				// Захист від критичних помилок: якщо шанс менше 1, встановлюємо 1 (максимальна швидкість)
				if (spawnRate < 1) spawnRate = 1;

				// Множимо ліміт монстрів на екрані, інакше збільшений спавн миттєво зупиниться
				maxSpawns = (int)(maxSpawns * SpawnRateMultiplier);
			}
		}
	}
}
