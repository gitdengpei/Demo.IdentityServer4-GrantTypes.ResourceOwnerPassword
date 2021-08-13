using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Demo.WebAPI
{
	public class Startup
	{

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			//��֤
			services.AddAuthentication("Bearer")
				.AddJwtBearer("Bearer", options =>
				 {
					 options.Authority = "https://localhost:5001";
					 options.RequireHttpsMetadata = false;
					 options.TokenValidationParameters = new TokenValidationParameters
					 {
						 ValidateAudience = false
					 };
				 });
			//��Ȩ
			services.AddAuthorization(options =>
			{
				//1.��Ӳ�������
				options.AddPolicy("ApiScope", builder =>
				 {
					 //�ж��û��Ƿ�ͨ����֤
					 builder.RequireAuthenticatedUser();
					 builder.RequireClaim("scope", "DemoScope");
				 });
			});
		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthentication();//��֤
			app.UseAuthorization();//��Ȩ

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				//Ӧ������api������
				//.RequireAuthorization("ApiScope");
			});
		}
	}
}
