using System.Collections.Generic;

namespace OneDay.OpenAi.Chat
{
    public class ChatGPTResponseModel
    {
        public string id;
        public string obj;
        public long created;
        public string model;
        public Usage usage;
        public List<Choice> choices; 
        public class Choice
        {
            public class Message
            {
                public string role;
                public string content;
                public string finish_reason;
                public int index;
            }

            public Message message;
        }

        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
}