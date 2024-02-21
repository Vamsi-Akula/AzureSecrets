using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
namespace AzureSecrets.Models
{
    [Serializable]
    public class Vaults
    {
        [JsonProperty("Vaults")]
        public List<String> vault {  get; set; }
    }
}
