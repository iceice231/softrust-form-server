using System.Drawing;
using FormServer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Drawing.Imaging;

namespace FormServer.Controllers
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string textMessage { get; set; }
        public int TopicId { get; set; }
        
        public string Topic { get; set; }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly DBContext _context;

        public TopicsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicEntity>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }
    }
    
    [Route("api/[controller]")]
    [ApiController]
     public class ContactsController : ControllerBase
    {
        private readonly DBContext _context;

        public ContactsController(DBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<ContactEntity>> PostContact([FromBody] ContactDto contactDto)
        {

            var existingContact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.email_user == contactDto.Email && c.phone_user == contactDto.Phone);
            
            if (existingContact != null)
            {
                var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.email_user == contactDto.Email);
                var topic = await _context.Topics.FirstOrDefaultAsync(t => t.name_topic == contactDto.Topic);
                
                var message = new MessageEntity
                {
                    text_message = contactDto.textMessage,
                    TopicEntity = topic,
                    ContactEntity = contact
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(PostContact), new ContactDto
                {
                    Id = contact.ID,
                    Name = contact.name_user,
                    Email = contact.email_user,
                    Phone = contact.phone_user,
                    textMessage = message.text_message,
                    Topic = topic.name_topic
                });
            }
            else
            {
                var newContact = new ContactEntity
                {
                    name_user = contactDto.Name,
                    email_user = contactDto.Email,
                    phone_user = contactDto.Phone
                };
                _context.Contacts.Add(newContact);
                await _context.SaveChangesAsync();

                var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.email_user == newContact.email_user);

                var topic = await _context.Topics.FirstOrDefaultAsync(t => t.name_topic == contactDto.Topic);

                var message = new MessageEntity
                {
                    text_message = contactDto.textMessage,
                    TopicEntity = topic,
                    ContactEntity = contact
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(PostContact), new ContactDto
                {
                    Id = contact.ID,
                    Name = contact.name_user,
                    Email = contact.email_user,
                    Phone = contact.phone_user,
                    textMessage = message.text_message,
                    Topic = topic.name_topic
                });
            }
        }
    }
}