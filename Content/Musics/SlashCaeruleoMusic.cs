using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using NiGiMod.Content.Items;

namespace NiGiMod.Content.Musics
{
    class SlashCaeruleoMusic : ModSceneEffect
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "NiGiMod/Content/Musics/SlashCaeruleoMusic");
        public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

        public static float Volume = 70f;

        public override float GetWeight(Player player) => Volume;

        // 控制播放条件
        public override bool IsSceneEffectActive(Player player)
        {
            return player.HeldItem.type == ModContent.ItemType<SlashCaeruleo>();
        }


    }
}