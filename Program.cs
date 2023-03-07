using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => //para mudar o retorno da api
    {
        options.SuppressModelStateInvalidFilter = true;
    });


builder.Services.AddDbContext<BlogDataContext>();
var app = builder.Build();

app.MapControllers();


app.Run();



// ! dotnet add package microsoft.entityframeworkcore.sqlserver
// dotnet add package microsoft.entityframeworkcore.design
