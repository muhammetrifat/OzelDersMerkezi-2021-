using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace degisimAkademi
{
    class homeworkTabloislemleri//ÖDEV ATAMASI
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


        programLog prlg;
        public homeworkTabloislemleri(int tr, int prg, int sy, int cg, int mat, int fn, int kim, int biy, int dn, int ing, int dg, int den)
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
            toplam = tr + prg + sy + cg + mat + fn + kim + biy + dn + ing + dg + den;
        }

        public void databaseinsert()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand("Insert Into homework(ogrId,turkce,paragraf,sosyal, cografya,matematik,fen,kimya,biyoloji, dinkulturu, ingilizce,diger,deneme, toplam, insertDate, userId,editDate) Values (" +
                    "@ogrId, @turkce,@paragraf, @sosyal,@cografya,@matematik,@fen,@kimya,@biyoloji,@dinkulturu,@ingilizce,@diger,@deneme, @toplam,@insertDate, @userId, @editDate)", con);

            command.Parameters.AddWithValue("@ogrId", homework.ogrid);
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
            command.Parameters.AddWithValue("@toplam", toplam);
            command.Parameters.AddWithValue("@insertDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@userId", login.userid);
            command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, "HOMEWORKTABLOİSLEMLERİCLASS-INSERT", "PRLG1");//PROGRAMLOG
                prlg.databaseinsert();
            }
            con.Close();
        }
        public void databaseEdit()
        {
            SqlConnection con = new SqlConnection(BaglanClass.connectionstring);
            SqlCommand command = new SqlCommand("update homework set turkce=@turkce, paragraf=@paragraf, sosyal=@sosyal, cografya=@cografya,matematik=@matematik," +
                        "fen=@fen, kimya=@kimya, biyoloji=@biyoloji, dinkulturu=@dinkulturu,ingilizce=@ingilizce, diger=@diger,deneme=@deneme, toplam=@toplam," +
                        "userId=@userId,editDate=@editDate where ogrId = '" + homework.ogrid + "'", con);

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
            command.Parameters.AddWithValue("@toplam", toplam);
            command.Parameters.AddWithValue("@userId", login.userid);
            command.Parameters.AddWithValue("@editDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            con.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                prlg = new programLog(ex.Message, "HOMEWORKTABLOİSLEMLERİCLASS-UPDATE", "PRLG2");//PROGRAMLOG
                prlg.databaseinsert();

            }
            con.Close();
        }
    }
}
