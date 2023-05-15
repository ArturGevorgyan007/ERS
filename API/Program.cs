using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DataAccess;
using Services;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// AddSingleton => The same instance is shared across the entire app over the lifetime of the application
// AddScoped => The instance is created every new request
// AddTransient => The instance is created every single time it is required as a dependency 
builder.Services.AddScoped<IRepository, DBRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("ersDB")));
builder.Services.AddScoped<ERSService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello! Welcome to Employee Expenses Reimbursement System");

// Query parameters don't get defined in the route it self, but you look for it in the argument/parameter of the lambda exp that is handling this request
// app.MapGet("/greet", (string? name, string? region) => {
//         if(string.IsNullOrWhiteSpace(name)) {
//             return Results.BadRequest("Name must not be empty or white spaces");
//         }
//         else
//         {
//             return Results.Ok($"Hello {name ?? "humans"} from {region ?? "a mysterious location"}!");
//         }
//     }
// );

app.MapGet("/users", ([FromServices] ERSService service) => {
    return service.ShowUsersList();
});

app.MapGet("/user/tickets", ([FromQuery] string? username,[FromServices] ERSService service) => {
    return service.ViewTickets(service.ShowUsersList().First(x => username==x.Username));
});

app.MapPost("/register", ([FromBody] User user, ERSService service) => {
    if (service.CreateNewUser(user).Username!=null)
        return Results.Created("/register", service.CreateNewUser(user));
    else
        return Results.BadRequest("Username or password format is not valid. Username length must be at least 4 and Password length must be at least 8 and must contain 1 uppercase, 1 lowercase letter, 1 digit and 1 special character.");
});

app.MapPost("/login", ([FromBody] User user, ERSService service) => {
    return service.UserLogin(user);
});

app.MapPost("/user/newticket", ([FromQuery] string? username,[FromBody] Ticket newTicket, ERSService service) => {
    return Results.Created("/user/newticket", service.CreateNewTicket(service.ShowUsersList().First(x => username==x.Username),newTicket));
});

app.MapPost("/user/approve", ([FromQuery] int ticketID, ERSService service) => {
    return Results.Created("/user/approve", service.ApproveTicket(ticketID));
});

app.MapPost("/user/reject", ([FromQuery] int ticketID, ERSService service) => {
    return Results.Created("/user/reject", service.RejectTicket(ticketID));
});

// Route params
// app.MapGet("/greet/{name}", (string name) => $"Hello {name} from route param!");

// app.MapGet("/users", ([FromQuery] string? search, [FromServices] WorkoutService service) => {
//     if(search != null) {
//         return service.SearchWorkoutsByExercise(search);
//     }
//     return service.GetAllWorkouts();
// });

// ToDo: Fix why searching by workout doesn't work
// app.MapGet("/workouts", ([FromQuery] string? search, [FromServices] WorkoutService service) => {
//     if(search != null) {
//         return service.SearchWorkoutsByExercise(search);
//     }
//     return service.GetAllWorkouts();
// });

// app.MapPost("/workouts", ([FromBody] WorkoutSession session, WorkoutService service) => {
//     return Results.Created("/workouts", service.CreateNewSession(session));
// });

app.Run();
