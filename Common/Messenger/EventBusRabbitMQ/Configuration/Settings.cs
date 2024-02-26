using System.ComponentModel.DataAnnotations;

namespace Messenger.EventBus.EventBusRabbitMQ.Configuration
{
    public class RabbitMQBaseSettings
    {
        [Required]
        public string Endpoint { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string User { get; set; }
        public string Broker { get; set; }
        public string Queue { get; set; }
        public string ConnRetries { get; set; }
        public string HasRetry { get; set; }
    }
}