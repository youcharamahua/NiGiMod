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
    public class NiGiprefix2 : ModPrefix //����һ����Ʒǰ׺ʾ��
    {
        public override string Name => "NiGiprefix2";//����ǰ׺���ڲ����ƣ���������ʾ���ƣ�����ȥhjson�޸���ʾ����
                                                     // �޸ĸ�ǰ׺�����Ĭ��Ϊ PrefixCategory.Custom��Ӱ����Щ��Ʒ���Ի�ô�ǰ׺
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;//�κ�����������ʹ��
		
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

        // ������������޸�ӵ�д�ǰ׺����Ʒ�����ԣ�
        // damageMult �˺�������knockbackMult ���˳�����useTimeMult ʹ��ʱ�������scaleMult ��С������
        // shootSpeedMult ���٣����٣�������ٶȣ�������manaMult ħ�����ĳ�����critBonus ��������
        public override void SetStats(ref float damageMult, ref float knockbackMult,
            ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1f + 2.20f;
            knockbackMult *= 1.05f;
            critBonus += 15;
            scaleMult *= 1.15f;
        }

        // �޸Ļ�ô�ǰ׺����Ʒ�ļ۸�valueMult Ϊ�۸����
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