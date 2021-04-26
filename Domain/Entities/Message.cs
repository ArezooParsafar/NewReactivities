using System;

namespace Domain.Entities
{
    public class Message
    {
        public string SenderAppUserId { get; set; }
        public string ReceiverAppUserId { get; set; }
        public string Body { get; set; }
        public DateTime SendDateTime { get; set; }
    }
}
