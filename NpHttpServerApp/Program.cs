var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Employee bob = new() { Name = "Bobby", Age = 29 };
List<Employee> employees = new List<Employee>()
{
    new() { Id = 1, Name = "Bobby", Age = 29 },
    new() { Id = 2, Name = "Sammy", Age = 29 },
    new() { Id = 3, Name = "Jimmy", Age = 29 },
};

app.MapGet("/{id?}", (int? id) =>
{
    if (id is null)
        return Results.BadRequest(new { Message = "Bad Request"});
    else
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee is null)
            return Results.NotFound(new { Message = "Employee not found" });
        else
            return Results.Json(employee);
    }
});

app.Run();


class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
