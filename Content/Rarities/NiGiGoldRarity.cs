using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NiGiMod.Content.Rarities
{
    public class NiGiGoldRarity : ModRarity
    {
        // ��ϡ�жȵ���ɫ
        public override Color RarityColor
                    => new Color(255, 215, 0);


        // ���δ�������Ӱ��ϡ�ж�
        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            return Type;
        }
    }
}