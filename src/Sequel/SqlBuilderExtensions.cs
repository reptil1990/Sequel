﻿namespace Sequel
{
    /// <summary>
    /// SQL builder ANSI extensions
    /// </summary>
    public static class SqlBuilderExtensions
    {
        /// <summary>
        /// EXISTS predicate subquery
        /// </summary>
        public static SqlBuilder Exists(this SqlBuilder sql, SqlBuilder sqlBuilder) =>
          sql.Where(
            predicates: string.Concat("EXISTS (", sqlBuilder.ToSql(), ")"));

        /// <summary>
        /// EXISTS predicate
        /// </summary>
        public static SqlBuilder Exists(this SqlBuilder sql, string predicate) =>
          sql.Where(
            predicates: string.Concat("EXISTS (", predicate, ")"));

        /// <summary>
        /// FROM table
        /// </summary>
        public static SqlBuilder From(this SqlBuilder sql, string table) =>
          sql.AddClause(
              keyword: "from",
              token: table,
              glue: null,
              pre: "FROM ",
              post: null);

        /// <summary>
        /// FROM table with alias
        /// </summary>

        public static SqlBuilder From(this SqlBuilder sql, string table, string alias) =>
          sql.From(
            table: string.Concat(table, " AS ", alias));

        /// <summary>
        /// FROM derived table
        /// </summary>

        public static SqlBuilder From(this SqlBuilder sql, SqlBuilder derivedTable, string alias) =>
          sql.AddClause(
              keyword: "from",
              token: derivedTable.ToSql(),
              glue: null,
              pre: "FROM (",
              post: ") as " + alias);

        /// <summary>
        /// DELETE clause
        /// </summary>

        public static SqlBuilder Delete(this SqlBuilder sql)
        {
            sql.SetTemplate("delete");
            return sql.AddClause(
                keyword: "delete",
                token: "",
                glue: null,
                pre: "DELETE",
                post: null);
        }

        /// <summary>
        /// DELETE from alias
        /// </summary>
        public static SqlBuilder Delete(this SqlBuilder sql, string alias)
        {
            sql.SetTemplate("delete");
            return sql.AddClause(
                keyword: "delete",
                token: alias,
                glue: null,
                pre: "DELETE ",
                post: null);
        }

        /// <summary>
        /// GROUP BY columns
        /// </summary>
        public static SqlBuilder GroupBy(this SqlBuilder sql, params string[] columns) =>
          sql.AddClause(
              keyword: "groupby",
              tokens: columns,
              glue: ", ",
              pre: "GROUP BY ",
              post: null);

        /// <summary>
        /// HAVING predicates
        /// </summary>
        public static SqlBuilder Having(this SqlBuilder sql, params string[] predicate) =>
          sql.AddClause(
              keyword: "having",
              tokens: predicate,
              glue: ", ",
              pre: "HAVING ",
              post: null);

        /// <summary>
        /// INSERT INTO table
        /// </summary>
        public static SqlBuilder Insert(this SqlBuilder sql, string table)
        {
            sql.SetTemplate("insert");
            return sql.AddClause(
                keyword: "insert",
                token: table,
                glue: null,
                pre: "INSERT INTO ",
                post: null);
        }
        /// <summary>
        /// Register columns for INSERT
        /// </summary>
        public static SqlBuilder Into(this SqlBuilder sql, params string[] into) =>
          sql.AddClause(
              keyword: "columns",
              tokens: into,
              glue: ", ",
              pre: "(",
              post: ")");

        /// <summary>
        /// [INNER] JOIN table and predicate
        /// </summary>
        public static SqlBuilder Join(this SqlBuilder sql, string tableAndPredicate) =>
          sql.AddClause(
              keyword: "join",
              token: tableAndPredicate,
              glue: "INNER JOIN ",
              pre: null,
              post: null,
              singular: false);

        /// <summary>
        /// [INNER] JOIN table with predicate
        /// </summary>

        public static SqlBuilder Join(this SqlBuilder sql, string table, string predicate) =>
          sql.Join(
              tableAndPredicate: string.Concat(table, " ON ", predicate));

        /// <summary>
        /// [INNER] JOIN table with alias and predicate
        /// </summary>


        public static SqlBuilder Join(this SqlBuilder sql, string table, string alias, string predicate) =>
          sql.Join(
              tableAndPredicate: string.Concat(table, " AS ", alias, " ON ", predicate));

        /// <summary>
        /// [INNER] JOIN table from SqlBuilder with alias and predicate
        /// </summary>


        public static SqlBuilder Join(this SqlBuilder sql, SqlBuilder derivedTable, string alias, string predicate) =>
          sql.Join(
              table: string.Concat("(", derivedTable.ToSql(), ")"),
              alias: alias,
              predicate: predicate);

        /// <summary>
        /// LEFT JOIN table and predicate
        /// </summary>
        public static SqlBuilder LeftJoin(this SqlBuilder sql, string tableAndPredicate) =>
          sql.AddClause(
              keyword: "join",
              token: tableAndPredicate,
              glue: "LEFT JOIN ",
              pre: null,
              post: null,
              singular: false);

        /// <summary>
        /// LEFT JOIN table with predicate
        /// </summary>

        public static SqlBuilder LeftJoin(this SqlBuilder sql, string table, string predicate) =>
          sql.LeftJoin(
              tableAndPredicate: string.Concat(table, " ON ", predicate));

        /// <summary>
        /// LEFT JOIN table with alias and predicate
        /// </summary>


        public static SqlBuilder LeftJoin(this SqlBuilder sql, string table, string alias, string predicate) =>
          sql.LeftJoin(
              tableAndPredicate: string.Concat(table, " AS ", alias, " ON ", predicate));

        /// <summary>
        /// LEFT JOIN table from SqlBuilder with alias and predicate
        /// </summary>


        public static SqlBuilder LeftJoin(this SqlBuilder sql, SqlBuilder derivedTable, string alias, string predicate) =>
          sql.LeftJoin(
              table: string.Concat("(", derivedTable.ToSql(), ")"),
              alias: alias,
              predicate: predicate);

        /// <summary>
        /// ORDER BY columns
        /// </summary>
        public static SqlBuilder OrderBy(this SqlBuilder sql, params string[] columns) =>
          sql.AddClause(
              keyword: "orderby",
              tokens: columns,
              glue: ", ",
              pre: "ORDER BY ",
              post: null);

        /// <summary>
        /// ORDER BY DESC columns (i.e. col desc, col2 desc)
        /// </summary>
        public static SqlBuilder OrderByDesc(this SqlBuilder sql, params string[] columns)
        {
            var columnsDesc = new string[columns.Length];
            for (var i = 0; i < columns.Length; i++)
            {
                columnsDesc[i] = columns[i] + " DESC";
            }

            return sql.AddClause("orderby", columnsDesc, ", ", "ORDER BY ", null);
        }

        /// <summary>
        /// RIGHT JOIN table and predicate
        /// </summary>
        public static SqlBuilder RightJoin(this SqlBuilder sql, string tableAndPredicate) =>
          sql.AddClause(
              keyword: "join",
              token: tableAndPredicate,
              glue: "RIGHT JOIN ",
              pre: null,
              post: null,
              singular: false);

        /// <summary>
        /// RIGHT JOIN table with predicate
        /// </summary>

        public static SqlBuilder RightJoin(this SqlBuilder sql, string table, string predicate) =>
          sql.RightJoin(
              tableAndPredicate: string.Concat(table, " ON ", predicate));

        /// <summary>
        /// RIGHT JOIN table with alias and predicate
        /// </summary>


        public static SqlBuilder RightJoin(this SqlBuilder sql, string table, string alias, string predicate) =>
          sql.RightJoin(
              tableAndPredicate: string.Concat(table, " AS ", alias, " ON ", predicate));

        /// <summary>
        /// RIGHT JOIN table from SqlBuilder with alias and predicate
        /// </summary>


        public static SqlBuilder RightJoin(this SqlBuilder sql, SqlBuilder derivedTable, string alias, string predicate) =>
          sql.RightJoin(
              table: string.Concat("(", derivedTable.ToSql(), ")"),
              alias: alias,
              predicate: predicate);

        /// <summary>
        /// SELECT columns
        /// </summary>
        public static SqlBuilder Select(this SqlBuilder sql, params string[] columns)
        {
            sql.AddClause(
                keyword: "select",
                token: null,
                glue: null,
                pre: "SELECT",
                post: null);

            return sql.AddClause(
                keyword: "fields",
                tokens: columns,
                glue: ", ",
                pre: null,
                post: null);
        }

        /// <summary>
        /// SELECT columns and apply provided alias
        /// </summary>

        public static SqlBuilder SelectWithAlias(this SqlBuilder sql, string alias, params string[] columns)
        {
            var columnsAliased = new string[columns.Length];
            var aliasProper = (alias[alias.Length - 1] == '.') ? alias : alias + ".";

            for (var i = 0; i < columns.Length; i++)
            {
                columnsAliased[i] = aliasProper + columns[i];
            }

            return
                sql
                .AddClause(
                    keyword: "select",
                    string.Empty,
                    string.Empty,
                    "SELECT",
                    null)
                .AddClause(
                    keyword: "fields",
                    tokens: columnsAliased,
                    glue: ", ",
                    pre: null,
                    post: null);
        }

        /// <summary>
        /// UPDATE &gt; SET column/value pairs
        /// </summary>
        public static SqlBuilder Set(this SqlBuilder sql, params string[] columnAndValuePairs) =>
          sql.AddClause(
              keyword: "set",
              tokens: columnAndValuePairs,
              glue: ", ",
              pre: "SET ",
              post: null);

        /// <summary>
        /// UPDATE table
        /// </summary>
        public static SqlBuilder Update(this SqlBuilder sql, string tableOrAlias)
        {
            sql.SetTemplate("update");
            return sql.AddClause(
                keyword: "update",
                token: tableOrAlias,
                glue: null,
                pre: "UPDATE ",
                post: null);
        }

        /// <summary>
        /// INSERT single record
        /// </summary>
        public static SqlBuilder Value(this SqlBuilder sql, params string[] columnAndValuePairs) =>
          sql.AddClause(
              keyword: "values",
              tokens: columnAndValuePairs,
              glue: ", ",
              pre: "VALUES (",
              post: ")");

        /// <summary>
        /// INSERT multiple records
        /// </summary>
        public static SqlBuilder Values(this SqlBuilder sql, params string[] columnAndValuePairs) =>
          sql.AddClause(
              keyword: "values",
              tokens: columnAndValuePairs,
              glue: "), (",
              pre: "VALUES (",
              post: ")");

        /// <summary>
        /// WHERE [AND] predicates
        /// </summary>
        public static SqlBuilder Where(this SqlBuilder sql, params string[] predicates) =>
          sql.AddClause(
              keyword: "where",
              token: string.Join(" AND ", predicates),
              glue: " AND ",
              pre: "WHERE ",
              post: null);

        /// <summary>
        /// WHERE [OR] predicates
        /// </summary>
        public static SqlBuilder WhereOr(this SqlBuilder sql, params string[] predicates) =>
          sql.AddClause(
              keyword: "where",
              token: string.Join(" OR ", predicates),
              glue: " OR ",
              pre: "WHERE ",
              post: null);
    }
}
