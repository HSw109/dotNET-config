using System.Net;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/poc", () => Results.Ok("solve!"));

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
