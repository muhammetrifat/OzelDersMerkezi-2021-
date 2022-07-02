using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace degisimAkademi
{
    class BaglanClass//HOME FORMDAKİ BACKUP AYARI VE BURDAKİ CONNECTİON STRİNGİ DÜZENLEYEREK SETUP YAP
    {
        //public static string connectionstring = @"Data Source = DESKTOP-H56L443\SQLEXPRESS ; Initial Catalog = DegisimAkademi ; User ID = sa; Password = 1234";

        public static string connectionstring = File.ReadAllText(@"C:\connection.txt");
    }
}