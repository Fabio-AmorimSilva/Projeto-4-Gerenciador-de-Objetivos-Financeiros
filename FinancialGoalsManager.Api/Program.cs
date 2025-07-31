var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApi();

builder.Services.AddAuthorization();

var app = builder.Build();

UserEndpoints.Map(app);
FinancialGoalEndpoints.Map(app);
TransactionEndpoints.Map(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.ConfigureEventBusHandlers();

app.UseHttpsRedirection();

app.Run();