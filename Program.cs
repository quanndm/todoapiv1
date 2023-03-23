using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add db context
builder.Services.AddDbContext<MyContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.

    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/api/todos");
    }
    else{
        await next.Invoke();
    }
    
    // Do logging or other work that doesn't write to the Response.
});
app.Run();
