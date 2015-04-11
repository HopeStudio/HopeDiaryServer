using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hope.HopeDiary.Model;
using Hope.HopeDiary.Utils;

namespace Hope.HopeDiary.Model
{
    public class DiaryModel
    {

        public DiaryModel() {

            this.UpVoteCount = this.CommentCount = 0;
        }

        public int Id
        {
            get;
            set;
        }

        public string DiaryTitle
        {
            get;
            set;
        }

        public string DiarySubtitle
        {
            get;
            set;
        }

        public int AuthorId
        {
            get;
            set;
        }

        public string DiaryCreateTime
        {
            get;
            set;
        }

        public string DiaryModifyTime
        {
            get;
            set;
        }

        public string DiaryContent
        {
            get;
            set;
        }

        public int UpVoteCount
        {
            get;
            set;
        }

        public int CommentCount
        {
            get;
            set;
        }

        public void LoadData(SqlDataReader dataReader)
        {
            this.Id = StringHandler.getInt(dataReader["id"].ToString());
            this.DiaryTitle = dataReader["DiaryTitle"].ToString();
            this.DiarySubtitle = dataReader["DiarySubtitle"].ToString();
            this.DiaryCreateTime = dataReader["DiaryCreateTime"].ToString();
            this.DiaryModifyTime = dataReader["DiaryModifyTime"].ToString();
            this.DiaryContent = dataReader["DiaryContent"].ToString();
            this.UpVoteCount = StringHandler.getInt(dataReader["UpVoteCount"].ToString());
            this.CommentCount = StringHandler.getInt(dataReader["CommentCount"].ToString());
            this.AuthorId = StringHandler.getInt(dataReader["AuthorId"].ToString());
        }

    }
}
