using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepositoriesContext>(options => // Lambda fonksiyonu yardımıyla bir options ifadesi girildi ve bu options aracılığıyla bağlantı dizesi getir
options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")) // builder ile appsettings.json daki sqlConnection ulaşıyoruz. 
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("Umut");

app.Run();

Console.WriteLine("betul");