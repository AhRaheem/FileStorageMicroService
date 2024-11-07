using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Commands.DeleteFile
{
    public record DeleteFileCommand(string FileId) : IRequest<bool>;
}
