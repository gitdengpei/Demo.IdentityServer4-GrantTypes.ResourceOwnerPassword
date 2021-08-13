using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Demo.IdentityServer4
{
	public static class Config
	{
		/// <summary>
		/// api资源列表
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
		{
			new  ApiResource("DemoScope", "DemoScope Service")
		};
		/// <summary>
		/// 4.0版本需要添加apiscope
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
		{
			new ApiScope("DemoScope","DemoScope")
		};
		public static IEnumerable<Client> Clients => new Client[]
		{
			new Client
			{
				ClientId = "DemoScope_client",
				ClientName = "DemoScope_Client",
				ClientSecrets = { new Secret("secret".Sha256()) },
				AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
				AllowedScopes = {
					"DemoScope"
				}
			}
		};
		public static List<TestUser> TestUsers => new List<TestUser>()
		{
			new TestUser
			{
				SubjectId = "1",
				Username = "admin",
				Password = "123"
			}
		};
	}
}
