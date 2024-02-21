using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
namespace AzureSecrets.Models
{
    [Serializable]
    public class Secrets
    {
        [JsonProperty("VaultName")]
        public string vaultName {  get; set; }
        [JsonProperty("Secrets")]
        public List<Secret> secrets { get; set;}
    }
}
