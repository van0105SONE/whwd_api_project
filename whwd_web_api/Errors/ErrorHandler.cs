using ApplicationCore.Constanst;
using Microsoft.AspNetCore.Mvc;

namespace whwd_web_api.Errors
{
    public class ErrorHandler
    {
        public ErrorHandler() {
        }

        public ObjectResult HandleErrorResponse(ErrorMsg errorMsg)
        {
            try
            {
                switch (errorMsg.ErrorCode)
                {
                    case ErrorCodes.Conflict:
                        return new ObjectResult(errorMsg)
                        {
                            StatusCode = StatusCodes.Status409Conflict
                        };

                    case ErrorCodes.Unauthorized:
                        return new ObjectResult(errorMsg)
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                    case ErrorCodes.InternalError:
                        return new ObjectResult(errorMsg)
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    default:
                        return new ObjectResult(errorMsg)
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                }
 
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
