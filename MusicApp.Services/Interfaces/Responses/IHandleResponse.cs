namespace MusicApp.Services.Interfaces.Responses
{
    public interface IHandleResponse
    {
        public object Response { get; set; }
        public int Status { get; }
        public bool? Error { get; }
    }
}