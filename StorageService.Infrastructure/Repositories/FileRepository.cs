using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly MongoDbContext _context;

        public FileRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task SaveFileAsync(FILE file, byte[] content)
        {
            await _context.Files.InsertOneAsync(file);

            var filePath = Path.Combine("storage", file.Id);
            await File.WriteAllBytesAsync(filePath, content);
        }

        public async Task<byte[]> RetrieveFileAsync(string fileId)
        {
            var file = await _context.Files.Find(f => f.Id == fileId).FirstOrDefaultAsync();
            if (file == null) return null;

            var filePath = Path.Combine("storage", file.Id);
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            // Delete metadata from MongoDB
            var deleteResult = await _context.Files.DeleteOneAsync(f => f.Id == fileId);
            if (deleteResult.DeletedCount == 0)
            {
                return false; // File not found
            }

            // Delete the file from physical storage or cloud storage
            var filePath = Path.Combine("storage", fileId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return true;
        }
    }
}
