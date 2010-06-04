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
    public partial class Editor_知识面 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _original_row = null;

        public Editor_知识面()
        {
            InitializeComponent();

            _s.知识面_更新Completed += new EventHandler<服务.知识面_更新CompletedEventArgs>(_s_知识面_更新Completed);
        }

        public Editor_知识面(题.知识面 original_row)
        {
            _original_row = original_row;
        }

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
            _s.知识面_更新Async(_original_row.知识面编号, _名称_TextBox.Text.Trim());
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void _还原_Button_Click(object sender, RoutedEventArgs e)
        {
            _名称_TextBox.Text = _original_row.名称;
        }
    }
}

