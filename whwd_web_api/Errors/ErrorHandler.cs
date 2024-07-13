using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace whwd_web_api.Errors
{
    public class ErrorHandler<T>
    {
        public ErrorHandler() {
        }

        public static MessageReponse<T> HandleErrorResponse(string ErrorCode, string errorMessage)
        {
            try
            {
                switch (ErrorCode)
                {
                    case ErrorCodes.Conflict:
                        return new MessageReponse<T>() { 
                            statusCode = 409,
                            isSuccess = false,
                            message = errorMessage
                        };

                    case ErrorCodes.Unauthorized:
                        return new MessageReponse<T>()
                        {
                            statusCode = 401,
                            isSuccess = false,
                            message = errorMessage,

                        };

                    case ErrorCodes.Validation:
                        return new MessageReponse<T>()
                        {
                            statusCode = 400,
                            isSuccess = false,
                            message = errorMessage,

                        };
                    case ErrorCodes.InternalError:
                        return new MessageReponse<T>()
                        {
                            statusCode = 500,
                            isSuccess = false,
                            message = errorMessage,

                        };
                    default:
                        return new MessageReponse<T>()
                        {
                            statusCode = 500,
                            isSuccess = false,
                            message = "Unexpect error, system can't regonize this error",

                        };
                }
 
            }catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
