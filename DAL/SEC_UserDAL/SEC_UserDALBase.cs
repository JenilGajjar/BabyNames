using BabyNames.Areas.BabyName.Models;
using BabyNames.Areas.SEC_User.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Windows.Input;

namespace BabyNames.DAL.SEC_UserDAL
{
    public class SEC_UserDALBase : DAL_Helper
    {


        #region PhotoFile Upload

        private readonly IWebHostEnvironment _webHostEnvironment;
        public SEC_UserDALBase(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion


        #region Method : PR_SEC_UserSelectByUserNamePassword
        public DataTable PR_SEC_UserSelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SEC_UserSelectByUserNamePassword");
                sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, UserName);
                sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch
            {
                return null;

            }
        }
        #endregion

        #region SEC_User_Register

        public bool SEC_User_Register(SEC_UserModel modelSEC_User)
        {

            try
            {

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand db = sqlDatabase.GetStoredProcCommand("PR_SEC_UserSelectByUserName");
                sqlDatabase.AddInParameter(db, "@UserName", SqlDbType.VarChar, modelSEC_User.UserName);
                DataTable dt = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(db))
                {
                    dt.Load(dataReader);
                }
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine(modelSEC_User.File);

                    // for upload and get Photo into database


                    if (modelSEC_User.File != null)
                    {
                        string folder = "images/user/";
                        folder += Guid.NewGuid().ToString() + "_" + modelSEC_User.File.FileName;


                        modelSEC_User.PhotoPath = "/" + folder;

                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        modelSEC_User.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    }


                    Console.WriteLine(modelSEC_User.PhotoPath);

                    DbCommand dbCmd = sqlDatabase.GetStoredProcCommand("PR_SEC_User_INSERT");
                    sqlDatabase.AddInParameter(dbCmd, "@UserName", SqlDbType.NVarChar, modelSEC_User.UserName);
                    sqlDatabase.AddInParameter(dbCmd, "@Password", SqlDbType.NVarChar, modelSEC_User.Password);
                    sqlDatabase.AddInParameter(dbCmd, "@FirstName", SqlDbType.NVarChar, modelSEC_User.FirstName);
                    sqlDatabase.AddInParameter(dbCmd, "@LastName", SqlDbType.NVarChar, modelSEC_User.LastName);
                    sqlDatabase.AddInParameter(dbCmd, "@Email", SqlDbType.NVarChar, modelSEC_User.Email);
                    sqlDatabase.AddInParameter(dbCmd, "@PhotoPath", SqlDbType.NVarChar, modelSEC_User.PhotoPath);

                    bool res = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCmd));
                    return res;

                }

            }
            catch
            {

                return false;
            }
        }

        #endregion

        #region  Method : PR_SEC_User_SelectALL
        public List<SEC_UserModel> PR_SEC_User_SelectALL()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SEC_User_SelectALL");
                List<SEC_UserModel> list = new List<SEC_UserModel>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dr.Read())
                    {
                        SEC_UserModel model = new SEC_UserModel
                        {
                            UserID = (int)dr["UserID"],
                            UserName = dr["UserName"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            Email = dr["Email"].ToString(),
                            PhotoPath = dr["PhotoPath"].ToString(),
                            Created = (DateTime)dr["Created"],
                            Modified = (DateTime)dr["Modified"]
                        };
                        list.Add(model);

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


        #region Method:PR_SEC_UserSelectByUserName

        public SEC_UserModel PR_SEC_UserSelectByUserName(int UserID)
        {

            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand db = sqlDatabase.GetStoredProcCommand("PR_SEC_UserSelectByUserID");
                sqlDatabase.AddInParameter(db, "@UserID", SqlDbType.Int, UserID);
                DataTable dt = new DataTable();
                SEC_UserModel sEC_UserModel = new SEC_UserModel();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(db))
                {
                    dt.Load(dataReader);
                }

                foreach (DataRow dr in dt.Rows)
                {

                    sEC_UserModel.UserID = (int)dr["UserID"];
                    sEC_UserModel.FirstName = dr["FirstName"].ToString();
                    sEC_UserModel.LastName = dr["LastName"].ToString();
                    sEC_UserModel.UserName = dr["UserName"].ToString();
                    sEC_UserModel.Password = dr["Password"].ToString();
                    sEC_UserModel.Email = dr["Email"].ToString();
                    sEC_UserModel.PhotoPath = dr["PhotoPath"].ToString();
                }
                return sEC_UserModel;
            }
            catch
            {
                return null;
            }




        }

        #endregion

        #region Method : PR_SEC_User_UPDATEByPK
        public bool PR_SEC_User_UPDATEByPK(SEC_UserModel sEC_UserModel)
        {
            try
            {

                if (sEC_UserModel.File != null)
                {
                    string folder = "images/user/";
                    folder += Guid.NewGuid().ToString() + "_" + sEC_UserModel.File.FileName;


                    sEC_UserModel.PhotoPath = "/" + folder;

                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    sEC_UserModel.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                Console.WriteLine(sEC_UserModel.PhotoPath);

                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand db = sqlDatabase.GetStoredProcCommand("PR_SEC_User_UPDATEByPK");
                sqlDatabase.AddInParameter(db, "@UserID", DbType.Int32, sEC_UserModel.UserID);
                sqlDatabase.AddInParameter(db, "@UserName", DbType.String, sEC_UserModel.UserName);
                sqlDatabase.AddInParameter(db, "@Password", DbType.String, sEC_UserModel.Password);
                sqlDatabase.AddInParameter(db, "@FirstName", DbType.String, sEC_UserModel.FirstName);
                sqlDatabase.AddInParameter(db, "@LastName", DbType.String, sEC_UserModel.LastName);
                sqlDatabase.AddInParameter(db, "@Email", DbType.String, sEC_UserModel.Email);
                sqlDatabase.AddInParameter(db, "@PhotoPath", DbType.String, sEC_UserModel.PhotoPath);




                bool IsSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(db));



                return IsSuccess;

            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region Method : PR_SEC_User_CHANGE_PASSWORD

        public bool PR_SEC_User_CHANGE_PASSWORD(int UserID, string NewPassword)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SEC_User_CHANGE_PASSWORD");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, NewPassword);

                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));

                return isSuccess;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        

    }
}
