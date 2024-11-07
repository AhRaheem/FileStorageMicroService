using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Commands.AddFile
{
    public record AddFileCommand(AddFileDto Model) : IRequest<string>;
}
