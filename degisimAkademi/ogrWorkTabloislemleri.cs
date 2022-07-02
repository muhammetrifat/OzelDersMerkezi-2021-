using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace degisimAkademi
{
    class ogrWorkTabloislemleri//YAPILAN ÖDEVLERİN GİRİLMESİ
    {
        int turkce;
        int paragraf;
        int sosyal;
        int cografya;
        int matematik;
        int fen;
        int kimya;
        int biyoloji;
        int dinkulturu;
        int ingilizce;
        int diger;
        int deneme;
        int toplam;
        int totalintlimit;
        bool checkboxx;


        programLog prlg;
        public ogrWorkTabloislemleri(int tr, int prg, int sy, int cg, int mat, int fn, int kim, int biy, int dn, int ing, int dg, int den, bool ch, int totinlim)
        {
            turkce = tr;
            paragraf = prg;
            sosyal = sy;
            cografya = cg;
            matematik = mat;
            fen = fn;
            kimya = kim;
            biyoloji = biy;
            dinkulturu = dn;
            ingilizce = ing;
            diger = dg;
            deneme = den;
            checkboxx = ch;
            toplam = tr + prg + sy + cg + mat + fn + kim + biy + dn + ing + dg + den;
            totalintlimit = totinlim;
        }

        public void databaseinsert()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand("Insert Into ogrWork(ogrId,turkce,paragraf,sosyal,cografya,matematik,fen,kimya,biyoloji, dinkulturu, ingilizce, diger, deneme, total, totalint, totalintlimit,status, insertDate, userId,editDate) Values (" +
                    "@ogrId, @turkce, @paragraf, @sosyal,@cografya,@matematik,@fen,@kimya,@biyoloji,@dinkulturu,@ingilizce,@diger,@deneme, @total,@totalint,@totalintlimit,@status,@insertDate, @userId, @editDate)", con);

            command.Parameters.AddWithValue("@ogrId", homework.ogridh);
            command.Parameters.AddWithValue("@turkce", turkce);
            command.Parameters.AddWithValue("@paragraf", paragraf);
            command.Parameters.AddWithValue("@sosyal", sosyal);
            command.Parameters.AddWithValue("@cografya", cografya);
            command.Parameters.AddWithValue("@matematik", matematik);
            command.Parameters.AddWithValue("@fen", fen);
            command.Parameters.AddWithValue("@kimya", kimya);
            command.Parameters.AddWithValue("@biyoloji", biyoloji);
            command.Parameters.AddWithValue("@dinkulturu", dinkulturu);
            command.Parameters.AddWithValue("@ingilizce", ingilizce);
            command.Parameters.AddWithValue("@diger", diger);
            command.Parameters.AddWithValue("@deneme", deneme);

            if (checkboxx == true)
            {
                command.Parameters.AddWithValue("@total", 1);
            }
            else
            {
                command.Parameters.AddWithValue("@total", 0);
            }

            command.Parameters.AddWithValue("@totalint", toplam);

            command.Parameters.AddWithValue("@totalintlimit", totalintlimit);
            command.Parameters.AddWithValue("@status", 1);
            command.Parameters.AddWithValue("@insertDate", Convert.ToDateTime(homework.tarihh));
            command.Parameters.AddWithValue("@userId", login.userid);
            command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, "OGRWORKTABLOİSLEMLERİCLASS - INSERT", "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();

            }
            con.Close();
        }

        public void databaseEdit()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand("update ogrWork set turkce=@turkce,paragraf=@paragraf, sosyal=@sosyal,cografya=@cografya,matematik=@matematik," +
                        "fen=@fen, kimya=@kimya,biyoloji=@biyoloji, dinkulturu=@dinkulturu,ingilizce=@ingilizce,diger=@diger,deneme=@deneme, total=@total, totalint=@totalint," +
                        "userId=@userId,editDate=@editDate where ogrId = '" + homework.ogridh + "' and convert(varchar, insertDate, 104) = '" + homework.tarihh + "'", con);

            command.Parameters.AddWithValue("@turkce", turkce);
            command.Parameters.AddWithValue("@paragraf", paragraf);
            command.Parameters.AddWithValue("@sosyal", sosyal);
            command.Parameters.AddWithValue("@cografya", cografya);
            command.Parameters.AddWithValue("@matematik", matematik);
            command.Parameters.AddWithValue("@fen", fen);
            command.Parameters.AddWithValue("@kimya", kimya);
            command.Parameters.AddWithValue("@biyoloji", biyoloji);
            command.Parameters.AddWithValue("@dinkulturu", dinkulturu);
            command.Parameters.AddWithValue("@ingilizce", ingilizce);
            command.Parameters.AddWithValue("@diger", diger);
            command.Parameters.AddWithValue("@deneme", deneme);

            if (checkboxx == true)
            {
                command.Parameters.AddWithValue("@total", 1);
            }
            else
            {
                command.Parameters.AddWithValue("@total", 0);
            }
            command.Parameters.AddWithValue("@totalint", toplam);
            command.Parameters.AddWithValue("@userId", login.userid);
            command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            try
            {
                command.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, "OGRWORKTABLOİSLEMLERİCLASS - EDIT", "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();

            }
            con.Close();
        }
    }
}
