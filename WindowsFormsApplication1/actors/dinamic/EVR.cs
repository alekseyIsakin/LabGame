using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.HelpingClass;

namespace Game.Actors
{
    class EVR : AbstrUnit, IMovable, ISolid, IDrawable
    {
        readonly List<PointF> Directs = new List<PointF>();
        int curdir = 0;

        public AbstrShape Shape { get; private set; }
        public float Speed { get; private set; }

        public PointF Direction { get; private set; }

        public AbstrSprite Sprite { get; private set; }

        public void Set_Direction(PointF d)
        { Direction = d; }

        public void Set_Speed(float spd)
        { Speed = spd; }

        public void Set_Sprite(AbstrSprite spr)
        { Sprite = spr; }

        public void Set_Shape(AbstrShape sp)
        { Shape = sp; }

        public EVR() 
        {
            Directs.Add(new PointF(1, 0));
            Directs.Add(new PointF(0, 1));
            Directs.Add(new PointF(-1, 0));
            Directs.Add(new PointF(0, -1));

            Pos = new PointF();
            Shape = new SquareShape(0.6f);
            Sprite = new SquareSprite(default(SizeF));
            Direction = new PointF(1,0);

            Speed = 0.01f;
        }

        public void Move() 
        { Move(Direction); }

        public void Move(PointF direct) 
        {
            PointF tmp_pos = new PointF(0, 0);

            tmp_pos.X = Pos.X + direct.X * Speed;
            tmp_pos.Y = Pos.Y + direct.Y * Speed;

            Pos = tmp_pos;
        }

        public void ChangeDir()
        {
            curdir += 1;
            curdir = curdir % 4;
            Direction = Directs[curdir];
        }

        public void Draw(Graphics gr)
        { Sprite.Draw(gr, this.Pos); }
        public void Draw(Graphics gr, PointF pos, SizeF sz)
        { Sprite.Draw(gr, pos); }
    }
}
