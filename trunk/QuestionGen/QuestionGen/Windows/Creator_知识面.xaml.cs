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
    public partial class Creator_知识面 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _original_row = new 题.知识面();

        public Creator_知识面()
        {
            InitializeComponent();

            this.DataContext = _original_row;

            _s.知识面_插入Completed += new EventHandler<服务.知识面_插入CompletedEventArgs>(_s_知识面_插入Completed);
        }

        void _s_知识面_插入Completed(object sender, 服务.知识面_插入CompletedEventArgs e)
        {
            if (e.Result < 0)
            {
                MessageBox.Show("知识面 名称 已存在, 插入失败");
                this._Submit_Button.IsEnabled = true;
                _名称_TextBox.Focus();
            }
            else this.DialogResult = true;
        }

        private void _Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            this._Submit_Button.IsEnabled = false;
            _s.知识面_插入Async(_original_row.GetBytes());
        }

        private void _Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

