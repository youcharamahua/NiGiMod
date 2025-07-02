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

        private Random random = new Random(); // �����������

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
            modPlayer.rotatingBulletItem = Item; // ���ݵ�ǰ��Ʒʵ��


            if (!hideVisual)
            {
                //����Ч��
                int dustType = MyDustId.PinkMagic;

                // ��â�ǵĶ�������
                int vertexCount = 5;

                // ��â�ǵ���뾶
                float outerRadius = 70f;

                // ��â�ǵ��ڰ뾶
                float innerRadius = 60f;

                // ÿ������֮��ĽǶ�
                float angleStep = MathHelper.TwoPi / vertexCount;

                // ��ǰʱ����������ڿ������ӵ��˶�
                float time = Main.GameUpdateCount * 0.02f;

                // ��������
                for (int i = 0; i < vertexCount; i++)
                {
                    // ��ǰ�������һ������ĽǶ�
                    float angle1 = i * angleStep;
                    float angle2 = (i + 1) * angleStep;

                    // ��ǰ�������һ�������λ��
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

                    // ��ֵ�������ӵ�λ��
                    float t = (time + i) % 1.0f; // ��ֵ����
                    Vector2 position = Vector2.Lerp(Vector2.Lerp(outerPoint1, innerPoint, t), outerPoint2, t);

                    // ������ƫ����
                    float randomOffset = 10f; // ���ƫ�Ʒ�Χ
                    position += new Vector2(
                        RandomFloat(-randomOffset, randomOffset),
                        RandomFloat(-randomOffset, randomOffset)
                    );

                    // ��������
                    int dustIndex = Dust.NewDust(position, 1, 1, dustType, 0f, 0f, 0, default, 1f);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true; // ���Ӳ�������Ӱ��

                    // �������ٶ�
                    float randomSpeed = 0.5f; // ����ٶȷ�Χ
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
            recipe.AddTile(TileID.DemonAltar); // ָ���ڶ�ħ��̳�Ϻϳ�
            recipe.Register();

        }
	}
}
