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
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace QuestionGen.Windows
{
    public partial class 填空录入 : FloatableWindow
    {
        public 填空录入()
        {
            InitializeComponent();
        }

        //public 填空录入()
        //    : this()
        //{
        //}

        private void _提交_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void _取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}

