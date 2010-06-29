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
using SqlLib;
using DAL.Database.Tables;
using query = DAL.Queries.Tables;
using QuestionGen.Modules;

namespace QuestionGen.Windows
{
    public partial class 题_选择_修改 : FloatableWindow
    {
        #region 字段

        选择题 _选择题 = null;
        服务.题Client _s = null;
        Timer _timer = null;

        #endregion

        #region 构造函数

        public 题_选择_修改()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            _重置_Button_Click();
        }

        public 题_选择_修改(选择题 o)
            : this()
        {
            _选择题 = o;

            _s = new 服务.题Client();
            _s.题_选择_修改Completed += new EventHandler<服务.题_选择_修改CompletedEventArgs>(_s_题_修改Completed);

            SetValues(o);
        }

        #endregion

        #region 热键支持

        private void FloatableWindow_KeyDown(object sender, KeyEventArgs e)
        {
            var 组合键按下 = false;

            var keys = Keyboard.Modifiers;
            if (Application.Current.InstallState == InstallState.Installed)
                if ((keys & ModifierKeys.Control) == ModifierKeys.Control) 组合键按下 = true;
                else
                    if ((keys & ModifierKeys.Control) == ModifierKeys.Control && (keys & ModifierKeys.Alt) == ModifierKeys.Alt) 组合键按下 = true;
            if (组合键按下)
            {
                switch (e.Key)
                {
                    case Key.S:
                        if (_提交_Button.IsEnabled) _提交_Button_Click();
                        break;
                    case Key.W:
                        if (_取消_Button.IsEnabled) _取消_Button_Click();
                        break;
                }
            }
        }

        #endregion

        #region 实现自动添加选项

        private void 添加选项(int i, string text = "")
        {

            /*
<StackPanel Orientation="Horizontal">
<TextBlock  Text="1"/>
<TextBox VerticalScrollBarVisibility="Visible" />
</StackPanel>
            */

            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            var tbl = new TextBlock { Text = i.ToString() };
            var tb = new TextBox { Text = text, Tag = i, VerticalScrollBarVisibility = ScrollBarVisibility.Visible };
            tb.TextChanged += new TextChangedEventHandler(选项_TextChanged);
            tb.GotFocus += new RoutedEventHandler(tb_GotFocus);
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
                for (int i = cc - 1; i >= count; i--)
                {
                    _答案格子_StackPanel.Children.RemoveAt(i);
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

        private StackPanel 添加答案格子组(int i)
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
            return sp_1;
        }

        private void 添加答案格子(StackPanel parent, int i, string text = "")
        {
            var tb = new TextBox { Text = text, Tag = i };
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

        #region SetValues

        private void SetValues(选择题 o)
        {
            _显示模板_TextBox.Text = o.题.显示模板;

            _选项_StackPanel.Children.Clear();
            _答案格子_StackPanel.Children.Clear();

            var options = o.选项.OrderBy(a => a.选项序号);
            foreach (var c in options)
            {
                添加选项(c.选项序号, c.显示模板);
            }
            添加选项(o.选项.Last().选项序号 + 1);

            var gs = from answer in o.答案 group answer by answer.格子序号 into g orderby g.Key select g;
            foreach (var g in gs)
            {
                var sp = 添加答案格子组(g.Key);
                sp.Children.Clear();
                var answers = g.ToList();
                for (int i = 0; i < answers.Count; i++)
                {
                    添加答案格子(sp, i + 1, answers[i].选项序号.ToString());
                }
                添加答案格子(sp, answers.Count + 1);
            }

            _预览_Button_Click();
        }

        #endregion

        #region GetValues

        /// <summary>
        /// 将控件内容填充到 选择题 容器 并返回
        /// </summary>
        private 选择题 GetValues()
        {
            var result = new 选择题
            {
                题 = _选择题.题,
                选项 = new List<题.题_选择_选项>(),
                答案 = new List<题.题_选择_答案>()
            };

            //result.题.更新时间 = DateTime.Now;
            result.题.显示模板 = _显示模板_TextBox.Text.Trim();

            for (int i = 0; i < _选项_StackPanel.Children.Count; i++)
            {
                var sp = (StackPanel)_选项_StackPanel.Children[i];
                var idx = int.Parse(((TextBlock)sp.Children[0]).Text);
                var txt = ((TextBox)sp.Children[1]).Text;
                if (i == _选项_StackPanel.Children.Count - 1 && txt.Length == 0) continue;
                result.选项.Add(new 题.题_选择_选项 { 选项序号 = idx, 显示模板 = txt });
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
                    result.答案.Add(new 题.题_选择_答案 { 格子序号 = idx, 选项序号 = int.Parse(txt) });
                }
            }

            return result;
        }

        #endregion

        #region 重置控件显示

        private void _重置_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _显示模板_TextBox.Text = @"
<c/>";

            _预览_RichTextBox.Blocks.Clear();

            _选项_StackPanel.Children.Clear();
            添加选项(1);

            _答案格子_StackPanel.Children.Clear();
        }

        #endregion

        #region 预览

        private void _预览_Button_Click(object sender = null, RoutedEventArgs e = null)
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
                    Text = Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[c.选项序号 - 1] + ".  "
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
                        Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[a.选项序号 - 1] + " "
                    });
                }
            }

            _预览_RichTextBox.Blocks.Add(p);
        }

        #endregion

        void _s_题_修改Completed(object sender, 服务.题_选择_修改CompletedEventArgs e)
        {
            if (e.Result > 0)
            {
                this.DialogResult = true;
            }
            else
            {
                _提交_Button.IsEnabled = true;
                MessageBox.Show("保存失败!");
            }
        }

        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            选择题 result = null;
            try
            {
                result = GetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (result != null)
            {
                _提交_Button.IsEnabled = false;
                _s.题_选择_修改Async(result.题.GetBytes(), result.选项.GetBytes(), result.答案.GetBytes());
            }
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }



        private void _帮助_Button_Click(object sender, RoutedEventArgs e)
        {
            // todo: 弹出帮助示例窗口
        }



        private TextBox _当前焦点文本框 = null;

        private void _模板编辑器_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_当前焦点文本框 == null) return;
            var f = new 模板编辑器(_当前焦点文本框, _当前焦点文本框 == _显示模板_TextBox) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += new EventHandler(f_Closed);
            f.ShowDialog();
        }

        void f_Closed(object sender, EventArgs e)
        {
            _当前焦点文本框.Focus();
        }

        void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            _当前焦点文本框 = (TextBox)sender;
        }


    }
}

