
using EntityFrameworkTest.Data;
using Microsoft.EntityFrameworkCore;

DbContextHelper.SeedData();

var _db = DbContextHelper.InitialDatabase();

var query = _db.Documents.Where(d => d.RegNumber == "Test").ToQueryString();

Console.WriteLine(query);

Console.ReadKey();
