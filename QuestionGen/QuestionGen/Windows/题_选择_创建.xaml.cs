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
using System.Threading;
using System.Xml;
using System.Xml.Linq;

using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Windows
{
    public partial class 选择题
    {
        public 题.题 题 { get; set; }
        public List<题.题_选择_选项> 选项 { get; set; }
        public List<题.题_选择_答案> 答案 { get; set; }
    }

    public partial class 题_选择_创建 : FloatableWindow
    {
        选择题 _选择题 = null;
        Timer _timer = null;

        public 题_选择_创建()
        {
            InitializeComponent();

            _重置_Button_Click();
        }

        public 题_选择_创建(题.题 o)
            : this()
        {
            _选择题 = new 选择题
            {
                题 = o,
                选项 = new List<题.题_选择_选项>(),
                答案 = new List<题.题_选择_答案>()
            };
        }

        #region 实现自动添加选项

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
            var tb = new TextBox { Text = "", Tag = i, VerticalScrollBarVisibility = ScrollBarVisibility.Visible };
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
        #endregion

        #region 实现自动添加答案格子组

        private void 设置答案格子组(int count)
        {
            var cc = _答案格子_StackPanel.Children.Count;
            if (count == cc) return;
            else if (count < cc)    // remove
            {
                for (int i = cc - 1; i < cc - count; i++)
                {
                    _选项_StackPanel.Children.RemoveAt(cc - 1);
                }
            }
            else                    // add
            {
                for (int i = cc + 1; i <= count; i++)
                {
                    添加答案格子组(i);
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
        #endregion

        #region 显示模板 同步 答案格子组

        private void _显示模板_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_timer != null) _timer.Dispose();
            _timer = new Timer(o =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        var s = "<___root___>" + _显示模板_TextBox.Text.Trim() + "</___root___>";
                        var xe = XElement.Parse(s);
                        var count = xe.Elements("c").Count();
                        设置答案格子组(count);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }));
            }, null, 1000, 0);
        }

        #endregion

        /// <summary>
        /// 将控件内容填充到 选择题 容器 并返回
        /// </summary>
        private 选择题 GetValues()
        {
            var result = new 选择题
            {
                题 = new 题.题(),
                选项 = new List<题.题_选择_选项>(),
                答案 = new List<题.题_选择_答案>()
            };

            _选择题.题.显示模板 = _显示模板_TextBox.Text.Trim();
            _选择题.选项.Clear();
            _选择题.答案.Clear();

            for (int i = 0; i < _选项_StackPanel.Children.Count; i++)
            {
                var sp = (StackPanel)_选项_StackPanel.Children[i];
                var idx = int.Parse(((TextBlock)sp.Children[0]).Text);
                var txt = ((TextBox)sp.Children[1]).Text;
                if (i == _选项_StackPanel.Children.Count - 1 && txt.Length == 0) continue;
                _选择题.选项.Add(new 题.题_选择_选项 { 选项编号 = idx, 显示模板 = txt });
            }

            for (int i = 0; i < _答案格子_StackPanel.Children.Count; i++)
            {
                var sp = (StackPanel)_答案格子_StackPanel.Children[i];
                var idx = int.Parse(((TextBlock)sp.Children[0]).Text);
                var csp = (StackPanel)sp.Children[1];
                foreach (var uie in csp.Children)
                {
                    var txt = ((TextBox)uie).Text;
                    if (txt.Length == 0) continue;
                    _选择题.答案.Add(new 题.题_选择_答案 { 格子序号 = idx, 选项编号 = int.Parse(txt) });
                }
            }

            return result;
        }

        private void _帮助_Button_Click(object sender, RoutedEventArgs e)
        {
            // todo: 弹出帮助示例窗口
        }


        private void _重置_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _显示模板_TextBox.Text = @"
<c/>";

            _预览_RichTextBox.Blocks.Clear();

            _选项_StackPanel.Children.Clear();
            添加选项(1);

            _答案格子_StackPanel.Children.Clear();
        }




        private void _预览_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = GetValues();

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + result.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xns = xe.Nodes();
            var p = new Paragraph { FontSize = 16 };
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"题面:

"
            });
            foreach (var xn in xns)
            {
                if (xn.NodeType == XmlNodeType.Text)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Normal
                        ,
                        Text = ((XText)xn).Value
                    });
                }
                else
                {
                    // todo: 判断是图片 还是 答案格子

                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                        ,
                        FontWeight = FontWeights.Bold
                        ,
                        Text = "___"
                    });
                }
            }
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

选项:
"
            });

            foreach (var c in result.选项)
            {
                p.Inlines.Add(new Run
                {
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                    ,
                    FontWeight = FontWeights.Bold
                    ,
                    Text = Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[c.选项编号 - 1] + ".  "
                });

                xe = XElement.Parse("<___root___>" + c.显示模板 + "</___root___>");
                xns = xe.Nodes();
                foreach (var xn in xns)
                {
                    if (xn.NodeType == XmlNodeType.Text)
                    {
                        p.Inlines.Add(new Run
                        {
                            Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                            ,
                            FontWeight = FontWeights.Normal
                            ,
                            Text = ((XText)xn).Value
                        });
                    }
                    else
                    {
                        // todo: 处理图片
                    }
                }
            }


            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

答案:
"
            });

            // 先按格子号 分组

            var group = from x in result.答案 group x by x.格子序号 into g select g;

            foreach (var g in group)
            {
                p.Inlines.Add(new Run
                {
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                    ,
                    FontWeight = FontWeights.Normal
                    ,
                    Text = Environment.NewLine + g.Key.ToString() + ".  "
                });

                foreach (var a in g)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Bold
                        ,
                        Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[a.选项编号 - 1] + " "
                    });
                }
                //"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[c.选项编号 - 1]
            }


            _预览_RichTextBox.Blocks.Add(p);
        }


        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            // todo: 从控件取值

            // todo: 保存

            this.DialogResult = true;
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}

