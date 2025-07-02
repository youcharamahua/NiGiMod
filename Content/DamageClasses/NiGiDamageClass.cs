using Terraria;
using Terraria.ModLoader;

using Microsoft.Xna.Framework;

namespace NiGiMod.Content.DamageClasses
{
    public class NiGiDamageClass : DamageClass
    {

        // ����̳й�ϵ
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            // �����ϣ����NiGiΰ�㡱�̳��ԡ�ͨ�á��˺����͵�Ч�������Է��� true
            return damageClass == Generic;
        }

        // ����Ĭ������
        public override void SetDefaultStats(Player player)
        {
            // ���������������Ĭ�ϵ����ԣ��������������
            player.GetCritChance(this) += 15; // �������� 4% �ı�����
        }

        // �Զ��幤����ʾ
        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            // �����������ĳЩĬ�ϵĹ�����ʾ�����������ﷵ�� false
            return base.ShowStatTooltipLine(player, lineName);
        }
    }
}
