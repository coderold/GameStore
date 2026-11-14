using GameStore.Api;
using GameStore.Api.DTOs;
using GameStore.Api.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGameEndpoint();

app.Run();
