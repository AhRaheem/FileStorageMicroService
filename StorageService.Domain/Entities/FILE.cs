using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Domain.Entities
{
    public class FILE
    {
        public string Id { get; set; } // Unique identifier
        public string Path { get; set; } // Physical or cloud path to the file
        public string FileName { get; set; }
        public string ContentType { get; set; } // File type (e.g., image/jpeg)
        public long Size { get; set; } // File size in bytes
        public DateTime UploadedAt { get; set; }
        public string UploadedBy { get; set; } // ID of the user/service who uploaded
    }
}
