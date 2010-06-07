using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SqlLib;

namespace DAL.Database.Tables.人
{

    partial class 岗位 : ISerial
    {

        #region Constructor

        public 岗位() {
        }
        public 岗位(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 岗位(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.岗位编号.GetBytes(),
                this.名称.GetBytes(),
                this.备注.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.岗位编号 = buffer.ToInt32(ref startIndex);
            this.名称 = buffer.ToString(ref startIndex);
            this.备注 = buffer.ToString(ref startIndex);
        }
        #endregion

    }
    partial class 岗位_知识面 : ISerial
    {

        #region Constructor

        public 岗位_知识面() {
        }
        public 岗位_知识面(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 岗位_知识面(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.岗位编号.GetBytes(),
                this.知识面编号.GetBytes(),
                this.深度.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.岗位编号 = buffer.ToInt32(ref startIndex);
            this.知识面编号 = buffer.ToInt32(ref startIndex);
            this.深度 = buffer.ToInt32(ref startIndex);
        }
        #endregion

    }
}
namespace DAL.Database.Tables.题
{

    partial class 附件 : ISerial
    {

        #region Constructor

        public 附件() {
        }
        public 附件(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 附件(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.附件编号.GetBytes(),
                this.题编号.GetBytes(),
                this.是否为图片.GetBytes(),
                this.数据.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.附件编号 = buffer.ToGuid(ref startIndex);
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.是否为图片 = buffer.ToBoolean(ref startIndex);
            this.数据 = buffer.ToBytes(ref startIndex);
        }
        #endregion

    }
    partial class 类型 : ISerial
    {

        #region Constructor

        public 类型() {
        }
        public 类型(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 类型(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.类型编号.GetBytes(),
                this.类型名.GetBytes(),
                this.是否需要专业阅卷.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.类型编号 = buffer.ToInt32(ref startIndex);
            this.类型名 = buffer.ToString(ref startIndex);
            this.是否需要专业阅卷 = buffer.ToBoolean(ref startIndex);
        }
        #endregion

    }
    partial class 题 : ISerial
    {

        #region Constructor

        public 题() {
        }
        public 题(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.新版题编号.GetBytes(),
                this.类型编号.GetBytes(),
                this.知识面编号.GetBytes(),
                this.难度系数.GetBytes(),
                this.显示模板.GetBytes(),
                this.考核意图.GetBytes(),
                this.备注.GetBytes(),
                this.更新时间.GetBytes(),
                this.是否启用.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.新版题编号 = buffer.ToNullableInt32(ref startIndex);
            this.类型编号 = buffer.ToInt32(ref startIndex);
            this.知识面编号 = buffer.ToInt32(ref startIndex);
            this.难度系数 = buffer.ToInt32(ref startIndex);
            this.显示模板 = buffer.ToString(ref startIndex);
            this.考核意图 = buffer.ToString(ref startIndex);
            this.备注 = buffer.ToString(ref startIndex);
            this.更新时间 = buffer.ToDateTime(ref startIndex);
            this.是否启用 = buffer.ToBoolean(ref startIndex);
        }
        #endregion

    }
    partial class 题_连线 : ISerial
    {

        #region Constructor

        public 题_连线() {
        }
        public 题_连线(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_连线(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.连线编号.GetBytes(),
                this.题编号.GetBytes(),
                this.组序号.GetBytes(),
                this.显示模板.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.连线编号 = buffer.ToInt32(ref startIndex);
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.组序号 = buffer.ToInt32(ref startIndex);
            this.显示模板 = buffer.ToString(ref startIndex);
        }
        #endregion

    }
    partial class 题_连线_答案 : ISerial
    {

        #region Constructor

        public 题_连线_答案() {
        }
        public 题_连线_答案(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_连线_答案(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.连线编号A.GetBytes(),
                this.连线编号B.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.连线编号A = buffer.ToInt32(ref startIndex);
            this.连线编号B = buffer.ToInt32(ref startIndex);
        }
        #endregion

    }
    partial class 题_判断 : ISerial
    {

        #region Constructor

        public 题_判断() {
        }
        public 题_判断(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_判断(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.答案.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.答案 = buffer.ToBoolean(ref startIndex);
        }
        #endregion

    }
    partial class 题_填空_答案 : ISerial
    {

        #region Constructor

        public 题_填空_答案() {
        }
        public 题_填空_答案(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_填空_答案(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.格子序号.GetBytes(),
                this.显示模板.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.格子序号 = buffer.ToInt32(ref startIndex);
            this.显示模板 = buffer.ToString(ref startIndex);
        }
        #endregion

    }
    partial class 题_问答 : ISerial
    {

        #region Constructor

        public 题_问答() {
        }
        public 题_问答(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_问答(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.参考答案.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.参考答案 = buffer.ToString(ref startIndex);
        }
        #endregion

    }
    partial class 题_选择_答案 : ISerial
    {

        #region Constructor

        public 题_选择_答案() {
        }
        public 题_选择_答案(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_选择_答案(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.题编号.GetBytes(),
                this.选项编号.GetBytes(),
                this.格子序号.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.选项编号 = buffer.ToInt32(ref startIndex);
            this.格子序号 = buffer.ToInt32(ref startIndex);
        }
        #endregion

    }
    partial class 题_选择_选项 : ISerial
    {

        #region Constructor

        public 题_选择_选项() {
        }
        public 题_选择_选项(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 题_选择_选项(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.选项编号.GetBytes(),
                this.题编号.GetBytes(),
                this.显示模板.GetBytes(),
                this.排序.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.选项编号 = buffer.ToInt32(ref startIndex);
            this.题编号 = buffer.ToInt32(ref startIndex);
            this.显示模板 = buffer.ToString(ref startIndex);
            this.排序 = buffer.ToInt32(ref startIndex);
        }
        #endregion

    }
    partial class 知识面 : ISerial
    {

        #region Constructor

        public 知识面() {
        }
        public 知识面(byte[] buffer, ref int startIndex)
            : this() {
            Fill(buffer, ref startIndex);
        }
        public 知识面(byte[] buffer)
            : this() {
            var startIndex = 0;
            Fill(buffer, ref startIndex);
        }

        #endregion

        #region Serial
        public byte[] GetBytes()
        {
            return new byte[][]
            {
                this.知识面编号.GetBytes(),
                this.名称.GetBytes(),
            }.Combine();
        }
        public void Fill(byte[] buffer, ref int startIndex) {
            this.知识面编号 = buffer.ToInt32(ref startIndex);
            this.名称 = buffer.ToString(ref startIndex);
        }
        #endregion

    }
}