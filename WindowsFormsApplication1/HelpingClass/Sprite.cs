using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game.HelpingClass
{
    public abstract class AbstrSprite : IDisposable
    {
        protected SizeF Size; 
        public bool isDispose { get; protected set; }

        public AbstrSprite(SizeF sz) 
        {
            this.Size = sz;
        }

        public abstract void Draw(Graphics gr, PointF pos);
        public abstract void Dispose();
        
        public void SetSize(SizeF sz)
        { this.Size = sz; }
        protected void checkDispose()
        { if (isDispose) throw new Exception("Объект удалён"); }
    }

    ///<summary>
    /// Класс позволяет отрисовывать изображение
    ///<summary>
    public class ImageSprite : AbstrSprite
    {
        Image Img;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img">Картинка</param>
        /// <param name="size">Размер, в котром изображение будет отрисованно</param>
        public ImageSprite(Image img, SizeF size) : base(size)
        { this.Img = img; }

        public override void Draw(Graphics gr, PointF pos)
        {
            checkDispose();
            gr.DrawImage(Img, new RectangleF(pos.X - Size.Width / 2, pos.Y - Size.Height / 2, Size.Width, Size.Height));
            
#if DEBUG
            gr.DrawEllipse(Pens.Red, pos.X - Size.Width / 2, pos.Y - Size.Height / 2, Size.Width, Size.Height);
            gr.DrawEllipse(Pens.Red, pos.X, pos.Y, 1, 1);
#endif
        }
        public override void Dispose() 
        {
            if (isDispose) return;
            Img.Dispose();
            isDispose = true;
        }
        
    }

    /// <summary>
    /// Класс позволяет отрисовывать заполненый квадрат
    /// </summary>
    public class SquareSprite : AbstrSprite
    {
        Brush brush;
        public SquareSprite(SizeF size, Brush brush = null)
            : base(size)
        {
            this.brush = brush ?? Brushes.Yellow;
        }

        public override void Draw(Graphics gr, PointF pos)
        {
            checkDispose();
            gr.FillRectangle(brush, pos.X - Size.Width / 2, pos.Y - Size.Height / 2, Size.Width, Size.Height);
#if DEBUG
            gr.DrawEllipse(Pens.Red, pos.X, pos.Y, 1, 1);
#endif
        }
        public override void Dispose() 
        {
            if (isDispose) return;
            isDispose = true;
        }

    }
}
