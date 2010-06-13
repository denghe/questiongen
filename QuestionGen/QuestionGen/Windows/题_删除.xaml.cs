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

using SqlLib;
using 题 = DAL.Database.Tables.题;
using exp = DAL.Expressions.Tables;
using QuestionGen.Modules;
using QuestionGen.Converters;


namespace QuestionGen.Windows
{
    public partial class 题_删除 : FloatableWindow
    {
        服务.题Client _s = null;
        题.题 _题 = null;

        public 题_删除(题.题 o)
        {
            InitializeComponent();

            _题 = o;

            _s = new 服务.题Client();
            _s.题_删除Completed += new EventHandler<服务.题_删除CompletedEventArgs>(_s_题_删除Completed);

            _知识面_ComboBox.DisplayMemberPath = "名称";
            _知识面_ComboBox.ItemsSource = 题_知识面编号_名称._知识面s;
            _知识面_ComboBox.SelectedIndex = 0;
            _确认删除_Button.IsEnabled = true;

            SetValues(_题);
        }


        private void SetValues(题.题 o)
        {
            switch (o.类型编号)
            {
                case 1:
                    _类型_选择_RadioButton.IsChecked = true;
                    break;
                case 2:
                    _类型_填空_RadioButton.IsChecked = true;
                    break;
                case 3:
                    _类型_判断_RadioButton.IsChecked = true;
                    break;
                case 4:
                    _类型_问答_RadioButton.IsChecked = true;
                    break;
                case 5:
                    _类型_连线_RadioButton.IsChecked = true;
                    break;
            }
            _难度系数_Slider.Value = o.难度系数;
            _知识面_ComboBox.SelectedIndex = 0;
            for (int i = 0; i < 题_知识面编号_名称._知识面s.Count; i++)
            {
                if (题_知识面编号_名称._知识面s[i].知识面编号 == o.知识面编号)
                {
                    _知识面_ComboBox.SelectedIndex = i;
                    break;
                }
            }
            _考核意图_TextBox.Text = o.考核意图;
            _备注_TextBox.Text = o.备注;
            _是否启用_CheckBox.IsChecked = o.是否启用;
            _显示模板_TextBox.Text = o.显示模板;
        }

        private void _确认删除_Button_Click(object sender, RoutedEventArgs e)
        {
            _确认删除_Button.IsEnabled = false;
            _s.题_删除Async(exp.题.题.New(o => o.题编号 == _题.题编号).GetBytes());
        }
        void _s_题_删除Completed(object sender, 服务.题_删除CompletedEventArgs e)
        {
            if (e.Result >= 0)
            {
                this.DialogResult = true;
            }
            else
            {
                _确认删除_Button.IsEnabled = true;
                MessageBox.Show("删除失败");
            }
        }


        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

