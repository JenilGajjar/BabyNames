using BabyNames.Areas.BabyName.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BabyNames.DAL.SEC_AdminDAL
{
    public class SEC_AdminDALBase : DAL_Helper
    {
        #region Method : PR_Baby_Name_Delete

        public bool PR_Baby_Name_Delete(int NameID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand db = sqlDatabase.GetStoredProcCommand("PR_Baby_Name_Delete");
                sqlDatabase.AddInParameter(db, "@NameID", DbType.Int32, NameID);
                bool IsSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(db));
                return IsSuccess;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Method : Insert & Update

        public bool PR_Baby_Name_Save(BabyModel babyModel)
        {
            Console.WriteLine("Id is" +  babyModel.NameID);

            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);

                if (babyModel.NameID ==null)
                {
                    DbCommand db = sqlDatabase.GetStoredProcCommand("PR_Baby_Name_INSERT");
                    sqlDatabase.AddInParameter(db, "@Name", DbType.String, babyModel.Name);
                    sqlDatabase.AddInParameter(db, "@Meaning", DbType.String, babyModel.Meaning);
                    sqlDatabase.AddInParameter(db, "@Numerology", DbType.Int32, babyModel.Numerology);
                    sqlDatabase.AddInParameter(db, "@Gender", DbType.String, babyModel.Gender);
                    sqlDatabase.AddInParameter(db, "@NakshatraID", DbType.Int32, babyModel.NakshatraID);
                    sqlDatabase.AddInParameter(db, "@ZodiacID", DbType.Int32, babyModel.ZodiacName);
                    sqlDatabase.AddInParameter(db, "@ReligionID", DbType.Int32, babyModel.ReligionID);
                    sqlDatabase.AddInParameter(db, "@Syllables", DbType.String, babyModel.Syllables);
                    sqlDatabase.AddInParameter(db, "@CategoryID", DbType.Int32, babyModel.CategoryID);
                    bool IsSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(db));
                    return IsSuccess;
                }
                else
                {
                    DbCommand db = sqlDatabase.GetStoredProcCommand("PR_Baby_Name_UPDATEByPK");
                    sqlDatabase.AddInParameter(db, "@NameID", DbType.Int32, babyModel.NameID);
                    sqlDatabase.AddInParameter(db, "@Name", DbType.String, babyModel.Name);
                    sqlDatabase.AddInParameter(db, "@Meaning", DbType.String, babyModel.Meaning);
                    sqlDatabase.AddInParameter(db, "@Numerology", DbType.Int32, babyModel.Numerology);
                    sqlDatabase.AddInParameter(db, "@Gender", DbType.String, babyModel.Gender);
                    sqlDatabase.AddInParameter(db, "@NakshatraID", DbType.Int32, babyModel.NakshatraID);
                    sqlDatabase.AddInParameter(db, "@ZodiacID", DbType.Int32, babyModel.ZodiacName);
                    sqlDatabase.AddInParameter(db, "@ReligionID", DbType.Int32, babyModel.ReligionID);
                    sqlDatabase.AddInParameter(db, "@Syllables", DbType.String, babyModel.Syllables);
                    sqlDatabase.AddInParameter(db, "@CategoryID", DbType.Int32, babyModel.CategoryID);
                    bool IsSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(db));
                    return IsSuccess;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Method : PR_Baby_Name_SelectByPK

        public BabyModel PR_Baby_Name_SelectByPK(int NameID)
        {
            try
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
                        Console.WriteLine(dr["Meaning"]);
                        Console.WriteLine(dr["Numerology"]);
                        Console.WriteLine(dr["Gender"]);
                        Console.WriteLine(dr["NakshatraID"]);
                        Console.WriteLine(dr["ZodiacID"]);
                        Console.WriteLine(dr["ReligionID"]);
                        Console.WriteLine(dr["Syllables"]);
                        Console.WriteLine(dr["CategoryID"]);

                        babyModel.NameID = NameID;
                        babyModel.Name = dr["Name"].ToString();
                        babyModel.Meaning = dr["Meaning"].ToString();
                        babyModel.Numerology =  (int)dr["Numerology"]; 
                        babyModel.Gender = dr["Gender"].ToString();
                        babyModel.NakshatraID = (dr["NakshatraID"] == DBNull.Value) ? null: (int)dr["NakshatraID"];
                        babyModel.ZodiacID = (dr["ZodiacID"] == DBNull.Value) ? null : (int)dr["ZodiacID"];
                        babyModel.ReligionID = (dr["ReligionID"] == DBNull.Value) ? null : (int)dr["ReligionID"];
                        babyModel.Syllables = dr["Syllables"].ToString();
                        babyModel.CategoryID = (dr["CategoryID"] == DBNull.Value) ? null : (int)dr["CategoryID"];

                    }

                }
                return babyModel;
            }
            catch
            {
                return null;
            }



        }
        #endregion

        #region Method : PR_COUNT_RECORDS
        public DataTable PR_COUNT_RECORDS()
        {

            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_COUNT_RECORDS");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
