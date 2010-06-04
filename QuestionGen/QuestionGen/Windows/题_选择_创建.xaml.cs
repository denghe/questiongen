using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Windows
{
    public partial class 题_选择_创建 : FloatableWindow
    {
        题.题 _题 = null;

        public 题_选择_创建()
        {
            InitializeComponent();

            _选项_StackPanel.Children.Clear();
            添加选项(1);

            _答案格子_StackPanel.Children.Clear();
            添加答案格子组(1);
            添加答案格子组(2);
        }


        private void 添加选项(int i)
        {

            /*
<StackPanel Orientation="Horizontal">
<TextBlock  Text="1"/>
<TextBox VerticalScrollBarVisibility="Visible" />
</StackPanel>
            */

            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            var tbl = new TextBlock { Text = i.ToString() };
            var tb = new TextBox { Tag = i, VerticalScrollBarVisibility = ScrollBarVisibility.Visible };
            tb.TextChanged += new TextChangedEventHandler(选项_TextChanged);
            sp.Children.Add(tbl);
            sp.Children.Add(tb);
            _选项_StackPanel.Children.Add(sp);
        }

        void 选项_TextChanged(object sender, TextChangedEventArgs e)
        {
            var o = sender as TextBox;
            var idx = (int)o.Tag;
            var count = _选项_StackPanel.Children.Count;
            if (o.Text.Length > 0)
            {
                if (idx == count)
                {
                    添加选项(count + 1);
                }
            }
            else
            {
                if (idx == count - 1)
                {
                    var sp = (StackPanel)_选项_StackPanel.Children[idx];
                    var tb = (TextBox)sp.Children[1];
                    if (tb.Text.Length == 0)
                    {
                        _选项_StackPanel.Children.RemoveAt(idx);
                    }
                }
            }
        }

        private void 添加答案格子组(int i)
        {
            /*
<StackPanel Orientation="Horizontal">
    <TextBlock Text="1" />
    <StackPanel Orientation="Horizontal">
        <TextBox/>
        <TextBox/>
        <TextBox/>
    </StackPanel>
</StackPanel>
             */

            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            var tbl = new TextBlock { Text = i.ToString() };
            var sp_1 = new StackPanel { Orientation = Orientation.Horizontal };
            sp.Children.Add(tbl);
            sp.Children.Add(sp_1);
            添加答案格子(sp_1, 1);
            _答案格子_StackPanel.Children.Add(sp);
        }

        private void 添加答案格子(StackPanel parent, int i)
        {
            var tb = new TextBox { Tag = i };
            tb.TextChanged += new TextChangedEventHandler(答案格子_TextChanged);
            parent.Children.Add(tb);
        }

        void 答案格子_TextChanged(object sender, TextChangedEventArgs e)
        {
            var o = sender as TextBox;
            var idx = (int)o.Tag;
            var parent = (StackPanel)o.Parent;
            var count = parent.Children.Count;
            if (o.Text.Length > 0)
            {
                if (idx == count)
                {
                    添加答案格子(parent, count + 1);
                }
            }
            else
            {
                if (idx == count - 1)
                {
                    var tb = (TextBox)parent.Children[1];
                    if (tb.Text.Length == 0)
                    {
                        parent.Children.RemoveAt(idx);
                    }
                }
            }
        }

        public 题_选择_创建(题.题 o)
            : this()
        {
            _题 = o;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

