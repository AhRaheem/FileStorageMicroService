﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Events
{
    public class FileDeletedEvent
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
    }
}