using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace NiGiMod.Content.BackGrounds
{
    public class ShadySkyBackground : CustomSky
    {
        private bool _isActive;
        private float _fadeOpacity; // 用于淡入淡出效果

        // 当玩家手持武器时激活天空
        public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
            _fadeOpacity = 0f; // 初始透明度为0
        }

        public override void Deactivate(params object[] args)
        {
            _isActive = false;
        }

        public override void Reset()
        {
            _isActive = false;
            _fadeOpacity = 0f;
        }

        public override bool IsActive()
        {
            return _isActive || _fadeOpacity > 0f; // 当淡出未完成时保持激活
        }

        public override void Update(GameTime gameTime)
        {
            const float fadeSpeed = 0.05f;

            if (_isActive && _fadeOpacity < 1f)
            {
                _fadeOpacity += fadeSpeed; // 淡入效果
            }
            else if (!_isActive && _fadeOpacity > 0f)
            {
                _fadeOpacity -= fadeSpeed; // 淡出效果
            }

            _fadeOpacity = MathHelper.Clamp(_fadeOpacity, 0f, 1f);
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                // 绘制背景（在默认天空之后）
                Texture2D skyTexture = ModContent.Request<Texture2D>("NiGiMod/Content/BackGrounds/snightBG").Value;

                // 获取屏幕尺寸并平铺纹理
                int width = Main.screenWidth;
                int height = Main.screenHeight;

                spriteBatch.Draw(
                    skyTexture,
                    new Rectangle(0, 0, width, height),
                    new Color(255, 255, 255) * _fadeOpacity // 应用淡入淡出透明度
                );
            }
        }

        public override float GetCloudAlpha()
        {
            return 0f; // 隐藏原版云朵
        }
    }
}