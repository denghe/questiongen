﻿using System;
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
    public partial class 知识面_创建 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _original_row = new 题.知识面();

        public 知识面_创建()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            this.DataContext = _original_row;
            _s.知识面_插入Completed += new EventHandler<服务.知识面_插入CompletedEventArgs>(_s_知识面_插入Completed);
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

        void _s_知识面_插入Completed(object sender, 服务.知识面_插入CompletedEventArgs e)
        {
            if (e.Result < 0)
            {
                MessageBox.Show("知识面 名称 已存在, 插入失败");
                this._提交_Button.IsEnabled = true;
                _名称_TextBox.Focus();
            }
            else this.DialogResult = true;
        }

        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            this._提交_Button.IsEnabled = false;
            _s.知识面_插入Async(_original_row.GetBytes());
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

