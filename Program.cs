
using EntityFrameworkTest.Data;
using EntityFrameworkTest.Services;

//DbContextHelper.SeedData();

var _db = DbContextHelper.InitialDatabase();

var documentId = UpdateChangeTrackerTester.AddData(_db);

var _db_2 = DbContextHelper.InitialDatabase();

UpdateChangeTrackerTester.UpdateCommandChangeTrackerWithRandomKey(_db_2);

Console.ReadKey();
