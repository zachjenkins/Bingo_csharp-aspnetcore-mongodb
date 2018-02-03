using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Bingo.Repository
{
    public static class Support
    {
        public static bool DeepEquals(this object actual, object expected)
        {
            CompareLogic compareLogic = new CompareLogic
            {
                Config =
                {
                    AttributesToIgnore = new List<Type>
                    {
                        typeof(JsonIgnoreAttribute),
                        typeof(MongoDB.Bson.Serialization.Attributes.BsonIdAttribute)
                    }
                }
            };
            return compareLogic.Compare(actual, expected).AreEqual;
        }
    }
}
