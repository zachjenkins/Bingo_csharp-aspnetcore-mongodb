﻿namespace Bingo.Repository.Entities
{
    public class Activation
    {
        public ActivationType Type { get; set; }
        public int RefId { get; set; }
        public double Value { get; set; }
    }
}