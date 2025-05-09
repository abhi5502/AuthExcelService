﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExcelService.Repositoies.IGenericRepository
{
    public interface ICustomDbContextFactory
    {
        DbContext GetDbContextByCountry(string country);
    }
}
