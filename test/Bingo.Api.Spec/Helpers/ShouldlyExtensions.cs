using MongoDB.Driver;
using Shouldly;
using System.Collections.Generic;

namespace Bingo.Specification.Helpers
{
    public static class ShouldlyExtensions
    {
        private static List<T> SearchAll<T>(IMongoCollection<T> collection)
        {
            var filter = FilterDefinition<T>.Empty;
            return collection.FindAsync<T>(filter).Result.ToList();
        }

        public static void ShouldContain<T>(this IMongoCollection<T> collection, T expected)
        {
            var actual = SearchAll(collection);

            actual.ShouldContain(expected);
        }

        public static void ShouldNotContain<T>(this IMongoCollection<T> collection, T expected)
        {
            var actual = SearchAll(collection);

            actual.ShouldNotContain(expected);
        }

        public static void ShouldBeEmpty<T>(this IMongoCollection<T> collection)
        {
            var actual = SearchAll(collection);

            actual.Count.ShouldBe(0);
        }

        public static void ShouldNotBeEmpty<T>(this IMongoCollection<T> collection)
        {
            var actual = SearchAll(collection);

            actual.Count.ShouldBeGreaterThan(0);
        }
    }
}
