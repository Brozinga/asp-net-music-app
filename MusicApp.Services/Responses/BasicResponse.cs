using MusicApp.Services.Interfaces.Responses;

namespace MusicApp.Services.Responses
{
    public class BasicResponse<T> : IHandleResponse where T : class
    {
        public BasicResponse(T response)
        {
            Response = response;
            Status = 200;
            Error = false;
        }

        public BasicResponse(T response, int status, bool error = false)
        {
            Response = response;
            Status = status;
            Error = error;
        }

        public object Response { get; set; }
        public int Status { get; }
        public bool? Error { get; }
    }
}