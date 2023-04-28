using StocksAppAssignment.UI.Middleware;

using Serilog;
using StocksAppAssignment.UI.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

//Startup Extensions
builder.Services.ConfigureServicesExtension(builder.Configuration);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
        .ReadFrom.Services(serviceProvider); //read out current app's services and make them available to Serilog
});

//Logging
builder.Host.ConfigureLogging(logger =>
{
    logger.ClearProviders();
    logger.AddConsole();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
});

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}

if(builder.Environment.IsEnvironment("Test") == false)
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseCors();

app.UseHsts();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
