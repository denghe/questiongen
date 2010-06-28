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
    public partial class 题_判断_创建 : FloatableWindow
    {
        #region 字段

        题.题 _题 = null;
        服务.题Client _s = null;

        #endregion

        #region 构造函数

        public 题_判断_创建()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            _重置_Button_Click();
        }

        public 题_判断_创建(题.题 o)
            : this()
        {
            _题 = o;
            _s = new 服务.题Client();
            _s.题_判断_插入Completed += new EventHandler<服务.题_判断_插入CompletedEventArgs>(_s_题_判断_插入Completed);
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
                        _提交_Button_Click(null, null);
                        break;
                    case Key.W:
                        _取消_Button_Click(null, null);
                        break;
                }
            }
        }

        #endregion

        #region GetValues

        /// <summary>
        /// 将控件内容填充到 判断题 容器 并返回
        /// </summary>
        private 判断题 GetValues()
        {
            var result = new 判断题
            {
                题 = _题,
                答案 = new 题.题_判断_答案 { 题编号 = _题.题编号, 答案 = _答案_正确_RadioButton.IsChecked.Value }
            };
            result.题.显示模板 = _显示模板_TextBox.Text.Trim();

            return result;
        }

        #endregion

        #region 重置控件显示

        private void _重置_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _显示模板_TextBox.Text = @"";
            _预览_RichTextBox.Blocks.Clear();
            _答案_正确_RadioButton.IsChecked = true;
        }

        #endregion

        #region 预览

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

答案:
"
            });

            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = result.答案.答案 ? "正确" : "错误"
            });

            _预览_RichTextBox.Blocks.Add(p);
        }

        #endregion

        void _s_题_判断_插入Completed(object sender, 服务.题_判断_插入CompletedEventArgs e)
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
            判断题 result = null;
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

                //result.题.更新时间 = DateTime.Now;   // 修正序列化时的时间合法性问题

                _s.题_判断_插入Async(result.题.GetBytes(), result.答案.GetBytes());
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







        private void _模板编辑器_Button_Click(object sender, RoutedEventArgs e)
        {
            var f = new 模板编辑器(_显示模板_TextBox) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += new EventHandler(f_Closed);
            f.ShowDialog();
        }

        void f_Closed(object sender, EventArgs e)
        {
            _显示模板_TextBox.Focus();
        }

    }
}

