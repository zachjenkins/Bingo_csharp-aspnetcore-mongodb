using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bingo.Domain.Entities
{
    public class Exercise
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public ICollection<Activation> Activations { get; set; }
    }
}
