using EntityFrameworkTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTest.Data
{
    internal class DbContextHelper
    {
        public static SimpleDbContext InitialDatabase()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<SimpleDbContext>();
            return new SimpleDbContext(dbContextOptionBuilder.Options);
        }

        public static void SeedData()
        {
            var _db = InitialDatabase();

            var document = new Document
            {
                DocumentId = Guid.NewGuid(),
                Content = "this is a book",
                DocumentViewId = Guid.NewGuid(),
                JournalId = Guid.NewGuid(),
                RegDate = DateTime.UtcNow,
                RegNumber = "AV-485"
            };
            _db.Documents.Add(document);

            var message_1 = new Message
            {
                MessageId = Guid.NewGuid(),
                DeadLine = DateTime.UtcNow.AddDays(10),
                Note = "test uchun",
                DocumentId = document.DocumentId
            };
            _db.Messages.Add(message_1);

            var message_2 = new Message
            {
                MessageId = Guid.NewGuid(),
                DocumentId = document.DocumentId,
                Note = "bir kun",
                DeadLine = DateTime.UtcNow.AddDays(11),
            };
            _db.Messages.Add(message_2);

            var member_1 = new Member
            {
                MemberId = Guid.NewGuid(),
                MessageId = message_1.MessageId,
                DocumentId = document.DocumentId,
                StaffId = Guid.NewGuid(),
                FullName = "Abdusattarov Jamoliddin Bahtiyor o'g'li"
            };
            _db.Members.Add(member_1);

            var member_2 = new Member
            {
                MemberId = Guid.NewGuid(),
                MessageId = message_2.MessageId,
                DocumentId = document.DocumentId,
                StaffId = Guid.NewGuid(),
                FullName = "Qodirov Anvar Hoshim o'g'li"
            };
            _db.Members.Add(member_2);

            _db.SaveChanges();

            Console.WriteLine("SeedData Hosil qilindi !!!");
        }
    }
}
