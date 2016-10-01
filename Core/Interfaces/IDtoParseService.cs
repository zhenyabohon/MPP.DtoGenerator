﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDtoParseService
    {
        List<DtoClassModel> GetDtoClassModels(string path);
    }
}
