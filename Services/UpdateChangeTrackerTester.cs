using EntityFrameworkTest.Data;
using EntityFrameworkTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTest.Services
{
    internal class UpdateChangeTrackerTester
    {
        /// <summary>
        /// yangi entity qo'shilganidan so'ng savechanges() methodidan so'ng EF entityni track qilib qo'yadi
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Guid AddData(SimpleDbContext db)
        {
            var document = new Document
            {
                DocumentId = Guid.NewGuid(),
                Content = "entity frame work update tester",
                DocumentViewId = Guid.NewGuid(),
                JournalId = Guid.NewGuid(),
                RegDate = DateTime.UtcNow,
                RegNumber = "RT-12A"
            };
            db.Documents.Add(document);
            db.SaveChanges();
            return document.DocumentId;
        }

        /// <summary>
        /// odatiy update comandasiga bitta maydonni o'zgaritrsak ham sql so'rovda barcha maydonlarni yangilaydi
        /// </summary>
        /// <param name="db"></param>
        /// <param name="documentId"></param>
        public static void SimpleUpdate(SimpleDbContext db, Guid documentId)
        {
            var document = db.Documents.FirstOrDefault(l => l.DocumentId == documentId);
            document.RegNumber = "56-FD-89";
            db.Documents.Update(document);
            db.SaveChanges();
        }

        /// <summary>
        /// Update comandasiz savechanges ni chaqirsak faqat shu maydonni yangilaydi
        /// </summary>
        /// <param name="db"></param>
        /// <param name="documentId"></param>
        public static void UpdateWithOutUpdateCommand(SimpleDbContext db, Guid documentId)
        {
            var document = db.Documents.FirstOrDefault(l => l.DocumentId == documentId);
            document.RegNumber = "WR-48";
            db.SaveChanges();
        }

        /// <summary>
        /// Update comandasini ichida ham track qilinmagan bo'lsa avtomatik track qiladi
        /// </summary>
        /// <param name="db"></param>
        /// <param name="documentId"></param>
        public static void UpdateCommandWithAsNotracking(SimpleDbContext db, Guid documentId)
        {
            var document = db.Documents.AsNoTracking().FirstOrDefault(l => l.DocumentId == documentId);
            document.RegNumber = "SU-35C";
            db.Documents.Update(document);
            db.SaveChanges();
        }

        /// <summary>
        /// bu usulda malumotlar bazasidan ma'lumotlarni olib kelmasdan ma'lumotlarni yangilashni ko'rishimiz mumkin
        /// </summary>
        /// <param name="db"></param>
        /// <param name="documentId"></param>
        public static void UpdateCommandChangeTracker(SimpleDbContext db, Guid documentId)
        {
            var document = new Document { DocumentId = documentId, RegNumber = "MIG-35" };
            db.Documents.Attach(document);
            db.Entry(document).Property(l => l.RegNumber).IsModified = true;
            db.SaveChanges();
        }

        /// <summary>
        /// Agar boshqa Id berib yuborsak Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException qaytaradi
        /// </summary>
        /// <param name="db"></param>
        public static void UpdateCommandChangeTrackerWithRandomKey(SimpleDbContext db)
        {
            try
            {
                Guid documentId = Guid.NewGuid();
                var document = new Document { DocumentId = documentId, RegNumber = "TU-160" };
                db.Documents.Attach(document);
                db.Entry(document).Property(l => l.RegNumber).IsModified = true;
                db.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Exceptionni ushladik");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
