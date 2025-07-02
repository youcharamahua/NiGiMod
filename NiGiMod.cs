using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using NiGiMod.Content.BackGrounds;
using NiGiMod.Content.Items;
using Microsoft.Xna.Framework;
using Terraria.GameInput;
using Terraria.DataStructures;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Utils;
using NiGiMod.Content.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Effects;

namespace NiGiMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class NiGiMod : Mod
	{
        public override void Load()
        {


        }

        public class MyGlobalNPC : GlobalNPC
        {
            public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
            {
                if (npc.type == NPCID.ChaosElemental)
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NiGiSoul>(), 1));
                }
            }

        }


    }
}
