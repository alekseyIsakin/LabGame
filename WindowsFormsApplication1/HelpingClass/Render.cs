using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.Actors;

namespace Game.HelpingClass
{
    /// <summary>
    /// Отрисовывает объекты на холсте
    /// </summary>
    static class Render
    {
        /// <summary>
        /// Смещение камеры, относительно центра
        /// </summary>
        static PointF Offset = new PointF();

        /// <summary>
        /// Размер холста
        /// </summary>
        static SizeF Screen = new SizeF();

        public static void SetOffset(PointF offs)
        { Offset = offs; }
        public static void SetScreen(SizeF scr)
        { Screen = scr; }
        public static void DrawAll(Graphics gr)
        {
            foreach (var unit in Factory.Units.Where(u => (u is IDrawable) && !(u is Game.Actors.Static.Area)))
            { Draw(gr, unit as IDrawable); }
            foreach (var unit in Factory.Units.Where(u => (u is IDrawable) && (u is Game.Actors.Static.Area)))
            { Draw(gr, unit as IDrawable); }
        }
        public static void Draw(Graphics gr, IDrawable obj, bool always = false) 
        {
            PointF pointDraw = PointOp.Sub(
                (obj as AbstrUnit).Pos, Offset-Screen);

            if (!always && (
                pointDraw.X < -1 || pointDraw.X > Screen.Width * 2 + 1 ||
                pointDraw.Y < -1 || pointDraw.Y > Screen.Height * 2 + 1))
                return;

            if (obj is AbstrUnit)
            {
                obj.Draw(gr,
                    PointOp.Mul(pointDraw, MainGame.CellEdge), MainGame.CellSize);
            }
            else 
            { obj.Draw(gr); }
        }
    }
}
