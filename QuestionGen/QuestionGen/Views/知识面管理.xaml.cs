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
using db = DAL.Database.Tables;
using query = DAL.Queries.Tables;

namespace QuestionGen.Views
{
    public partial class 知识面管理 : Page
    {
        服务.题Client _s = new 服务.题Client();

        db.题.知识面 _selected_row = null;

        public 知识面管理()
        {
            InitializeComponent();

            _s.知识面_获取Completed += new EventHandler<服务.知识面_获取CompletedEventArgs>(_s_知识面_获取Completed);
        }

        void _s_知识面_获取Completed(object sender, 服务.知识面_获取CompletedEventArgs e)
        {
            var rows = e.Result.ToList<db.题.知识面>();
            _DataGrid.ItemsSource = rows;
            _刷新_Button.IsEnabled = true;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void _刷新_Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _刷新_Button.IsEnabled = false;
            _s.知识面_获取Async(query.题.知识面.New().GetBytes());
            _selected_row = null;
            EnableControls();
        }

        private void _创建_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new Creator_知识面 { ParentLayoutRoot = this.LayoutRoot };
            fw.ShowDialog();
            fw.Closed += new EventHandler(fw_Closed);
        }

        void fw_Closed(object sender, EventArgs e)
        {
            _刷新_Button_Click();
        }

        private void _修改_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _删除_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected_row = (db.题.知识面)e.AddedItems[0];
            EnableControls();
        }

        void EnableControls()
        {
            if (_selected_row == null)
            {
                _修改_Button.IsEnabled = _删除_Button.IsEnabled = false;
            }
            else
            {
                _修改_Button.IsEnabled = _删除_Button.IsEnabled = false;
            }
        }
    }
}