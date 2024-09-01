namespace FormServer.Entity;

public class TopicEntity
{
    public int ID { get; set; }
    public string name_topic { get; set; }
    public ICollection<MessageEntity> Messages { get; set; }

    public TopicEntity()
    {
        Messages = new List<MessageEntity>();
    }
}