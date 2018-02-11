using BackOffice.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BackOffice
{
   
    public partial class App : Application
    {
        private static BDUsers users = new BDUsers();
        public static BDUsers Users { get => users; }

        private static BDRoles roles = new BDRoles();
        public static BDRoles Roles { get => roles; }
        private static BDCharts estatisticas = new BDCharts();
        public static BDCharts Estatisticas { get => estatisticas; }
    }
}
