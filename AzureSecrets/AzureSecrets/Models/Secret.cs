using Azure.Security.KeyVault.Secrets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureSecrets.Models
{
    [Serializable]
    public class Secret
    {
       [JsonProperty("Name")]
       public string name{  get; set; }
        [JsonProperty("Value")]
        public string value { get; set; }
        [JsonProperty("ContentType")]
        public string contentType { get; set; }

    }
}
