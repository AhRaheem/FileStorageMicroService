using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Domain.Repositories
{
    public interface IFileMetadataRepository
    {
        Task<FileMetadata> AddFileMetadataAsync(FileMetadata metadata);
        Task<FileMetadata> GetFileMetadataByIdAsync(string id);
        Task<bool> DeleteFileMetadataAsync(string id);
        Task<IEnumerable<FileMetadata>> GetFilesByOwnerAsync(string ownerId);
    }
}
