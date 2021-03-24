using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Actors;
using Game.HelpingClass;

namespace Game.Actors.Static
{
    /// <summary>
    /// Препятсвует движению
    /// </summary>
	class Wall : AbstrUnit, IDrawable, ISolid
	{
        public AbstrShape Shape { get; protected set; }
        public AbstrSprite Sprite {get; protected set;}

        public Wall(System.Drawing.PointF pos, System.Drawing.Bitmap img = null)
        {
            this.Pos = pos;
            this.Sprite = new ImageSprite(
                img ?? Properties.Resources.wall, 
                Game.HelpingClass.PointOp.Mul(MainGame.CellSize, 1.05f));
            this.Shape = new SquareShape();
        }

        public void Draw(System.Drawing.Graphics gr)
        { Sprite.Draw(gr, this.Pos); }
        public void Draw(System.Drawing.Graphics gr, System.Drawing.PointF pos, System.Drawing.SizeF sz)
        { Sprite.Draw(gr, pos); }
	}
}
