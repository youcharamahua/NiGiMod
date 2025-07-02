using Microsoft.Xna.Framework;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Prefixs;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NiGiMod.Common
{
    // 这个类代表对所有的物品进行操作，你可以针对任何一个符合条件的物品独立修改
    // 这意味着你可以修改任何原版和模组物品(甚至别的模组的物品)
    public class NiGiGlobalItems : GlobalItem //这是一个全局物品的示例
    {
        public override bool InstancePerEntity => true;
        // 使得每个实体有自己的属性, 否则所有物品将共用你设置的属性

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == ModContent.PrefixType<NiGiprefix1>())
            {
                player.GetDamage(DamageClass.Generic) += 0.06f;
                player.GetDamage(ModContent.GetInstance<NiGiDamageClass>()) += 0.15f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == ModContent.PrefixType<NiGiprefix1>())
            {
                string name = Language.GetTextValue("Mods.NiGiMod.Prefixes.NiGiprefix1.DisplayName");
                string tooltip = "[c/00ffff:玮][c/09f5ff:毅][c/12ecff:之][c/1ce2ff:神][c/25d9ff:的][c/2fcfff:赐][c/38c6ff:福][c/42bcff::]\n[c/4bb3ff:+][c/55a9ff:6][c/5ea0ff:%][c/6797ff:伤][c/718dff:害]\n[c/7a84ff:+][c/847aff:1][c/8d71ff:5][c/9767ff:%][c/a05eff:玮][c/aa54ff:毅][c/b34bff:之][c/bc42ff:力][c/c638ff:伤][c/cf2fff:害]\n[c/d925ff:+][c/e21cff:1][c/ec12ff:0][c/f509ff:护][c/ff00ff:甲]";
                tooltips.Add(new TooltipLine(Mod,name , tooltip));
            }
        }


    }
}