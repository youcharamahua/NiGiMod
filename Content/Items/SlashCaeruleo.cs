using NiGiMod.Content.DamageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using NiGiMod.Content.Rarities;
using NiGiMod.Content.Projectiles;
using Microsoft.Xna.Framework.Audio;

namespace NiGiMod.Content.Items
{
    class SlashCaeruleo : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Item.damage = 150;
            Item.width = 100;
            Item.height = 90;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(silver: 99);
            Item.rare = ModContent.RarityType<NiGiGoldRarity>();
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<SlashCaeruleoProj>();
            Item.shootSpeed = 1f;
        }
        public override void HoldItem(Player player)
        {
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
        }
    }
}
