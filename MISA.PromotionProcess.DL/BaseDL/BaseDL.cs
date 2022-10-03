using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.PromotionProcess.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.CNTT2.LOI.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field
        private readonly IConfiguration _configuration;
        private readonly string _conn;
        #endregion

        #region Constructor
        public BaseDL()
        {
            _conn = DBContext.ConnectionStrings;
        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        public IEnumerable<dynamic> GetAll()
        {

            using (var connection = new MySqlConnection(this._conn))
            {

                string className = typeof(T).Name;
                var sql = $"SELECT * FROM {className}";
                var employeees = connection.Query(sql).ToList();
                return employeees;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        public T GetByID(Guid id)
        {
            using (var connection = new MySqlConnection(this._conn))
            {
                string className = typeof(T).Name;
                var idName = typeof(T).GetProperties().First().Name;
                var sql = $"SELECT * FROM {className} WHERE {idName} = @id";
                var record = connection.Query<T>(sql, new { id = id }).FirstOrDefault();
                return record;
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        public int Add(T entity)
        {
            BeforeSaveAsyn(entity);
            using var connection = new MySqlConnection(this._conn);
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {

                string className = typeof(T).Name;
                var sql = $"Proc_{className}_Add";
                var numberOfAffectedRows = connection.Execute(sql, entity, commandType: CommandType.StoredProcedure, transaction: transaction);

                transaction.Commit();
                return numberOfAffectedRows;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual void BeforeSaveAsyn(T entity)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual void AfterSaveAsyn(T entity)
        {

        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Kiểu đối tượng của bản ghi</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        public int Update(Guid id, T entity)
        {
            using var connection = new MySqlConnection(this._conn);
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                string className = typeof(T).Name;
                var sql = $"Proc_{className}_Update";
                var numberOfAffectedRows = connection.Execute(sql, entity, commandType: CommandType.StoredProcedure, transaction: transaction);
                transaction.Commit();
                return numberOfAffectedRows;
            }
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi muốn xóa</param>
        /// <returns>Số dòng ảnh hưởng</returns>
        public int Delete(Guid id)
        {
            using var connection = new MySqlConnection(this._conn);
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                string className = typeof(T).Name;
                var idName = typeof(T).GetProperties().First().Name;
                var sql = $"DELETE FROM {className} WHERE {idName} = '{id}'";
                var numberOfAffectedRows = connection.Execute(sql, transaction: transaction);

                transaction.Commit();
                return numberOfAffectedRows;
            }
        }

        #endregion
    }
}
