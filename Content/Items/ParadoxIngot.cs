using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;


namespace NiGiMod.Content.Items
{ 
	public class ParadoxIngot : ModItem
	{
        public override void SetDefaults()
		{
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Lime;
            Item.value = 200;
        }

        public override void PostUpdate()
        {

        }

	}
}
