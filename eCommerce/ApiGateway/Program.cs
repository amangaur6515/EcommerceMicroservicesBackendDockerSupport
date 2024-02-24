using Ocelot.Configuration.Creator;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

var builder = WebApplication.CreateBuilder(args);
// Configure Eureka URL
var eurekaUrl = "http://eureka-service:8761/eureka/"; // Assuming the Eureka service is accessible via the service name 'eureka-service'
builder.Configuration["Eureka:Client:ServiceUrl"] = eurekaUrl;
var routes = "Routes.dev";
//#if DEBUG
//routes = "Routes.dev";
//#else
//routes = "Routes.prod";
//#endif
//;
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();


builder.Services.AddOcelot(builder.Configuration).AddEureka().AddPolly();

builder.Services.AddServiceDiscovery(o => o.UseEureka());

var app = builder.Build();

await app.UseOcelot();



app.Run();
