using BabyNames.Areas.BabyName.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using BabyNames.BAL;

namespace BabyNames.DAL.Baby_DAL
{
    public class Baby_DALBase : DAL_Helper
    {
        #region PR_SELECT_ALL_BabyNames

        public List<BabyModel> PR_SELECT_ALL_BabyNames()
        {

            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SELECT_ALL_BabyNames");

                List<BabyModel> babyModels = new List<BabyModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        BabyModel babyModel = new BabyModel();
                        babyModel.NameID = (int)dr["NameID"];
                        babyModel.Name = dr["Name"].ToString();
                        babyModel.Meaning = dr["Meaning"].ToString();
                        babyModel.Numerology = (int)dr["Numerology"];
                        babyModel.Gender = dr["Gender"].ToString();
                        babyModel.Nakshatra = dr["Nakshatra"].ToString();
                        babyModel.ZodiacName = dr["ZodiacName"].ToString();
                        babyModel.ReligionName = dr["ReligionName"].ToString();
                        babyModel.Syllables = dr["Syllables"].ToString();
                        babyModel.CategoryName = dr["CategoryName"].ToString();
                        babyModels.Add(babyModel);
                    }
                    return babyModels;
                }

            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region ZodiacFilter
        public List<ZodiacModel> ZodiacFilter()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_MST_Zodiac_ComboBox");
                List<ZodiacModel> list = new List<ZodiacModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        ZodiacModel zodiacModel = new()
                        {
                            ZodiacID = (int)dr["ZodiacID"],
                            ZodiacName = dr["ZodiacName"].ToString()
                        };
                        list.Add(zodiacModel);

                    }

                    return list;
                }

            }
            catch
            {

                return null;
            }
        }

        #endregion

        #region ReligionFilter
        public List<ReligionModel> ReligionFilter()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_MST_Religion_ComboBox");
                List<ReligionModel> list = new List<ReligionModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        ReligionModel religionModel = new()
                        {
                            ReligionID = (int)dr["ReligionID"],
                            ReligionName = dr["ReligionName"].ToString()
                        };
                        list.Add(religionModel);
                    }
                    return list;
                }
            }
            catch
            {

                return null;
            }


        }

        #endregion

        #region NakshatraFilter
        public List<NakshatraModel> NakshatraFilter()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_MST_Nakshatra_ComboBox");
                List<NakshatraModel> list = new List<NakshatraModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        NakshatraModel nakshatraModel = new()
                        {
                            NakshatraID = (int)dr["NakshatraID"],
                            Nakshatra = dr["Nakshatra"].ToString()
                        };
                        list.Add(nakshatraModel);
                    }
                    return list;
                }
            }
            catch
            {

                return null;
            }


        }

        #endregion

        #region BabyNameFilter

        public List<BabyModel> PR_BabyNames_FILTER(BabyFilterModel filterModel)
        {
            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);



                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_BabyNames_FILTER");
                sqlDatabase.AddInParameter(dbCommand, "@ZodiacID", SqlDbType.Int, filterModel.ZodiacID);
                sqlDatabase.AddInParameter(dbCommand, "@Gender", DbType.String, filterModel.Gender);
                sqlDatabase.AddInParameter(dbCommand, "@ReligionID", SqlDbType.Int, filterModel.ReligionID);
                sqlDatabase.AddInParameter(dbCommand, "@NakshatraID", SqlDbType.Int, filterModel.NakshatraID);
                sqlDatabase.AddInParameter(dbCommand, "@Name", SqlDbType.NVarChar, filterModel.Name);


                List<BabyModel> babyModels = new List<BabyModel>();

                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        BabyModel babyModel = new BabyModel();
                        babyModel.NameID = (int)dr["NameID"];
                        babyModel.Name = dr["Name"].ToString();
                        babyModel.Meaning = dr["Meaning"].ToString();
                        babyModel.Numerology = (int)dr["Numerology"];
                        babyModel.Gender = dr["Gender"].ToString();
                        babyModel.Nakshatra = dr["Nakshatra"].ToString();
                        babyModel.ZodiacName = dr["ZodiacName"].ToString();
                        babyModel.ReligionName = dr["ReligionName"].ToString();
                        babyModel.Syllables = dr["Syllables"].ToString();
                        babyModel.CategoryName = dr["CategoryName"].ToString();
                        babyModels.Add(babyModel);
                    }
                    Console.WriteLine(babyModels.Count);
                    return babyModels;

                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region PR_Baby_Name_SelectByPK


        public BabyModel PR_Baby_Name_SelectByPK(int NameID)
        {

            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Baby_Name_SelectByPK");
            sqlDatabase.AddInParameter(dbCommand, "@NameID", DbType.Int32, NameID);
            BabyModel babyModel = new BabyModel();

            using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    Console.WriteLine(dr);
                    Console.WriteLine(dr["Name"]);
                    babyModel.Name = dr["Name"].ToString();
                }

            }
            return babyModel;

        }
        #endregion

        #region PR_GetFavoriteStatus_For_SEC_User
        public Dictionary<int, bool> PR_GetFavoriteStatus_For_SEC_User()
        {
            try
            {
                Console.WriteLine("The value of id   " + CV.UserID());
                Console.WriteLine("The value of name    " + CV.UserName());

                int UserID = CV.UserID() ?? 0;

                Dictionary<int, bool> favoriteStatus = new Dictionary<int, bool>();
                if (UserID == 0) return favoriteStatus;
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_GetFavoriteStatus_For_SEC_User");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);

                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {

                    while (dr.Read())
                    {
                        int NameID = (int)dr["NameID"];
                        bool IsFavorite = Convert.ToBoolean(dr["IsFavorite"]);
                        favoriteStatus.Add(NameID, IsFavorite);
                    }
                }


                return favoriteStatus;
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region PR_SelectFavoriteName
        public bool PR_SelectFavoriteName(int UserID, int NameID)
        {
            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectFavoriteName");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@NameID", DbType.Int32, NameID);
                bool IsSuccuss = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return IsSuccuss;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region PR_UnselectFavoriteName


        public bool PR_UnselectFavoriteName(int UserID, int NameID)
        {
            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UnselectFavoriteName");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@NameID", DbType.Int32, NameID);
                bool IsSuccuss = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return IsSuccuss;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region PR_GetUserFavoriteNames
        public List<BabyModel> PR_GetUserFavoriteNames()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_GetUserFavoriteNames");
                int UserID = (int)CV.UserID();
                sqlDatabase.AddInParameter(dbCommand,  "@UserID", SqlDbType.Int,UserID);



                List<BabyModel> babyModels = new List<BabyModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        BabyModel babyModel = new BabyModel();
                        babyModel.NameID = (int)dr["NameID"];
                        babyModel.Name = dr["Name"].ToString();
                        babyModel.Meaning = dr["Meaning"].ToString();
                        babyModel.Numerology = (int)dr["Numerology"];
                        babyModel.Gender = dr["Gender"].ToString();
                        babyModel.Nakshatra = dr["Nakshatra"].ToString();
                        babyModel.ZodiacName = dr["ZodiacName"].ToString();
                        babyModel.ReligionName = dr["ReligionName"].ToString();
                        babyModel.Syllables = dr["Syllables"].ToString();
                        babyModel.CategoryName = dr["CategoryName"].ToString();
                        babyModels.Add(babyModel);
                    }
                    return babyModels;
                }
            }
            catch
            {
                return null; ;
            }
        }
        #endregion
    }
}
