using Bingo.Repository.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bingo.Api.Models
{
    public class PostLactateResultDto : RequestObject
    {
        [Required, Range(0, double.MaxValue)]
        public double? LactateProduction { get; set; }

        [Range(0, double.MaxValue)]
        public double? AerobicRespiration { get; set; }

        [Range(0, double.MaxValue)]
        public double? AnaerobicRespiration { get; set; }

        public LactateResult ToLactateResult()
        {
            return new LactateResult
            {
                LactateProduction = LactateProduction,
                AerobicRespiration = AerobicRespiration,
                AnaerobicRespiration = AnaerobicRespiration
            };
        }
    }
}
