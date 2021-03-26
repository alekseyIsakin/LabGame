using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Game.Actors;
using Game.HelpingClass;
using Game.Actors.Dinamic;
using Game.Actors.Static;

namespace Game
{
    class MainGame
    {
        Color bg = Color.Gray; 
        bool GameIdle = true;
        readonly List<Bitmap> Maps;
        int curMap = 0;

        public static float CellEdge {get; private set;}
        public static SizeF CellSize{get; private set;}

        Actor actor;
        InformDraw Inform;

        List<AbstrUnit> Units;

        PointF StartPos = new PointF(0,0);
        PointF ExitPos = CellSize.ToPointF();

        static MainGame() 
        {
            CellEdge = 48;
            CellSize = new SizeF(CellEdge, CellEdge);
        }

        public MainGame()
        {
            Units = new List<AbstrUnit>();

            Maps = new List<Bitmap>();

            Maps.Add( Properties.Resources.map);
            Maps.Add( Properties.Resources.map2);
            Maps.Add( Properties.Resources.map3);

            GameReset();
        }

        public void GameStop()
        { GameIdle = false; Inform.SolidScore(); }
        public void GameRun()
        { GameIdle = true; }

        /// <summary>
        /// Перезапуск игры
        /// </summary>
        public void GameReset() 
        {
            Factory.Units.Clear();

            Inform = new InformDraw();

            LoadMap(curMap);
        }

        public void Step() 
        {
            if (!GameIdle)
                return;

            List<AbstrUnit> ToRemoveList = new List<AbstrUnit>();
            Inform.Step();

            foreach (var unit in Factory.GetMovableUnits())
            {
                if (!GameIdle)
                { break; }

                if (unit is IMovable)
                {
                    bool move = true;
                    foreach (var obj in Factory.GetSolidUnits())
                    {
                        if (unit is ISolid && !(obj == unit))
                        {
                            if (obj is Area && unit is Actor)
                            {
                                if (CollideDetect.TestCollide((unit as ISolid), obj as ISolid))
                                {
                                    switch ((obj as Area).Type)
                                    {
                                        case Areas.Exit:
                                            GameStop();
                                            curMap += 1;
                                            System.Windows.Forms.MessageBox.Show("Win!!!\n" + Inform.GetString());
                                            break;

                                        case Areas.Coin:
                                            (obj as Area).Dispose();
                                            Inform.AddScore();
                                            ToRemoveList.Add(obj);
                                            break;

                                        case Areas.Dmg:
                                            Inform.AddSolidScore(-1);
                                            Inform.ClearScore();
                                            GameStop();
                                            break;
                                    }
                                }
                            }

                            else
                                if (!(obj is Area) && CollideDetect.TestMoveCollide((unit as IMovable), obj as ISolid))
                                { move = false; }
                        }
                    }

                    if (move)
                        (unit as IMovable).Move();
                    if (!move && unit is EVR)
                        (unit as EVR).ChangeDir();
                }
            }

            if (ToRemoveList.Count > 0) 
            {
                foreach (var obj in ToRemoveList)
                { Factory.Units.Remove(obj); }
                ToRemoveList.Clear();
            }

            if (!GameIdle)
            {
                if (curMap < Maps.Count)
                {
                    Inform.Pause();
                    LoadMap(curMap);
                }
                else
                    System.Windows.Forms.MessageBox.Show("fin");
            }
        }

        public void Redraw(Graphics gr) 
        {
            long tm1 = DateTime.Now.Millisecond;

            gr.Clear(bg);

            Render.SetOffset(actor.Pos);
            Render.Draw(actor, gr);
            
            foreach (var unit in Factory.Units) 
            {
                if (unit is IDrawable && !(unit is Actor)) 
                {
                    Render.Draw(unit as IDrawable, gr);
                }
            }
            Inform.Draw(gr);
#if DEBUG
            gr.DrawString(string.Format("pos: {0}, spd: {1}\n time: {2}", actor.Pos.ToString(), actor.Speed, DateTime.Now.Millisecond- tm1 ),
                new Font(FontFamily.GenericSansSerif, 16), Brushes.White, new PointF(0, 16));
#endif
        }

        public void LoadMap(int curMap) 
        {
            Factory.Units.Clear();
            Bitmap bmp = Maps[curMap];

            for (int y = 0; y < bmp.Height; y++)
            { 
                Factory.SetHiddenWall(new PointF(-1, y));
                Factory.SetHiddenWall(new PointF(bmp.Height, y));
            }
            for (int x = 0; x < bmp.Width; x++)
            {
                Factory.SetHiddenWall(new PointF(x, -1));
                Factory.SetHiddenWall(new PointF(x, bmp.Width));
            }

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    string cell = bmp.GetPixel(x, y).Name;
                    PointF CellPos = new PointF(x,y);

                    if (x == 0 && y == 0)
                    { cell = bmp.GetPixel(0, 1).Name; }

                    switch (cell)
                    {
                        case "ff000000":
                            Factory.SetWall(CellPos);
                            break;
                        case "ffff0000":
                            StartPos = CellPos;
                            break;
                        case "ff00ff00":
                            Factory.SetExitArea(CellPos);
                            break;
                        case "ffffff00":
                            Factory.SetBaseEnemy(CellPos);
                            break;
                        case "ff0000ff":
                            Factory.SetDecal(CellPos);
                            break;
                        case "ffff00ff":
                            Factory.SetDmgArea(CellPos);
                            break;
                    }
                }
            }
            bg = bmp.GetPixel(0, 0);

            Inform.Set_Pos(new PointF());

            actor = new Actor();
            actor.Set_Pos(StartPos);
            actor.Set_Sprite(new SquareSprite(PointOp.Mul( CellSize, 0.5f)));
            actor.Set_Shape(new SquareShape(0.5f));
            actor.Set_Speed(0.12f);

            Factory.RegistrNewUnit(actor);

            GameIdle = true;
        }

        public void KeyDown(System.Windows.Forms.KeyEventArgs e) 
        {
            if (!Inform.idle)
            { Inform.Start(); }

            PointF dir = new PointF();

            if (e.KeyCode == System.Windows.Forms.Keys.Left)
            { dir.X = -1;}
            if (e.KeyCode == System.Windows.Forms.Keys.Right)
            { dir.X = 1;}
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
            { dir.Y = 1;}
            if (e.KeyCode == System.Windows.Forms.Keys.Up)
            { dir.Y = -1; }

            if (e.KeyCode == System.Windows.Forms.Keys.R)
            { curMap = 0; GameReset(); }

            if (PointOp.lenght(dir) > 0)
            {
                dir = PointOp.normalize(ref dir);
                actor.Set_Direction(dir);
            }
        }
    }
}
