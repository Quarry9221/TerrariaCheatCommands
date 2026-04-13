using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace terrariacheat.Commands
{
	public class ExplodeCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "explode";
		public override string Usage => "/explode";
		public override string Description => "Створює справжній руйнівний вибух прямо на вас!";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Спавнимо снаряд динаміту прямо в координатах гравця.
			int projIndex = Projectile.NewProjectile(
				caller.Player.GetSource_FromThis(), 
				caller.Player.Center, 
				Vector2.Zero, // Швидкості немає, він просто падає під ноги
				ProjectileID.Dynamite, // ID класичного динаміту, що руйнує блоки
				250, // Шкода вибуху
				10f, // Відкидання (Knockback)
				caller.Player.whoAmI
			);

			// 2 секунди затримки перед вибухом (60 кадрів в секунду * 2 = 120)
			if (projIndex < Main.maxProjectiles)
			{
				Main.projectile[projIndex].timeLeft = 120; 
			}

			caller.Reply("💥 БУМ через 2 секунди! БІЖІТЬ!");
		}
	}
}
