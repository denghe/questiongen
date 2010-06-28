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
using System.Windows.Navigation;
using System.Windows.Shapes;

using QuestionGen.Windows;
using SqlLib;
using 题 = DAL.Database.Tables.题;
using query = DAL.Queries.Tables;

namespace QuestionGen.Views
{
    public partial class 知识面管理 : Page
    {
        服务.题Client _s = new 服务.题Client();

        题.知识面 _selected_row = null;

        public 知识面管理()
        {
            InitializeComponent();

            _s.知识面_获取Completed += _s_知识面_获取Completed;

            _刷新_Button_Click();
        }

        void _s_知识面_获取Completed(object sender, 服务.知识面_获取CompletedEventArgs e)
        {
            var rows = e.Result.ToList<题.知识面>();
            _DataGrid.ItemsSource = rows;
            _刷新_Button.IsEnabled = true;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }



        /// <summary>
        /// todo: 没有数据时, 提示 无数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _刷新_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _刷新_Button.IsEnabled = false;
            _s.知识面_获取Async(query.题.知识面.New().GetBytes());
            _selected_row = null;
            EnableControls();
        }

        题.知识面 _selected_row_backup = null;

        private void _创建_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new 知识面_创建 { ParentLayoutRoot = this.LayoutRoot };
            fw.ShowDialog();
            fw.Closed += (sender1, e1) =>
            {
                if (fw.DialogResult != null && fw.DialogResult.Value)
                {
                    _刷新_Button_Click();
                }

            };
        }

        private void _修改_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new 知识面_修改(_selected_row) { ParentLayoutRoot = this.LayoutRoot };
            fw.ShowDialog();
            fw.Closed += (sender1, e1) =>
            {
                if (fw.DialogResult != null && fw.DialogResult.Value)
                {
                    _selected_row_backup = _selected_row;
                    _刷新_Button_Click();
                }
            };
        }

        private void _删除_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new 知识面_删除(_selected_row) { ParentLayoutRoot = this.LayoutRoot };
            fw.ShowDialog();
            fw.Closed += (sender1, e1) =>
            {
                if (fw.DialogResult != null && fw.DialogResult.Value)
                {
                    _刷新_Button_Click();
                }
            };
        }

        private void _DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                _selected_row = (题.知识面)e.AddedItems[0];
            else _selected_row = null;
            EnableControls();
        }

        private void _DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (_selected_row_backup != null)
            {
                var row = (题.知识面)e.Row.DataContext;
                if (row.知识面编号 == _selected_row_backup.知识面编号)
                {
                    _selected_row_backup = null;
                    _DataGrid.SelectedIndex = e.Row.GetIndex();
                }
            }
        }





        void EnableControls()
        {
            if (_selected_row == null)
            {
                _修改_Button.IsEnabled = _删除_Button.IsEnabled = false;
            }
            else
            {
                _修改_Button.IsEnabled = _删除_Button.IsEnabled = true;
            }
        }
    }
}