﻿using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bingo.Api.Models
{
    public class PostExerciseDto : IValidatableObject
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(20)]
        public string ShortName { get; set; }

        [Required, MaxLength(60)]
        public string LongName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Model Business logic must go here!
            return new List<ValidationResult>();
        }
        
        public Exercise ToExercise()
        {
            return new Exercise
            {
                Name = Name,
                ShortName = ShortName,
                LongName = LongName,
            };
        }
    }
}
