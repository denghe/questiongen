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

using DAL.Database.Tables;
using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Windows
{
    public partial class 知识面_修改 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _original_row = null;
        题.知识面 _current_row = null;

        public 知识面_修改()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            _s.知识面_更新Completed += new EventHandler<服务.知识面_更新CompletedEventArgs>(_s_知识面_更新Completed);
        }

        public 知识面_修改(题.知识面 original_row)
            : this()
        {
            _original_row = original_row;
            _current_row = new 题.知识面 { 知识面编号 = _original_row.知识面编号, 名称 = _original_row.名称 };

            this.DataContext = _current_row;

            _还原_Button_Click();
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
                        if (_提交_Button.IsEnabled) _提交_Button_Click();
                        break;
                    case Key.W:
                        if (_取消_Button.IsEnabled) _取消_Button_Click();
                        break;
                }
            }
        }

        #endregion

        void _s_知识面_更新Completed(object sender, 服务.知识面_更新CompletedEventArgs e)
        {
            if (e.Result < 0)
            {
                MessageBox.Show("知识面 名称 更新失败");
                this._提交_Button.IsEnabled = true;
                _名称_TextBox.Focus();
            }
            else this.DialogResult = true;
        }

        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            this._提交_Button.IsEnabled = false;

            _s.知识面_更新Async(_current_row.GetBytes());
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void _还原_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _current_row.名称 = _original_row.名称;
            this.DataContext = null;
            this.DataContext = _current_row;
        }
    }
}

