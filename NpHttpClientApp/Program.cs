using System.Net.Http.Json;

HttpClient client = new();

//object? dataJson = await client.GetFromJsonAsync("https://localhost:7169/", typeof(Employee));

//if(dataJson is Employee employee)
//    Console.WriteLine($"Name: {employee.Name}, Age: {employee.Age}");

using HttpResponseMessage response = await client.GetAsync("https://localhost:7169/2");

if(response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
    response.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    ErrorMessage? error = await response.Content.ReadFromJsonAsync<ErrorMessage>();
    Console.Write($"Server response code: {response.StatusCode} ");
    Console.WriteLine($"Message: {error?.Message}");
}
else
{
    var employee = await response.Content.ReadFromJsonAsync<Employee>();
    Console.WriteLine($"Id: {employee?.Id}, Name: {employee?.Name}, Age: {employee?.Age}");
}


class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

class ErrorMessage
{
    public string? Message { get; set; }
}