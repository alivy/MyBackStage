using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace backStageIDal
{
    [InheritedExport]
    public interface IBaseDal<T> where T : class, new()
    {


        /// <summary>
        /// 获取符合条件的实体个数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> where = null);



        /// <summary>
        /// 获取实体集指定总和
        /// </summary>
        /// <param name="field">求和字段</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        decimal? GetSum(Expression<Func<T, decimal?>> field, Expression<Func<T, bool>> where = null);



        #region 获取实体集
        /// <summary>
        /// 获取实体集
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        List<T> LoadEntities(Expression<Func<T, bool>> where = null);

        #endregion

        #region 获取实体集指定条数
        /// <summary>
        /// 获取实体集指定条数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        List<T> LoadEntities<S>(Expression<Func<T, bool>> where = null, Func<T, S> order = null, bool isAsc = false, int top = 0);

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
        List<T> GetPageList<S>(Expression<Func<T, bool>> where, Func<T, S> order,
           out int total, int pageSize = 10, int pageIndex = 1, bool isAsc = false);

        #endregion

        #region 添加实体
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int AddEntity(T entity);

        #endregion

        #region 更新实体
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">要更新的实体对象</param>
        /// <returns></returns>
        int UpdateEntity(T entity);

        #endregion

        #region 更新指定属性的记录
        /// <summary>
        /// 更新指定属性的记录
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertys">属性</param>
        int UpdateEntity(T model, string[] propertys);
        #endregion

        #region 删除实体
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除实体的对象，我们一般只需给
        /// 该实体的Id字段赋值即可，其他字段不用赋值</param>
        /// <returns></returns>
        int DeleteEntity(T entity);

        #endregion

        #region 获取第一个或者null的实体
        /// <summary>
        /// 获取第一个或者null的实体
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        T FirstOrDefault<S>(Expression<Func<T, bool>> where = null);

        #endregion

        #region 获取第一个实体
        /// <summary>
        /// 获取第一个或者null的实体
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        T First<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true);

        #endregion

        #region 获取一个实体,实体数量大于1或者不存在将抛出异常
        /// <summary>
        /// 获取一个实体,实体数量大于1或者不存在将抛出异常
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        T Single<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true);

        #endregion

        #region 获取一个实体,实体数量大于1将抛出异常
        /// <summary>
        /// 获取一个实体,实体数量大于1将抛出异常
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        T SingleOrDefault<S>(Expression<Func<T, bool>> where = null, Expression<Func<T, S>> order = null, bool isAsc = true);

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
        S GetFirstEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
           bool isAsc = true, Expression<Func<T, S>> selector = null) where S : class;

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
        S GetFirstOrDefaultEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
           bool isAsc = true, Expression<Func<T, S>> selector = null) where S : class;

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
        S GetSingleOrDefaultEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
           bool isAsc = true, Expression<Func<T, S>> selector = null);

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
        S GetSingleEntity<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
           bool isAsc = true, Expression<Func<T, S>> selector = null);


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
        List<S> GetEntities<S, K>(Expression<Func<T, bool>> where = null, Expression<Func<T, K>> order = null,
                 bool isAsc = true, Expression<Func<T, S>> selector = null);

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
        List<S> GetEntityPageList<S, K>(Expression<Func<T, bool>> where, Expression<Func<T, K>> order,
           Expression<Func<T, S>> selector, out int total, bool isAsc = true, int pageSize = 20, int pageIndex = 1)
           where S : class, new();


        #endregion

        #region 执行sql语句，返回指定的类型
        /// <summary>
        /// 执行sql语句，返回指定的类型
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        List<S> ExecuteSql<S>(string sql, params SqlParameter[] parameters) where S : class;


        #endregion

        #region 执行sql语句，返回Int型
        Int32 Executesql(string sql);

        #endregion

        #region 获取一个DataSet
        /// <summary>
        /// 获取一个DataSet
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="sqlParams">传递的参数</param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, System.Data.CommandType cmdType, params SqlParameter[] sqlParams);


        #endregion

        #region 获取脉冲号
        int GetPulse(string name);

        #endregion

        #region 批量插入
        /// <summary>  
        /// 批量插入  
        /// </summary>  
        /// <typeparam name="T">泛型集合的类型</typeparam>  
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>  
        /// <param name="list">要插入大泛型集合</param>  
        void BulkInsert(string tableName, List<T> list);


        /// <summary>
        ///  批量插入 
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="tableName">本地数据库表的表名</param>
        void BulkInsert(string tableName, DataTable dt);



        #endregion



        #region 使用EF.Extensions批量处理
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int BulkUpdate(IEnumerable<T> entity);




        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int BulkInsert(IEnumerable<T> entity);



        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int BulkDelete(IEnumerable<T> entity);


        #endregion

        #region 使用EntityFramework.Extended批量处理

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int BulkDelete(Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        int BulkUpdate(Expression<Func<T, T>> entity, Expression<Func<T, bool>> where = null);


        /// <summary>
        ///批量删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int BulkInsert2(IEnumerable<T> entity);
        #endregion
    }
}
