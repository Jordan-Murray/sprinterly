using AutoMapper;
using Microsoft.OpenApi.Models;
using OpenAIPluginMiddleware;
using Sprinterly.Models.AutoMapper_Profiles;
using Sprinterly.Services;
using Sprinterly.Services.Interfaces;
//using OpenAIPluginMiddleware

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDevOpsService, DevOpsService>();
builder.Services.AddScoped<ISprintService, SprintService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IWorkItemService, WorkItemService>();

builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() 
    { 
        Title = "Sprinterly", 
        Version = "v1", 
        Description = "Sprinterly provides you with a detailed view into your agile project's progress. Track your teams, sprints, and individual contributions with ease. Get real-time insights into completed tasks, resolved bugs, and overall team velocity to keep your project on track.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddAiPluginGen(options =>
{
    options.NameForHuman = "Sprinterly";
    options.NameForModel = "sprinterly";
    options.LegalInfoUrl = "https://www.microsoft.com/en-us/legal/";
    options.ContactEmail = "jlmurray100@gmail.com";
    options.LogoUrl = "/sprinterly.png";
    options.DescriptionForHuman = "Sprinterly provides you with a detailed view into your agile project's progress.";
    options.DescriptionForModel = "Interface/Plugin for the 'Sprinterly' API, designed to track agile project metrics within an organization. Use this to extract detailed statistics on team performance, sprint progress, and individual achievements. Essential for evaluating user stories, bugs, and issues addressed within a specific timeframe.";
    options.ApiDefinition = new Api() { RelativeUrl = "/swagger/v1/swagger.yaml" };
});

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

builder.Services.AddAutoMapper(typeof(WorkItemProfile), typeof(SprintProfile), typeof(BasicMappings));

builder.Services.AddHealthChecks();

builder.Services.AddCors();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseCors(policy => policy
    .WithOrigins("https://chat.openai.com", "https://sprinterly.vercel.app")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAiPluginGen();

app.Run();
