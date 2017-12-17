namespace Bingo.Domain.Entities
{
    public class Muscle
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}
