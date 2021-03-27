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
        enum GameUnits 
        {
            WALL,
            BASE_ENEMY,
            COIN,
            EXIT,
            DMG,
            DECAL
        }

        public static HashSet<AbstrUnit> Units { get; private set; }

        static Factory() 
        {
            Units = new HashSet<AbstrUnit>();
        }
        public static void ClearUnits()
        { Units.Clear(); }

        public static void RegistrNewUnit(AbstrUnit unit)
        { 
            if (unit != null)
            { Units.Add(unit); }
        }

        public static void SetWall(PointF pos, Bitmap img = null)
        { Units.Add(GetWall(pos, img)); }

        public static void SetHiddenWall(PointF pos)
        { Units.Add(GetHiddenWall(pos)); }

        public static void SetBaseEnemy(PointF pos)
        { 
            EVR enemy = GetBaseEnemy(pos);
            enemy.area.SetSprite(new SquareSprite(MainGame.CellSize,fill:false));
            Units.Add(enemy);
            Units.Add(enemy.area);
        }

        public static void SetCoin(PointF pos, Bitmap img = null)
        { Units.Add(GetCoin(pos,img)); }

        public static void SetExitArea(PointF pos, Bitmap img = null)
        { Units.Add(GetExitArea(pos, img)); }

        public static void SetDmgArea(PointF pos, Bitmap img = null, float scaleArea = 0.75f)
        { Units.Add(GetDmgArea(pos, img, scaleArea)); }

        public static void SetDecal(PointF pos, Bitmap img = null)
        { Units.Add(GetDecal(pos, img)); }

        public static Wall GetWall(PointF pos, Bitmap img = null) 
        {
            return new Wall(
                pos,
                img ?? Properties.Resources.wall);
        }

        public static HiddenWall GetHiddenWall(PointF pos)
        {
            return new HiddenWall(pos);
        }
        public static EVR GetBaseEnemy(PointF pos)
        {
            return new EVR(
                pos,
                GetDmgArea(pos),
                scaleArea: 0.7f, scaleSprite: 0.7f);
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
        public static Area GetDmgArea(PointF pos, Bitmap img = null, float scaleArea=1f) 
        {
            return new Area(
                pos,
                Areas.Dmg, 
                img ?? Properties.Resources.dmg,
                scaleArea: scaleArea);
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
