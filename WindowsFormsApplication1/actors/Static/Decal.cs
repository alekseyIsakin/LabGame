using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.HelpingClass;

namespace Game.Actors.Static
{
    /// <summary>
    /// Объект, только для отрисовки изображений
    /// </summary>
    class Decal : AbstrUnit, IDrawable
    {
        public AbstrSprite Sprite { get; private set; }
        public Decal(PointF pos, Image sprite, float scaleSprite=1.05f)
        {
            this.Pos = pos;
            Sprite = new ImageSprite(sprite, PointOp.Mul(MainGame.CellSize,scaleSprite));
        }

        public void Draw(Graphics gr)
        { Sprite.Draw(gr, this.Pos); }
        public void Draw(Graphics gr, PointF pos,  SizeF sz)
        { Sprite.Draw(gr, pos); }
    }
}
