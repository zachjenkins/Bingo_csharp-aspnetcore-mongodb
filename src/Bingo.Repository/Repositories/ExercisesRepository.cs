using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Bingo.Repository.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        // This is all going to change so we're just gonna store the data in-memory until I get mongo up and going.
     
        public Exercise SelectExerciseById(string id)
        {
            return _data.FirstOrDefault(exercise => exercise.Id == id);
        }

        public IEnumerable<Exercise> SelectAllExercises()
        {
            return _data;
        }

        // Be aware that this is not close the the actual mongo implementation
        public Exercise InsertExercise(Exercise exercise)
        {
            exercise.Id = (_data.Count + 1).ToString();

            _data.Add(exercise);

            return exercise;
        }

        public bool DeleteExercise(string id)
        {
            return (_data.RemoveAll(ex => ex.Id == id) > 0);
        }

        private static readonly List<Exercise> _data = new List<Exercise>
        {
            new Exercise
            {
                Id = "1",
                Name = "Bench Press",
                ShortName = "Bench",
                LongName = "Flat Barbell Bench Press",
                Activations = new List<Activation>
                {
                    new Activation
                    {
                        Type = ActivationType.Group,
                        RefId = 1,
                        Value = 74.13
                    },
                    new Activation
                    {
                        Type = ActivationType.Muscle,
                        RefId = 2,
                        Value = 78.31
                    }
                }
            },
            new Exercise
            {
                Id = "2",
                Name = "Barbell Squat",
                ShortName = "Squat",
                LongName = "Barbell Back Squat"
            },
            new Exercise
            {
                Id = "3",
                Name = "Barbell Deadlift",
                ShortName = "Deadlift",
                LongName = "Conventional Barbell Deadlift"
            }
        };
    }
}
