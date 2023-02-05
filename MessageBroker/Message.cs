namespace MessageBroker
{
    public class Message
    {
        public int GameId { get; init; }
        public int ObjectId { get; init; }
        public int OperationId { get; init; }
        public string ArgsJson { get; init; }
    }
}