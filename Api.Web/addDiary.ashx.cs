using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hope.HopeDiary.BLL;
using Hope.HopeDiary.Model;
using Hope.HopeDiary.Utils;

namespace Api.Web
{
    /// <summary>
    /// addDaily 的摘要说明
    /// </summary>
    public class addDiary : BaseClass,IHttpHandler
    {
        private DiaryModel _diaryModel;

        private Hope_DiaryBLL _hopeDiaryBll;

        private MessageModel _message;

        public void ProcessRequest(HttpContext context)
        {
            //todo:首先应该检测用户是否登录session[]
            if (!CheckLogin(context))
            {
                _message.Type = 0;
                _message.Title = "添加日志失败";
                _message.Content = "您尚未登录，请重新登录后再执行本操作，若该问题持续存在，请联系管理员！";
                _message.CreatedTime = DateTime.Now.ToLongDateString();

                context.Response.Write(_message.ToJson());
                context.Response.End();
            }

            //获取客户端发送的消息:authorId,title,subtitle,content
            _diaryModel = new DiaryModel();
            HttpRequest httpReq = context.Request;

            //保存消息到DiaryModel中
            _diaryModel.AuthorId = StringHandler.getInt(httpReq["authorId"]);
            _diaryModel.DiaryTitle = httpReq["title"].Trim();
            _diaryModel.DiarySubtitle = httpReq["subtitle"].Trim();
            _diaryModel.DiaryContent = httpReq["content"];
            
            //设置日志创建时间
            _diaryModel.DiaryCreateTime = _diaryModel.DiaryModifyTime = DateTime.Now.ToLongDateString();

            //todo:对content中的内容进行合法性检测
            _hopeDiaryBll = new Hope_DiaryBLL();
            _message = new MessageModel();

            if (_hopeDiaryBll.StoreSingleData(_diaryModel))
            {
                //0 for failed , 1 for succeed
                _message.Type = 1;
                _message.Title = "添加日志成功";
                _message.Content = "您已成功添加一条日志！";
                _message.CreatedTime = DateTime.Now.ToLongDateString();

                context.Response.Write(_message.ToJson());
                context.Response.End();
            }
            else
            {
                _message.Type = 0;
                _message.Title = "添加日志失败";
                _message.Content = "添加日志失败，请重新添加，若该问题持续存在，请联系管理员！";
                _message.CreatedTime = DateTime.Now.ToLongDateString();

                context.Response.Write(_message.ToJson());
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}