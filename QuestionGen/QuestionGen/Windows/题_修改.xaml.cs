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
using QuestionGen.Modules;


namespace QuestionGen.Windows
{
    public partial class 题_修改 : FloatableWindow
    {
        服务.题Client _s = null;
        题.题 _题 = null;
        List<题.知识面> _知识面s = null;

        public 题_修改(题.题 o)
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;

            _s = new 服务.题Client();
            _题 = o;

            _s.知识面_获取Completed += (sender1, e1) =>
            {
                _知识面s = e1.Result.ToList<题.知识面>();
                _知识面s.Insert(0, new 题.知识面 { 知识面编号 = -1, 名称 = "请选择:" });
                _知识面_ComboBox.DisplayMemberPath = "名称";
                _知识面_ComboBox.ItemsSource = _知识面s;
                _知识面_ComboBox.SelectedIndex = 0;
                _下一步_Button.IsEnabled = true;

                SetValues(_题);
            };

            _s.题_选择_选项_获取Completed += new EventHandler<服务.题_选择_选项_获取CompletedEventArgs>(_s_题_选择_选项_获取Completed);
            _s.题_选择_答案_获取Completed += new EventHandler<服务.题_选择_答案_获取CompletedEventArgs>(_s_题_选择_答案_获取Completed);
            _s.题_判断_答案_获取Completed += new EventHandler<服务.题_判断_答案_获取CompletedEventArgs>(_s_题_判断_答案_获取Completed);
            _s.题_填空_答案_获取Completed += new EventHandler<服务.题_填空_答案_获取CompletedEventArgs>(_s_题_填空_答案_获取Completed);
            _s.题_问答_答案_获取Completed += new EventHandler<服务.题_问答_答案_获取CompletedEventArgs>(_s_题_问答_答案_获取Completed);
            // _s.题_连线_.....

            _s.知识面_获取Async(query.题.知识面.New().GetBytes());
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
                        if (_下一步_Button.IsEnabled) _下一步_Button_Click(null, null);
                        break;
                    case Key.W:
                        if (_取消_Button.IsEnabled) _取消_Button_Click(null, null);
                        break;
                }
            }
        }

        #endregion

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
            for (int i = 0; i < _知识面s.Count; i++)
            {
                if (_知识面s[i].知识面编号 == o.知识面编号)
                {
                    _知识面_ComboBox.SelectedIndex = i;
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
                备注 = _备注_TextBox.Text.Trim(),
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
            _题.难度系数 = o.难度系数;
            _题.考核意图 = o.考核意图;
            _题.知识面编号 = o.知识面编号;
            _题.备注 = o.备注;
            _题.是否启用 = o.是否启用;

            _下一步_Button.IsEnabled = false;

            switch (_题.类型编号)
            {
                case 1:
                    {
                        _题_选择_选项 = null;
                        _题_选择_答案 = null;

                        _s.题_选择_选项_获取Async(query.题.题_选择_选项.New(a => a.题编号 == _题.题编号).GetBytes());
                        _s.题_选择_答案_获取Async(query.题.题_选择_答案.New(a => a.题编号 == _题.题编号).GetBytes());
                        break;
                    }
                case 2:
                    {
                        _s.题_填空_答案_获取Async(query.题.题_填空_答案.New(a => a.题编号 == _题.题编号).GetBytes());
                        break;
                    }
                case 3:
                    {
                        _s.题_判断_答案_获取Async(query.题.题_判断_答案.New(a => a.题编号 == _题.题编号).GetBytes());
                        break;
                    }
                case 4:
                    {
                        _s.题_问答_答案_获取Async(query.题.题_问答_答案.New(a => a.题编号 == _题.题编号).GetBytes());
                        break;
                    }
                case 5:
                    {
                        break;
                    }
            }

        }

        List<题.题_选择_选项> _题_选择_选项 = null;
        void _s_题_选择_选项_获取Completed(object sender, 服务.题_选择_选项_获取CompletedEventArgs e)
        {
            lock (_syncObj)
            {
                _题_选择_选项 = e.Result.ToList<题.题_选择_选项>();
                _s_题_选择_获取Completed();
            }
        }

        List<题.题_选择_答案> _题_选择_答案 = null;
        void _s_题_选择_答案_获取Completed(object sender, 服务.题_选择_答案_获取CompletedEventArgs e)
        {
            lock (_syncObj)
            {
                _题_选择_答案 = e.Result.ToList<题.题_选择_答案>();
                _s_题_选择_获取Completed();
            }
        }

        object _syncObj = new object();
        void _s_题_选择_获取Completed()
        {
            if (_题_选择_答案 == null || _题_选择_选项 == null) return;
            var data = new 选择题 { 题 = _题, 选项 = _题_选择_选项, 答案 = _题_选择_答案 };
            var f = new 题_选择_修改(data) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += f_Closed;
            f.ShowDialog();
        }


        void _s_题_判断_答案_获取Completed(object sender, 服务.题_判断_答案_获取CompletedEventArgs e)
        {
            var data = new 判断题 { 题 = _题, 答案 = e.Result.ToList<题.题_判断_答案>().FirstOrDefault() };
            var f = new 题_判断_修改(data) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += f_Closed;
            f.ShowDialog();
        }



        void _s_题_填空_答案_获取Completed(object sender, 服务.题_填空_答案_获取CompletedEventArgs e)
        {
            var data = new 填空题 { 题 = _题, 答案 = e.Result.ToList<题.题_填空_答案>() };
            var f = new 题_填空_修改(data) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += f_Closed;
            f.ShowDialog();
        }

        void _s_题_问答_答案_获取Completed(object sender, 服务.题_问答_答案_获取CompletedEventArgs e)
        {
            var data = new 问答题 { 题 = _题, 答案 = e.Result.ToList<题.题_问答_答案>().FirstOrDefault() };
            var f = new 题_问答_修改(data) { ParentLayoutRoot = this.LayoutRoot };
            f.Closed += f_Closed;
            f.ShowDialog();
        }



        void f_Closed(object sender, EventArgs e)
        {
            var f = sender as FloatableWindow;
            if (f.DialogResult != null && f.DialogResult.Value) this.DialogResult = true;
            else _下一步_Button.IsEnabled = true;
        }


        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

