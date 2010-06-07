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
    public partial class 题_修改 : FloatableWindow
    {
        服务.题Client _s = new 服务.题Client();
        题.题 _题 = null;
        List<题.知识面> _知识面s = null;

        public 题_修改(题.题 row)
        {
            InitializeComponent();

            _s.知识面_获取Completed += (sender1, e1) =>
            {
                _知识面s = e1.Result.ToList<题.知识面>();
                _知识面s.Insert(0, new 题.知识面 { 知识面编号 = -1, 名称 = "请选择:" });
                _知识面_ComboBox.DisplayMemberPath = "名称";
                _知识面_ComboBox.ItemsSource = _知识面s;
                _知识面_ComboBox.SelectedIndex = 0;
                _下一步_Button.IsEnabled = true;
            };
            _s.知识面_获取Async(query.题.知识面.New().GetBytes());

            _题 = row;
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
            for (int i = 0; i < _知识面s.Count; i++)
            {
                if (_知识面s[i].知识面编号 == o.知识面编号)
                {
                    _知识面_ComboBox.SelectedIndex = i + 1;
                    break;
                }
            }
            _考核意图_TextBox.Text = o.考核意图;
            _备注_TextBox.Text = o.备注;
            _是否启用_CheckBox.IsChecked = o.是否启用;
        }
        private 题.题 GetValues()
        {
            // 将控件的数据整理成数据对象
            var o = new 题.题
            {
                难度系数 = (int)_难度系数_Slider.Value,
                考核意图 = _考核意图_TextBox.Text.Trim(),
                知识面编号 = (_知识面_ComboBox.SelectedItem as 题.知识面).知识面编号,
                是否启用 = _是否启用_CheckBox.IsChecked.Value
            };
            if (_类型_选择_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 1;
            }
            else if (_类型_填空_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 2;
            }
            else if (_类型_判断_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 3;
            }
            else if (_类型_问答_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 4;
            }
            else if (_类型_连线_RadioButton.IsChecked.Value)
            {
                o.类型编号 = 5;
            }

            return o;
        }

        private void _下一步_Button_Click(object sender, RoutedEventArgs e)
        {
            var o = GetValues();
            o.更新时间 = DateTime.Now;
            o.题编号 = _题.题编号;
            o.新版题编号 = _题.新版题编号;
            o.显示模板 = _题.显示模板;

            switch (o.类型编号)
            {
                case 1:
                    {
                        var f = new 题_选择_修改(o) { ParentLayoutRoot = this.LayoutRoot };
                        f.ShowDialog();
                        f.Closed += (sender1, ea1) =>
                        {
                            if (f.DialogResult != null && f.DialogResult.Value) this.DialogResult = true;
                        };
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        break;
                    }
            }

        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

