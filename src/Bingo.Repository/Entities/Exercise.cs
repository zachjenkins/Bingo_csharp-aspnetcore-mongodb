﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bingo.Repository.Entities
{
    public class Exercise : BingoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public ExerciseTypeEnum ExerciseType { get; set; }
    }

    public enum ExerciseTypeEnum
    {
        Strength,
        Compound,
        Simple,
        Cardio,
        Athletic,
        Plyometric
    }
}
