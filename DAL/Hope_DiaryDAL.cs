using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Hope.HopeDiary.Utils;
using Hope.HopeDiary.Model;

namespace Hope.HopeDiary.DAL
{
    public class Hope_DiaryDAL
    {
        private string _sqlConText = WebConfigurationManager.ConnectionStrings["HopeDiary"].ToString();

        private List<SqlParameter> _sqlParas = new List<SqlParameter>();

        private List<DiaryModel> _diaryModels = new List<DiaryModel>(); 

        private SqlConnection sqlCon;

        private SqlCommand sqlCmd;

        private bool _isSucceed = false;

        private const string _addSqlText =
            "INSERT into User_Diary(DiaryTitle,DiarySubtitle,AuthorId,DiaryCreateTime,DiaryModifyTime,DiaryContent,UpVoteCount,CommentCount) values                         (@DiaryTitle,@DiarySubtitle,@AuthorId,@DiaryCreateTime,@DiaryModifyTime,@DiaryContent,@UpVoteCount,@CommentCount)";

        private const string _deleteByIdText = "DELETE from User_Diary WHERE id = @id";

        private const string _updateByIdText =
            "UPDATE User_Diary set DiaryTitle = @DiaryTitle,DiarySubtitle = @DiarySubtitle,DiaryModifyTime = @DiaryModifyTime,DiaryContent = @DiaryContent";

        private const string _updateUpVoteCount = "UPDATE User_Diary set UpVoteCount = @UpVoteCount where id = @id";

        private const string _updateCommentCount = "Update User_Diary set CommentCount=@CommentCount where id = @id";

        private const string _selectPagerText =
            "SELECT top @count * from User_Diary where id not in (select top @start id from User_Diary)";

        /// <summary>
        /// 将单个日志数据保存到数据库
        /// </summary>
        /// <param name="diaryModel"></param>
        /// <returns></returns>
        public bool StoreSingleDiary(DiaryModel diaryModel)
        {
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();

                sqlCmd.CommandText = _addSqlText;

                SqlHandler.SqlParaAdd<string>("@DiaryTitle",SqlDbType.NVarChar,diaryModel.DiaryTitle,_sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiarySubtitle", SqlDbType.NVarChar, diaryModel.DiarySubtitle, _sqlParas);
                SqlHandler.SqlParaAdd<int>("@AuthorId", SqlDbType.NVarChar, diaryModel.AuthorId, _sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiaryCreateTime", SqlDbType.NVarChar, diaryModel.DiaryCreateTime, _sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiaryModifyTime", SqlDbType.NVarChar, diaryModel.DiaryModifyTime, _sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiaryContent", SqlDbType.NVarChar, diaryModel.DiaryContent, _sqlParas);
                SqlHandler.SqlParaAdd<int>("@UpVoteCount", SqlDbType.NVarChar, diaryModel.UpVoteCount, _sqlParas);
                SqlHandler.SqlParaAdd<int>("@CommentCount", SqlDbType.NVarChar, diaryModel.CommentCount, _sqlParas);

                foreach (SqlParameter sqlPara in _sqlParas)
                {
                    sqlCmd.Parameters.Add(sqlPara);
                }


                //如果没有捕获到异常，说明保存数据成功
                try
                {
                    sqlCon.Open();

                    sqlCmd.ExecuteNonQuery();

                    return _isSucceed = true;
                }
                catch (Exception)
                {
                    return _isSucceed = false;
                }
            }
        }

        /// <summary>
        /// 根据id删除对应日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteDiaryById(int id)
        {
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = _deleteByIdText;

                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                try
                {
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();

                    return _isSucceed = true;
                }
                catch (Exception)
                {
                    return _isSucceed = false;
                }
            }
        }

        public bool UpdateDiaryById(DiaryModel diaryModel)
        {
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = _updateByIdText;
                
                SqlHandler.SqlParaAdd<string>("@DiaryTitle",SqlDbType.NVarChar,diaryModel.DiaryTitle,_sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiarySubtitle", SqlDbType.NVarChar, diaryModel.DiarySubtitle, _sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiaryContent",SqlDbType.NVarChar,diaryModel.DiaryContent,_sqlParas);
                SqlHandler.SqlParaAdd<string>("@DiaryModifyTime", SqlDbType.NVarChar, diaryModel.DiaryModifyTime, _sqlParas);

                foreach (SqlParameter sqlPara in _sqlParas)
                {
                    sqlCmd.Parameters.Add(sqlPara);
                }

                try
                {
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();

                    return _isSucceed = true;
                }
                catch (Exception)
                {
                    return _isSucceed = false;
                }

            }
        }

        /// <summary>
        /// 更新某篇日志的点赞数
        /// </summary>
        /// <param name="DiaryId"></param>
        /// <param name="NewUpVoteCount"></param>
        /// <returns></returns>
        public bool UpdateUpVoteCount(int diaryId, int newUpVoteCount)
        {
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = _updateUpVoteCount;

                sqlCmd.Parameters.Add("@UpVoteCount", SqlDbType.Int).Value = newUpVoteCount;
                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = diaryId;

                try
                {
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();

                    return _isSucceed = true;
                }
                catch (Exception)
                {
                    return _isSucceed = false;
                }
            }
        }

        /// <summary>
        /// 更新某一篇日志的评论数目
        /// </summary>
        /// <param name="diaryId"></param>
        /// <param name="newCommentCount"></param>
        /// <returns></returns>
        public bool UpdateCommentCount(int diaryId, int newCommentCount)
        {
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = _updateCommentCount;

                sqlCmd.Parameters.Add("@CommentCount", SqlDbType.Int).Value = newCommentCount;
                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = diaryId;

                try
                {
                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();

                    return _isSucceed = true;
                }
                catch (Exception)
                {
                    return _isSucceed = false;
                }
            }
        }

        /// <summary>
        /// 选择出从start到end之间的所有符合条件的记录
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<DiaryModel> SelectSomeDiary(int start, int end)
        {
            int count = end - start;
            DiaryModel diaryModel;
            using (sqlCon = new SqlConnection(_sqlConText))
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = _selectPagerText;

                sqlCmd.Parameters.Add("@count", SqlDbType.Int).Value = count;
                sqlCmd.Parameters.Add("@start", SqlDbType.Int).Value = start;

                try
                {
                    sqlCon.Open();
                    SqlDataReader sqlDataReader = sqlCmd.ExecuteReader();

                    if (!sqlDataReader.HasRows)
                    {
                        return null;
                    }

                    while (sqlDataReader.Read())
                    {
                        diaryModel = new DiaryModel();
                        diaryModel.LoadData(sqlDataReader);
                        _diaryModels.Add(diaryModel);
                    }

                    return _diaryModels;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
