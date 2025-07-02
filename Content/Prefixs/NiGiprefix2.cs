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
    public class NiGiprefix2 : ModPrefix //这是一个饰品前缀示例
    {
        public override string Name => "NiGiprefix2";//这是前缀的内部名称，不代表显示名称，请你去hjson修改显示名称
                                                     // 修改该前缀的类别，默认为 PrefixCategory.Custom。影响哪些物品可以获得此前缀
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;//任何武器都可以使用
		
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

        // 用这个方法来修改拥有此前缀的物品的属性：
        // damageMult 伤害乘数，knockbackMult 击退乘数，useTimeMult 使用时间乘数，scaleMult 大小乘数，
        // shootSpeedMult 弹速（射速，射出的速度）乘数，manaMult 魔力消耗乘数，critBonus 暴击增量
        public override void SetStats(ref float damageMult, ref float knockbackMult,
            ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1f + 2.20f;
            knockbackMult *= 1.05f;
            critBonus += 15;
            scaleMult *= 1.15f;
        }

        // 修改获得此前缀的物品的价格，valueMult 为价格乘数
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.314f;
        }

        public override void Apply(Item item)
        {
            item.rare = ModContent.RarityType<NiGiGoldRarity>();
        }
    }
}