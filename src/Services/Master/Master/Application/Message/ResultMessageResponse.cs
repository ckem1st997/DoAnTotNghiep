using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Application.Message
{
    [Serializable]
    public partial class ResultMessageResponse
    {
        public bool success { get; set; }

        public string code { get; set; }

        public int httpStatusCode { get; set; }

        public string title { get; set; }

        public string message { get; set; }

        public dynamic data { get; set; }

        public int totalCount { get; set; }

        public bool isRedirect { get; set; }

        public string redirectUrl { get; set; }

        public Dictionary<string, IEnumerable<string>> errors { get; set; }

        public ResultMessageResponse()
        {
            this.success = true;
            this.httpStatusCode = 200;
            this.errors = new Dictionary<string, IEnumerable<string>>();
        }

        public ResultMessageResponse(ResultMessageResponse obj)
        {
            this.success = obj.success;
            this.code = obj.code;
            this.httpStatusCode = obj.httpStatusCode;
            this.title = obj.title;
            this.message = obj.message;
            this.data = obj.data;
            this.totalCount = obj.totalCount;
            this.isRedirect = obj.isRedirect;
            this.redirectUrl = obj.redirectUrl;
            this.errors = obj.errors;
        }
    }
}