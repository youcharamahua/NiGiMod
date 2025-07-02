using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Utils;
using NiGiMod.Content.Projectiles;
using NiGiMod.Content.Items;
using NiGiMod.Content.BackGrounds;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using NiGiMod.Content.Rarities;
using Mono.Cecil;
using static System.Net.Mime.MediaTypeNames;
using Terraria.GameInput;
using NiGiMod.Content.Tiles;


namespace NiGiMod.Content.Items
{ 
	public class ShadySky : ModItem
	{
        public override void SetDefaults()
		{
            Item.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Item.damage = 102;
            Item.width = 82;
            Item.height = 84;
			Item.useTime = 20;
			Item.useAnimation = 17;
            Item.crit = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 18);
			Item.rare = ModContent.RarityType<NiGiGoldRarity>();
			Item.UseSound = SoundID.Item169;
			Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<ShadySkySwordProj>();
            Item.shootSpeed = 1f;

        }


        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI) // ȷ�����ڱ�����ҿͻ���ִ��
            {
                // ��ȡ����������꣨Ŀ��λ�ã�
                Vector2 targetPosition = Main.MouseWorld;

                // ��ȡ���ͷ��120�񴦣��߿���㣩
                Vector2 spawnPosition = player.Top - new Vector2(0, 90 * 16); // 1��=16����

                // ���㵯Ļ�����������Ӹ߿�ָ�����λ�ã�
                Vector2 direction = targetPosition - spawnPosition;
                direction.Normalize(); // ��λ������
                Vector2 velocity = direction * 25f; // �ٶ���Ϊ15����/֡

                // ����Ҽ���ס״̬
                if (PlayerInput.Triggers.Current.MouseRight)
                {
                    // ʹ��ModPlayer���Ʒ�����
                    var modPlayer = player.GetModPlayer<NiGiPlayer>();
                    modPlayer.shadySkyCounter++;

                    // ÿ8֡����һ�Σ�Լÿ��7.5�Σ�
                    if (modPlayer.shadySkyCounter >= 16)
                    {
                        // ���ڷ����������ɵ�Ļ�����˼��ݣ�
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Projectile.NewProjectileDirect(
                                player.GetSource_ItemUse(player.HeldItem),
                                spawnPosition,
                                velocity,
                                ModContent.ProjectileType<ShadySkySwordDownProj>(),
                                100,
                                5,
                                player.whoAmI
                            );
                        }

                        modPlayer.shadySkyCounter = 0;
                    }
                }
                else
                {
                    player.GetModPlayer<NiGiPlayer>().shadySkyCounter = 0;
                }
            }

            base.HoldItem(player);
        }

        public override void ModifyItemScale(Player player, ref float scale)
        {
            base.ModifyItemScale(player, ref scale);
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float maxRotate = 4.5f;//���������軡��
            if (velocity.X >= 0)//��������ҹ����Ļ�
            {
                var p = Projectile.NewProjectileDirect(source, player.Center, velocity,
                  type, damage, knockback, player.whoAmI, 1, maxRotate / 20f * 0.75f * player.GetAttackSpeed(ModContent.GetInstance<NiGiDamageClass>()));
                p.scale = Item.scale;
                p.rotation = velocity.ToRotation() - maxRotate / 2f;
            }
            else//��֮
            {
                var p = Projectile.NewProjectileDirect(source, player.Center, velocity,
                  type, damage, knockback, player.whoAmI, -1, maxRotate / 20f * 0.75f * player.GetAttackSpeed(ModContent.GetInstance<NiGiDamageClass>()));
                p.scale = Item.scale;
                p.rotation = velocity.ToRotation() - maxRotate / 2f;
            }
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, MyDustId.PurpleTorch);
        }


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ModContent.ItemType<NiGiSoul>(), 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(ModContent.TileType<NiGiSkyTile>()); 
            recipe.Register();
        }
	}
}
