using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Queries.RetrieveFile
{
    public record RetrieveFileQuery(string FileId) : IRequest<byte[]>;
}
