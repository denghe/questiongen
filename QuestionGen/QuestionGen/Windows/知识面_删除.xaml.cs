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
    public partial class 知识面_删除 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _original_row = null;

        public 知识面_删除()
        {
            InitializeComponent();

            _s.知识面_删除Completed += new EventHandler<服务.知识面_删除CompletedEventArgs>(_s_知识面_删除Completed);
        }

        public 知识面_删除(题.知识面 original_row)
            : this()
        {
            _original_row = original_row;

            this.DataContext = _original_row;
        }





        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void _确认删除_Button_Click(object sender, RoutedEventArgs e)
        {
            this._确认删除_Button.IsEnabled = false;
            // 删除操作只用传含有主键的对象就行了
            var row = new 题.知识面 { 知识面编号 = _original_row.知识面编号 };
            _s.知识面_删除Async(row.GetBytes());
        }

        void _s_知识面_删除Completed(object sender, 服务.知识面_删除CompletedEventArgs e)
        {
            if (e.Result < 0)
            {
                MessageBox.Show("知识面 删除失败");
                this._确认删除_Button.IsEnabled = true;
                _名称_TextBox.Focus();
            }
            else this.DialogResult = true;
        }

    }
}

