using System.ComponentModel.DataAnnotations;

namespace FormServer.Entity;

public class MessageEntity
{
    [Key]
    public int ID { get; set; }
    public string text_message { get; set; }
    
    public int? id_topic { get; set; }
    public TopicEntity TopicEntity { get; set; }
    
    public int? id_contact { get; set; }
    public ContactEntity ContactEntity { get; set; }
}