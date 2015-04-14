using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.HopeDiary.Model
{
    public class MessageModel
    {
        public int Type
        {
            get; set;
        }

        public string Title
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public string CreatedTime
        {
            get; set;
        }

        public string ToJson()
        {
            StringBuilder jsonStr = new StringBuilder("{");
            jsonStr.Append("'title':" + this.Title + ",");
            jsonStr.Append("'type':" + this.Type.ToString() + ",");
            jsonStr.Append("'content':" + this.Content + ",");
            jsonStr.Append("'createdTime':" + this.CreatedTime);
            jsonStr.Append("}");

            return jsonStr.ToString();
        }
    }
}
