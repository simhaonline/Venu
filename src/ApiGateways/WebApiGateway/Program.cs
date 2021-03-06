using System.Text;
using GrpcJsonTranscoder;
using GrpcJsonTranscoder.Grpc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Ticketing.API;

namespace Venu.ApiGateways.WebApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
 
        public static IWebHost BuildWebHost(string[] args)
        {
            var key = Encoding.ASCII.GetBytes("THIS_IS_A_RANDOM_SECRET_e03a00d0-5881-4267-9d41-fd948e04a35a");
            
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:80")
                .UseKestrel()
                .ConfigureAppConfiguration(ic =>
                {
                    ic.AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile("ocelot.json");
                })
                .ConfigureServices(s =>
                {
                    s.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy",
                            builder => builder.WithOrigins("http://localhost:3000")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials());
                    });
                    
                    s.AddGrpcJsonTranscoder(() => 
                        new GrpcAssemblyResolver().ConfigGrpcAssembly(
                            s.BuildServiceProvider().GetService<ILogger<GrpcAssemblyResolver>>(),
                            typeof(TicketingGrpc.TicketingGrpcBase).Assembly));
                    
                    s.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer("IdentityApiKey", x =>
                        {
                            x.RequireHttpsMetadata = false;
                            x.SaveToken = true;
                            x.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(key),
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                        });
                    s.AddOcelot();
                })
                .Configure(a =>
                {
                    var configuration = new OcelotPipelineConfiguration
                    {
                        PreQueryStringBuilderMiddleware = async (ctx, next) =>
                        {
                            await ctx.HandleGrpcRequestAsync(next);
                        }
                    };
                    
                    a.UseAuthentication();
                    a.UseCors("CorsPolicy");
                    a.UseOcelot(configuration).Wait();
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {Properties:j}] {Message:lj}{NewLine}{Exception}");
                })
                .Build();
        }
    }
}