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
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace QuestionGen.Windows
{
    public static class Extensions
    {
        public static string ToXml(this string s)
        {
            return s.Replace("&", "&amp;")
                .Replace(">", "&gt;")
                .Replace("'", "&apos")
                .Replace("\"", "&quot;")
                .Replace("<", "&lt;");
        }
    }

    public partial class 模板编辑器 : FloatableWindow
    {
        public 模板编辑器()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;
        }

        TextBox _文本框 = null;

        public 模板编辑器(TextBox tb, bool 是否显示_插入格子 = false, bool 是否显示_插入填空 = false)
            : this()
        {
            _文本框 = tb;
            _代码_RichTextBox.Blocks.Clear();
            var sr = new StringReader(tb.Text);
            while (sr.Peek() > 0)
            {
                var s = sr.ReadLine();
                var cp = new Paragraph();
                _代码_RichTextBox.Blocks.Add(cp);
                cp.Inlines.Add(new Run { Text = s });
            }

            //_代码_RichTextBox.Selection.Insert(new Run { Text = tb.Text });
            _插入格子_Button.IsEnabled = 是否显示_插入格子;
            _插入填空_Button.IsEnabled = 是否显示_插入填空;

            _模板_RichTextBox.ContentChanged += _模板_RichTextBox_ContentChanged;
            _代码_RichTextBox.ContentChanged += _代码_RichTextBox_ContentChanged;

            _代码_RichTextBox_ContentChanged(null, null);
        }

        private void _插入格子_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_is_模板_focus)
            {
                //　取 _模板_RichTextBox　当前光标位置，　插入
                _模板_RichTextBox.Selection.Insert(new InlineUIContainer
                {
                    Child = new TextBlock
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),
                        FontWeight = FontWeights.Bold,
                        Text = " ( ____ ) "
                    }
                });
                _模板_RichTextBox.Focus();
            }
            else
            {
                //　取 _代码_RichTextBox　当前光标位置，　插入
                _代码_RichTextBox.Selection.Insert(new Run { Text = "<c/>" });
                _代码_RichTextBox.Focus();
            }
        }

        private void _插入附件_Button_Click(object sender, RoutedEventArgs e)
        {
            // todo
        }

        private void _插入填空_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_is_模板_focus)
            {
                //　取 _模板_RichTextBox　当前光标位置，　插入
                _模板_RichTextBox.Selection.Insert(new InlineUIContainer
                {
                    Child = new TextBox
                    {
                        Width = 100,
                        Height = 20,
                        Text = ""
                    }
                });
                _模板_RichTextBox.Focus();
            }
            else
            {
                //　取 _代码_RichTextBox　当前光标位置，　插入
                _代码_RichTextBox.Selection.Insert(new Run { Text = "<c></c>" });
                _代码_RichTextBox.Focus();
            }
        }


        private void _模板_RichTextBox_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            _代码_RichTextBox.ContentChanged -= _代码_RichTextBox_ContentChanged;
            _代码_RichTextBox.Blocks.Clear();

            foreach (var tb in _模板_RichTextBox.Blocks)
            {
                var cp = new Paragraph();
                _代码_RichTextBox.Blocks.Add(cp);

                var tp = tb as Paragraph;
                foreach (var il in tp.Inlines)
                {
                    if (il is Run)
                    {
                        cp.Inlines.Add(new Run { Text = ((Run)il).Text.ToXml() });
                    }
                    else if (il is InlineUIContainer)
                    {
                        var child = ((InlineUIContainer)il).Child;
                        if (child is TextBlock)
                        {
                            cp.Inlines.Add(new Run { Text = "<c/>" });
                        }
                        else if (child is TextBox)
                        {
                            var tb1 = child as TextBox;
                            cp.Inlines.Add(new Run { Text = "<c>" + tb1.Text.ToXml() + "</c>" });
                        }
                        // todo: is Image ? Movie ?
                    }
                }
            }

            _代码_RichTextBox.ContentChanged += _代码_RichTextBox_ContentChanged;
        }

        private void _代码_RichTextBox_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            _模板_RichTextBox.ContentChanged -= _模板_RichTextBox_ContentChanged;
            _模板_RichTextBox.Blocks.Clear();

            foreach (var cb in _代码_RichTextBox.Blocks)
            {
                var tp = new Paragraph();
                _模板_RichTextBox.Blocks.Add(tp);

                var sb = new StringBuilder();
                var cp = cb as Paragraph;
                foreach (var il in cp.Inlines)
                {
                    sb.Append(((Run)il).Text);
                }

                var s = "<___root___>" + sb.ToString() + "</___root___>";
                XElement xe = null;
                try
                {
                    xe = XElement.Parse(s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }


                var xns = xe.Nodes();
                foreach (var xn in xns)
                {
                    if (xn.NodeType == XmlNodeType.Text)
                    {
                        tp.Inlines.Add(new Run
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
                        var xe1 = xn as XElement;
                        if (xe1.Name.LocalName == "c")
                        {
                            if (string.IsNullOrEmpty(xe1.Value))
                            {
                                tp.Inlines.Add(new InlineUIContainer
                                {
                                    Child = new TextBlock
                                    {
                                        Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),
                                        FontWeight = FontWeights.Bold,
                                        Text = " ( ____ ) "
                                    }
                                });
                            }
                            else
                            {
                                tp.Inlines.Add(new InlineUIContainer
                                {
                                    Child = new TextBox
                                    {
                                        Width = 100,
                                        Height = 20,
                                        Text = xe1.Value
                                    }
                                });
                            }
                        }
                        // todo: 处理图片
                    }
                }

            }

            _模板_RichTextBox.ContentChanged += _模板_RichTextBox_ContentChanged;
        }

        bool _is_模板_focus = true;
        private void _模板_RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _is_模板_focus = true;
        }

        private void _代码_RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _is_模板_focus = false;
        }


        private void _提交_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _代码_RichTextBox.Blocks.Count; i++)
            {
                if (i > 0) sb.Append(Environment.NewLine);
                var cb = _代码_RichTextBox.Blocks[i];
                var cp = cb as Paragraph;
                foreach (var il in cp.Inlines)
                {
                    sb.Append(((Run)il).Text);
                }
            }

            _文本框.Text = sb.ToString();
            this.DialogResult = true;
        }

        private void _取消_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            this.DialogResult = false;
        }


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
                        if (_提交_Button.IsEnabled) _提交_Button_Click(null, null);
                        break;
                    case Key.W:
                        if (_取消_Button.IsEnabled) _取消_Button_Click(null, null);
                        break;
                }
            }
        }

        #endregion
    }
}

