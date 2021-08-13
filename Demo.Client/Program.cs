using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demo.Client
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var client = new HttpClient();
			var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
			if (disco.IsError)
			{
				Console.WriteLine(disco.Error);
				return;
			}
			var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
			{
				Address = disco.TokenEndpoint,

				ClientId = "DemoScope_client",
				ClientSecret = "secret",
				UserName = "admin",
				Password = "123"

			});

			if (tokenResponse.IsError)
			{
				Console.WriteLine(tokenResponse.Error);
				return;
			}

			Console.WriteLine(tokenResponse.Json);

			var apiClient = new HttpClient();
			apiClient.SetBearerToken(tokenResponse.AccessToken);

			var response = await apiClient.GetAsync("http://localhost:5002/api/identity");
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.StatusCode);
			}
			else
			{
				var content = await response.Content.ReadAsStringAsync();
				Console.WriteLine(JArray.Parse(content));
			}
		}
	}
}
