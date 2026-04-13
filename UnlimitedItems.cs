using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace terrariacheat
{
	// Цей клас перехоплює логіку абсолютно всіх предметів у грі
	public class UnlimitedItems : GlobalItem
	{
		// Визначаємо, чи МОЖЕ гравець використати предмет
		public override bool CanUseItem(Item item, Player player)
		{
			// Якщо це кристал життя і гравець вичерпав ВАНІЛЬНИЙ ліміт (15 кристалів)
			if (item.type == ItemID.LifeCrystal && player.ConsumedLifeCrystals >= 15)
				return true; // Дозволяємо використовувати кристали й надалі!
				
			if (item.type == ItemID.LifeFruit && player.ConsumedLifeFruit >= 20)
				return true; // Дозволяємо використовувати фрукти життя (золоті серця) після 500 ХП

			if (item.type == ItemID.ManaCrystal && player.ConsumedManaCrystals >= 9)
				return true; // Дозволяємо їсти ману після 200 мани!

			return base.CanUseItem(item, player);
		}

		// Що РЕАЛЬНО робить предмет після того, як ми дозволили його з'їсти
		public override bool? UseItem(Item item, Player player)
		{
			CheatPlayer cp = player.GetModPlayer<CheatPlayer>();

			// Якщо це Кристал Життя поза ванільним лімітом
			if (item.type == ItemID.LifeCrystal && player.ConsumedLifeCrystals >= 15)
			{
				cp.bonusLife += 20; // Прокидаємо здоров'я в нашу Cheat-змінну
				player.statLife += 20;

				if (Main.myPlayer == player.whoAmI)
					player.HealEffect(20);

				return true; // Предмет успішно спожито (його кількість в інвентарі зменшиться на 1)
			}
			
			// Якщо це Фрукт Життя
			if (item.type == ItemID.LifeFruit && player.ConsumedLifeFruit >= 20)
			{
				cp.bonusLife += 5; // Фрукти дають 5 ХП
				player.statLife += 5;

				if (Main.myPlayer == player.whoAmI)
					player.HealEffect(5);

				return true;
			}

			// Якщо це Зірка Мани
			if (item.type == ItemID.ManaCrystal && player.ConsumedManaCrystals >= 9)
			{
				cp.bonusMana += 20;
				player.statMana += 20;

				if (Main.myPlayer == player.whoAmI)
					player.ManaEffect(20);

				return true;
			}

			return base.UseItem(item, player);
		}
	}
}
