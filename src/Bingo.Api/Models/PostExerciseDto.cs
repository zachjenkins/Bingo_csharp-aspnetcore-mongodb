using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bingo.Domain.Entities;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace Bingo.Api.Models
{
    public class PostExerciseDto : IValidatableObject
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string ShortName { get; set; }

        [MaxLength(60)]
        public string LongName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Model Business logic must go here!

            var result = new List<ValidationResult>();


            return result;
        }
        
        public Exercise ToExercise()
        {
            return new Exercise
            {
                Name = this.Name,
                ShortName = this.ShortName,
                LongName = this.LongName,
            };
        }
    }
}
