using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace whwd_web_api.Errors
{
    public class ErrorMsg
    {
       public string ErrorCode { get; set; }
       public string Message { get; set; }

       public ErrorMsg(string ErrorCode, string Message) {
        this.ErrorCode = ErrorCode;
       this.Message = Message;
        }
    }
}
