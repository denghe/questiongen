using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SqlLib;

namespace DAL.Database.Tables.人
{

    partial class 岗位
    {

        #region Select

        public static List<岗位> Select(Queries.Tables.人.岗位 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<岗位>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 岗位();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.名称 = reader.GetString(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.备注 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 岗位
                        {
                            岗位编号 = reader.GetInt32(0),
                            名称 = reader.GetString(1),
                            备注 = reader.GetString(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<岗位> Select(
            Expressions.Tables.人.岗位.Handler where = null
            , Orientations.Tables.人.岗位.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.人.岗位.Handler columns = null
            )
        {
            return Select(Queries.Tables.人.岗位.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 岗位 Select(int c0, ColumnEnums.Tables.人.岗位.Handler columns = null)
        {
            return Select(o => o.岗位编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        #endregion

        #region Insert

		public static int Insert(岗位 o, ColumnEnums.Tables.人.岗位 ics, ColumnEnums.Tables.人.岗位 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [人].[岗位] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "名称", DataRowVersion.Current, false, o.名称, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[名称]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@名称");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("备注", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "备注", DataRowVersion.Current, false, o.备注, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[备注]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@备注");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.岗位编号 = reader.GetInt32(0);
                        o.名称 = reader.GetString(1);
                        o.备注 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.名称 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.备注 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(岗位 o, ColumnEnums.Tables.人.岗位.Handler insertCols = null, ColumnEnums.Tables.人.岗位.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.人.岗位()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.人.岗位()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(岗位 o, Expressions.Tables.人.岗位 eh, ColumnEnums.Tables.人.岗位 ucs = null, ColumnEnums.Tables.人.岗位 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [人].[岗位]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "名称", DataRowVersion.Current, false, o.名称, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[名称] = @名称");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("备注", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "备注", DataRowVersion.Current, false, o.备注, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[备注] = @备注");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.人.岗位.New(a => a.岗位编号 == o.岗位编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.岗位编号 = reader.GetInt32(0);
                        o.名称 = reader.GetString(1);
                        o.备注 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.名称 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.备注 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(岗位 o, Expressions.Tables.人.岗位.Handler eh = null, ColumnEnums.Tables.人.岗位.Handler updateCols = null, ColumnEnums.Tables.人.岗位.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.人.岗位()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.人.岗位()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.人.岗位()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.人.岗位 eh)
		{
			var s = @"
DELETE FROM [人].[岗位]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.人.岗位.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.人.岗位()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.人.岗位 where,
            ColumnEnums.Tables.人.岗位 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [人].[岗位]";
                    else tsql = "SELECT COUNT(*) FROM [人].[岗位]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [人].[岗位]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [人].[岗位]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [人].[岗位]" + w;
                    else tsql = "SELECT COUNT(*) FROM [人].[岗位]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [人].[岗位]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [人].[岗位]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.人.岗位.Handler where = null,
            ColumnEnums.Tables.人.岗位.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.人.岗位());
            var c = column == null ? null : column(new ColumnEnums.Tables.人.岗位());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.人.岗位 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [人].[岗位]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [人].[岗位]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.人.岗位.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.人.岗位());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 岗位_知识面
    {

        #region Select

        public static List<岗位_知识面> Select(Queries.Tables.人.岗位_知识面 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<岗位_知识面>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 岗位_知识面();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.深度 = reader.GetInt32(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 岗位_知识面
                        {
                            岗位编号 = reader.GetInt32(0),
                            知识面编号 = reader.GetInt32(1),
                            深度 = reader.GetInt32(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<岗位_知识面> Select(
            Expressions.Tables.人.岗位_知识面.Handler where = null
            , Orientations.Tables.人.岗位_知识面.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.人.岗位_知识面.Handler columns = null
            )
        {
            return Select(Queries.Tables.人.岗位_知识面.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 岗位_知识面 Select(int c0, int c1, ColumnEnums.Tables.人.岗位_知识面.Handler columns = null)
        {
            return Select(o => o.岗位编号.Equal(c0) & o.知识面编号.Equal(c1), columns: columns).FirstOrDefault();
        }

        public static List<岗位_知识面> Select(Database.Tables.人.岗位 parent, Queries.Tables.人.岗位_知识面.Handler query = null) {
            if(query == null) return 岗位_知识面.Select(where: o => o.岗位编号 == parent.岗位编号);
            var q = query(new Queries.Tables.人.岗位_知识面());
            if(q.Where == null) q.SetWhere(o => o.岗位编号 == parent.岗位编号);
            else q.Where.And(o => o.岗位编号 == parent.岗位编号);
            return 岗位_知识面.Select(q);
        }

        public static List<岗位_知识面> Select(Database.Tables.题.知识面 parent, Queries.Tables.人.岗位_知识面.Handler query = null) {
            if(query == null) return 岗位_知识面.Select(where: o => o.知识面编号 == parent.知识面编号);
            var q = query(new Queries.Tables.人.岗位_知识面());
            if(q.Where == null) q.SetWhere(o => o.知识面编号 == parent.知识面编号);
            else q.Where.And(o => o.知识面编号 == parent.知识面编号);
            return 岗位_知识面.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(岗位_知识面 o, ColumnEnums.Tables.人.岗位_知识面 ics, ColumnEnums.Tables.人.岗位_知识面 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [人].[岗位_知识面] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("岗位编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "岗位编号", DataRowVersion.Current, false, o.岗位编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[岗位编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@岗位编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("知识面编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "知识面编号", DataRowVersion.Current, false, o.知识面编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[知识面编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@知识面编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("深度", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "深度", DataRowVersion.Current, false, o.深度, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[深度]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@深度");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.岗位编号 = reader.GetInt32(0);
                        o.知识面编号 = reader.GetInt32(1);
                        o.深度 = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.深度 = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(岗位_知识面 o, ColumnEnums.Tables.人.岗位_知识面.Handler insertCols = null, ColumnEnums.Tables.人.岗位_知识面.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.人.岗位_知识面()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.人.岗位_知识面()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(岗位_知识面 o, Expressions.Tables.人.岗位_知识面 eh, ColumnEnums.Tables.人.岗位_知识面 ucs = null, ColumnEnums.Tables.人.岗位_知识面 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [人].[岗位_知识面]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("岗位编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "岗位编号", DataRowVersion.Current, false, o.岗位编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[岗位编号] = @岗位编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("知识面编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "知识面编号", DataRowVersion.Current, false, o.知识面编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[知识面编号] = @知识面编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("深度", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "深度", DataRowVersion.Current, false, o.深度, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[深度] = @深度");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.人.岗位_知识面.New(a => a.岗位编号 == o.岗位编号 & a.知识面编号 == o.知识面编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.岗位编号 = reader.GetInt32(0);
                        o.知识面编号 = reader.GetInt32(1);
                        o.深度 = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.岗位编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.深度 = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(岗位_知识面 o, Expressions.Tables.人.岗位_知识面.Handler eh = null, ColumnEnums.Tables.人.岗位_知识面.Handler updateCols = null, ColumnEnums.Tables.人.岗位_知识面.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.人.岗位_知识面()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.人.岗位_知识面()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.人.岗位_知识面()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.人.岗位_知识面 eh)
		{
			var s = @"
DELETE FROM [人].[岗位_知识面]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.人.岗位_知识面.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.人.岗位_知识面()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.人.岗位_知识面 where,
            ColumnEnums.Tables.人.岗位_知识面 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [人].[岗位_知识面]";
                    else tsql = "SELECT COUNT(*) FROM [人].[岗位_知识面]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [人].[岗位_知识面]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [人].[岗位_知识面]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [人].[岗位_知识面]" + w;
                    else tsql = "SELECT COUNT(*) FROM [人].[岗位_知识面]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [人].[岗位_知识面]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [人].[岗位_知识面]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.人.岗位_知识面.Handler where = null,
            ColumnEnums.Tables.人.岗位_知识面.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.人.岗位_知识面());
            var c = column == null ? null : column(new ColumnEnums.Tables.人.岗位_知识面());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.人.岗位_知识面 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [人].[岗位_知识面]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [人].[岗位_知识面]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.人.岗位_知识面.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.人.岗位_知识面());
            return Exists(w);
        }

        #endregion

        #endregion

    }
}
namespace DAL.Database.Tables.题
{

    partial class 附件
    {

        #region Select

        public static List<附件> Select(Queries.Tables.题.附件 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<附件>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 附件();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.附件编号 = reader.GetGuid(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.是否为图片 = reader.GetBoolean(i); i++; }
                            else if(i < count && cols.Contains(3)) {row.数据 = reader.GetSqlBinary(i).Value; i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 附件
                        {
                            附件编号 = reader.GetGuid(0),
                            题编号 = reader.GetInt32(1),
                            是否为图片 = reader.GetBoolean(2),
                            数据 = reader.GetSqlBinary(3).Value
                        });
                    }
                }

            }
            return rows;
        }

        public static List<附件> Select(
            Expressions.Tables.题.附件.Handler where = null
            , Orientations.Tables.题.附件.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.附件.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.附件.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 附件 Select(Guid c0, ColumnEnums.Tables.题.附件.Handler columns = null)
        {
            return Select(o => o.附件编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        public static List<附件> Select(Database.Tables.题.题 parent, Queries.Tables.题.附件.Handler query = null) {
            if(query == null) return 附件.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.附件());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 附件.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(附件 o, ColumnEnums.Tables.题.附件 ics, ColumnEnums.Tables.题.附件 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[附件] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("附件编号", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, 0, 0, "附件编号", DataRowVersion.Current, false, o.附件编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[附件编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@附件编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("是否为图片", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否为图片", DataRowVersion.Current, false, o.是否为图片, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[是否为图片]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@是否为图片");
				isFirst = false;
			}
			if (ics == null || ics.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("数据", SqlDbType.VarBinary, 0, ParameterDirection.Input, 0, 0, "数据", DataRowVersion.Current, false, o.数据, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[数据]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@数据");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.附件编号 = reader.GetGuid(0);
                        o.题编号 = reader.GetInt32(1);
                        o.是否为图片 = reader.GetBoolean(2);
                        o.数据 = reader.GetSqlBinary(3).Value;
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.附件编号 = reader.GetGuid(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.是否为图片 = reader.GetBoolean(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.数据 = reader.GetSqlBinary(i).Value; i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(附件 o, ColumnEnums.Tables.题.附件.Handler insertCols = null, ColumnEnums.Tables.题.附件.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.附件()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.附件()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(附件 o, Expressions.Tables.题.附件 eh, ColumnEnums.Tables.题.附件 ucs = null, ColumnEnums.Tables.题.附件 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[附件]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("附件编号", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, 0, 0, "附件编号", DataRowVersion.Current, false, o.附件编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[附件编号] = @附件编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("是否为图片", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否为图片", DataRowVersion.Current, false, o.是否为图片, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[是否为图片] = @是否为图片");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("数据", SqlDbType.VarBinary, 0, ParameterDirection.Input, 0, 0, "数据", DataRowVersion.Current, false, o.数据, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[数据] = @数据");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.附件.New(a => a.附件编号 == o.附件编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.附件编号 = reader.GetGuid(0);
                        o.题编号 = reader.GetInt32(1);
                        o.是否为图片 = reader.GetBoolean(2);
                        o.数据 = reader.GetSqlBinary(3).Value;
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.附件编号 = reader.GetGuid(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.是否为图片 = reader.GetBoolean(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.数据 = reader.GetSqlBinary(i).Value; i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(附件 o, Expressions.Tables.题.附件.Handler eh = null, ColumnEnums.Tables.题.附件.Handler updateCols = null, ColumnEnums.Tables.题.附件.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.附件()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.附件()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.附件()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.附件 eh)
		{
			var s = @"
DELETE FROM [题].[附件]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.附件.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.附件()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.附件 where,
            ColumnEnums.Tables.题.附件 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[附件]";
                    else tsql = "SELECT COUNT(*) FROM [题].[附件]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[附件]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[附件]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[附件]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[附件]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[附件]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[附件]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.附件.Handler where = null,
            ColumnEnums.Tables.题.附件.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.附件());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.附件());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.附件 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[附件]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[附件]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.附件.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.附件());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 类型
    {

        #region Select

        public static List<类型> Select(Queries.Tables.题.类型 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<类型>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 类型();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.类型名 = reader.GetString(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.是否需要专业阅卷 = reader.GetBoolean(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 类型
                        {
                            类型编号 = reader.GetInt32(0),
                            类型名 = reader.GetString(1),
                            是否需要专业阅卷 = reader.GetBoolean(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<类型> Select(
            Expressions.Tables.题.类型.Handler where = null
            , Orientations.Tables.题.类型.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.类型.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.类型.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 类型 Select(int c0, ColumnEnums.Tables.题.类型.Handler columns = null)
        {
            return Select(o => o.类型编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        #endregion

        #region Insert

		public static int Insert(类型 o, ColumnEnums.Tables.题.类型 ics, ColumnEnums.Tables.题.类型 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[类型] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("类型编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "类型编号", DataRowVersion.Current, false, o.类型编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[类型编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@类型编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("类型名", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "类型名", DataRowVersion.Current, false, o.类型名, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[类型名]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@类型名");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("是否需要专业阅卷", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否需要专业阅卷", DataRowVersion.Current, false, o.是否需要专业阅卷, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[是否需要专业阅卷]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@是否需要专业阅卷");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.类型编号 = reader.GetInt32(0);
                        o.类型名 = reader.GetString(1);
                        o.是否需要专业阅卷 = reader.GetBoolean(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.类型名 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.是否需要专业阅卷 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(类型 o, ColumnEnums.Tables.题.类型.Handler insertCols = null, ColumnEnums.Tables.题.类型.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.类型()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.类型()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(类型 o, Expressions.Tables.题.类型 eh, ColumnEnums.Tables.题.类型 ucs = null, ColumnEnums.Tables.题.类型 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[类型]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("类型编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "类型编号", DataRowVersion.Current, false, o.类型编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[类型编号] = @类型编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("类型名", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "类型名", DataRowVersion.Current, false, o.类型名, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[类型名] = @类型名");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("是否需要专业阅卷", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否需要专业阅卷", DataRowVersion.Current, false, o.是否需要专业阅卷, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[是否需要专业阅卷] = @是否需要专业阅卷");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.类型.New(a => a.类型编号 == o.类型编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.类型编号 = reader.GetInt32(0);
                        o.类型名 = reader.GetString(1);
                        o.是否需要专业阅卷 = reader.GetBoolean(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.类型名 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.是否需要专业阅卷 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(类型 o, Expressions.Tables.题.类型.Handler eh = null, ColumnEnums.Tables.题.类型.Handler updateCols = null, ColumnEnums.Tables.题.类型.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.类型()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.类型()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.类型()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.类型 eh)
		{
			var s = @"
DELETE FROM [题].[类型]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.类型.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.类型()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.类型 where,
            ColumnEnums.Tables.题.类型 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[类型]";
                    else tsql = "SELECT COUNT(*) FROM [题].[类型]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[类型]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[类型]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[类型]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[类型]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[类型]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[类型]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.类型.Handler where = null,
            ColumnEnums.Tables.题.类型.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.类型());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.类型());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.类型 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[类型]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[类型]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.类型.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.类型());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题
    {

        #region Select

        public static List<题> Select(Queries.Tables.题.题 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.新版题编号 = reader.IsDBNull(i) ? null : new int?(reader.GetInt32(i)); i++; }
                            else if(i < count && cols.Contains(2)) {row.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(3)) {row.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(4)) {row.难度系数 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(5)) {row.显示模板 = reader.GetString(i); i++; }
                            else if(i < count && cols.Contains(6)) {row.考核意图 = reader.GetString(i); i++; }
                            else if(i < count && cols.Contains(7)) {row.备注 = reader.GetString(i); i++; }
                            else if(i < count && cols.Contains(8)) {row.更新时间 = reader.GetDateTime(i); i++; }
                            else if(i < count && cols.Contains(9)) {row.是否启用 = reader.GetBoolean(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题
                        {
                            题编号 = reader.GetInt32(0),
                            新版题编号 = reader.IsDBNull(1) ? null : new int?(reader.GetInt32(1)),
                            类型编号 = reader.GetInt32(2),
                            知识面编号 = reader.GetInt32(3),
                            难度系数 = reader.GetInt32(4),
                            显示模板 = reader.GetString(5),
                            考核意图 = reader.GetString(6),
                            备注 = reader.GetString(7),
                            更新时间 = reader.GetDateTime(8),
                            是否启用 = reader.GetBoolean(9)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题> Select(
            Expressions.Tables.题.题.Handler where = null
            , Orientations.Tables.题.题.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题 Select(int c0, ColumnEnums.Tables.题.题.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        public static List<题> Select(Database.Tables.题.类型 parent, Queries.Tables.题.题.Handler query = null) {
            if(query == null) return 题.Select(where: o => o.类型编号 == parent.类型编号);
            var q = query(new Queries.Tables.题.题());
            if(q.Where == null) q.SetWhere(o => o.类型编号 == parent.类型编号);
            else q.Where.And(o => o.类型编号 == parent.类型编号);
            return 题.Select(q);
        }

        public static List<题> Select(Database.Tables.题.题 parent, Queries.Tables.题.题.Handler query = null) {
            if(query == null) return 题.Select(where: o => o.新版题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题());
            if(q.Where == null) q.SetWhere(o => o.新版题编号 == parent.题编号);
            else q.Where.And(o => o.新版题编号 == parent.题编号);
            return 题.Select(q);
        }

        public static List<题> Select(Database.Tables.题.知识面 parent, Queries.Tables.题.题.Handler query = null) {
            if(query == null) return 题.Select(where: o => o.知识面编号 == parent.知识面编号);
            var q = query(new Queries.Tables.题.题());
            if(q.Where == null) q.SetWhere(o => o.知识面编号 == parent.知识面编号);
            else q.Where.And(o => o.知识面编号 == parent.知识面编号);
            return 题.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题 o, ColumnEnums.Tables.题.题 ics, ColumnEnums.Tables.题.题 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(1))
			{
                var p = new SqlParameter("新版题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "新版题编号", DataRowVersion.Current, false, null, "", "", "");
                if (o.新版题编号 == null) p.Value = DBNull.Value; else p.Value = o.新版题编号;
                cmd.Parameters.Add(p);
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[新版题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@新版题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("类型编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "类型编号", DataRowVersion.Current, false, o.类型编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[类型编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@类型编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("知识面编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "知识面编号", DataRowVersion.Current, false, o.知识面编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[知识面编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@知识面编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(4))
			{
                cmd.Parameters.Add(new SqlParameter("难度系数", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "难度系数", DataRowVersion.Current, false, o.难度系数, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[难度系数]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@难度系数");
				isFirst = false;
			}
			if (ics == null || ics.Contains(5))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[显示模板]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@显示模板");
				isFirst = false;
			}
			if (ics == null || ics.Contains(6))
			{
                cmd.Parameters.Add(new SqlParameter("考核意图", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "考核意图", DataRowVersion.Current, false, o.考核意图, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[考核意图]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@考核意图");
				isFirst = false;
			}
			if (ics == null || ics.Contains(7))
			{
                cmd.Parameters.Add(new SqlParameter("备注", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "备注", DataRowVersion.Current, false, o.备注, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[备注]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@备注");
				isFirst = false;
			}
			if (ics == null || ics.Contains(8))
			{
                cmd.Parameters.Add(new SqlParameter("更新时间", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "更新时间", DataRowVersion.Current, false, o.更新时间, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[更新时间]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@更新时间");
				isFirst = false;
			}
			if (ics == null || ics.Contains(9))
			{
                cmd.Parameters.Add(new SqlParameter("是否启用", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否启用", DataRowVersion.Current, false, o.是否启用, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[是否启用]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@是否启用");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.新版题编号 = reader.IsDBNull(1) ? null : new int?(reader.GetInt32(1));
                        o.类型编号 = reader.GetInt32(2);
                        o.知识面编号 = reader.GetInt32(3);
                        o.难度系数 = reader.GetInt32(4);
                        o.显示模板 = reader.GetString(5);
                        o.考核意图 = reader.GetString(6);
                        o.备注 = reader.GetString(7);
                        o.更新时间 = reader.GetDateTime(8);
                        o.是否启用 = reader.GetBoolean(9);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.新版题编号 = reader.IsDBNull(i) ? null : new int?(reader.GetInt32(i)); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(4)) {o.难度系数 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(5)) {o.显示模板 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(6)) {o.考核意图 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(7)) {o.备注 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(8)) {o.更新时间 = reader.GetDateTime(i); i++; }
                            else if(i < fccount && fcs.Contains(9)) {o.是否启用 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题 o, ColumnEnums.Tables.题.题.Handler insertCols = null, ColumnEnums.Tables.题.题.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题 o, Expressions.Tables.题.题 eh, ColumnEnums.Tables.题.题 ucs = null, ColumnEnums.Tables.题.题 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(1))
			{
                var p = new SqlParameter("新版题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "新版题编号", DataRowVersion.Current, false, null, "", "", "");
                if (o.新版题编号 == null) p.Value = DBNull.Value; else p.Value = o.新版题编号;
                cmd.Parameters.Add(p);
				sb.Append((isFirst ? @"" : @"
     , ") + "[新版题编号] = @新版题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("类型编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "类型编号", DataRowVersion.Current, false, o.类型编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[类型编号] = @类型编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("知识面编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "知识面编号", DataRowVersion.Current, false, o.知识面编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[知识面编号] = @知识面编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(4))
			{
                cmd.Parameters.Add(new SqlParameter("难度系数", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "难度系数", DataRowVersion.Current, false, o.难度系数, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[难度系数] = @难度系数");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(5))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[显示模板] = @显示模板");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(6))
			{
                cmd.Parameters.Add(new SqlParameter("考核意图", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "考核意图", DataRowVersion.Current, false, o.考核意图, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[考核意图] = @考核意图");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(7))
			{
                cmd.Parameters.Add(new SqlParameter("备注", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "备注", DataRowVersion.Current, false, o.备注, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[备注] = @备注");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(8))
			{
                cmd.Parameters.Add(new SqlParameter("更新时间", SqlDbType.DateTime, 0, ParameterDirection.Input, 0, 0, "更新时间", DataRowVersion.Current, false, o.更新时间, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[更新时间] = @更新时间");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(9))
			{
                cmd.Parameters.Add(new SqlParameter("是否启用", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "是否启用", DataRowVersion.Current, false, o.是否启用, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[是否启用] = @是否启用");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题.New(a => a.题编号 == o.题编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.新版题编号 = reader.IsDBNull(1) ? null : new int?(reader.GetInt32(1));
                        o.类型编号 = reader.GetInt32(2);
                        o.知识面编号 = reader.GetInt32(3);
                        o.难度系数 = reader.GetInt32(4);
                        o.显示模板 = reader.GetString(5);
                        o.考核意图 = reader.GetString(6);
                        o.备注 = reader.GetString(7);
                        o.更新时间 = reader.GetDateTime(8);
                        o.是否启用 = reader.GetBoolean(9);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.新版题编号 = reader.IsDBNull(i) ? null : new int?(reader.GetInt32(i)); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.类型编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(4)) {o.难度系数 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(5)) {o.显示模板 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(6)) {o.考核意图 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(7)) {o.备注 = reader.GetString(i); i++; }
                            else if(i < fccount && fcs.Contains(8)) {o.更新时间 = reader.GetDateTime(i); i++; }
                            else if(i < fccount && fcs.Contains(9)) {o.是否启用 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题 o, Expressions.Tables.题.题.Handler eh = null, ColumnEnums.Tables.题.题.Handler updateCols = null, ColumnEnums.Tables.题.题.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题 eh)
		{
			var s = @"
DELETE FROM [题].[题]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题 where,
            ColumnEnums.Tables.题.题 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题.Handler where = null,
            ColumnEnums.Tables.题.题.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_连线_答案
    {

        #region Select

        public static List<题_连线_答案> Select(Queries.Tables.题.题_连线_答案 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_连线_答案>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_连线_答案();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.连线序号A = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.连线序号B = reader.GetInt32(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_连线_答案
                        {
                            题编号 = reader.GetInt32(0),
                            连线序号A = reader.GetInt32(1),
                            连线序号B = reader.GetInt32(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_连线_答案> Select(
            Expressions.Tables.题.题_连线_答案.Handler where = null
            , Orientations.Tables.题.题_连线_答案.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_连线_答案.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_连线_答案.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_连线_答案 Select(int c0, int c1, int c2, ColumnEnums.Tables.题.题_连线_答案.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0) & o.连线序号A.Equal(c1) & o.连线序号B.Equal(c2), columns: columns).FirstOrDefault();
        }

        public static List<题_连线_答案> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_连线_答案.Handler query = null) {
            if(query == null) return 题_连线_答案.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_连线_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_连线_答案.Select(q);
        }

        public static List<题_连线_答案> Select(Database.Tables.题.题_连线_选项 parent, Queries.Tables.题.题_连线_答案.Handler query = null) {
            if(query == null) return 题_连线_答案.Select(where: o => o.题编号 == parent.题编号 & o.连线序号A == parent.连线序号);
            var q = query(new Queries.Tables.题.题_连线_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号 & o.连线序号A == parent.连线序号);
            else q.Where.And(o => o.题编号 == parent.题编号 & o.连线序号A == parent.连线序号);
            return 题_连线_答案.Select(q);
        }

        public static List<题_连线_答案> Select1(Database.Tables.题.题_连线_选项 parent, Queries.Tables.题.题_连线_答案.Handler query = null) {
            if(query == null) return 题_连线_答案.Select(where: o => o.题编号 == parent.题编号 & o.连线序号B == parent.连线序号);
            var q = query(new Queries.Tables.题.题_连线_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号 & o.连线序号B == parent.连线序号);
            else q.Where.And(o => o.题编号 == parent.题编号 & o.连线序号B == parent.连线序号);
            return 题_连线_答案.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_连线_答案 o, ColumnEnums.Tables.题.题_连线_答案 ics, ColumnEnums.Tables.题.题_连线_答案 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_连线_答案] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号A", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号A", DataRowVersion.Current, false, o.连线序号A, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[连线序号A]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@连线序号A");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号B", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号B", DataRowVersion.Current, false, o.连线序号B, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[连线序号B]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@连线序号B");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.连线序号A = reader.GetInt32(1);
                        o.连线序号B = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.连线序号A = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.连线序号B = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_连线_答案 o, ColumnEnums.Tables.题.题_连线_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_连线_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_连线_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_连线_答案()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_连线_答案 o, Expressions.Tables.题.题_连线_答案 eh, ColumnEnums.Tables.题.题_连线_答案 ucs = null, ColumnEnums.Tables.题.题_连线_答案 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_连线_答案]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号A", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号A", DataRowVersion.Current, false, o.连线序号A, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[连线序号A] = @连线序号A");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号B", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号B", DataRowVersion.Current, false, o.连线序号B, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[连线序号B] = @连线序号B");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_连线_答案.New(a => a.题编号 == o.题编号 & a.连线序号A == o.连线序号A & a.连线序号B == o.连线序号B);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.连线序号A = reader.GetInt32(1);
                        o.连线序号B = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.连线序号A = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.连线序号B = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_连线_答案 o, Expressions.Tables.题.题_连线_答案.Handler eh = null, ColumnEnums.Tables.题.题_连线_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_连线_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_连线_答案()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_连线_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_连线_答案()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_连线_答案 eh)
		{
			var s = @"
DELETE FROM [题].[题_连线_答案]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_连线_答案.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_连线_答案()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_连线_答案 where,
            ColumnEnums.Tables.题.题_连线_答案 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_连线_答案]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_连线_答案]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_连线_答案]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_连线_答案]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_连线_答案]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_连线_答案]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_连线_答案]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_连线_答案]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_连线_答案.Handler where = null,
            ColumnEnums.Tables.题.题_连线_答案.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_连线_答案());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_连线_答案());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_连线_答案 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_连线_答案]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_连线_答案]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_连线_答案.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_连线_答案());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_连线_选项
    {

        #region Select

        public static List<题_连线_选项> Select(Queries.Tables.题.题_连线_选项 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_连线_选项>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_连线_选项();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.连线序号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.组序号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(3)) {row.显示模板 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_连线_选项
                        {
                            题编号 = reader.GetInt32(0),
                            连线序号 = reader.GetInt32(1),
                            组序号 = reader.GetInt32(2),
                            显示模板 = reader.GetString(3)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_连线_选项> Select(
            Expressions.Tables.题.题_连线_选项.Handler where = null
            , Orientations.Tables.题.题_连线_选项.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_连线_选项.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_连线_选项.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_连线_选项 Select(int c0, int c1, ColumnEnums.Tables.题.题_连线_选项.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0) & o.连线序号.Equal(c1), columns: columns).FirstOrDefault();
        }

        public static List<题_连线_选项> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_连线_选项.Handler query = null) {
            if(query == null) return 题_连线_选项.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_连线_选项());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_连线_选项.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_连线_选项 o, ColumnEnums.Tables.题.题_连线_选项 ics, ColumnEnums.Tables.题.题_连线_选项 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_连线_选项] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号", DataRowVersion.Current, false, o.连线序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[连线序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@连线序号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("组序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "组序号", DataRowVersion.Current, false, o.组序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[组序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@组序号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[显示模板]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@显示模板");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.连线序号 = reader.GetInt32(1);
                        o.组序号 = reader.GetInt32(2);
                        o.显示模板 = reader.GetString(3);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.连线序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.组序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_连线_选项 o, ColumnEnums.Tables.题.题_连线_选项.Handler insertCols = null, ColumnEnums.Tables.题.题_连线_选项.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_连线_选项()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_连线_选项()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_连线_选项 o, Expressions.Tables.题.题_连线_选项 eh, ColumnEnums.Tables.题.题_连线_选项 ucs = null, ColumnEnums.Tables.题.题_连线_选项 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_连线_选项]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("连线序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "连线序号", DataRowVersion.Current, false, o.连线序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[连线序号] = @连线序号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("组序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "组序号", DataRowVersion.Current, false, o.组序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[组序号] = @组序号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(3))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[显示模板] = @显示模板");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_连线_选项.New(a => a.题编号 == o.题编号 & a.连线序号 == o.连线序号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.连线序号 = reader.GetInt32(1);
                        o.组序号 = reader.GetInt32(2);
                        o.显示模板 = reader.GetString(3);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.连线序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.组序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(3)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_连线_选项 o, Expressions.Tables.题.题_连线_选项.Handler eh = null, ColumnEnums.Tables.题.题_连线_选项.Handler updateCols = null, ColumnEnums.Tables.题.题_连线_选项.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_连线_选项()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_连线_选项()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_连线_选项()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_连线_选项 eh)
		{
			var s = @"
DELETE FROM [题].[题_连线_选项]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_连线_选项.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_连线_选项()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_连线_选项 where,
            ColumnEnums.Tables.题.题_连线_选项 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_连线_选项]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_连线_选项]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_连线_选项]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_连线_选项]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_连线_选项]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_连线_选项]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_连线_选项]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_连线_选项]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_连线_选项.Handler where = null,
            ColumnEnums.Tables.题.题_连线_选项.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_连线_选项());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_连线_选项());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_连线_选项 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_连线_选项]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_连线_选项]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_连线_选项.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_连线_选项());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_判断_答案
    {

        #region Select

        public static List<题_判断_答案> Select(Queries.Tables.题.题_判断_答案 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_判断_答案>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_判断_答案();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.答案 = reader.GetBoolean(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_判断_答案
                        {
                            题编号 = reader.GetInt32(0),
                            答案 = reader.GetBoolean(1)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_判断_答案> Select(
            Expressions.Tables.题.题_判断_答案.Handler where = null
            , Orientations.Tables.题.题_判断_答案.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_判断_答案.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_判断_答案.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_判断_答案 Select(int c0, ColumnEnums.Tables.题.题_判断_答案.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        public static List<题_判断_答案> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_判断_答案.Handler query = null) {
            if(query == null) return 题_判断_答案.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_判断_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_判断_答案.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_判断_答案 o, ColumnEnums.Tables.题.题_判断_答案 ics, ColumnEnums.Tables.题.题_判断_答案 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_判断_答案] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("答案", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "答案", DataRowVersion.Current, false, o.答案, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[答案]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@答案");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.答案 = reader.GetBoolean(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.答案 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_判断_答案 o, ColumnEnums.Tables.题.题_判断_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_判断_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_判断_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_判断_答案()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_判断_答案 o, Expressions.Tables.题.题_判断_答案 eh, ColumnEnums.Tables.题.题_判断_答案 ucs = null, ColumnEnums.Tables.题.题_判断_答案 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_判断_答案]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("答案", SqlDbType.Bit, 0, ParameterDirection.Input, 0, 0, "答案", DataRowVersion.Current, false, o.答案, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[答案] = @答案");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_判断_答案.New(a => a.题编号 == o.题编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.答案 = reader.GetBoolean(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.答案 = reader.GetBoolean(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_判断_答案 o, Expressions.Tables.题.题_判断_答案.Handler eh = null, ColumnEnums.Tables.题.题_判断_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_判断_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_判断_答案()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_判断_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_判断_答案()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_判断_答案 eh)
		{
			var s = @"
DELETE FROM [题].[题_判断_答案]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_判断_答案.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_判断_答案()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_判断_答案 where,
            ColumnEnums.Tables.题.题_判断_答案 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_判断_答案]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_判断_答案]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_判断_答案]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_判断_答案]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_判断_答案]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_判断_答案]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_判断_答案]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_判断_答案]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_判断_答案.Handler where = null,
            ColumnEnums.Tables.题.题_判断_答案.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_判断_答案());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_判断_答案());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_判断_答案 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_判断_答案]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_判断_答案]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_判断_答案.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_判断_答案());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_填空_答案
    {

        #region Select

        public static List<题_填空_答案> Select(Queries.Tables.题.题_填空_答案 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_填空_答案>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_填空_答案();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.格子序号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.显示模板 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_填空_答案
                        {
                            题编号 = reader.GetInt32(0),
                            格子序号 = reader.GetInt32(1),
                            显示模板 = reader.GetString(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_填空_答案> Select(
            Expressions.Tables.题.题_填空_答案.Handler where = null
            , Orientations.Tables.题.题_填空_答案.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_填空_答案.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_填空_答案.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_填空_答案 Select(int c0, int c1, ColumnEnums.Tables.题.题_填空_答案.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0) & o.格子序号.Equal(c1), columns: columns).FirstOrDefault();
        }

        public static List<题_填空_答案> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_填空_答案.Handler query = null) {
            if(query == null) return 题_填空_答案.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_填空_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_填空_答案.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_填空_答案 o, ColumnEnums.Tables.题.题_填空_答案 ics, ColumnEnums.Tables.题.题_填空_答案 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_填空_答案] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("格子序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "格子序号", DataRowVersion.Current, false, o.格子序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[格子序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@格子序号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[显示模板]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@显示模板");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.格子序号 = reader.GetInt32(1);
                        o.显示模板 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.格子序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_填空_答案 o, ColumnEnums.Tables.题.题_填空_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_填空_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_填空_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_填空_答案()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_填空_答案 o, Expressions.Tables.题.题_填空_答案 eh, ColumnEnums.Tables.题.题_填空_答案 ucs = null, ColumnEnums.Tables.题.题_填空_答案 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_填空_答案]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("格子序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "格子序号", DataRowVersion.Current, false, o.格子序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[格子序号] = @格子序号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[显示模板] = @显示模板");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_填空_答案.New(a => a.题编号 == o.题编号 & a.格子序号 == o.格子序号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.格子序号 = reader.GetInt32(1);
                        o.显示模板 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.格子序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_填空_答案 o, Expressions.Tables.题.题_填空_答案.Handler eh = null, ColumnEnums.Tables.题.题_填空_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_填空_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_填空_答案()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_填空_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_填空_答案()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_填空_答案 eh)
		{
			var s = @"
DELETE FROM [题].[题_填空_答案]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_填空_答案.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_填空_答案()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_填空_答案 where,
            ColumnEnums.Tables.题.题_填空_答案 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_填空_答案]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_填空_答案]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_填空_答案]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_填空_答案]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_填空_答案]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_填空_答案]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_填空_答案]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_填空_答案]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_填空_答案.Handler where = null,
            ColumnEnums.Tables.题.题_填空_答案.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_填空_答案());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_填空_答案());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_填空_答案 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_填空_答案]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_填空_答案]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_填空_答案.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_填空_答案());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_问答_答案
    {

        #region Select

        public static List<题_问答_答案> Select(Queries.Tables.题.题_问答_答案 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_问答_答案>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_问答_答案();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.参考答案 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_问答_答案
                        {
                            题编号 = reader.GetInt32(0),
                            参考答案 = reader.GetString(1)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_问答_答案> Select(
            Expressions.Tables.题.题_问答_答案.Handler where = null
            , Orientations.Tables.题.题_问答_答案.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_问答_答案.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_问答_答案.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_问答_答案 Select(int c0, ColumnEnums.Tables.题.题_问答_答案.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        public static List<题_问答_答案> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_问答_答案.Handler query = null) {
            if(query == null) return 题_问答_答案.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_问答_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_问答_答案.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_问答_答案 o, ColumnEnums.Tables.题.题_问答_答案 ics, ColumnEnums.Tables.题.题_问答_答案 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_问答_答案] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("参考答案", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "参考答案", DataRowVersion.Current, false, o.参考答案, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[参考答案]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@参考答案");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.参考答案 = reader.GetString(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.参考答案 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_问答_答案 o, ColumnEnums.Tables.题.题_问答_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_问答_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_问答_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_问答_答案()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_问答_答案 o, Expressions.Tables.题.题_问答_答案 eh, ColumnEnums.Tables.题.题_问答_答案 ucs = null, ColumnEnums.Tables.题.题_问答_答案 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_问答_答案]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("参考答案", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "参考答案", DataRowVersion.Current, false, o.参考答案, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[参考答案] = @参考答案");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_问答_答案.New(a => a.题编号 == o.题编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.参考答案 = reader.GetString(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.参考答案 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_问答_答案 o, Expressions.Tables.题.题_问答_答案.Handler eh = null, ColumnEnums.Tables.题.题_问答_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_问答_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_问答_答案()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_问答_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_问答_答案()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_问答_答案 eh)
		{
			var s = @"
DELETE FROM [题].[题_问答_答案]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_问答_答案.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_问答_答案()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_问答_答案 where,
            ColumnEnums.Tables.题.题_问答_答案 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_问答_答案]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_问答_答案]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_问答_答案]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_问答_答案]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_问答_答案]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_问答_答案]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_问答_答案]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_问答_答案]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_问答_答案.Handler where = null,
            ColumnEnums.Tables.题.题_问答_答案.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_问答_答案());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_问答_答案());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_问答_答案 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_问答_答案]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_问答_答案]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_问答_答案.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_问答_答案());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_选择_答案
    {

        #region Select

        public static List<题_选择_答案> Select(Queries.Tables.题.题_选择_答案 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_选择_答案>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_选择_答案();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.格子序号 = reader.GetInt32(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_选择_答案
                        {
                            题编号 = reader.GetInt32(0),
                            选项序号 = reader.GetInt32(1),
                            格子序号 = reader.GetInt32(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_选择_答案> Select(
            Expressions.Tables.题.题_选择_答案.Handler where = null
            , Orientations.Tables.题.题_选择_答案.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_选择_答案.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_选择_答案.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_选择_答案 Select(int c0, int c1, int c2, ColumnEnums.Tables.题.题_选择_答案.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0) & o.选项序号.Equal(c1) & o.格子序号.Equal(c2), columns: columns).FirstOrDefault();
        }

        public static List<题_选择_答案> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_选择_答案.Handler query = null) {
            if(query == null) return 题_选择_答案.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_选择_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_选择_答案.Select(q);
        }

        public static List<题_选择_答案> Select(Database.Tables.题.题_选择_选项 parent, Queries.Tables.题.题_选择_答案.Handler query = null) {
            if(query == null) return 题_选择_答案.Select(where: o => o.题编号 == parent.题编号 & o.选项序号 == parent.选项序号);
            var q = query(new Queries.Tables.题.题_选择_答案());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号 & o.选项序号 == parent.选项序号);
            else q.Where.And(o => o.题编号 == parent.题编号 & o.选项序号 == parent.选项序号);
            return 题_选择_答案.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_选择_答案 o, ColumnEnums.Tables.题.题_选择_答案 ics, ColumnEnums.Tables.题.题_选择_答案 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_选择_答案] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("选项序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "选项序号", DataRowVersion.Current, false, o.选项序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[选项序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@选项序号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("格子序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "格子序号", DataRowVersion.Current, false, o.格子序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[格子序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@格子序号");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.选项序号 = reader.GetInt32(1);
                        o.格子序号 = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.格子序号 = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_选择_答案 o, ColumnEnums.Tables.题.题_选择_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_选择_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_选择_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_选择_答案()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_选择_答案 o, Expressions.Tables.题.题_选择_答案 eh, ColumnEnums.Tables.题.题_选择_答案 ucs = null, ColumnEnums.Tables.题.题_选择_答案 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_选择_答案]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("选项序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "选项序号", DataRowVersion.Current, false, o.选项序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[选项序号] = @选项序号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("格子序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "格子序号", DataRowVersion.Current, false, o.格子序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[格子序号] = @格子序号");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_选择_答案.New(a => a.题编号 == o.题编号 & a.选项序号 == o.选项序号 & a.格子序号 == o.格子序号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.选项序号 = reader.GetInt32(1);
                        o.格子序号 = reader.GetInt32(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.格子序号 = reader.GetInt32(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_选择_答案 o, Expressions.Tables.题.题_选择_答案.Handler eh = null, ColumnEnums.Tables.题.题_选择_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_选择_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_选择_答案()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_选择_答案()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_选择_答案()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_选择_答案 eh)
		{
			var s = @"
DELETE FROM [题].[题_选择_答案]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_选择_答案.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_选择_答案()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_选择_答案 where,
            ColumnEnums.Tables.题.题_选择_答案 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_选择_答案]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_选择_答案]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_选择_答案]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_选择_答案]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_选择_答案]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_选择_答案]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_选择_答案]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_选择_答案]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_选择_答案.Handler where = null,
            ColumnEnums.Tables.题.题_选择_答案.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_选择_答案());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_选择_答案());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_选择_答案 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_选择_答案]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_选择_答案]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_选择_答案.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_选择_答案());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 题_选择_选项
    {

        #region Select

        public static List<题_选择_选项> Select(Queries.Tables.题.题_选择_选项 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<题_选择_选项>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 题_选择_选项();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.题编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(2)) {row.显示模板 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 题_选择_选项
                        {
                            题编号 = reader.GetInt32(0),
                            选项序号 = reader.GetInt32(1),
                            显示模板 = reader.GetString(2)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<题_选择_选项> Select(
            Expressions.Tables.题.题_选择_选项.Handler where = null
            , Orientations.Tables.题.题_选择_选项.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.题_选择_选项.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.题_选择_选项.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 题_选择_选项 Select(int c0, int c1, ColumnEnums.Tables.题.题_选择_选项.Handler columns = null)
        {
            return Select(o => o.题编号.Equal(c0) & o.选项序号.Equal(c1), columns: columns).FirstOrDefault();
        }

        public static List<题_选择_选项> Select(Database.Tables.题.题 parent, Queries.Tables.题.题_选择_选项.Handler query = null) {
            if(query == null) return 题_选择_选项.Select(where: o => o.题编号 == parent.题编号);
            var q = query(new Queries.Tables.题.题_选择_选项());
            if(q.Where == null) q.SetWhere(o => o.题编号 == parent.题编号);
            else q.Where.And(o => o.题编号 == parent.题编号);
            return 题_选择_选项.Select(q);
        }

        #endregion

        #region Insert

		public static int Insert(题_选择_选项 o, ColumnEnums.Tables.题.题_选择_选项 ics, ColumnEnums.Tables.题.题_选择_选项 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[题_选择_选项] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[题编号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@题编号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("选项序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "选项序号", DataRowVersion.Current, false, o.选项序号, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[选项序号]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@选项序号");
				isFirst = false;
			}
			if (ics == null || ics.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[显示模板]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@显示模板");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.选项序号 = reader.GetInt32(1);
                        o.显示模板 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(题_选择_选项 o, ColumnEnums.Tables.题.题_选择_选项.Handler insertCols = null, ColumnEnums.Tables.题.题_选择_选项.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.题_选择_选项()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_选择_选项()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(题_选择_选项 o, Expressions.Tables.题.题_选择_选项 eh, ColumnEnums.Tables.题.题_选择_选项 ucs = null, ColumnEnums.Tables.题.题_选择_选项 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[题_选择_选项]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(0))
			{
                cmd.Parameters.Add(new SqlParameter("题编号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "题编号", DataRowVersion.Current, false, o.题编号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[题编号] = @题编号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("选项序号", SqlDbType.Int, 0, ParameterDirection.Input, 0, 0, "选项序号", DataRowVersion.Current, false, o.选项序号, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[选项序号] = @选项序号");
				isFirst = false;
			}
			if (ucs == null || ucs.Contains(2))
			{
                cmd.Parameters.Add(new SqlParameter("显示模板", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "显示模板", DataRowVersion.Current, false, o.显示模板, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[显示模板] = @显示模板");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.题_选择_选项.New(a => a.题编号 == o.题编号 & a.选项序号 == o.选项序号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.题编号 = reader.GetInt32(0);
                        o.选项序号 = reader.GetInt32(1);
                        o.显示模板 = reader.GetString(2);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.题编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.选项序号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(2)) {o.显示模板 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(题_选择_选项 o, Expressions.Tables.题.题_选择_选项.Handler eh = null, ColumnEnums.Tables.题.题_选择_选项.Handler updateCols = null, ColumnEnums.Tables.题.题_选择_选项.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.题_选择_选项()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.题_选择_选项()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.题_选择_选项()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.题_选择_选项 eh)
		{
			var s = @"
DELETE FROM [题].[题_选择_选项]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.题_选择_选项.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.题_选择_选项()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.题_选择_选项 where,
            ColumnEnums.Tables.题.题_选择_选项 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_选择_选项]";
                    else tsql = "SELECT COUNT(*) FROM [题].[题_选择_选项]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_选择_选项]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_选择_选项]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[题_选择_选项]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[题_选择_选项]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[题_选择_选项]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[题_选择_选项]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.题_选择_选项.Handler where = null,
            ColumnEnums.Tables.题.题_选择_选项.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_选择_选项());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.题_选择_选项());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.题_选择_选项 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[题_选择_选项]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[题_选择_选项]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.题_选择_选项.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.题_选择_选项());
            return Exists(w);
        }

        #endregion

        #endregion

    }
    partial class 知识面
    {

        #region Select

        public static List<知识面> Select(Queries.Tables.题.知识面 q)
        {
            var tsql = q.ToSqlString();
            var rows = new List<知识面>();
            using(var reader = SqlHelper.ExecuteDataReader(tsql))
            {
                var count = q.Columns == null ? 0 : q.Columns.Count();
                if(count > 0) {
                    while(reader.Read()) {
                        var row = new 知识面();
                        var cols = q.Columns;
                        for(int i = 0; i < count; i++) {
                            if(cols.Contains(0)) {row.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < count && cols.Contains(1)) {row.名称 = reader.GetString(i); i++; }
                        }
                        rows.Add(row);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        rows.Add(new 知识面
                        {
                            知识面编号 = reader.GetInt32(0),
                            名称 = reader.GetString(1)
                        });
                    }
                }

            }
            return rows;
        }

        public static List<知识面> Select(
            Expressions.Tables.题.知识面.Handler where = null
            , Orientations.Tables.题.知识面.Handler orderby = null
            , int pageSize = 0
            , int pageIndex = 0
            , ColumnEnums.Tables.题.知识面.Handler columns = null
            )
        {
            return Select(Queries.Tables.题.知识面.New(where, orderby, pageSize, pageIndex, columns));
        }

        public static 知识面 Select(int c0, ColumnEnums.Tables.题.知识面.Handler columns = null)
        {
            return Select(o => o.知识面编号.Equal(c0), columns: columns).FirstOrDefault();
        }

        #endregion

        #region Insert

		public static int Insert(知识面 o, ColumnEnums.Tables.题.知识面 ics, ColumnEnums.Tables.题.知识面 fcs = null, bool isFillAfterInsert = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
INSERT INTO [题].[知识面] (");
			var sb2 = new StringBuilder();
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ics == null || ics.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "名称", DataRowVersion.Current, false, o.名称, "", "", ""));
				sb.Append((isFirst ? @"
       " : @"
     , ") + "[名称]");
				sb2.Append((isFirst ? @"
       " : @"
     , ") + "@名称");
				isFirst = false;
			}
            if(isFillAfterInsert)
            {
                if(fcs == null)
                {
                    sb.Append(@"
) 
OUTPUT INSERTED.* VALUES (");
                }
                else
                {
                    sb.Append(@"
) 
OUTPUT ");
                    for(int i = 0; i < fccount; i++)
                    {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                    sb.Append(@" VALUES (");
                }
            }
            else sb.Append(@"
) 
VALUES (");
			sb.Append(sb2);
			sb.Append(@"
);");
			cmd.CommandText = sb.ToString();
            if(!isFillAfterInsert)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.知识面编号 = reader.GetInt32(0);
                        o.名称 = reader.GetString(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.名称 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
		}

		public static int Insert(知识面 o, ColumnEnums.Tables.题.知识面.Handler insertCols = null, ColumnEnums.Tables.题.知识面.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Insert(o,
                insertCols == null ? null : insertCols(new ColumnEnums.Tables.题.知识面()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.知识面()),
                isFillAfterInsert
            );
        }

        #endregion

        #region Update

		public static int Update(知识面 o, Expressions.Tables.题.知识面 eh, ColumnEnums.Tables.题.知识面 ucs = null, ColumnEnums.Tables.题.知识面 fcs = null, bool isFillAfterUpdate = true)
		{
			var cmd = new SqlCommand();
			var sb = new StringBuilder(@"
UPDATE [题].[知识面]
   SET ");
			var isFirst = true;
            var fccount = fcs == null ? 0 : fcs.Count();
			if (ucs == null || ucs.Contains(1))
			{
                cmd.Parameters.Add(new SqlParameter("名称", SqlDbType.NVarChar, 0, ParameterDirection.Input, 0, 0, "名称", DataRowVersion.Current, false, o.名称, "", "", ""));
				sb.Append((isFirst ? @"" : @"
     , ") + "[名称] = @名称");
				isFirst = false;
			}
            if(isFillAfterUpdate) {
                if(fcs == null) {
                    sb.Append(@"
OUTPUT INSERTED.*");
                }
                else {
                    sb.Append(@"
OUTPUT ");
                    for(int i = 0; i < fccount; i++) {
                        if(i > 0) sb.Append(@", ");
                        sb.Append(@"INSERTED.[" + fcs.GetColumnName(i).Replace("]", "]]") + "]");
                    }
                }
            }

            if (eh == null) eh = Expressions.Tables.题.知识面.New(a => a.知识面编号 == o.知识面编号);

            var ws = eh.ToString();
            if (ws.Length > 0)
                sb.Append(@"
 WHERE " + ws);

			cmd.CommandText = sb.ToString();
			if (!isFillAfterUpdate)
                return SqlHelper.ExecuteNonQuery(cmd);

            using(var reader = SqlHelper.ExecuteDataReader(cmd))
            {
                if(fccount == 0)
                {
                    while(reader.Read())
                    {
                        o.知识面编号 = reader.GetInt32(0);
                        o.名称 = reader.GetString(1);
                    }
                }
                else
                {
                    while(reader.Read())
                    {
                        for(int i = 0; i < fccount; i++)
                        {
                            if(fcs.Contains(0)) {o.知识面编号 = reader.GetInt32(i); i++; }
                            else if(i < fccount && fcs.Contains(1)) {o.名称 = reader.GetString(i); i++; }
                        }
                    }
                }
                return reader.RecordsAffected;
            }
            
		}
        public static int Update(知识面 o, Expressions.Tables.题.知识面.Handler eh = null, ColumnEnums.Tables.题.知识面.Handler updateCols = null, ColumnEnums.Tables.题.知识面.Handler fillCols = null, bool isFillAfterUpdate = true)
        {
            return Update(o,
                eh == null ? null : eh(new Expressions.Tables.题.知识面()),
                updateCols == null ? null : updateCols(new ColumnEnums.Tables.题.知识面()),
                fillCols == null ? null : fillCols(new ColumnEnums.Tables.题.知识面()),
                isFillAfterUpdate
            );
        }
        #endregion

        #region Delete

		public static int Delete(Expressions.Tables.题.知识面 eh)
		{
			var s = @"
DELETE FROM [题].[知识面]";
            if (eh != null)
            {
                var ws = eh.ToString();
                if(ws.Length > 0)
    			    s += @"
 WHERE " + ws;
            }
			return SqlHelper.ExecuteNonQuery(s);
		}
        public static int Delete(Expressions.Tables.题.知识面.Handler eh)
        {
            return Delete(eh(new Expressions.Tables.题.知识面()));
        }
        #endregion

        #region Others

        #region Count

        public static int Count(
            Expressions.Tables.题.知识面 where,
            ColumnEnums.Tables.题.知识面 column = null,
            bool isDistinct = false
        )
        {
            string tsql;
            if (where == null)
            {
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[知识面]";
                    else tsql = "SELECT COUNT(*) FROM [题].[知识面]";
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[知识面]";
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[知识面]";
                }
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                if (column == null)
                {
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT *) FROM [题].[知识面]" + w;
                    else tsql = "SELECT COUNT(*) FROM [题].[知识面]" + w;
                }
                else
                {
                    var c = column.ToString();
                    if (c.Length == 0) c = "*";
                    if (isDistinct) tsql = "SELECT COUNT(DISTINCT " + c + ") FROM [题].[知识面]" + w;
                    else tsql = "SELECT COUNT(" + c + ") FROM [题].[知识面]" + w;
                }
            }
            return SqlHelper.ExecuteScalar<int>(tsql);
        }

        public static int Count(
            Expressions.Tables.题.知识面.Handler where = null,
            ColumnEnums.Tables.题.知识面.Handler column = null,
            bool isDistinct = false
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.知识面());
            var c = column == null ? null : column(new ColumnEnums.Tables.题.知识面());
            return Count(w, c, isDistinct);
        }

        #endregion

        #region Exists

        public static bool Exists(
            Expressions.Tables.题.知识面 where
        )
        {
            string tsql;
            if (where == null)
            {
                tsql = "SELECT TOP(1) 1 FROM [题].[知识面]";
            }
            else
            {
                var w = where.ToString();
                if (w.Length > 0) w = " WHERE " + w;
                tsql = "SELECT TOP(1) 1 FROM [题].[知识面]" + w;
            }
            var o = SqlHelper.ExecuteScalar(tsql);
            return !(o == null || o == DBNull.Value);
        }
        public static bool Exists(
            Expressions.Tables.题.知识面.Handler where = null
        )
        {
            var w = where == null ? null : where(new Expressions.Tables.题.知识面());
            return Exists(w);
        }

        #endregion

        #endregion

    }
}