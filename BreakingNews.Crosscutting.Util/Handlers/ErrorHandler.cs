using System;
using BreakingNews.Application.Dto;

namespace BreakingNews.Crosscutting.Util.Handlers
{
    public static class ErrorHandler
    {
        public static ErrorDto ApiError(string friendlyMessage, Exception ex)
        {
            var error = new ErrorDto(friendlyMessage, ex.Message);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                error.AddDetail(ex.Message);
            }

            return error;
        }
    }
}
