using NiGiMod.Content.DamageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace NiGiMod.Content.Projectiles
{
    class SlashCaeruleoProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 94; // 弹幕的碰撞箱宽度
            Projectile.height = 80; // 弹幕的碰撞箱高度
            Projectile.scale = 1.0f;
            Projectile.penetrate = 5;
            Projectile.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = ProjAIStyleID.Sickle;
            Projectile.ignoreWater = false;
        }

        public override void AI()
        {

        }
    }
}
