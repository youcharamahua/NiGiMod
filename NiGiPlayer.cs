using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.DataStructures;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Utils;
using NiGiMod.Content.Projectiles;
using NiGiMod.Content.Items;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using NiGiMod.Content.Musics;



namespace NiGiMod
{
    public class NiGiPlayer : ModPlayer
    {
        public bool hasRotatingBulletAccessory;
        public Item rotatingBulletItem;
        public int shadySkyCounter = 0;
        private const float MaxMusicFade = 0.9f;
        private float _musicFade;

        public override void ResetEffects()
        {
            hasRotatingBulletAccessory = false;
            rotatingBulletItem = null;
        }
        public override void PostUpdateMiscEffects()
        {
            if (Main.netMode == NetmodeID.Server) return;

            //影于空背景绘制
            if (Player.whoAmI == Main.myPlayer &&
                Player.HeldItem.type == ModContent.ItemType<ShadySky>()) 
            {
                if (!SkyManager.Instance["ShadySky"].IsActive())
                {
                    SkyManager.Instance.Activate("ShadySky");
                }
            }
            else
            {
                if (SkyManager.Instance["ShadySky"].IsActive())
                {
                    SkyManager.Instance.Deactivate("ShadySky");
                }
            }

            //SlashCaeruleoMusic
            bool holdingSlashCaeruleo = Player.HeldItem.type == ModContent.ItemType<SlashCaeruleo>();


            // 只在主玩家控制时生成弹幕
            if (Player.whoAmI == Main.myPlayer
                && hasRotatingBulletAccessory
                && Player.ownedProjectileCounts[ModContent.ProjectileType<NiGiMainFz>()] < 1)
            {
                var source = Player.GetSource_Accessory(rotatingBulletItem);

                Projectile.NewProjectile(
                    source,
                    Player.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<NiGiMainFz>(),
                    ((int)(Player.statLife / 25)) + 20,
                    0f,
                    Player.whoAmI
                );
            }
        }


        /*
        public override void PreUpdate()
        {
            if (!Main.gamePaused)//游戏暂停时不执行
            {
                Item heldItem = Player.HeldItem;
                if (heldItem.type == ModContent.ItemType<ShadySky>())
                {
                    if (SkyManager.Instance["ShadySkyBackground"].IsActive())
                    {
                        SkyManager.Instance.Deactivate("ShadySkyBackground");//消失
                    }
                }
            }
        }
        */
    }
}
