// See https://aka.ms/new-console-template for more information
using BooksClient;

Console.WriteLine("Hello, World!");


var http = new HttpClient();
var json = await http.GetStringAsync("https://www.googleapis.com/books/v1/volumes?q=WCF");

Books books = System.Text.Json.JsonSerializer.Deserialize<Books>(json);

foreach (var b in books.items.Select(x => x.volumeInfo))
{
    Console.WriteLine($"{b.title}");
}