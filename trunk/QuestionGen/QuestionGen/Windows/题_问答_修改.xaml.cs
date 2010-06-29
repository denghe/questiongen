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
    public partial class 题_问答_修改 : FloatableWindow
    {
        #region 字段

        问答题 _问答题 = null;
        服务.题Client _s = null;

        #endregion

        #region 构造函数

        public 题_问答_修改()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            _重置_Button_Click();
        }

        public 题_问答_修改(问答题 o)
            : this()
        {
            _问答题 = o;       // todo: backup

            _s = new 服务.题Client();
            _s.题_问答_修改Completed += new EventHandler<服务.题_问答_修改CompletedEventArgs>(_s_题_问答_修改Completed);

            SetValues();
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
                        if (_提交_Button.IsEnabled) _提交_Button_Click(null, null);
                        break;
                    case Key.W:
                        if (_取消_Button.IsEnabled) _取消_Button_Click(null, null);
                        break;
                }
            }
        }

        #endregion

        #region SetValues

        private void SetValues()
        {
            _显示模板_TextBox.Text = _问答题.题.显示模板;
            _答案_TextBox.Text =_问答题.答案.参考答案;

            _预览_Button_Click();
        }

        #endregion

        #region GetValues

        /// <summary>
        /// 将控件内容填充到 问答题 容器 并返回
        /// </summary>
        private void GetValues()
        {
            _问答题.题.显示模板 = _显示模板_TextBox.Text.Trim();
            _问答题.答案 = new 题.题_问答_答案 { 参考答案 = _答案_TextBox.Text };
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

            var s = "<___root___>" + _问答题.题.显示模板 + "</___root___>";
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
                    // todo: 图片处理
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
                Text = _问答题.答案.参考答案
            });

            _预览_RichTextBox.Blocks.Add(p);
        }

        #endregion

        void _s_题_问答_修改Completed(object sender, 服务.题_问答_修改CompletedEventArgs e)
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

                _s.题_问答_修改Async(_问答题.题.GetBytes(), _问答题.答案.GetBytes());
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
            var f = new 模板编辑器(_当前焦点文本框) { ParentLayoutRoot = this.LayoutRoot };
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

