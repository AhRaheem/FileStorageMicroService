using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Interfaces
{
    public interface IFileRepository
    {
        Task SaveFileAsync(FILE file, byte[] content);
        Task<byte[]> RetrieveFileAsync(string fileId);
        Task<bool> DeleteFileAsync(string fileId);
    }
}
