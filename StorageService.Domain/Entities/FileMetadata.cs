using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Domain.Entities
{
    public class FileMetadata
    {
        public string FileId { get; set; }
        public string Version { get; set; }
        public string[] Tags { get; set; } // Optional tags for categorization
        public DateTime LastUpdated { get; set; }
        public string Description { get; set; }
    }
}
