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
using QuestionGen.Converters;
using SqlLib;
using 题 = DAL.Database.Tables.题;
using query = DAL.Queries.Tables;
using QuestionGen.Modules;
using System.Xml.Linq;
using System.Xml;

namespace QuestionGen.Views
{
    public partial class 题管理 : Page
    {
        服务.题Client _s = new 服务.题Client();
        题.题 _selected_row = null;
        volatile int _count = 0;
        object _syncObj = new object();

        List<题.知识面> _知识面s = null;

        public 题管理()
        {
            InitializeComponent();

            _s.题_获取Completed += new EventHandler<服务.题_获取CompletedEventArgs>(_s_题_获取Completed);
            _s.类型_获取Completed += new EventHandler<服务.类型_获取CompletedEventArgs>(_s_类型_获取Completed);
            _s.知识面_获取Completed += new EventHandler<服务.知识面_获取CompletedEventArgs>(_s_知识面_获取Completed);

            _s.题_选择_答案_获取Completed += new EventHandler<服务.题_选择_答案_获取CompletedEventArgs>(_s_题_选择_答案_获取Completed);
            _s.题_选择_选项_获取Completed += new EventHandler<服务.题_选择_选项_获取CompletedEventArgs>(_s_题_选择_选项_获取Completed);

            _s.题_填空_答案_获取Completed += new EventHandler<服务.题_填空_答案_获取CompletedEventArgs>(_s_题_填空_答案_获取Completed);

            _s.题_问答_答案_获取Completed += new EventHandler<服务.题_问答_答案_获取CompletedEventArgs>(_s_题_问答_答案_获取Completed);

            _s.题_判断_答案_获取Completed += new EventHandler<服务.题_判断_答案_获取CompletedEventArgs>(_s_题_判断_答案_获取Completed);

            _刷新_Button_Click();
        }


        void _s_类型_获取Completed(object sender, 服务.类型_获取CompletedEventArgs e)
        {
            题_类型编号_类型名._类型s = e.Result.ToList<题.类型>();
            lock (_syncObj)
            {
                _count++;
                if (_count == 2)
                {
                    _s_数据_获取Completed();
                }
            }
        }

        void _s_知识面_获取Completed(object sender, 服务.知识面_获取CompletedEventArgs e)
        {
            题_知识面编号_名称._知识面s = e.Result.ToList<题.知识面>();
            lock (_syncObj)
            {
                _count++;
                if (_count == 2)
                {
                    _s_数据_获取Completed();
                }
            }
        }

        void _s_数据_获取Completed()
        {
            // 备份 知识面ComboBox 已选择数据
            var 知识面编号 = -1;
            if (_知识面_ComboBox.SelectedIndex > 0) 知识面编号 = ((题.知识面)_知识面_ComboBox.SelectedItem).知识面编号;

            // 构造　知识面ComboBox　数据源
            var rows = 题_知识面编号_名称._知识面s.Select(o => o).ToList();
            rows.Insert(0, new 题.知识面 { 知识面编号 = -1, 名称 = "（所有知识面）" });
            _知识面_ComboBox.DisplayMemberPath = "名称";
            _知识面_ComboBox.ItemsSource = rows;
            _知识面_ComboBox.SelectedIndex = 0;

            // 还原 知识面ComboBox 选择
            if (知识面编号 > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];
                    if (row.知识面编号 == 知识面编号)
                    {
                        _知识面_ComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }


            // 构造条件
            var q = query.题.题.New();
            if (_知识面_ComboBox.SelectedIndex > 0) q.Where.And(o => o.知识面编号 == ((题.知识面)_知识面_ComboBox.SelectedItem).知识面编号);
            if (_类型_选择_RadioButton.IsChecked.Value) q.Where.And(o => o.类型编号 == 1);
            else if (_类型_填空_RadioButton.IsChecked.Value) q.Where.And(o => o.类型编号 == 2);
            else if (_类型_判断_RadioButton.IsChecked.Value) q.Where.And(o => o.类型编号 == 3);
            else if (_类型_问答_RadioButton.IsChecked.Value) q.Where.And(o => o.类型编号 == 4);
            else if (_类型_连线_RadioButton.IsChecked.Value) q.Where.And(o => o.类型编号 == 5);
            if (_题面过滤_TextBox.Text.Length > 0) q.Where.And(o => o.显示模板.Like("%" + _题面过滤_TextBox.Text.Trim() + "%"));
            if (_意图过滤_TextBox.Text.Length > 0) q.Where.And(o => o.显示模板.Like("%" + _意图过滤_TextBox.Text.Trim() + "%"));
            if (_备注过滤_TextBox.Text.Length > 0) q.Where.And(o => o.显示模板.Like("%" + _备注过滤_TextBox.Text.Trim() + "%"));
            _s.题_获取Async(q.GetBytes());
        }

        void _s_题_获取Completed(object sender, 服务.题_获取CompletedEventArgs e)
        {
            var rows = e.Result.ToList<题.题>();
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
            _count = 0;
            _s.类型_获取Async(query.题.类型.New().GetBytes());
            _s.知识面_获取Async(query.题.知识面.New().GetBytes());
            _selected_row = null;
            EnableControls();
        }

        题.题 _selected_row_backup = null;

        private void _创建_Button_Click(object sender, RoutedEventArgs e)
        {
            var fw = new 题_创建 { ParentLayoutRoot = this.LayoutRoot };
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
            var fw = new 题_修改(_selected_row) { ParentLayoutRoot = this.LayoutRoot };
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
            // todo
        }

        private void _DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                _selected_row = (题.题)e.AddedItems[0];
            else _selected_row = null;
            EnableControls();
        }

        private void _DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (_selected_row_backup != null)
            {
                var row = (题.题)e.Row.DataContext;
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
                _考核意图_TextBox.Text = "";
                _备注_TextBox.Text = "";
                _预览_RichTextBox.Blocks.Clear();
            }
            else
            {
                _修改_Button.IsEnabled = _删除_Button.IsEnabled = true;
                _考核意图_TextBox.Text = _selected_row.考核意图;
                _备注_TextBox.Text = _selected_row.备注;
                _DataGrid.IsEnabled = false;

                #region 生成预览
                switch (_selected_row.类型编号)
                {
                    case 1:
                        {
                            _选择题 = new 选择题 { 题 = _selected_row };
                            _选择题_数据获取次数 = 0;

                            _s.题_选择_选项_获取Async(query.题.题_选择_选项.New(where: o => o.题编号 == _选择题.题.题编号).GetBytes());
                            _s.题_选择_答案_获取Async(query.题.题_选择_答案.New(where: o => o.题编号 == _选择题.题.题编号).GetBytes());
                        }
                        break;
                    case 2:
                        {
                            _填空题 = new 填空题 { 题 = _selected_row };
                            _s.题_填空_答案_获取Async(query.题.题_填空_答案.New(where: o => o.题编号 == _填空题.题.题编号).GetBytes());
                        }
                        break;
                    case 3:
                        {
                            _判断题 = new 判断题 { 题 = _selected_row };
                            _s.题_判断_答案_获取Async(query.题.题_判断_答案.New(where: o => o.题编号 == _判断题.题.题编号).GetBytes());
                        }
                        break;
                    case 4:
                        {
                            _问答题 = new 问答题 { 题 = _selected_row };
                            _s.题_问答_答案_获取Async(query.题.题_问答_答案.New(where: o => o.题编号 == _问答题.题.题编号).GetBytes());
                        }
                        break;
                    case 5:
                        {
                        }
                        break;
                }
                #endregion
            }
        }

        #region 选择题预览

        选择题 _选择题 = null;
        volatile int _选择题_数据获取次数 = 0;
        object _选择_syncObj = new object();
        void _s_题_选择_选项_获取Completed(object sender, 服务.题_选择_选项_获取CompletedEventArgs e)
        {
            _选择题.选项 = e.Result.ToList<题.题_选择_选项>();
            lock (_选择_syncObj)
            {
                if (++_选择题_数据获取次数 == 2) _s_题_选择_获取Completed();
            }
        }
        void _s_题_选择_答案_获取Completed(object sender, 服务.题_选择_答案_获取CompletedEventArgs e)
        {
            _选择题.答案 = e.Result.ToList<题.题_选择_答案>();
            lock (_选择_syncObj)
            {
                if (++_选择题_数据获取次数 == 2) _s_题_选择_获取Completed();
            }
        }
        void _s_题_选择_获取Completed()
        {
            if (_selected_row == null || _selected_row.题编号 != _选择题.题.题编号) return;

            #region 预览

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + _选择题.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xns = xe.Nodes();
            var p = new Paragraph { FontSize = 16 };
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"题面:

"
            });
            foreach (var xn in xns)
            {
                if (xn.NodeType == XmlNodeType.Text)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Normal
                        ,
                        Text = ((XText)xn).Value
                    });
                }
                else
                {
                    // todo: 判断是图片 还是 答案格子

                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                        ,
                        FontWeight = FontWeights.Bold
                        ,
                        Text = "___"
                    });
                }
            }
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

选项:
"
            });

            foreach (var c in _选择题.选项)
            {
                p.Inlines.Add(new Run
                {
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                    ,
                    FontWeight = FontWeights.Bold
                    ,
                    Text = Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[c.选项序号 - 1] + ".  "
                });

                xe = XElement.Parse("<___root___>" + c.显示模板 + "</___root___>");
                xns = xe.Nodes();
                foreach (var xn in xns)
                {
                    if (xn.NodeType == XmlNodeType.Text)
                    {
                        p.Inlines.Add(new Run
                        {
                            Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                            ,
                            FontWeight = FontWeights.Normal
                            ,
                            Text = ((XText)xn).Value
                        });
                    }
                    else
                    {
                        // todo: 处理图片
                    }
                }
            }


            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

答案:
"
            });

            // 先按格子号 分组

            var group = from x in _选择题.答案 group x by x.格子序号 into g select g;

            foreach (var g in group)
            {
                p.Inlines.Add(new Run
                {
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                    ,
                    FontWeight = FontWeights.Normal
                    ,
                    Text = Environment.NewLine + g.Key.ToString() + ".  "
                });

                foreach (var a in g)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Bold
                        ,
                        Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[a.选项序号 - 1] + " "
                    });
                }
            }

            _预览_RichTextBox.Blocks.Add(p);

            // todo: 翻到最上面
            #endregion

            // 还原控件状态
            _DataGrid.IsEnabled = true;
            _DataGrid.Focus();
        }

        #endregion

        填空题 _填空题 = null;
        void _s_题_填空_答案_获取Completed(object sender, 服务.题_填空_答案_获取CompletedEventArgs e)
        {
            if (_selected_row == null || _selected_row.题编号 != _填空题.题.题编号) return;
            _填空题.答案 = e.Result.ToList<题.题_填空_答案>();

            #region 预览

            var result = _填空题;

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + result.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xns = xe.Nodes();
            var p = new Paragraph { FontSize = 16 };
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"题面:

"
            });
            foreach (var xn in xns)
            {
                if (xn.NodeType == XmlNodeType.Text)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Normal
                        ,
                        Text = ((XText)xn).Value
                    });
                }
                else
                {
                    if (xn.NodeType == XmlNodeType.Element)
                    {
                        var x = (XElement)xn;
                        if (x.Name.LocalName == "c")
                        {
                            p.Inlines.Add(new Run
                            {
                                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                                ,
                                FontWeight = FontWeights.Bold
                                ,
                                Text = " _" + x.Value + "_ "
                            });
                        }
                        else if (x.Name.LocalName == "a")
                        {
                            // todo: 图片
                        }
                    }
                }
            }

            _预览_RichTextBox.Blocks.Add(p);

            #endregion

            // 还原控件状态
            _DataGrid.IsEnabled = true;
            _DataGrid.Focus();
        }

        判断题 _判断题 = null;
        void _s_题_判断_答案_获取Completed(object sender, 服务.题_判断_答案_获取CompletedEventArgs e)
        {
            if (_selected_row == null || _selected_row.题编号 != _判断题.题.题编号) return;
            _判断题.答案 = e.Result.ToList<题.题_判断_答案>().FirstOrDefault();

            #region 预览
            var result = _判断题;

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + result.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xns = xe.Nodes();
            var p = new Paragraph { FontSize = 16 };
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"题面:

"
            });
            foreach (var xn in xns)
            {
                if (xn.NodeType == XmlNodeType.Text)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Normal
                        ,
                        Text = ((XText)xn).Value
                    });
                }
                else
                {
                    // todo: 判断是图片 还是 答案格子

                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255))
                        ,
                        FontWeight = FontWeights.Bold
                        ,
                        Text = "___"
                    });
                }
            }

            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

答案:
"
            });

            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = result.答案.答案 ? "正确" : "错误"
            });

            _预览_RichTextBox.Blocks.Add(p);
            #endregion

            // 还原控件状态
            _DataGrid.IsEnabled = true;
            _DataGrid.Focus();
        }

        问答题 _问答题 = null;
        void _s_题_问答_答案_获取Completed(object sender, 服务.题_问答_答案_获取CompletedEventArgs e)
        {
            if (_selected_row == null || _selected_row.题编号 != _问答题.题.题编号) return;
            _问答题.答案 = e.Result.ToList<题.题_问答_答案>().FirstOrDefault();

            #region 预览
            var result = _问答题;

            _预览_RichTextBox.Blocks.Clear();

            var s = "<___root___>" + result.题.显示模板 + "</___root___>";
            var xe = XElement.Parse(s);
            var xns = xe.Nodes();
            var p = new Paragraph { FontSize = 16 };
            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"题面:

"
            });
            foreach (var xn in xns)
            {
                if (xn.NodeType == XmlNodeType.Text)
                {
                    p.Inlines.Add(new Run
                    {
                        Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                        ,
                        FontWeight = FontWeights.Normal
                        ,
                        Text = ((XText)xn).Value
                    });
                }
                else
                {
                    // todo: 图片处理
                }
            }

            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = @"

答案:
"
            });

            p.Inlines.Add(new Run
            {
                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
                ,
                FontWeight = FontWeights.Bold
                ,
                Text = result.答案.参考答案
            });

            _预览_RichTextBox.Blocks.Add(p);
            #endregion

            // 还原控件状态
            _DataGrid.IsEnabled = true;
            _DataGrid.Focus();
        }

    }
}