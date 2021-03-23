using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.Actors;

namespace Game.HelpingClass
{
    static class CollideDetect
    {
        public static bool TestMoveCollide(IMovable obj1, ISolid obj2) 
        {
            if (!(obj1 is ISolid))
                return false;

            PointF test_pos =  PointOp.Sum(
                (obj1 as AbstrUnit).Pos,
                PointOp.Mul(obj1.Direction, obj1.Speed));

            return TestCollide(obj1 as ISolid, obj2, test_pos);
        }

        public static bool TestCollide(ISolid obj1, ISolid obj2, PointF p1 = new PointF(), PointF p2 = new PointF()) 
        {
            CollideZone c1 = obj1.Shape.GetCollideZone();
            CollideZone c2 = obj1.Shape.GetCollideZone();

            PointF pos1 = p1 == new PointF(0,0) ? 
                (obj1 as AbstrUnit).Pos : p1;

            PointF pos2 = p2 == new PointF(0, 0) ?
                (obj2 as AbstrUnit).Pos : p2;

            

            if (!(obj1.Shape.IsActive & obj2.Shape.IsActive) ||
                (PointOp.lenght(PointOp.Sub(pos1, pos2)) > c1.R + c2.R))
                return false;



            if ((obj1.Shape is SquareShape) && (obj1.Shape is SquareShape)) 
            {
                float edge1 = (obj1.Shape as SquareShape).Edge;
                float edge2 = (obj2.Shape as SquareShape).Edge;

                if (Math.Abs(pos1.X - pos2.X) <= (edge1 / 2 + edge2 / 2) &&
                    Math.Abs(pos1.Y - pos2.Y) <= (edge1 / 2 + edge2 / 2))
                    return true;
            }
            return false;
        }

        private static bool check_square_shape_collide(SquareShape s1, SquareShape s2)
        {
            return false;
        }
    }
}
