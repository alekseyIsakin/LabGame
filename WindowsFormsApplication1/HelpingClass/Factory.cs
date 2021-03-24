using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.Actors;
using Game.Actors.Static;
using Game.Actors.Dinamic;

namespace Game.HelpingClass
{
    static class Factory
    {
        public static Wall GetWall(PointF pos, Bitmap img = null) 
        {
            return new Wall(
                pos,
                img ?? Properties.Resources.wall);
        }
        public static Area GetCoin(PointF pos, Bitmap img = null) 
        {
            return new Area(
                pos,
                Areas.Coin, img ?? Properties.Resources.coin,
                scaleArea: 0.5f, scaleSprite: 0.5f);
        }
        public static Area GetExitArea(PointF pos, Bitmap img = null) 
        {
            return new Area(
                pos,
                Areas.Exit,
                img ?? Properties.Resources.exit,
                scaleArea: 0.2f);
        }
        public static Area GetDmgArea(PointF pos, Bitmap img = null) 
        {
            return new Area(
                pos,
                Areas.Dmg, 
                img ?? Properties.Resources.dmg,
                scaleArea: 0.75f);
        }
        public static Decal GetDecal(PointF pos, Bitmap img = null) 
        {
            return new Decal(
                pos,
                img ?? Properties.Resources.decal1);
        }
        // static Wall GetWall(PointF pos) { }
    
    }
}
