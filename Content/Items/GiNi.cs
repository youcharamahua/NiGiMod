using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;

namespace NiGiMod.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class GiNi : ModItem
	{


        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.NiGiMod.hjson' file.
        public override void SetDefaults()
		{
            Item.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Item.damage = 500;
            Item.width = 400;
            Item.height = 400;
            Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
		}
	}
}
