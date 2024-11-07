using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Dtos
{
    public class AddFileDto
    {
        public byte[] Content { get; set; } // File content in binary format
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string UploadedBy { get; set; }
    }
}
