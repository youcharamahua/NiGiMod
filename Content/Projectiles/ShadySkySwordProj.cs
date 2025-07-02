using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NiGiMod.Content.DamageClasses;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NiGiMod.Content.Projectiles
{
    public class ShadySkySwordProj : ModProjectile
    {
        Player player => Main.player[Projectile.owner];
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            ProjectileID.Sets.TrailCacheLength[Type] = 15;//拖尾长度
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;//拖尾模式
            Projectile.width = 99;
            Projectile.height = 99;
            Projectile.friendly = true;//友方弹幕                                          
            Projectile.tileCollide = false;//false就能让他穿墙
            Projectile.timeLeft = 60;//消散时间
            Projectile.aiStyle = -1;//不使用原版AI
            Projectile.DamageType = ModContent.GetInstance<NiGiDamageClass>();
            Projectile.penetrate = -1;//表示能穿透几次,-1代表无限
            Projectile.ignoreWater = true;//无视液体
            Projectile.hide = true; // 隐藏
            base.SetDefaults();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float Length = (100 * Projectile.scale * Projectile.ai[0])+90;//定义剑的长度
            //这个函数用于控制弹幕碰撞判断，符合你的碰撞条件时返回真即可
            float point = 0f;//这个照抄就行
            Vector2 startPoint = player.Center;//判定从玩家中心开始延伸
            Vector2 endPoint = player.Center + Projectile.rotation.ToRotationVector2() * Length;//从玩家处延伸到指定长度的末端
            bool K =
                Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), //对方碰撞箱的位置
                targetHitbox.Size(),//对方碰撞箱的大小 
                startPoint,//线形碰撞箱起始点 
                endPoint,//结束点
                10 * Projectile.scale//线的宽度
                , ref point);
            if (K) return true;//如果满足这个碰撞判断，返回真，也就是进行碰撞伤害
            return base.Colliding(projHitbox, targetHitbox);
        }
        public override void AI()
        {
            Projectile.timeLeft = 2;//我们不以timeleft作为弹幕消失要求,因此需要始终维持timeleft
            float rotatespeed = Projectile.ai[1];//我们生成这个弹幕时，传入ai1作为挥舞速度
            player.heldProj = Projectile.whoAmI; // 持有这个弹幕
            player.itemTime = player.itemAnimation = 2;//维持住玩家的使用
            //我们在发射这个弹幕时给ai0传入-1或1（玩家朝着右边挥舞就传入1，朝着左边就传入-1，因为这是不对称的武器）
            Projectile.rotation += Projectile.ai[0] * rotatespeed;//让这个弹幕顺时针/逆时针转，取决于弹幕生成出来时向左还是向右
                                                                  // Main.NewText(Projectile.localAI[0]);
            Projectile.localAI[0] += rotatespeed;//让弹幕的一个变量每帧加上这个角速度，用以判断武器挥舞到什么程度了
            float maxRotate = 4.5f;//定义武器最大能挥舞到什么程度
            if (Math.Abs(Projectile.localAI[0]) > maxRotate)//如果挥了这么多，就kill掉
            {
                Projectile.Kill();
            }
            player.itemRotation = Projectile.rotation;//控制玩家手臂
            Projectile.Center = player.Center;//弹幕固定在玩家中心

            // 新增粒子生成代码（每帧执行）
            if (Main.netMode != NetmodeID.Server) // 确保只在客户端生成
            {
                // 控制粒子生成频率（每5帧生成一次）
                if (Main.rand.NextBool(5))
                {
                    // 计算随机位置
                    Vector2 randomOffset = Main.rand.NextVector2CircularEdge(100f, 100f);
                    Vector2 spawnPosition = Projectile.Center + randomOffset;

                    // 创建动态运动向量（带随机方向和速度）
                    Vector2 movement = new Vector2(
                        Main.rand.NextFloat(-3f, 3f),
                        Main.rand.NextFloat(-2f, 0f)
                    );

                    // 生成粒子效果
                    ParticleOrchestrator.RequestParticleSpawn(
                        clientOnly: true,
                        ParticleOrchestraType.StardustPunch,
                        new ParticleOrchestraSettings
                        {
                            PositionInWorld = spawnPosition,
                            MovementVector = movement,
                            UniqueInfoPiece = Main.rand.Next(30, 90) // 添加随机旋转
                        });
                }

            }
            base.AI();
        }

        public override bool ShouldUpdatePosition()//禁止弹幕因为速度更新位置，说白了就是禁用速度
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)//重写predraw，不使用原版绘制，自己写绘制非常自由
        {
            float rangeFix = 95 * Projectile.scale;
            Texture2D weapon = TextureAssets.Projectile[Type].Value;

            List<CustomVertexInfo> vertices = new List<CustomVertexInfo>();//声明一个顶点结构体的list，顶点结构体需要自己写，本mod自带一份，可以拿来用
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                float hue = (float)(0.75f +
                    Math.Sin(Main.GlobalTimeWrappedHourly * 0.8f + i * 0.05f) * 0.05f +
                    Math.Cos(i * 0.03f) * 0.03f
                );
                Color coordColor = Color.Lerp(
                    Main.hslToRgb(hue, 0.9f, 0.6f),
                    Color.DeepSkyBlue,
                    0.3f
                ) * (1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.2f) ;



                if (Projectile.ai[0] == 1)
                {
                    

                    vertices.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition + rangeFix * Projectile.oldRot[i].ToRotationVector2() * 1.9f,
                      new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 1, 1), coordColor));//上底
                    vertices.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition + rangeFix * Projectile.oldRot[i].ToRotationVector2() * 0.25f,
                        new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 0, 1), coordColor));//下底

                }
                else
                {
                    vertices.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition - rangeFix * Projectile.oldRot[i].ToRotationVector2() * 1.9f,
                                       new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 1, 1), coordColor));//上底
                    vertices.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition - rangeFix * Projectile.oldRot[i].ToRotationVector2() * 0.25f,
                        new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 0, 1), coordColor));//下底

                }
            }
            SpriteBatch spriteBatch = Main.spriteBatch;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("NiGiMod/Images/SlashTex").Value;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices.ToArray(), 0, vertices.Count - 2);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            //第二层
            float rangeFix2 = 65 * Projectile.scale;
            List<CustomVertexInfo> vertices2 = new List<CustomVertexInfo>();//声明一个顶点结构体的list，顶点结构体需要自己写，本mod自带一份，可以拿来用
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Type]; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;

                // 核心参数
                float time = Main.GlobalTimeWrappedHourly * 2f;
                float hue = 0.78f + (float)Math.Sin(time * 0.5f) * 0.05f; // 基准紫色区域
                float sat = 0.85f + (float)Math.Cos(time * 3f) * 0.1f;    // 饱和度波动
                float lum = 0.55f + (float)Math.Sin(time * 4f) * 0.15f;   // 亮度波动

                // 添加随机星光效果
                if (Main.rand.NextFloat() < 0.15f)
                {
                    lum += Main.rand.NextFloat(0.2f, 0.4f);
                    sat *= 1.2f;
                }

                Color coordColor = Main.hslToRgb(hue, sat, lum);

                // 叠加蓝色通道增强
                coordColor = new Color(
                    (coordColor.R * 0.8f + 50) / 255f,
                    (coordColor.G * 0.6f + 30) / 255f,
                    (coordColor.B * 1.2f + 80) / 255f,
                    coordColor.A
                );

                if (Projectile.ai[0] == 1)
                {


                    vertices2.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition + rangeFix2 * Projectile.oldRot[i].ToRotationVector2() * 1.9f,
                      new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 1, 1), coordColor));//上底
                    vertices2.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition + rangeFix2 * Projectile.oldRot[i].ToRotationVector2() * 0.25f,
                        new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 0, 1), coordColor));//下底

                }
                else
                {
                    vertices2.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition - rangeFix2 * Projectile.oldRot[i].ToRotationVector2() * 1.9f,
                                       new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 1, 1), coordColor));//上底
                    vertices2.Add(new CustomVertexInfo(Projectile.Center - Main.screenPosition - rangeFix2 * Projectile.oldRot[i].ToRotationVector2() * 0.25f,
                        new Vector3((float)i / ProjectileID.Sets.TrailCacheLength[Type], 0, 1), coordColor));//下底

                }
            }
            SpriteBatch spriteBatch2 = Main.spriteBatch;
            spriteBatch2.End();
            spriteBatch2.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("NiGiMod/Images/SlashTex").Value;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices2.ToArray(), 0, vertices2.Count - 2);
            spriteBatch2.End();
            spriteBatch2.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //为了让弹幕具有打击感，我们还要在击中函数中写一次生成爆炸的弹幕
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.StardustPunch, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Center,
                MovementVector = Vector2.Zero
            });

        }
    }
    
}