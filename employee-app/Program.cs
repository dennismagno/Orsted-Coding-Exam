using Microsoft.AspNetCore.SpaServices.AngularCli;
using Employee_App.FileProcessor;
using Employee_App.FileUpload;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
                    
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IFileProcessorFactory, FileProcessorFactory>();
builder.Services.AddTransient<IFileUploadService, FileUploadService>();

builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
    {
        app.UseSpaStaticFiles();
    }
app.UseRouting();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.UseSpa(spa =>
    {
        spa.Options.SourcePath = "ClientApp";

        if (app.Environment.IsDevelopment())
        {
            spa.UseAngularCliServer(npmScript: "start");
        }
    });

app.Run();
