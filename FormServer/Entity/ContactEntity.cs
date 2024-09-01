namespace FormServer.Entity;

public class ContactEntity
{
    public int ID { get; set; }
    public string name_user { get; set; }
    public string email_user { get; set; }
    public string phone_user { get; set; }

    public ICollection<MessageEntity> Messages { get; set; }

    public ContactEntity()
    {
        Messages = new List<MessageEntity>();
    }
    
}