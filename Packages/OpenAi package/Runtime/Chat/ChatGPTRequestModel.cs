using System.Collections.Generic;

namespace OneDay.OpenAi.Chat
{
    public class ChatGPTRequestModel
    {
        public string model;
        public List<Message> messages;

        public class Message
        {
            public string role;
            public string content;
        }
    }
}