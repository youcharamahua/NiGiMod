using NiGiMod.Content.DamageClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using NiGiMod.Content.Tiles;
using NiGiMod.Content.Rarities;

namespace NiGiMod.Content.Items
{
    class NiGiSkyTileItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;
            Item.value = Item.buyPrice(silver: 99);
            Item.rare = ModContent.RarityType<NiGiGoldRarity>();
            Item.consumable = true;
            Item.DefaultToPlaceableTile(ModContent.TileType<NiGiSkyTile>(), 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SwordStatue, 1);
            recipe.AddIngredient(ItemID.Obsidian, 25);
            recipe.AddIngredient(ItemID.Hellstone, 21);
            recipe.AddIngredient(ItemID.ShadowScale, 17);
            recipe.AddIngredient(ItemID.Amethyst, 9);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ModContent.ItemType<NiGiSoul>(), 10);
            recipe.AddTile(TileID.DemonAltar); // 指定在恶魔祭坛上合成
            recipe.Register();
        }
    }
}
