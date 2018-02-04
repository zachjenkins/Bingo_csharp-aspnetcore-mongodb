using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bingo.Api.Models
{
    public abstract class RequestObject
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
