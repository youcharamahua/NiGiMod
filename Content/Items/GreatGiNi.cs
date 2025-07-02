using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NiGiMod.Content.DamageClasses;

namespace NiGiMod.Content.Items
{ 
	public class GreatGiNi : ModItem
	{

        public override void SetDefaults()
		{
            Item.width = 200;
            Item.height = 200;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 5;
            Item.value = Item.sellPrice(gold: 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 10; // 玩家生命回复增加 3点每秒
            player.GetDamage(DamageClass.Generic) += 0.1f; // 玩家全伤害增加10%
            player.runAcceleration += 0.2f;
            player.maxRunSpeed += 0.6f;
            player.endurance += 0.09f;
            player.noFallDmg = true;
            player.statDefense += (int)(player.statLife / 40);
        }


        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.DemonAltar); // 指定在恶魔祭坛上合成
            recipe.Register();
        }
	}
}
