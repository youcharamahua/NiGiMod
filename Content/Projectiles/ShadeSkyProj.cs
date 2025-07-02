using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NiGiMod.Content.Utils;
using Terraria.DataStructures;
using NiGiMod.Content.DamageClasses;
using NiGiMod.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.Localization;

namespace NiGiMod.Content.Projectiles
{
    public class ShadeSkyProj : ModProjectile
    {
        Player player => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            Main.projFrames[Projectile.type] = 5;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = 200; // 弹幕的碰撞箱宽度
            Projectile.height = 60; // 弹幕的碰撞箱高度
            Projectile.scale = 2.5f;
            Projectile.penetrate = -1;
            Projectile.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            //Projectile.usesLocalNPCImmunity = true; // 启用本地无敌帧
            //Projectile.localNPCHitCooldown = 2;    // 该弹幕对同一NPC的伤害间隔（单位：帧，20帧=1/3秒）
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Player player = Main.player[Projectile.owner];
            // 根据方向偏移碰撞箱
            int xOffset = player.direction == 1 ? 30 : -70; // 向右时右移，向左时左移
            hitbox.X += xOffset;
        }


        public override void AI()
        {

            // 动画帧更新逻辑
            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                    return;
                }
            }
            base.AI();
        }

        public override bool PreDraw(ref Color lightColor)//predraw返回false即可禁用原版绘制
        {
            //同时，需要进行的绘制在这里面写就好
            Player player = Main.player[Projectile.owner];

            Texture2D texture = TextureAssets.Projectile[Type].Value;//声明本弹幕的材质
            Rectangle rectangle = new Rectangle(//因为手动绘制需要自己填写帧图框,所以要先算出来
                0,//这个框的左上角的水平坐标(填0就好)
                texture.Height / Main.projFrames[Type] * Projectile.frame,//框的左上角的纵向坐标 
                texture.Width, //框的宽度(材质宽度即可)
                texture.Height / Main.projFrames[Type]//框的高度（用材质高度除以帧数得到单帧高度）
                );

            Color MyColor = Color.White;
            int x_move = 40;
            if(player.direction == 1)
            {
                x_move = -40;
            }

            Main.EntitySpriteDraw(  //entityspritedraw是弹幕，NPC等常用的绘制方法
                texture,//第一个参数是材质
                player.Center - Main.screenPosition,//注意，绘制时的位置是以屏幕左上角为0点
                                                        //因此要用弹幕世界坐标减去屏幕左上角的坐标
                rectangle,//第三个参数就是帧图选框了
                MyColor,//第四个参数是颜色，这里我们用自带的lightcolor，可以受到自然光照影响
                Projectile.rotation,//第五个参数是贴图旋转方向
                new Vector2(texture.Width / 2+ x_move, texture.Height / 2 / Main.projFrames[Type]),
                //第六个参数是贴图参照原点的坐标，这里写为贴图单帧的中心坐标，这样旋转和缩放都是围绕中心
                new Vector2(2.7f, 2.9f),//第七个参数是缩放，X是水平倍率，Y是竖直倍率
                player.direction  == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                //第八个参数是设置图片翻转效果，需要手动判定并设置spriteeffects
                0//第九个参数是绘制层级，但填0就行了，不太好使
                );

            return false;//return false阻止自动绘制
        }
    }


}
