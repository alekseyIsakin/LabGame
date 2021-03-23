using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.HelpingClass
{
    // Зона в которой будут считываться столкновения
    struct CollideZone 
    { public float R; }

    abstract class AbstrShape
    {
        public bool IsActive { get; protected set; }
        public void SetActive(bool b)
        { IsActive = b; }
        abstract public CollideZone GetCollideZone();
        public AbstrShape() 
        { IsActive = true; }
    }

    class SquareShape : AbstrShape
    {
        public readonly float Edge;
        public SquareShape(float ed = 1) 
        {
            Edge = ed;
        }

        override public CollideZone GetCollideZone()
        {
            return new CollideZone { R = 0.5f};
        }
    }
}
