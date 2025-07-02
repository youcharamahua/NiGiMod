using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using NiGiMod.Content.Items;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ObjectData;
using static Terraria.ID.ContentSamples.CreativeHelper;
using Terraria.Localization;

namespace NiGiMod.Content.Tiles
{
    class NiGiSkyTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileTable[Type] = false; 
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;//帧对齐
            Main.tileCut[Type] = false;
            Main.tileBlockLight[Type] = true;
            MineResist = 3f;//挖掘速度
            MinPick = 70;//最小镐力
            AddMapEntry(new Color(138, 43, 226), Language.GetText("空影裁决之冢"));
            DustType = 84;

            Main.tileLighted[Type] = true;

            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.DrawYOffset = 6;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            // Additional edits here, such as lava immunity, alternate placements, and subtiles
            TileObjectData.addTile(Type);

        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0.3f;
            b = 0.9f;

        }

    }
}
