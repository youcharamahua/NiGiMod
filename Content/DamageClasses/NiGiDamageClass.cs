using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace NiGiMod.Content.DamageClasses
{
    public class NiGiDamageClass : DamageClass
    {

        // 定义继承关系
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            // 如果你希望“NiGi伟毅”继承自“通用”伤害类型的效果，可以返回 true
            return damageClass == Generic;
        }

        // 定义默认属性
        public override void SetDefaultStats(Player player)
        {
            // 你可以在这里设置默认的属性，例如基础暴击率
            player.GetCritChance(this) += 15; // 假设增加 4% 的暴击率
        }

        // 自定义工具提示
        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            // 如果你想隐藏某些默认的工具提示，可以在这里返回 false
            return base.ShowStatTooltipLine(player, lineName);
        }
    }
}
