using CashFlow.API.CultureMiddleware;
using CashFlow.API.Filters;
using CashFlow.Application;
using CashFlow.Infraestructure;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Configuration.GetConnectionString("Connection"); //recupera connection string do appsetings

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

//resumido de DependencyInjectionExtension.AddInfraestructure(builder.Services); ao utilizar THIS em DependencyINjection
// this deixa implicito o valor de IServiceCollection
builder.Services.AddInfraestructure(builder.Configuration); 
builder.Services.AddAplication(); 



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
