using Microsoft.Xna.Framework;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Rarities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NiGiMod.Content.Prefixs
{
    public class NiGiprefix1 : ModPrefix //这是一个饰品前缀示例
    {
        public override string Name => "NiGiprefix1";//这是前缀的内部名称，不代表显示名称，请你去hjson修改显示名称
                                                   // 修改该前缀的类别，默认为 PrefixCategory.Custom。影响哪些物品可以获得此前缀
        public override PrefixCategory Category => PrefixCategory.Accessory;//饰品


		
        public override float RollChance(Item item)
        {
            return 5f;
        }

        // 决定该前缀是否能在重铸时随机到
        // 设为 true 就是能，false 就是不能
        public override bool CanRoll(Item item)
        {
            return true;
        }

        // 修改获得此前缀的物品的价格，valueMult 为价格乘数
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.314f;
        }

        // 这个方法用来修改获得此前缀的物品的其它属性，例如物品防御力
        //然而，要想将这些加成写在物品描述中，你需要额外写tooltip
        public override void Apply(Item item)
        {
            
            item.defense += 10;
            item.rare = ModContent.RarityType<NiGiGoldRarity>();
        }
    }
}