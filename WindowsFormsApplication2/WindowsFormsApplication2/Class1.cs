using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CCWin;

namespace WindowsFormsApplication2
{
    public class DbNode<Button>
    {
        //私有变量
        private Button _data; //节点的值
        private int _type,x,y;
        private double _time;
        private DbNode<Button> _prev; //前驱节点
        private DbNode<Button> _next; //后继结点

        //属性

        /// <summary>
        /// 节点的值
        /// </summary>
        public Button Data
        {
            get { return this._data; }
            set { this._data = value; }
        }
        public int Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
		public int X
		{
			get { return this.x; }
			set { this.x = value; }
		}
		public int Y
		{
			get { return this.y; }
			set { this.y = value; }
		}
		public double Time
        {
            get { return this._time; }
            set { this._time = value; }
        }

        /// <summary>
        /// 前驱节点
        /// </summary>
        public DbNode<Button> Prev
        {
            get { return this._prev; }
            set { this._prev = value; }
        }

        /// <summary>
        /// 后继结点
        /// </summary>
        public DbNode<Button> Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        // 构造函数 6个

        public DbNode(Button data, int type, double time, DbNode<Button> prev, DbNode<Button> next)
        {
            this._data = data;
            this._type = type;
            this._time = time;
            this._prev = prev;
            this._next = next;
        }
        public DbNode(Button data, int type, double time)
        {
            this._data = data;
            this._type = type;
            this._time = time;

        }
          public DbNode(DbNode<Button> next)
          {
              //使用 default 关键字，此关键字对于引用类型会返回空，对于数值类型会返回零。
              //对于结构，此关键字将返回初始化为零或空的每个结构成员，具体取决于这些结构是值类型还是引用类型
              this._data = default(Button);
              this._next = next;
              this._prev = null;
          }

          public DbNode(Button data)
          {
              this._data = data;
              this._prev = null;
              this._next = null;
          }

          public DbNode()
          {
              this._data = default(Button);
              this._prev = null;
              this._next = null;
          }
    }






//  双链表操作实现代码

/// <summary>
/// 双向链表类
/// </summary>
    public class DbLinkedList<Button>
{

    private DbNode<Button> _head;

    public DbNode<Button> Head
    {
        get { return this._head; }
        set { this._head = value; }
    }

    //构造函数
    public DbLinkedList()
    {
        Head = null;
    }



    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DbNode<Button> this[int index]
    {
        get
        {
            return this.GetItemAt(index);
        }
    }

    /// <summary>
    /// 获取指定位置的元素
    /// </summary>
    /// <param name="i">指定的位置</param>
    /// <returns>T node</returns>
    public DbNode<Button> GetItemAt(int i)
    {

        DbNode<Button> p = new DbNode<Button>();
        p = Head;

        // 如果是第一个node
        if (0 == i)
        {
            return p;
        }

        int j = 0;
        while (p.Next != null && j < i)//移动j的指针到i的前一个node
        {
            j++;
            p = p.Next;
        }

        if (j == i)
        {
            return p;
        }
        else return p;

    }
    /// <summary>
    /// 判断双向链表是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return Head == null;
    }

    /// <summary>
    /// 清空双链表
    /// </summary>
    public void Clear()
    {
        this.Head = null;
    }

    /// <summary>
    /// 在位置i后插入node Button
    /// </summary>
    /// <param name="item"></param>
    /// <param name="i"></param>
    public void AddAfter(Button item, int i)
    {
        if (IsEmpty() || i < 0)
        {
            Console.WriteLine("The double linked list is empty or the position is uncorrect.");
            return;
        }

        if (0 == i) //在头之后插入元素
        {
            DbNode<Button> newNode = new DbNode<Button>(item);
            newNode.Next = Head.Next;
            Head.Next.Prev = newNode;
            Head.Next = newNode;
            newNode.Prev = Head;
            return;
        }

        DbNode<Button> p = Head; //p指向head
        int j = 0;

        while (p != null && j < i)
        {
            p = p.Next;
            j++;
        }

        if (j == i)
        {
            DbNode<Button> newNode = new DbNode<Button>(item);
            newNode.Next = p.Next;
            if (p.Next != null)
            {
                p.Next.Prev = newNode;
            }
            newNode.Prev = p;
            p.Next = newNode;
        }
        else
        {
            Console.WriteLine("The position is uncorrect.");
        }



    }

    /// <summary>
    /// 在位置i前插入node Button
    /// </summary>
    /// <param name="item"></param>
    /// <param name="i"></param>
    public void AddBefore(DbNode<Button> item, int i)
    {
        if (IsEmpty() || i < 0)
        {
            Console.WriteLine("The double linked list is empty or the position is uncorrect.");
            return;
        }

        if (0 == i) //在头之前插入元素
        {
            DbNode<Button> newNode = item;
            newNode.Next = Head; //把头改成第二个元素
            Head.Prev = newNode;
            Head = newNode; //把新元素设置为头
            return;
        }

        DbNode<Button> n = Head;
        DbNode<Button> d = new DbNode<Button>();
        int j = 0;

        while (n.Next != null && j < i)
        {
            d = n; //把d设置为头
            n = n.Next;
            j++;
        }

        {
            if (j == i)
            {
                DbNode<Button> newNode = item;
                d.Next = newNode;
                newNode.Prev = d;
                newNode.Next = n;
                n.Prev = newNode;
            }
        }
    }

    /// <summary>
    /// 在链表最后插入node
    /// </summary>
    /// <param name="item"></param>
    public void AddLast(DbNode<Button> item)
    {
        DbNode<Button> newNode = item;
        DbNode<Button> p = new DbNode<Button>();

        if (Head == null)
        {
            Head = newNode;
            return;
        }
        p = Head; //如果head不为空，head就赋值给第一个节点
        while (p.Next != null)
        {
            p = p.Next;
        }
        p.Next = newNode;
        newNode.Prev = p;
    }

    /// <summary>
    /// 移除指定位置的node
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Button RemoveAt(int i)
    {
        if (IsEmpty() || i < 0)
        {
            Console.WriteLine("The double linked list is empty or the position is uncorrect.");
            return default(Button);
        }

        DbNode<Button> q = new DbNode<Button>();
        if (0 == i)
        {
            q = Head;
            Head = Head.Next;
            Head.Prev = null;//删除掉了第一个元素
            return q.Data;
        }

        DbNode<Button> p = Head;
        int j = 0;

        while (p.Next != null && j < i)
        {
            j++;
            q = p;
            p = p.Next;
        }

        if (i == j) //?
        {
            p.Next.Prev = q;
            q.Next = p.Next;
            return p.Data;
        }
        else
        {
            Console.WriteLine("The position is uncorrect.");
            return default(Button);
        }
    }

    /// <summary>
    /// 根据元素的值查找索引
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int IndexOf(Button value)
    {
        if (IsEmpty())
        {
            Console.WriteLine("The list is empty.");
            return -1;
        }

        DbNode<Button> p = new DbNode<Button>();
        p = Head;
        int i = 0;
        while (p.Next != null && !p.Data.Equals(value))//查找value相同的item
        {
            p = p.Next;
            i++;
        }
        return i;
    }
    /// <summary>
    /// 返回链表的长度
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        DbNode<Button> p = Head;
        int length = 0;
        while (p != null)
        {
            length++;
            p = p.Next;
        }
        return length;
    }

    /// <summary>
    /// 根据元素位置得到指定的节点
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private DbNode<Button> GetNodeAt(int i)
    {
        if (IsEmpty())
        {
            Console.WriteLine("The list is empty.");
            return null;
        }

        DbNode<Button> p = new DbNode<Button>();
        p = this.Head;

        if (0 == i)
        {
            return p;
        }

        int j = 0;
        while (p.Next != null && j < i)
        {
            j++;
            p = p.Next;
        }
        if (j == i)
        {
            return p;
        }
        else
        {
            Console.WriteLine("The node does not exist.");
            return null;
        }
    }
    /// <summary>
    /// 打印双向链表的每个元素
    /// </summary>
    public string Print()
    {
        string s="";
        DbNode<Button> current = new DbNode<Button>();
        current = this.Head;
        bool a=false,b=false,c=false,d=false;
        DbNode<Button> recycle1 = new DbNode<Button>();
        DbNode<Button> recycle2 = new DbNode<Button>();
        DbNode<Button> recycle3 = new DbNode<Button>();
        DbNode<Button> recycle4 = new DbNode<Button>();
        while (current != null )
        {
            if (current.Type != 10 && current.Type != 11 && current.Type != 12 && current.Type != 13 && current.Type != 20 && current.Type != 21 && current.Type != 22 && current.Type != 23)
            {
                s += current.Type + "        " + current.Time + "\r\n";
                current = current.Next;
            }
            else{
            if (current.Type == 10)
            {
                a = true;
                recycle1 = current.Next;
                current = current.Next;
            }
            if (current.Type == 11)
            {
                b = true;
                recycle2 = current.Next;
                current = current.Next;
            }
            if (current.Type == 12)
            {
                c = true;
                recycle3 = current.Next;
                current = current.Next;
            }
            if (current.Type == 13)
            {
                d = true;
                recycle4 = current.Next;
                current = current.Next;
            }
            if (current.Type == 20)
            {
                if (a == true)
                {
                    a = false;
                    current = recycle1;
                    s += current.Type + "        " + current.Time + "\r\n";
                    current = current.Next;
                }
                else current = current.Next;
            }
            if (current.Type == 21)
            {
                if (b == true)
                {
                    b = false;
                    current = recycle2;
                    s += current.Type + "        " + current.Time + "\r\n";
                    current = current.Next;
                }
                else current = current.Next;
            }
            if (current.Type == 22)
            {
                if (c == true)
                {
                    c = false;
                    current = recycle3;
                    s += current.Type + "        " + current.Time + "\r\n";
                    current = current.Next;
                }
                else current = current.Next;
            }
            if (current.Type == 23)
            {
                if (d == true)
                {
                    
                    current = recycle4;
                    s += current.Type + "        " + current.Time + "\r\n";
                    current = current.Next;
                }
                else current = current.Next;
            }
            }

        }
        
        return s;
    }
}
}
