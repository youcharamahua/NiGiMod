using NiGiMod.Content.BackGrounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace NiGiMod.Common.Systems
{
    class SkySystem : ModSystem
    {
        private static bool IsSkyKeyUnloaded = false;
        private const string ShadySkyKey = "ShadySky";
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                // 必须确保注册名称与激活时完全一致
                SkyManager.Instance[ShadySkyKey] = new ShadySkyBackground();
            }
        }

        public override void Unload()
        {
            if (IsSkyKeyUnloaded)return;
            try
            {
                // 仅在天空管理器仍存在时操作
                if (SkyManager.Instance != null)
                {
                    // 停用并移除天空
                    if (SkyManager.Instance[ShadySkyKey] != null)
                    {
                        SkyManager.Instance.Deactivate(ShadySkyKey);
                        SkyManager.Instance[ShadySkyKey] = null;
                    }
                }
            }
            catch (Exception ex)
            {}
            finally
            {
                IsSkyKeyUnloaded = true;
            }
        }
    }

}