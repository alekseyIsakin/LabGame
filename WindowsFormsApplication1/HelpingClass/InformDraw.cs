using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game.HelpingClass
{
    class InformDraw : Game.Actors.AbstrUnit
    {
        private readonly Font f1 = new Font(FontFamily.GenericSansSerif, 16);
        private readonly Font f2 = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Italic);


        public int Score { get; private set; }
        
        private int _nonBurnScore = 0;
        public int NonBurnScore {
            get { return _nonBurnScore; }
            private set 
            {
                if (value < 0)
                    _nonBurnScore = 0;
                else
                    _nonBurnScore = value;
            }
        }

        public TimeSpan Time { get; private set; }
        private TimeSpan TimeStorage = new TimeSpan();
        private TimeSpan StartTime;
        public bool idle { get; private set; }

        public InformDraw() 
        {
            idle = false;
            Reset();
        }

        public void AddScore(int scr = 1) { Score += scr; }
        public void AddSolidScore(int scr = 1) { NonBurnScore += scr; }

        public void SolidScore() 
        { 
            NonBurnScore = Score;
            Score = 0;
        }

        public void ClearScore() { Score = 0; }

        /// <summary>
        /// Обновляем время забега
        /// </summary>
        public void Step() 
        {
            if (idle)
                Time = TimeStorage + DateTime.Now.TimeOfDay - StartTime;
        }

        public void Start() 
        {
            StartTime = DateTime.Now.TimeOfDay;
            idle = true;
        }

        public void Reset()
        { 
            Score = 0;
            idle = false;
            StartTime = DateTime.Now.TimeOfDay;
            Time = new TimeSpan();
        }

        public void Pause() 
        {
            Score = 0;
            idle = false;
            TimeStorage = Time;
        }

        public void Draw(System.Drawing.Graphics gr)
        {
            gr.DrawString(GetString(), f2, Brushes.Black, Pos);
            gr.DrawString(GetString(), f1, Brushes.White, Pos);
        }

        public void Draw(Graphics gr, PointF pos, SizeF sz=new SizeF())
        {
            gr.DrawString(GetString(), new Font(FontFamily.GenericSansSerif, 16, FontStyle.Italic), Brushes.Black, this.Pos);
            gr.DrawString(GetString(), new Font(FontFamily.GenericSansSerif, 16), Brushes.White, this.Pos);
        }

        public string GetString()
        {
            string paramet = @"ss\:ffff";
            if (Time.Minutes > 0)
                paramet = @"mm\:" + paramet;
            if (Time.Hours > 0)
                paramet = @"hh\:" + paramet;

            return String.Format("score: {0}, time: {1}", NonBurnScore + Score, Time.ToString(paramet));
        }
    }
}
