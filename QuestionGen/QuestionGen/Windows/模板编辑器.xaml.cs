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

namespace QuestionGen.Windows
{
    public static class Extensions
    {
        public static string ToXml(this string s)
        {
            return s.Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("&", "&amp;")
                .Replace("'", "&apos")
                .Replace("\"", "&quot;");
        }
    }

    public partial class 模板编辑器 : FloatableWindow
    {
        public 模板编辑器()
        {
            InitializeComponent();

            _模板_RichTextBox.ContentChanged += _模板_RichTextBox_ContentChanged;
            _代码_RichTextBox.ContentChanged += _代码_RichTextBox_ContentChanged;
        }

        TextBox _文本框 = null;

        public 模板编辑器(TextBox tb, bool 是否显示_插入格子 = false)
            : this()
        {
            _文本框 = tb;
            _代码_RichTextBox.Selection.Insert(new Run { Text = tb.Text });
            _插入格子_Button.IsEnabled = 是否显示_插入格子;
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


        private void _模板_RichTextBox_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            var tb = _模板_RichTextBox.Blocks.FirstOrDefault();
            if (tb != null)
            {

                _代码_RichTextBox.ContentChanged -= _代码_RichTextBox_ContentChanged;
                _代码_RichTextBox.Blocks.Clear();
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
                        // todo: is Image ? Movie ?
                    }
                }


                _代码_RichTextBox.ContentChanged += _代码_RichTextBox_ContentChanged;
            }
        }

        private void _代码_RichTextBox_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            var cb = _代码_RichTextBox.Blocks.FirstOrDefault();
            if (cb != null)
            {
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                _模板_RichTextBox.ContentChanged -= _模板_RichTextBox_ContentChanged;
                _模板_RichTextBox.Blocks.Clear();
                var tp = new Paragraph();
                _模板_RichTextBox.Blocks.Add(tp);

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
                        tp.Inlines.Add(new InlineUIContainer
                        {
                            Child = new TextBlock
                            {
                                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),
                                FontWeight = FontWeights.Bold,
                                Text = " ( ____ ) "
                            }
                        });
                        // todo: 处理图片
                    }
                }

                _模板_RichTextBox.ContentChanged += _模板_RichTextBox_ContentChanged;
            }
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


        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            var cb = _代码_RichTextBox.Blocks.FirstOrDefault();
            var cp = cb as Paragraph;
            foreach (var il in cp.Inlines)
            {
                sb.Append(((Run)il).Text);
            }

            _文本框.Text = sb.ToString();
            this.DialogResult = true;
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}

