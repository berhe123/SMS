using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Core
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }

        public Response()
        {

        }

        public Response(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public Response SetId(Guid id)
        {
            this.Id = id;
            return this;
        }

        public Response SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public Response ReplaceMessage(string oldString, string newString)
        {
            this.Message = this.Message.Replace(oldString, newString);
            return this;
        }
    }

    public class SuccessResponse : Response
    {
        public SuccessResponse(string message)
            :base(message, true)
        {
        }
    }

    public class FailedResponse : Response
    {
        public FailedResponse(string message)
            : base(message, false)
        {
        }
    }
}
