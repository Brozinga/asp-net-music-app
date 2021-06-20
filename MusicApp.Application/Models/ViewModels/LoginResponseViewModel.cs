using Newtonsoft.Json;

namespace music_app.Models.ViewModels
{
    public class LoginResponseViewModel
    {
        [JsonProperty("response")]
        public LoginMessageViewModel Response { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("error")]
        public bool Error { get; set; }
    }

    public class LoginMessageViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("message")]
        public LoginSuccessViewModel Message { get; set; }
    }

    public class LoginSuccessViewModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }          
    }

}
