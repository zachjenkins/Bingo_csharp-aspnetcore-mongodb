using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Bingo.Api.Models
{
    public class PostRepetitionTempoDto
    {
        [Required]
        public string Type { get; set; }

        [Range(0, double.MaxValue)]
        public double? Duration { get; set; }

        [Range(0, double.MaxValue)]
        public double? ConcentricDuration { get; set; }

        [Range(0, double.MaxValue)]
        public double? EccentricDuration { get; set; }

        [Range(0, double.MaxValue)]
        public double? IsometricDuration { get; set; }

        public RepetitionTempo ToRepetitionTempo()
        {
            return new RepetitionTempo
            {
                Type = Type,
                Duration = Duration,
                ConcentricDuration = ConcentricDuration,
                EccentricDuration = EccentricDuration,
                IsometricDuration = IsometricDuration
            };
        }
    }
}
