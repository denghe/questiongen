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
using System.IO;

namespace QuestionGen.Windows
{
    public partial class 试题导出器 : FloatableWindow
    {
        public 试题导出器()
        {
            InitializeComponent();
            this.KeyDown += FloatableWindow_KeyDown;
        }

        TextBox _文本框 = null;

        public 试题导出器(String text)
            : this()
        {
            _代码_RichTextBox.Blocks.Clear();
            var sr = new StringReader(text);
            while (sr.Peek() > 0)
            {
                var s = sr.ReadLine();
                var cp = new Paragraph();
                _代码_RichTextBox.Blocks.Add(cp);
                cp.Inlines.Add(new Run { Text = s });
            }
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
                    case Key.W:
                        this.DialogResult = false;
                        break;
                }
            }
        }

        #endregion
    }
}

