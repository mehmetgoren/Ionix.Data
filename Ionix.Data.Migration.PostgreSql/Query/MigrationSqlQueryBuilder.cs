﻿namespace Ionix.Data.Migration.PostgreSql
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Utils.Extensions;
    using Data.Common;
    using Common;

    public class MigrationSqlQueryBuilder : IMigrationSqlQueryBuilder
    {
        public static readonly MigrationSqlQueryBuilder Instance = new MigrationSqlQueryBuilder();

        private MigrationSqlQueryBuilder()
        {
        }

        //tableattribute check edilip gönderiliyor types' a
        public virtual SqlQuery CreateTable(IEnumerable<Type> types, DbSchemaMetaDataProvider provider,
            IColumnDbTypeResolver typeResolver)
        {
            SqlQuery query = new SqlQuery();
            if (!types.IsNullOrEmpty() && null != typeResolver)
            {
                foreach (Type type in types)
                {
                    IEntityMetaData metaData = provider.CreateEntityMetaData(type);
                    List<Column> columns = new List<Column>();
                    foreach (PropertyMetaData prop in metaData.Properties)
                    {
                        var column = typeResolver.GetColumn(prop);
                        columns.Add(column);
                    }

                    CreateTableQueryBuilder item = new CreateTableQueryBuilder(metaData.TableName, columns);
                    query.Combine(item.ToQuery());
                    query.Text.AppendLine();

                    //indexes
                    IEnumerable<TableIndexAttribute> indexAttrs = type.GetCustomAttributes<TableIndexAttribute>();
                    if (!indexAttrs.IsNullOrEmpty())
                    {
                        foreach (TableIndexAttribute indexAttr in indexAttrs)
                        {
                            CreateIndexQueryBuilder ci = new CreateIndexQueryBuilder(metaData.TableName, indexAttr);
                            query.Combine(ci.ToQuery());
                            query.Text.AppendLine();
                        }
                    }

                    //fks
                    IEnumerable<TableForeignKeyAttribute>
                        fkAttrs = type.GetCustomAttributes<TableForeignKeyAttribute>();
                    if (!fkAttrs.IsNullOrEmpty())
                    {
                        foreach (TableForeignKeyAttribute fkAttr in fkAttrs)
                        {
                            CreateForeignKeyQueryBuilder cfk =
                                new CreateForeignKeyQueryBuilder(metaData.TableName, fkAttr);
                            query.Combine(cfk.ToQuery());
                            query.Text.AppendLine();
                        }

                        query.Text.AppendLine();
                    }


                    query.Text.AppendLine();
                }
            }

            return query;
        }
    }
}