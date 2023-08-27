﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.IntegrationEvents.Model
{
    [Serializable]
    public partial class MessageResponse
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

        public MessageResponse()
        {
            success = true;
            httpStatusCode = 200;
            errors = new Dictionary<string, IEnumerable<string>>();
            isRedirect = false;
        }

        public MessageResponse(MessageResponse obj)
        {
            success = obj.success;
            code = obj.code;
            httpStatusCode = obj.httpStatusCode;
            title = obj.title;
            message = obj.message;
            data = obj.data;
            totalCount = obj.totalCount;
            isRedirect = obj.isRedirect;
            redirectUrl = obj.redirectUrl;
            errors = obj.errors;
        }
    }
}