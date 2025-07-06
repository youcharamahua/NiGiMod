using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;


namespace NiGiMod.Content.Items
{ 
	public class BioShyStone : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = 80;
        }

        public override void PostUpdate()
        {

        }

	}
}
