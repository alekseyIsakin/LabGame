using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game.HelpingClass
{
    public static class PointOp
    {
        public static PointF normalize(ref PointF p1) 
        {
            float len = lenght(p1);

            p1.X = p1.X / len;
            p1.Y = p1.Y / len;
            
            return p1;
        }
        public static float lenght(PointF p1) 
        {
            float f_out = (float)Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y);
            return f_out;
        }

        public static PointF Sum(PointF p1, PointF p2)
        { return new PointF(p1.X + p2.X, p1.Y + p2.Y); }

        public static PointF Sub(PointF p1, PointF p2)
        { return new PointF(p1.X - p2.X, p1.Y - p2.Y); }

        public static PointF Mul(PointF p1, PointF p2)
        { return new PointF(p1.X * p2.X, p1.Y * p2.Y); }

        public static PointF Mul(PointF p1, float f)
        { return new PointF(p1.X * f, p1.Y * f); }

        public static PointF Div(PointF p1, float f)
        { return new PointF(p1.X / f, p1.Y / f); }

        public static SizeF Div(SizeF p1, float f)
        { return new SizeF(p1.Width / f, p1.Height / f); }

        public static SizeF Mul(SizeF p1, SizeF p2)
        { return new SizeF(p1.Width * p2.Width, p1.Height * p2.Height); }

        public static SizeF Mul(SizeF p1, float f)
        { return new SizeF(p1.Width * f, p1.Height * f); }
        
    }
}
