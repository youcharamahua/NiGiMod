using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Projectiles;
using NiGiMod.Content.Projectiles;
using NiGiMod.Content.Rarities;
using NiGiMod.Content.Tiles;
using NiGiMod.Content.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NiGiMod.Content.Items
{
    internal class ParadoxSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Item.damage = 174;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 20;
            Item.useAnimation = 17;
            Item.crit = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 30);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item169;
            Item.autoReuse = true;
            Item.useTurn = true;
        }
        public override void AddRecipes()
        {

        }

    }
}
