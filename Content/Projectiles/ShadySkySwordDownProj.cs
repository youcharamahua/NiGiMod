using NiGiMod.Content.DamageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Drawing;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace NiGiMod.Content.Projectiles
{
    class ShadySkySwordDownProj : ModProjectile
    {
        Player player => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 17; // 弹幕的碰撞箱宽度
            Projectile.height = 117; // 弹幕的碰撞箱高度
            Projectile.scale = 1.5f;
            Projectile.penetrate = -1;
            Projectile.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 2;   
        }


        public override void AI()
        {
            // 保持弹幕旋转方向与运动方向一致
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // 添加拖尾粒子效果（可选）
            if (Main.rand.NextBool(3))
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Electric,
                    Vector2.Zero,
                    155,
                    Color.Cyan,
                    1f
                );
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //为了让弹幕具有打击感，我们还要在击中函数中写一次生成爆炸的弹幕
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Center,
                MovementVector = Vector2.Zero
            });

        }

    }
}
