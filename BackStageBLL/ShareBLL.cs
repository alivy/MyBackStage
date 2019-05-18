using BackStageDAL;
using BackStageIBLL;
using backStageIDal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace BackStageBLL
{
    /// <summary>
    /// BLL基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[Export]
    public class ShareBLL<T> : IShareBLL<T> 
        where T : class, new()
    {
        //[Import]
        protected IBaseDal<T> _baseDal;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShareBLL()
        {
            _baseDal = new ShareDal<T>();
        }

        #region 获取符合条件的实体个数
        /// <summary>
        /// 获取符合条件的实体个数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> where = null)
        {
            return _baseDal.GetCount(where);
        }
        #endregion

        #region 获取实体集指定总和
        /// <summary>
        /// 获取实体集指定总和
        /// </summary>
        /// <param name="field">求和字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public decimal? GetSum(Expression<Func<T, decimal?>> field, Expression<Func<T, bool>> where = null)
        {
            return _baseDal.GetSum(field, where);
        }
        #endregion

        #region 获取实体集指定条数
        /// <summary>
        /// 获取实体集指定条数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public List<T> LoadEntities<S>(Expression<Func<T, bool>> where = null, Func<T, S> order = null, bool isAsc = false, int top = 0)
        {
            return _baseDal.LoadEntities<S>(where, order, isAsc, top);
        }
        #endregion

        #region 获取实体集
        /// <summary>
        /// 获取实体集
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public List<T> LoadEntities(Expression<Func<T, bool>> where = null)
        {
            return _baseDal.LoadEntities(where);
        }
        #endregion

        #region 数据分页
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="where">条件的lambda</param>
        /// <param name="order">排序的lambda</param>
        /// <param name="total">总数，返回值</param>
        /// <param name="pageSize">每一页数据的大小</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="isAsc">排序方式，true为正序，false为倒序</param>
        /// <returns></returns>
        public List<T> GetPageList<S>(Expression<Func<T, bool>> where, Func<T, S> order,
            out int total, int pageSize = 10, int pageIndex = 1, bool isAsc = false)
        {
            return _baseDal.GetPageList(where, order, out total, pageSize, pageIndex, isAsc);
        }
        #endregion

        #region 添加实体
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public bool AddEntity(T entity)
        {
            return _baseDal.AddEntity(entity) > 0;
        }
        #endregion

        #region 更新实体
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">要更新的实体对象</param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            return _baseDal.UpdateEntity(entity) > 0;
        }

        /// <summary>
        /// 更新指定属性的记录
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertys">属性</param>
        public bool UpdateEntity(T model, string[] propertys)
        {
            return _baseDal.UpdateEntity(model, propertys) > 0;
        }
        #endregion

        #region 删除实体
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除实体的对象，我们一般只需给
        /// 该实体的Id字段赋值即可，其他字段不用赋值</param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            return _baseDal.DeleteEntity(entity) > 0;
        }
        #endregion

        #region 获取第一个实体
        /// <summary>
        /// 获取第一个或者null的实体
        /// </summary>
        /// <typeparam name="S">泛型类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T FirstOrDefault<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true)
        {
            return _baseDal.FirstOrDefault(where, order, isAsc);
        }
        #endregion

        #region 获取第一个的实体
        /// <summary>
        /// 获取第一个或者null的实体
        /// </summary>
        /// <typeparam name="S">泛型类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T First<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true)
        {
            return _baseDal.FirstOrDefault(where, order, isAsc);
        }
        #endregion

        #region 获取一个实体
        /// <summary>
        /// 获取一个实体,实体数量大于1或者不存在将抛出异常
        /// </summary>
        /// <typeparam name="S">泛型类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T Single<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true)
        {
            return _baseDal.Single(where, order, isAsc);
        }
        #endregion

        #region 获取一个的实体
        /// <summary>
        /// 获取一个实体,实体数量大于1将抛出异常
        /// </summary>
        /// <typeparam name="S">泛型类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public T SingleOrDefault<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true)
        {
            return _baseDal.SingleOrDefault(where, order, isAsc);
        }
        #endregion

        #region 获取指定类型的第一个实体
        /// <summary>
        /// 获取指定类型的第一个实体
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序，默认升序</param>
        /// <param name="selector">要返回对象的表达式</param>
        /// <returns></returns>
        public S GetFirstEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
            bool isAsc = true, Expression<Func<T, S>> selector = null) where S : class
        {
            return _baseDal.GetFirstEntity(where, order, isAsc, selector);
        }
        #endregion

        #region 获取指定类型的第一个实体或null
        /// <summary>
        /// 获取指定类型的第一个实体或null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序，默认升序</param>
        /// <param name="selector">要返回对象的表达式</param>
        /// <returns></returns>
        public S GetFirstOrDefaultEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
            bool isAsc = true, Expression<Func<T, S>> selector = null) where S : class
        {
            return _baseDal.GetFirstOrDefaultEntity(where, order, isAsc, selector);
        }
        #endregion

        #region 获取指定类型的一个实体或null
        /// <summary>
        /// 获取指定类型的一个实体或null
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序，默认升序</param>
        /// <param name="selector">要返回对象的表达式</param>
        /// <returns></returns>
        public S GetSingleOrDefaultEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
            bool isAsc = true, Expression<Func<T, S>> selector = null)
        {
            return _baseDal.GetSingleOrDefaultEntity(where, order, isAsc, selector);
        }
        #endregion

        #region 获取指定类型的一个实体
        /// <summary>
        /// 获取指定类型的一个实体
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序，默认升序</param>
        /// <param name="selector">要返回对象的表达式</param>
        /// <returns></returns>
        public S GetSingleEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
            bool isAsc = true, Expression<Func<T, S>> selector = null)
        {
            return _baseDal.GetSingleEntity(where, order, isAsc, selector);
        }
        #endregion

        #region 返回指定类型的集合
        /// <summary>
        /// 返回指定类型的集合
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="selector">返回指定对象</param>
        /// <returns></returns>
        public List<S> GetEntities<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
                  bool isAsc = true, Expression<Func<T, S>> selector = null)
        {
            return _baseDal.GetEntities(where, order, isAsc, selector);
        }
        #endregion

        #region 返回指定类型的分页
        /// <summary>
        /// 返回指定类型的分页
        /// </summary>
        /// <param name="where">条件，不能为null</param>
        /// <param name="order">排序，不能为null</param>
        /// <param name="selector">返回对象，不能为null</param>
        /// <param name="total">总数</param>
        /// <param name="isAsc">是否升序，默认升序</param>
        /// <param name="pageSize">每页数据大小，默认20</param>
        /// <param name="pageIndex">当前页数，默认1</param>
        /// <returns></returns>
        public List<S> GetEntityPageList<S, K>(Expression<Func<T, bool>> where, Expression<Func<T, K>> order,
            Expression<Func<T, S>> selector, out int total, bool isAsc = true, int pageSize = 20, int pageIndex = 1)
            where S : class, new()
        {
            return _baseDal.GetEntityPageList(where, order, selector, out total, isAsc, pageSize, pageIndex);
        }
        #endregion

        #region 批量插入list集合
        public void BulkInsert(string tableName, List<T> list)
        {
            _baseDal.BulkInsert(tableName, list);
        }
        #endregion

        #region 批量插入表
        public void BulkInsert(string tableName, DataTable dt)
        {
            _baseDal.BulkInsert(tableName, dt);
        }
        #endregion


        #region bll层不开放sql直接代入
        public List<S> ExecuteSql<S>(string sql, params SqlParameter[] parameters) where S : class
        {
            throw new System.NotImplementedException("bll层不开放sql直接代入");
            //return _baseDal.ExecuteSql<S>(sql, parameters);
        }

        public int Executesql(string sql)
        {
            throw new System.NotImplementedException("bll层不开放sql直接代入");
            //return _baseDal.Executesql(sql);
        }



        public DataSet GetDataSet(string sql, CommandType cmdType, params SqlParameter[] sqlParams)
        {
            throw new System.NotImplementedException("bll层不开放sql直接代入");
            //return _baseDal.GetDataSet(sql, cmdType, sqlParams);
        }

        #endregion

        #region 获取编号
        public int GetPulse(string name)
        {
            return _baseDal.GetPulse(name);
        }
        #endregion



    }
}
