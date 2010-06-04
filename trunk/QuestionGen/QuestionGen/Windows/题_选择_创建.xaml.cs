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
    public partial class 题_选择_创建 : FloatableWindow
    {
        题.题 _orginal_题 = null;

        public 题_选择_创建()
        {
            InitializeComponent();
        }
        public 题_选择_创建(题.题 o) : this()
        {
            _orginal_题 = o;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

