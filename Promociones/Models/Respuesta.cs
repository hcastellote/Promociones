using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Respuesta<T>
    {
        [JsonProperty("resultado")]
        public int Resultado { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

    }
}
