using Bingo.Api.Models;
using Bingo.Repository.Entities;
using System.Collections.Generic;

namespace Bingo.Specification.TestData
{
    public static class Exercises
    {
        public static Exercise ContractExercise => new Exercise
        {
            Id = "012345678901234567894542",
            Name = "Overhead Press",
            LongName = "Overhead Barbell Press",
            ShortName = "Press"
        };

        public static IEnumerable<Exercise> ContractExercises => new List<Exercise>
        {
            ContractExercise,
            new Exercise
            {
                Id = "012345678901234567894578",
                Name = "Barbell Curls",
                LongName = "EZ Bar Curls",
                ShortName = "Curls"
            }
        };

        public static PostExerciseDto ContractExercisePostDto => new PostExerciseDto
        {
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Tricep Extensions"
        };

        public static Exercise ContractExercisePostDtoResponse => new Exercise
        {
            Id = "123456789012345678904578",
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Tricep Extensions"
        };

        public static Exercise ExerciseWithoutId => new Exercise
        {
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Tricep Extensions"
        };
    }
}
