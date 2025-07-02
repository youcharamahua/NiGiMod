using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NiGiMod.Content.Rarities
{
    public class NiGiGoldRarity : ModRarity
    {
        // 该稀有度的颜色
        public override Color RarityColor
                    => new Color(255, 215, 0);


        // 修饰词条可以影响稀有度
        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            return Type;
        }
    }
}