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

            /*
<StackPanel Orientation="Horizontal">
    <TextBlock  Text="1"/>
    <TextBox VerticalScrollBarVisibility="Visible" />
</StackPanel>
             */

            _选项_StackPanel.Children.Clear();
            _选项_StackPanel_Children_Add(1);
        }


        private void _选项_StackPanel_Children_Add(int i)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            var tbl = new TextBlock { Text = i.ToString() };
            var tb = new TextBox { Tag = i, VerticalScrollBarVisibility = ScrollBarVisibility.Visible };
            tb.TextChanged += new TextChangedEventHandler(tb_TextChanged);
            sp.Children.Add(tbl);
            sp.Children.Add(tb);
            _选项_StackPanel.Children.Add(sp);
        }

        void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var o = sender as TextBox;
            var idx = (int)o.Tag;
            var count = _选项_StackPanel.Children.Count;
            if (o.Text.Length > 0)
            {
                if (idx == count)
                {
                    _选项_StackPanel_Children_Add(count + 1);
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

