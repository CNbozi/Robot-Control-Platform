using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.Drawing;//提供对GDI+基本图形功能的访问
using System.Drawing.Drawing2D;//提供高级的二维和矢量图像功能
using System.Drawing.Imaging;//提供高级GDI+图像处理功能
using System.Drawing.Printing;//提供打印相关服务
using System.Drawing.Text;//提供高级GDI+排版功能
using System.Drawing.Design;
using System.IO;//扩展设计时，用户界面逻辑和绘制的类。用于扩展，自定义


namespace WindowsFormsApplication2
{
    public partial class Form1 : CCSkinMain
    {     
        
        public string type;//按钮类型
        DbLinkedList<Button> h1 =new DbLinkedList<Button>();
        DbLinkedList<Button> h2 = new DbLinkedList<Button>();
        DbLinkedList<Button> h3 = new DbLinkedList<Button>();
        List<DbLinkedList<Button>> k = new List<DbLinkedList<Button>>();
        

        public Form1()
        {
            k.Add(h1);
            k.Add(h2);
            k.Add(h3);
;           DragDrop += tabPage1_DragDrop;
            DragEnter += tabPage1_DragEnter;
            Load += Form1_Load;
            InitializeComponent();
            button1.MouseDown += new MouseEventHandler(OnMouseDown);//新建选项卡的程序开始和程序结束按钮的拖动
            button1.MouseMove += new MouseEventHandler(OnMouseMove);
            button2.MouseDown += new MouseEventHandler(OnMouseDown);
            button2.MouseMove += new MouseEventHandler(OnMouseMove);
            DbNode<Button> start = new DbNode<Button>(button1, 0, 0, null, null);
			start.X = button1.Location.X;
			start.Y = button1.Location.Y;
            k[l].Head = start;
            DbNode<Button> end = new DbNode<Button>(button2, 1, 0, null, null);
			end.X = button2.Location.X;
			end.Y = button2.Location.Y;
			k[l].AddLast(end);
            //this.WindowState = FormWindowState.Maximized;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            



            
        }

        public int[] b = {0, 1, 0, 0 };//任务数组
        public int n = 1;
        //新建任务
        private void skinButton1_Click(object sender, EventArgs e)
        {
            
            n++;
            if (n <= 3)
            {
                string Title = "";
                //声明一个字符串变量，用于生成新增选项卡的名称  
                foreach (Control c in tabControl1.TabPages)
                {
                    if (c.Text == "unknown1")
                        b[1] = 1;
                    if (c.Text == "unknown2")
                        b[2] = 1;
                    if (c.Text == "unknown3")
                        b[3] = 1;
                }
                if (b[1] != 0)
                {
                    if(b[2]!=0)
                    {
                        Title = "unknown3";
                    }
                        else Title = "unknown2";
                }
                else
                    Title = "unknown1";
                
                TabPage MyTabPage = new TabPage(Title);//创建TabPage对象  
                MyTabPage.BackColor = Color.White;
                //使用TabControl控件的TabPages 属性的Add方法添加新的选项卡  
                tabControl1.TabPages.Add(MyTabPage);
                tabControl1.SelectedTab = MyTabPage;
                MyTabPage.Paint += new PaintEventHandler(tabPage1_Paint);
                //设置TabPages界面可以拖入控件
                tabControl1.SelectedTab.DragDrop += new DragEventHandler(tabPage1_DragDrop);
                tabControl1.SelectedTab.DragEnter += new DragEventHandler(tabPage1_DragEnter);
                tabControl1.SelectedTab.AllowDrop = true;


                //在初始页面生成开始、结束按钮

                Button START = new Button();
                START.Image = button1.Image;
                START.Size = new Size(80, 80);
                START.Location = new Point(83, 43);
                MyTabPage.Controls.Add(START);
                START.MouseDown += new MouseEventHandler(OnMouseDown);
                START.MouseMove += new MouseEventHandler(OnMouseMove);
                DbNode<Button> start = new DbNode<Button>(START, 0, 0, null, null);
				start.X = START.Location.X;
				start.Y = START.Location.Y;
				k[l].Head = start;




                Button END = new Button();
                END.Image = button2.Image;
                END.Size = new Size(80, 80);
                END.Location = new Point(83, 343);
                MyTabPage.Controls.Add(END);
                END.MouseDown += new MouseEventHandler(OnMouseDown);
                END.MouseMove += new MouseEventHandler(OnMouseMove);
                DbNode<Button> end = new DbNode<Button>(END, 1, 0, null, null);
				end.X = END.Location.X;
				end.Y = END.Location.Y;
				k[l].AddLast(end);
                /* dblink.AddLast(END);*/
            }
            else
                MessageBox.Show("最多只能拥有三个选项卡！");
        }


       
        




        private void tabPage1_DragEnter(object sender, DragEventArgs e)
        {
             //当Button被拖拽到WinForm上时候，鼠标效果出现
            if ((e.Data.GetDataPresent(typeof(Button))))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        int ox, oy;
        void OnMouseDown(object sender, MouseEventArgs e)
        {
            ox = e.X;
            oy = e.Y;
            BringToFront();
        }
        Button z;
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            var el = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                el.Top = el.Top + e.Y - oy;
                el.Left = el.Left + e.X - ox;
                tabControl1.SelectedTab.Invalidate();
                z = (Button)el;
				k[l].GetItemAt(k[l].IndexOf(z)).X = z.Location.X;
				k[l].GetItemAt(k[l].IndexOf(z)).Y = z.Location.Y;
			}

        }

        //buttonnumber 号码
        /*
          start     0
          end       1
          move      2
          back      3
          turnleft  4
          turnright 5
          follow    6
          stop      7
          delay     8  //延时

         * 
          recycle_begin  10
          recycle_begin1 11
          recycle_begin2 12
          infinite_begin 13
          recycle_over   20
          recycle_over1  21
          recycle_over2  22
          infinite_over  23
         
         * 
          Fturnleft      30
          Fturnright     31
          Sturnleft      32
          Sturnright     33
          
         * 
          motorstop      41
          motormove      42
          motorback      43
          Lmotor         44
          Rmotor         45
          
         * 
          beep     50
          music1   51
          music2   52
          music3   53
          music4   54
          music5   55
          music6   56
         * 
         * 
          L_move   60
          L_back   61
          L_left   62
          L_right  63
          L_stop   64
          L_water  65
          L_cw     66
          L_atcw   67
          L_blink  68
          L_morse  69
        */

        public int buttonnumber; //buttonnumber 是判断 mousedown 时所在的按钮
        
     
        private void tabPage1_DragDrop(object sender, DragEventArgs e)
        {



                //拖放完毕之后，自动生成新控件
                Button btn = new Button();
                btn.Size = move.Size;
                btn.Location = tabControl1.SelectedTab.PointToClient(new Point(e.X, e.Y));
                //用这个方法计算出客户端容器界面的X，Y坐标。否则直接使用X，Y是屏幕坐标
                tabControl1.SelectedTab.Controls.Add(btn);
                btn.ContextMenuStrip = contextMenuStrip1;
                btn.MouseDown += new MouseEventHandler(OnMouseDown);
                btn.MouseMove += new MouseEventHandler(OnMouseMove);
                switch (buttonnumber)
                {
                    case 2:
                        {
                            btn.Image = Properties.Resources.move;
                            DbNode<Button> a = new DbNode<Button>(btn, 2, 0);
                            k[l].AddBefore(a, k[l].Count()-1);
                            break;
                        }
                    case 3:
                        {
                            btn.Image = Properties.Resources.back;
                            DbNode<Button> a = new DbNode<Button>(btn, 3, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 4:
                        {
                            btn.Image = Properties.Resources.turnleft;
                            DbNode<Button> a = new DbNode<Button>(btn, 4, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 5:
                        {
                            btn.Image = Properties.Resources.turnright;
                            DbNode<Button> a = new DbNode<Button>(btn, 5, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 6:
                        {
                            btn.Image = Properties.Resources.follow;
                            DbNode<Button> a = new DbNode<Button>(btn, 6, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 7:
                        {
                            btn.Image = Properties.Resources.stop;
                            DbNode<Button> a = new DbNode<Button>(btn, 7, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;       
                        }
                    //延时 delay

                    case 8:
                        {
                            btn.Image = Properties.Resources.延时;
                            DbNode<Button> a = new DbNode<Button>(btn, 8, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 10:
                        {
                            btn.Image = Properties.Resources.循环开始11;
                            DbNode<Button> a = new DbNode<Button>(btn, 10, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 11:
                        {
                            btn.Image = Properties.Resources.循环开始21;
                            DbNode<Button> a = new DbNode<Button>(btn, 11, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 12:
                        {
                            btn.Image = Properties.Resources.循环开始31;
                            DbNode<Button> a = new DbNode<Button>(btn, 12, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 13:
                        {
                            btn.Image = Properties.Resources.无限循环开始;
                            DbNode<Button> a = new DbNode<Button>(btn, 13, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 20:
                        {
                            btn.Image = Properties.Resources.循环结束1;
                            DbNode<Button> a = new DbNode<Button>(btn, 20, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 21:
                        {
                            btn.Image = Properties.Resources.循环结束2;
                            DbNode<Button> a = new DbNode<Button>(btn, 21, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 22:
                        {
                            btn.Image = Properties.Resources.循环结束3;
                            DbNode<Button> a = new DbNode<Button>(btn, 22, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 23:
                        {
                            btn.Image = Properties.Resources.无限循环结束;
                            DbNode<Button> a = new DbNode<Button>(btn, 23, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 30:
                        {
                            btn.Image = Properties.Resources.小车快速左转;
                            DbNode<Button> a = new DbNode<Button>(btn, 30, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 31:
                        {
                            btn.Image = Properties.Resources.小车快速右转;
                            DbNode<Button> a = new DbNode<Button>(btn, 31, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 32:
                        {
                            btn.Image = Properties.Resources.小车慢速做转;
                            DbNode<Button> a = new DbNode<Button>(btn, 32, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 33:
                        {
                            btn.Image = Properties.Resources.小车慢速右转;
                            DbNode<Button> a = new DbNode<Button>(btn, 33, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 41:
                        {
                            btn.Image = Properties.Resources.电机停转;
                            DbNode<Button> a = new DbNode<Button>(btn, 41, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 42:
                        {
                            btn.Image = Properties.Resources.电机正转;
                            DbNode<Button> a = new DbNode<Button>(btn, 42, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 43:
                        {
                            btn.Image = Properties.Resources.电机反转;
                            DbNode<Button> a = new DbNode<Button>(btn, 43, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 44:
                        {
                            btn.Image = Properties.Resources.左电机转动;
                            DbNode<Button> a = new DbNode<Button>(btn, 44, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 45:
                        {
                            btn.Image = Properties.Resources.右电机转动;
                            DbNode<Button> a = new DbNode<Button>(btn, 45, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 50:
                        {
                            btn.Image = Properties.Resources.蜂鸣;
                            DbNode<Button> a = new DbNode<Button>(btn, 50, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 51:
                        {
                            btn.Image = Properties.Resources.乐曲1;
                            DbNode<Button> a = new DbNode<Button>(btn, 51, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 52:
                        {
                            btn.Image = Properties.Resources.乐曲2;
                            DbNode<Button> a = new DbNode<Button>(btn, 52, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 53:
                        {
                            btn.Image = Properties.Resources.乐曲3;
                            DbNode<Button> a = new DbNode<Button>(btn, 53, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 54:
                        {
                            btn.Image = Properties.Resources.乐曲4;
                            DbNode<Button> a = new DbNode<Button>(btn, 54, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 55:
                        {
                            btn.Image = Properties.Resources.乐曲5;
                            DbNode<Button> a = new DbNode<Button>(btn, 55, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 56:
                        {
                            btn.Image = Properties.Resources.乐曲6;
                            DbNode<Button> a = new DbNode<Button>(btn, 56, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                        //灯光控制
                    case 60:
                        {
                            btn.Image = Properties.Resources.前进等;
                            DbNode<Button> a = new DbNode<Button>(btn, 60, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 61:
                        {
                            btn.Image = Properties.Resources.倒车灯;
                            DbNode<Button> a = new DbNode<Button>(btn, 61, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 62:
                        {
                            btn.Image = Properties.Resources.亮左转灯;
                            DbNode<Button> a = new DbNode<Button>(btn, 62, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 63:
                        {
                            btn.Image = Properties.Resources.亮右转灯;
                            DbNode<Button> a = new DbNode<Button>(btn, 63, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 64:
                        {
                            btn.Image = Properties.Resources.流水灯;
                            DbNode<Button> a = new DbNode<Button>(btn, 64, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 65:
                        {
                            btn.Image = Properties.Resources.停止灯;
                            DbNode<Button> a = new DbNode<Button>(btn, 65, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 66:
                        {
                            btn.Image = Properties.Resources.顺时针亮一圈;
                            DbNode<Button> a = new DbNode<Button>(btn, 66, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 67:
                        {
                            btn.Image = Properties.Resources.逆时针亮一圈;
                            DbNode<Button> a = new DbNode<Button>(btn, 67, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 68:
                        {
                            btn.Image = Properties.Resources.小灯闪烁一次;
                            DbNode<Button> a = new DbNode<Button>(btn, 68, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }
                    case 69:
                        {
                            btn.Image = Properties.Resources.摩斯电码__2_;
                            DbNode<Button> a = new DbNode<Button>(btn, 69, 0);
                            k[l].AddBefore(a, k[l].Count() - 1);
                            break;
                        }

                
            }
            
        }
        //功能按钮的拖动   mousedown 事件
        //形成拖拽效果，移动+拷贝的组合效果
        private void move_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 2;
            move.DoDragDrop(move, DragDropEffects.Move | DragDropEffects.Copy); //形成拖拽效果，移动+拷贝的组合效果
        }

        private void back_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 3;
            back.DoDragDrop(back, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void left_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 4;
            left.DoDragDrop(left, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void right_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 5;
            right.DoDragDrop(right, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void follow_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 6;
            follow.DoDragDrop(follow, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void stop_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 7;
            follow.DoDragDrop(follow, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void delay_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 8;
            delay.DoDragDrop(delay, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void recycle_begin_MouseDown_1(object sender, MouseEventArgs e)
        {
            buttonnumber = 10;
            recycle_begin.DoDragDrop(recycle_begin, DragDropEffects.Move | DragDropEffects.Copy);
        }

        
        private void recycle_begin2_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 11;
            recycle_begin2.DoDragDrop(recycle_begin2, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void recycle_begin3_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 12;
            recycle_begin3.DoDragDrop(recycle_begin3, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void infinite_begin_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 13;
            infinite_begin.DoDragDrop(infinite_begin, DragDropEffects.Move | DragDropEffects.Copy);
        } 
        private void recycle_over_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 20;
            recycle_over.DoDragDrop(recycle_over, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void recycle_over2_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 21;
            recycle_over2.DoDragDrop(recycle_over2, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void recycle_over3_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 22;
            recycle_over3.DoDragDrop(recycle_over3, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void infinite_over_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 23;
            infinite_over.DoDragDrop(infinite_over, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void Fturnleft_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 30;
            Fturnleft.DoDragDrop(Fturnleft, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void Fturnright_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 31;
            Fturnright.DoDragDrop(Fturnright, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void Sturnleft_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 32;
            Sturnleft.DoDragDrop(Sturnleft, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void Sturnright_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 33;
            Sturnright.DoDragDrop(Sturnright, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void motorstop_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 41;
            motorstop.DoDragDrop(motorstop, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void motormove_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 42;
            motormove.DoDragDrop(motormove, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void motorback_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 43;
            motorback.DoDragDrop(motorback, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void Lmotor_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 44;
            Lmotor.DoDragDrop(Lmotor, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void Rmotor_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 45;
            Rmotor.DoDragDrop(Rmotor, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void beep_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 50;
            beep.DoDragDrop(beep, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void music1_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 51;
            music1.DoDragDrop(music1, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void music2_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 52;
            music2.DoDragDrop(music2, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void music3_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 53;
            music3.DoDragDrop(music3, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void music4_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 54;
            music4.DoDragDrop(music4, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void music5_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 55;
            music5.DoDragDrop(music5, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void music6_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 56;
            music6.DoDragDrop(music6, DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void L_move_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 60;
            L_move.DoDragDrop(L_move, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_back_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 61;
            L_back.DoDragDrop(L_back, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_left_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 62;
            L_left.DoDragDrop(L_left, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_right_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 63;
            L_right.DoDragDrop(L_right, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_water_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 64;
            L_water.DoDragDrop(L_water, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_stop_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 65;
            L_stop.DoDragDrop(L_stop, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_cw_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 66;
            L_cw.DoDragDrop(L_cw, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_atcw_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 67;
            L_atcw.DoDragDrop(L_atcw, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_blink_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 68;
            L_blink.DoDragDrop(L_blink, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void L_morse_MouseDown(object sender, MouseEventArgs e)
        {
            buttonnumber = 69;
            L_morse.DoDragDrop(L_morse, DragDropEffects.Move | DragDropEffects.Copy);
        }



        //按钮的删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            k[l].RemoveAt(k[l].IndexOf((Button)contextMenuStrip1.SourceControl));
            this.Controls.Remove(contextMenuStrip1.SourceControl);
            contextMenuStrip1.SourceControl.Dispose();
            this.Refresh();
        }
        //通过GDI绘图绘制tabpage的箭头连线  
        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Black,3);//创建画笔
            AdjustableArrowCap cap = new AdjustableArrowCap(3, 3);//创建键帽
            cap.WidthScale = 2;//键帽的宽度
            cap.BaseCap = LineCap.Square;
            cap.Height = 2;//键帽高度
            p.CustomEndCap = cap;
            Point a = new Point(k[l][0].Data.Location.X+40, k[l][0].Data.Location.Y+80);
            Point b = new Point(k[l][0].Next.Data.Location.X + 40, k[l][0].Next.Data.Location.Y);
            g.DrawLine(p, a, b);
            DbNode<Button> n = new DbNode<Button>();
            DbNode<Button> s = new DbNode<Button>();

            for (int i = 2, j = 0; i < k[l].Count(); i++)
            {
                while (j == 0)
                {
                     n = k[l][0].Next;
                     s = k[l][0].Next.Next;
                    j++;
                }
                Point c = new Point(n.Data.Location.X + n.Data.Size.Width / 2, n.Data.Location.Y + n.Data.Size.Height);//确定画笔起始终止位置
                Point d = new Point(s.Data.Location.X + n.Data.Size.Width / 2, s.Data.Location.Y);
                g.DrawLine(p, c, d);
                n = n.Next;
                s = s.Next;
                
            }


        }

        int l=0;
        //任务选项卡的切换
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "unknown1")
                l = 0;
            if (tabControl1.SelectedTab.Text == "unknown2")
                l = 1;
            if (tabControl1.SelectedTab.Text == "unknown3")
                l = 2;
        }
        //保存任务
        private void skinButton3_Click(object sender, EventArgs e)
        {
			string str="";
			DbNode<Button> g = new DbNode<Button>();
			g = k[l].Head;
			while (g != null)
			{
				str += Convert.ToString(g.Type) + "\r\n" + Convert.ToString(g.X) + "\r\n" + Convert.ToString(g.Y) + "\r\n" + Convert.ToString(g.Time) + "\r\n";
				g = g.Next;
			}
			SaveFileDialog saveFile1 = new SaveFileDialog();
			saveFile1.Filter = "文本文件(.txt)|*.txt";
			saveFile1.FilterIndex = 1;
			if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
			{
				System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile1.FileName, false);
				try
				{
					sw.WriteLine(str); //只要这里改一下要输出的内容就可以了
				}
				catch
				{
					throw;
				}
				finally
				{
					sw.Close();
				}
			}
		}
        //删除任务
        private void skinButton4_Click(object sender, EventArgs e)
        {
            
            if (tabControl1.TabPages.Count > 1)
            {
                b[Convert.ToInt32(tabControl1.SelectedTab.Text.Substring(tabControl1.SelectedTab.Text.Length - 1, 1))] = 0;
                k[l].Clear();
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                n--;
            }
            else
                MessageBox.Show("最少要保留一页！");
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            k[l].GetItemAt(k[l].IndexOf(z)).Time = Convert.ToDouble(textBox1.Text);
        }
        //导出当前选项卡配置 以txt形式导出
        private void skinButton5_Click(object sender, EventArgs e)
        {
            
        }





        private void button15_Click(object sender, EventArgs e)
        {
            textBox2.Text = k[l].Print();
        }


        












      

        

        


        

        

       

     
    }


}








