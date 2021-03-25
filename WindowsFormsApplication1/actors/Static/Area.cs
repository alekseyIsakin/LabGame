using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.HelpingClass;

namespace Game.Actors.Static
{
    enum Areas 
    {
        Exit,
        Coin,
        Dmg
    }

    /// <summary>
    /// Объект, который реагирует на столкновения
    /// но не препятсвует движению
    /// </summary>
    class Area : AbstrUnit, ISolid, IDrawable, IDisposable
    {
        public AbstrShape Shape { get; private set; }
        public AbstrSprite Sprite { get; private set; }
        public readonly Areas Type;
        public bool isDispose { get; private set; }

        public Area(PointF pos, Areas type, Image sprite = null, float scaleArea = 1, float scaleSprite = 1.05f)
        {
            this.Pos = pos;
            this.Type = type;
            Shape = new SquareShape(scaleArea);
            isDispose = false;

            if (sprite != null)
                Sprite = new ImageSprite(sprite, PointOp.Mul(MainGame.CellSize, scaleSprite));
        }

        public void SetSprite(AbstrSprite sprite) 
        { this.Sprite = sprite; }

        public void Draw(Graphics gr)
        {
            checkDispose();
            if (Sprite != null) Sprite.Draw(gr, this.Pos); 
        }
        public void Draw(Graphics gr, PointF pos, SizeF sz)
        {
            checkDispose();
            if (Sprite != null) Sprite.Draw(gr, pos); 
        }
        public void Dispose() 
        {
            if (isDispose) return;

            Sprite.Dispose();
         
            Shape = null;
            Sprite = null;
        
            isDispose = true;
        }

        private void checkDispose()
        { if (isDispose) throw new Exception("Объект удалён"); }
    }
}
