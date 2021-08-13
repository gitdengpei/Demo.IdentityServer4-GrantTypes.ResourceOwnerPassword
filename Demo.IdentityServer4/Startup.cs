using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.IdentityServer4
{
	public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer();
            builder.AddDeveloperSigningCredential();//���ɵ���Կ�������浽�ļ�ϵͳ���Ա��ڷ�������������֮�䱣���ȶ�������ͨ������������false��
            builder.AddInMemoryClients(Config.Clients);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryApiResources(Config.ApiResources);
            builder.AddTestUsers(Config.TestUsers);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseIdentityServer();
        }
    }
}
