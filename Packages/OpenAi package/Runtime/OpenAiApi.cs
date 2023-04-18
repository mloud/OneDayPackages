using OneDay.OpenAi.Chat;

namespace OneDay.OpenAi
{
    public class OpenAiApi
    {
        public ChatGPT ChatGpt { get; }
        
        public OpenAiApi(OpenAiConfig config)
        {
            ChatGpt = new ChatGPT(config.ChatGptUrl, config.ApiKey);
        }
    }
}