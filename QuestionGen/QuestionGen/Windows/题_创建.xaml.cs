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
    public partial class 题_创建 : FloatableWindow
    {
        题.题 _original_题 = null;

        public 题_创建()
        {
            InitializeComponent();

            _original_题 = new 题.题();

            this.DataContext = _original_题;
        }


        private void _下一步_Button_Click(object sender, RoutedEventArgs e)
        {
            // 将数据上下文传到弹出窗口

            var f = new 题_选择_创建(_original_题) { ParentLayoutRoot = this.LayoutRoot };
            f.ShowDialog();
            f.Closed += (sender1, ea1) =>
            {
                if (f.DialogResult != null && f.DialogResult.Value) this.DialogResult = true;
            };
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

