using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureSecrets.Contracts;
using AzureSecrets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSecrets.Services
{
    public class Options : IOptions
    {
        public int fetchSecrets(string file)
        {
            Vaults vaults = new JsonOps(file).convertVaultsToDTO();
            List<Secrets> secrets = new List<Secrets>();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Vault :    FetchStatus" );
            Console.WriteLine("-------------------------");

            foreach (var vault in vaults.vault)
            {
                Secrets secretList = new Secrets();

                var kvURI = $"https://{vault}.vault.azure.net/";

                var azureClient = new SecretClient(new Uri(kvURI), new AzureCliCredential());

                Azure.Pageable<SecretProperties> secretProps = azureClient.GetPropertiesOfSecrets();

                Console.Write(vault + " : ");
                try
                {
                    if(secretProps.ToList().Count == 0)
                    {
                        Console.WriteLine("No Secrets Found");
                    }
                }catch (AggregateException ex) { 
                
                    Console.WriteLine($"{vault} not found");
                    continue;
                }catch(Azure.RequestFailedException ex)
                {
                    Console.WriteLine($"Access Denied for {vault}");
                }
                secretList.vaultName = vault;
                secretList.secrets = new List<Secret>();
                foreach (var secret in secretProps.ToList()) {

                    Secret secretObj = new Secret();
                    KeyVaultSecret secretVal = azureClient.GetSecret(secret.Name);
                    secretObj.name = secretVal.Name;
                    secretObj.value = secretVal.Value;
                    secretObj.contentType = secretVal.Properties.ContentType;
                    
                    
                    secretList.secrets.Add(secretObj);

                }
                secrets.Add(secretList);
                Console.WriteLine("Done\n");

                string json = null;

                try
                {
                    json = JsonConvert.SerializeObject(secrets.ToArray());
                }catch(Exception ex)
                {
                    Console.WriteLine("Error Pasing the Secrets object");
                    Environment.Exit(1);
                }

                try
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + "/" + "Secrets.json",json);
                }catch(Exception e)
                {
                    Console.WriteLine("Error creating the secrets file");
                    Environment.Exit(1);
                }
                Console.WriteLine("\nSecrets.json file is created in current dirrctory");
            }
            return 2;
           
        }

        public int uploadSecrets(string file)
        {
            List<Secrets> secrets = new JsonOps(file).convertSecretsToDTO();
            foreach(Secrets secret in secrets)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Vault :    Upload Status");
                Console.WriteLine("--------------------");

                Console.Write(secret.vaultName + " : ");

                var kvURI = $"https://{secret.vaultName}.vault.azure.net/";
                var azureClient = new SecretClient(new Uri(kvURI), new AzureCliCredential());

                try
                {
                    foreach(Secret secretObj in secret.secrets)
                    {
                        KeyVaultSecret secretVal = new KeyVaultSecret(secretObj.name,secretObj.value);
                        secretVal.Properties.ContentType = secretObj.contentType;
                        azureClient.SetSecret(secretVal);
                    }
                }catch(AggregateException ex) {
                    Console.WriteLine(" No Such Vault Found");
                    continue;
                }catch(Azure.RequestFailedException ex)
                {
                    Console.WriteLine("Access Denied fro the Vault");
                    continue;
                }
                Console.WriteLine("\nSecrets are uploaded");
            }
            return 1;
        }
    }
}
