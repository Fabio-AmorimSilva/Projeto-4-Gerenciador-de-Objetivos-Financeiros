var builder = WebApplication.CreateBuilder(args);

builder
    .AddUserProvider()
    .AddRabbitMq(builder.Configuration);

builder.Services
    .AddOpenApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi()
    .AddAuthorization()
    .AddAuthentication();

var app = builder.Build();

UserEndpoints.Map(app);
FinancialGoalEndpoints.Map(app);
TransactionEndpoints.Map(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.UseAuthorization();

app.ConfigureEventBusHandlers();

app.UseHttpsRedirection();

app.Run();