﻿using BlazorLayer.Components;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using ServiceLayer;

namespace BlazorLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddScoped<CreatleDbContext, CreatleDbContext>();

            builder.Services.AddScoped<AnswerContext, AnswerContext>();
            builder.Services.AddScoped<AnswerManager, AnswerManager>();
            builder.Services.AddScoped<CategoriesContext, CategoriesContext>();
            builder.Services.AddScoped<CategoriesManager, CategoriesManager>();
            builder.Services.AddScoped<CategoriesValuesContext, CategoriesValuesContext>();
            builder.Services.AddScoped<CategoriesValuesManager, CategoriesValuesManager>();
            builder.Services.AddScoped<GameContext, GameContext>();
            builder.Services.AddScoped<GameManager, GameManager>();
            builder.Services.AddScoped<HeroMetadataContext, HeroMetadataContext>();
            builder.Services.AddScoped<HeroMetadataManager, HeroMetadataManager>();
            builder.Services.AddScoped<HeroProfileContext, HeroProfileContext>();
            builder.Services.AddScoped<HeroProfileManager, HeroProfileManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
