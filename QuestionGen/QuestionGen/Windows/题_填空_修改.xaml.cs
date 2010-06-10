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
    public partial class 题_填空_修改 : FloatableWindow
    {
        #region 字段

        填空题 _填空题 = null;
        服务.题Client _s = null;

        #endregion

        #region 构造函数

        public 题_填空_修改()
        {
            InitializeComponent();

            _重置_Button_Click();
        }

        public 题_填空_修改(填空题 o)
            : this()
        {
            _填空题 = o;       // todo: backup

            _s = new 服务.题Client();
            _s.题_填空_修改Completed += new EventHandler<服务.题_填空_修改CompletedEventArgs>(_s_题_填空_修改Completed);

            SetValues();
        }

        #endregion

        #region SetValues

        private void SetValues()
        {
            _显示模板_TextBox.Text = _填空题.题.显示模板;

            _预览_Button_Click();
        }

        #endregion

        #region GetValues

        /// <summary>
        /// 将控件内容填充到 填空题 容器 并返回
        /// </summary>
        private void GetValues()
        {
            _填空题.题.显示模板 = _显示模板_TextBox.Text.Trim();
            _填空题.答案.Clear();

            var s = "<___root___>" + _填空题.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xes = xe.Elements("c");
            var i = 1;
            foreach (var o in xes)
            {
                _填空题.答案.Add(new 题.题_填空_答案 { 格子序号 = i++, 显示模板 = o.Value });
            }
        }

        #endregion

        #region 重置控件显示

        private void _重置_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            // todo: restore
        }

        #endregion

        #region 预览

        private void _预览_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            GetValues();

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + _填空题.题.显示模板 + "</___root___>";
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
                    if (xn.NodeType == XmlNodeType.Element)
                    {
                        var x = (XElement)xn;
                        if (x.Name.LocalName == "c")
                        {
                            p.Inlines.Add(new Run
                            {
                                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                                ,
                                FontWeight = FontWeights.Bold
                                ,
                                Text = " _" + x.Value + "_ "
                            });
                        }
                        else if (x.Name.LocalName == "a")
                        {
                            // todo: 图片
                        }
                    }
                }
            }

            _预览_RichTextBox.Blocks.Add(p);
        }

        #endregion

        void _s_题_填空_修改Completed(object sender, 服务.题_填空_修改CompletedEventArgs e)
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
            bool b = false;
            try
            {
                GetValues();
                b = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (b)
            {
                _提交_Button.IsEnabled = false;

                _s.题_填空_修改Async(_填空题.题.GetBytes(), _填空题.答案.GetBytes());
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

    }
}

