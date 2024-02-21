using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSecrets.Contracts
{
    public interface IOptions
    {
        public int fetchSecrets(string file);
        public int uploadSecrets(string file);
    }
}
