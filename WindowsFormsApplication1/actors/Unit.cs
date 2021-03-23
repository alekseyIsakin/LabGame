using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.HelpingClass;

namespace Game.Actors
{
    /// <summary>
    /// Базовый класс
    /// Описывает позицию элемента
    /// </summary>
    public abstract class AbstrUnit
    {
        protected PointF _pos;
        public PointF Pos 
        {
            get { return _pos; } 
            protected set { _pos = value; } 
        }

        public void Set_Pos(PointF new_pos)
        { Pos = new_pos; }
    }

    /// <summary>
    /// Интерфейс движения
    /// содержит поля
    /// <value>Speed {get}</value>
    /// Скорость передвижения
    /// <value>Direction {get}</value>
    /// Направление
    /// <value>Move</value>
    /// Метод движения
    /// </summary>
    interface IMovable 
    {
        float Speed { get; }
        PointF Direction { get; }
        void Move();
    }

    /// <summary>
    /// Интерфейс для тел, способных
    /// считать столкновение
    /// </summary>
    interface ISolid 
    { AbstrShape Shape { get; } }

    /// <summary>
    /// Интерфейс для отрисовки объекта
    /// </summary>
    public interface IDrawable 
    {
        AbstrSprite Sprite {get;}
        void Draw(Graphics gr);
        void Draw(Graphics gr, PointF pos, SizeF sz);
    }
}
