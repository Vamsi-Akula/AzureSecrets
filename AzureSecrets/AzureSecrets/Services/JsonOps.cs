using AzureSecrets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSecrets.Services
{
    public  class JsonOps
    {
        public string file;
        public JsonOps(string file)
        {
            this.file = file;   
        }

        public Vaults convertVaultsToDTO()
        {
            StreamReader sr = new StreamReader(this.file);

            Vaults result = new Vaults(); 
            result = JsonConvert.DeserializeObject<Vaults>(sr.ReadToEnd());
            return result;
        }
        public List<Secrets> convertSecretsToDTO()
        {
            StreamReader sr = new StreamReader(this.file);

            List<Secrets> result = new List<Secrets>();
            result = JsonConvert.DeserializeObject<List<Secrets>>(sr.ReadToEnd());
            return result;
        }
    }
}
