﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSTI
{
    interface ISluzba
    {
        Task<List<File>> VratSouboryCommituPoCaseAsync(DateTime cas);
        Task<Decimal> VratPrehledRadkuJazykuRepozitareAsync(string typ);
        Task<List<StatistikaSouboru>> VratStatistikuZmenyRadkuSouboruAsync(string cesta);
    }
}