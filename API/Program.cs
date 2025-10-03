using System.Net.Mime;
using System.Text.Json.Serialization;
using API.Transformers;
using Application.Common.Interfaces;
using Infrastructure.Helper;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
            builder.Host.UseSerilog(logger);

            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inforce.API", Version = "v1" }));

            builder.Services.Configure<ConnectionOptions>(
                builder.Configuration.GetSection(ConnectionOptions.SectionName));
            builder.Services.Configure<TokenOptions>(
                builder.Configuration.GetSection(TokenOptions.SectionName));

            builder.Services.AddDbContext<DatabaseContext>(opts =>
                opts.UseNpgsql(
                    builder.Configuration.GetConnectionString("ApiDatabase")
                )
            );

            builder.Services.AddCors(o => o.AddPolicy("AllowAny", corsPolicyBuilder =>
            {
                corsPolicyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            }));

            // Add services to the container.

            builder.Services.AddScoped<IAuth, AuthService>();
            builder.Services.AddTransient<Hashing>();
            builder.Services.AddTransient<TokenManipulation>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer()));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAny");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
