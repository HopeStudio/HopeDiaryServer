using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hope.HopeDiary.Model;
using Hope.HopeDiary.Utils;

namespace Api.Web
{
    /// <summary>
    /// addDaily 的摘要说明
    /// </summary>
    public class addDaily : IHttpHandler
    {
        private DiaryModel _diaryModel;

        public void ProcessRequest(HttpContext context)
        {
            //获取客户端发送的消息:authorId,title,subtitle,content
            _diaryModel = new DiaryModel();
            HttpRequest httpReq = context.Request;

            //保存消息到DiaryModel中
            _diaryModel.AuthorId = StringHandler.getInt(httpReq["authorId"]);
            _diaryModel.DiaryTitle = httpReq["title"];
            _diaryModel.DiarySubtitle = httpReq["subtitle"];
            _diaryModel.DiaryContent = httpReq["content"];
            
            //设置日志创建时间
            _diaryModel.DiaryCreateTime = _diaryModel.DiaryModifyTime = DateTime.Now.ToLongDateString();

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