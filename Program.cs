
var builder = WebApplication.CreateBuilder(args);

// 1. 기존 AddOpenApi 대신(또는 함께) Swagger 서비스 추가
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // 이 줄이 Swagger를 만듭니다!

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
// 2. Swagger UI 활성화
app.UseSwagger();
app.UseSwaggerUI(); // 이게 있어야 브라우저에서 화면이 나옵니다!

app.MapOpenApi(); // 이건 기본 OpenAPI 데이터용
// }

app.UseHttpsRedirection();

var summaries = new[]
{
"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
var forecast = Enumerable.Range(1, 5).Select(index =>
new WeatherForecast
(
DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
Random.Shared.Next(-20, 55),
summaries[Random.Shared.Next(summaries.Length)]
))
.ToArray();
return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
