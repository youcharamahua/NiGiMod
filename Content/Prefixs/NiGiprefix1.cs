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
    public class NiGiprefix1 : ModPrefix //����һ����Ʒǰ׺ʾ��
    {
        public override string Name => "NiGiprefix1";//����ǰ׺���ڲ����ƣ���������ʾ���ƣ�����ȥhjson�޸���ʾ����
                                                   // �޸ĸ�ǰ׺�����Ĭ��Ϊ PrefixCategory.Custom��Ӱ����Щ��Ʒ���Ի�ô�ǰ׺
        public override PrefixCategory Category => PrefixCategory.Accessory;//��Ʒ


		
        public override float RollChance(Item item)
        {
            return 5f;
        }

        // ������ǰ׺�Ƿ���������ʱ�����
        // ��Ϊ true �����ܣ�false ���ǲ���
        public override bool CanRoll(Item item)
        {
            return true;
        }

        // �޸Ļ�ô�ǰ׺����Ʒ�ļ۸�valueMult Ϊ�۸����
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.314f;
        }

        // ������������޸Ļ�ô�ǰ׺����Ʒ���������ԣ�������Ʒ������
        //Ȼ����Ҫ�뽫��Щ�ӳ�д����Ʒ�����У�����Ҫ����дtooltip
        public override void Apply(Item item)
        {
            
            item.defense += 10;
            item.rare = ModContent.RarityType<NiGiGoldRarity>();
        }
    }
}