using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;

using SqlLib;
using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Converters
{
    public class 题_类型编号_类型名 : IValueConverter
    {
        public static List<题.类型> _类型s = null;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (_类型s == null) return value.ToString();
            var s = value.ToString();
            try
            {
                var i = int.Parse(value.ToString());
                s = _类型s.First(o => o.类型编号 == i).类型名;
            }
            catch (Exception)
            {
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }

    public class 题_知识面编号_名称 : IValueConverter
    {
        public static List<题.知识面> _知识面s = null;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (_知识面s == null) return value.ToString();
            var s = value.ToString();
            try
            {
                var i = int.Parse(value.ToString());
                s = _知识面s.First(o => o.知识面编号 == i).名称;
            }
            catch (Exception)
            {
            }
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

    }
}
