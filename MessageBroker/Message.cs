namespace MessageBroker
{
    public class Message
    {
        public int GameId { get; init; }
        public string ObjectId { get; init; }
        public string OperationId { get; init; }
        public string ArgsJson { get; init; }
    }
}