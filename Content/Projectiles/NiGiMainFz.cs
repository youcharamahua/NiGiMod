using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Items;

namespace NiGiMod.Content.Projectiles
{
    public class NiGiMainFz : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 150; // 弹幕的碰撞箱宽度
            Projectile.height = 150; // 弹幕的碰撞箱高度
            Projectile.scale = 1.0f;
            Projectile.penetrate = -1;
            Projectile.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true; // 启用本地无敌帧
            Projectile.localNPCHitCooldown = 20;    // 该弹幕对同一NPC的伤害间隔（单位：帧，20帧=1/3秒）
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 0.04f; // 每帧旋转 0.05 弧度
            Projectile.velocity *= 0;
            Projectile.Center = player.Center;
            

        }
    }
}
