using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebStore.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public string OrderDetails { get; set; }
        
        public int CustomerId { get; set; }
        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}