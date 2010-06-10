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
using query = DAL.Queries.Tables;


namespace QuestionGen.Windows
{
    public partial class 题_创建 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();

        public 题_创建()
        {
            InitializeComponent();
            _下一步_Button.IsEnabled = false;
            _s.知识面_获取Completed += (sender1, e1) =>
            {
                var rows = e1.Result.ToList<题.知识面>();
                rows.Insert(0, new 题.知识面 { 知识面编号 = -1, 名称 = "请选择:" });
                _知识面_ComboBox.DisplayMemberPath = "名称";
                _知识面_ComboBox.ItemsSource = rows;
                _知识面_ComboBox.SelectedIndex = 0;
                _下一步_Button.IsEnabled = true;
            };
            _s.知识面_获取Async(query.题.知识面.New().GetBytes());
        }

        private void _下一步_Button_Click(object sender, RoutedEventArgs e)
        {
            // 前置判断
            if (!_类型_选择_RadioButton.IsChecked.Value &&
                !_类型_填空_RadioButton.IsChecked.Value &&
                !_类型_判断_RadioButton.IsChecked.Value &&
                !_类型_问答_RadioButton.IsChecked.Value &&
                !_类型_连线_RadioButton.IsChecked.Value
                )
            {
                MessageBox.Show("请选择 题 类型.");
                return;
            }

            if (_知识面_ComboBox.SelectedIndex < 1)
            {
                MessageBox.Show("请选择 题 知识面.");
                _知识面_ComboBox.Focus();
                return;
            }

            // 将控件的数据整理成数据对象
            var o = new 题.题
            {
                难度系数 = (int)_难度系数_Slider.Value,
                考核意图 = _考核意图_TextBox.Text.Trim(),
                知识面编号 = (_知识面_ComboBox.SelectedItem as 题.知识面).知识面编号,
                备注 = _备注_TextBox.Text.Trim(),
                是否启用 = _是否启用_CheckBox.IsChecked.Value
            };
            if (_类型_选择_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 1;

                var f = new 题_选择_创建(o) { ParentLayoutRoot = this.LayoutRoot };
                f.Closed += f_Closed;
                f.ShowDialog();
            }
            else if (_类型_填空_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 2;

                var f = new 题_填空_创建(o) { ParentLayoutRoot = this.LayoutRoot };
                f.Closed += f_Closed;
                f.ShowDialog();
            }
            else if (_类型_判断_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 3;

                var f = new 题_判断_创建(o) { ParentLayoutRoot = this.LayoutRoot };
                f.Closed += f_Closed;
                f.ShowDialog();
            }
            else if (_类型_问答_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 4;
            }
            else if (_类型_连线_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 5;
            }

        }

        void f_Closed(object sender, EventArgs e)
        {
            var f = sender as FloatableWindow;
            if (f.DialogResult != null && f.DialogResult.Value) this.DialogResult = true;
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

