using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hope.HopeDiary.Model;
using Hope.HopeDiary.DAL;

namespace Hope.HopeDiary.BLL
{
    public class Hope_DiaryBLL
    {
        private Hope_DiaryDAL _hopeDiaryDal;

        public Hope_DiaryBLL()
        {
            _hopeDiaryDal = new Hope_DiaryDAL();
        }

        public bool StoreSingleData(DiaryModel diary)
        {
            //将单条日志记录保存到数据库中
            //若成功，则为true，反之则为false
            return _hopeDiaryDal.StoreSingleDiary(diary);
        }

        public bool DeleteDiaryById(DiaryModel diary)
        {
            int id = diary.Id;
            return  _hopeDiaryDal.DeleteDiaryById(id);
        }

        public bool UpdateDiaryById(DiaryModel diary)
        {
            return _hopeDiaryDal.UpdateDiaryById(diary);
        }

        public bool UpdateUpVoteCount(DiaryModel diary)
        {
            int id = diary.Id;
            int upVoteCount = diary.UpVoteCount;
            return _hopeDiaryDal.UpdateUpVoteCount(id, upVoteCount);
        }

        public bool UpdateCommentCount(DiaryModel diary)
        {
            int id = diary.Id;
            int commentCount = diary.CommentCount;
            return _hopeDiaryDal.UpdateCommentCount(id, commentCount);
        }

        public List<DiaryModel> SelectSomeDiary(int start, int end)
        {
            return _hopeDiaryDal.SelectSomeDiary(start, end);
        }
    }
}
