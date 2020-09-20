using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Npgsql;
using Api.Config;
using Api.Models;
using static System.Console;

namespace Api.Services
{
    public abstract class BaseService<T> where T : BaseEntity
    {
        private readonly IDbConnection _conn;

        protected readonly string TableName;

        public IDbTransaction Transaction { get; private set; }

        protected BaseService(AppConfig config)
        {
            _conn = new NpgsqlConnection(config.ConnectionString);
            TableName = (Activator.CreateInstance(typeof(T)) as T).EntityName;
        }

        protected void Execute(string query, object parameters = null)
        {
            try
            {
                Log(query);
                _conn.Execute(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected U ExecuteScalar<U>(string query, object parameters = null)
        {
            try
            {
                Log(query);
                return _conn.ExecuteScalar<U>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected T FirstOrDefault(string query, object parameters = null)
        {
            try
            {
                Log(query);
                return _conn.QuerySingleOrDefault<T>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected IEnumerable<T> Query(string query, object parameters = null)
        {
            try
            {
                Log(query);
                return _conn.Query<T>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public long CurrentId(T entity)
        {
            try
            {
                var query = $"SELECT MAX(ID) FROM \"{TableName}\"";
                Log(query);
                return _conn.ExecuteScalar<long>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void Log(string query)
        {
            WriteLine("");
            WriteLine("");
            WriteLine($"Query:{query}");
            WriteLine("");
            WriteLine("");
        }
    }
}