using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/poc", () => Results.Ok("solve!"));

app.MapPost("/poc", async (HttpContext ctx) =>
{
    string body;
    var reader = new StreamReader(ctx.Request.Body);
    body = await reader.ReadToEndAsync();
    Console.WriteLine(body);
    return Results.Ok(body);
});

app.MapGet("/admin", (HttpContext ctx) =>
{
    var rip = ctx.Connection.RemoteIpAddress;
    Console.WriteLine(rip);
    var isLocal = rip != null && IPAddress.IsLoopback(rip);
    var fromTrustedProxy = rip != null && rip.Equals(IPAddress.Parse("192.168.1.10")); 

    if (!(isLocal || fromTrustedProxy)) return Results.Unauthorized();

    return Results.Ok("admin ok");
});

app.Run();
