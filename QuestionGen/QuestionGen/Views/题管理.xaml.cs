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

namespace QuestionGen.Views
{
    public partial class 题管理 : Page
    {
        服务.题Client _s = new 服务.题Client();

        public 题管理()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void _Select_Button_Click(object sender, RoutedEventArgs e)
        {
            //_s.
        }

        private void _Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new Editor_题 { ParentLayoutRoot = this.LayoutRoot };
            fw.ShowDialog();
        }
    }
}