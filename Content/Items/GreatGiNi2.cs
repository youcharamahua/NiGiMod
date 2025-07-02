using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Utils;
using NiGiMod.Content.Projectiles;
using NiGiMod.Content.Items;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NiGiMod.Content.Items
{ 
	public class GreatGiNi2 : ModItem
	{

        private Random random = new Random(); // 随机数生成器

        public override void SetDefaults()
		{
            Item.width = 200;
            Item.height = 200;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 5;
            Item.value = Item.sellPrice(gold: 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 16;
            player.GetDamage(DamageClass.Generic) += 0.20f;
            player.GetDamage(ModContent.GetInstance<NiGiDamageClass>()) += 0.35f;
            player.runAcceleration += 0.2f;
            player.maxRunSpeed += 0.6f;
            player.endurance += 0.11f;
            player.noFallDmg = true;
            player.statDefense += (int)(player.statLife / 30);

            var modPlayer = player.GetModPlayer<NiGiPlayer>();
            modPlayer.hasRotatingBulletAccessory = true;
            modPlayer.rotatingBulletItem = Item; // 传递当前物品实例


            if (!hideVisual)
            {
                //粒子效果
                int dustType = MyDustId.PinkMagic;

                // 五芒星的顶点数量
                int vertexCount = 5;

                // 五芒星的外半径
                float outerRadius = 70f;

                // 五芒星的内半径
                float innerRadius = 60f;

                // 每个顶点之间的角度
                float angleStep = MathHelper.TwoPi / vertexCount;

                // 当前时间变量，用于控制粒子的运动
                float time = Main.GameUpdateCount * 0.02f;

                // 生成粒子
                for (int i = 0; i < vertexCount; i++)
                {
                    // 当前顶点和下一个顶点的角度
                    float angle1 = i * angleStep;
                    float angle2 = (i + 1) * angleStep;

                    // 当前顶点和下一个顶点的位置
                    Vector2 outerPoint1 = player.Center + new Vector2(
                        outerRadius * (float)Math.Cos(angle1),
                        outerRadius * (float)Math.Sin(angle1)
                    );

                    Vector2 innerPoint = player.Center + new Vector2(
                        innerRadius * (float)Math.Cos(angle1 + angleStep / 2),
                        innerRadius * (float)Math.Sin(angle1 + angleStep / 2)
                    );

                    Vector2 outerPoint2 = player.Center + new Vector2(
                        outerRadius * (float)Math.Cos(angle2),
                        outerRadius * (float)Math.Sin(angle2)
                    );

                    // 插值计算粒子的位置
                    float t = (time + i) % 1.0f; // 插值参数
                    Vector2 position = Vector2.Lerp(Vector2.Lerp(outerPoint1, innerPoint, t), outerPoint2, t);

                    // 添加随机偏移量
                    float randomOffset = 10f; // 随机偏移范围
                    position += new Vector2(
                        RandomFloat(-randomOffset, randomOffset),
                        RandomFloat(-randomOffset, randomOffset)
                    );

                    // 生成粒子
                    int dustIndex = Dust.NewDust(position, 1, 1, dustType, 0f, 0f, 0, default, 1f);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true; // 粒子不受重力影响

                    // 添加随机速度
                    float randomSpeed = 0.5f; // 随机速度范围
                    dust.velocity = new Vector2(
                        RandomFloat(-randomSpeed, randomSpeed),
                        RandomFloat(-randomSpeed, randomSpeed)
                    );
                }
            }
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }



        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GreatGiNi>(), 1);
            recipe.AddIngredient(ModContent.ItemType<NiGiSoul>(), 10);
            recipe.AddIngredient(ItemID.DarkShard, 1);
            recipe.AddIngredient(ItemID.LightShard, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddTile(TileID.DemonAltar); // 指定在恶魔祭坛上合成
            recipe.Register();

        }
	}
}
