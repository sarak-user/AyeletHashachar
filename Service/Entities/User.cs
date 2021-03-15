using Service.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Runtime.Serialization;

namespace Service.Entities
{
    [DataContract]
    public class User
    {
        #region Members
            [DataMember]
            public int iUserId { get; set; }
            [DataMember]
            public int iCoordinatorId { get; set; }
            [DataMember]
            public int iDepartmentId { get; set; }
            [DataMember]
            public int iPersonId { get; set; }
            [DataMember]
            public string nvName { get; set; }
            [DataMember]
            public int iUserType { get; set; }
            [DataMember][NoSendToSQL]
            public string nvUserType { get; set; }
            [DataMember]
            public string nvGuide { get; set; }

            [DataMember]
            public Boolean bIsScheduller { get; set; }

            [DataMember][NoSendToSQL]
            public string nvUserName { get; set; }
            [DataMember][NoSendToSQL]
            public string nvPassword { get; set; }
            [DataMember][NoSendToSQL]
            public string nvDepartmentEmail { get; set; }
            [DataMember] [NoSendToSQL]
            public string nvDepartmentMobileNumber { get; set; }
            [DataMember][NoSendToSQL]
            public string nvDepartmentName { get; set; }
        #endregion

        #region Methods
        public static User Login(string nvUserName, string nvPassword)
        {
            try
            {
                OperationContext oOperationContext = OperationContext.Current;
                MessageProperties oMessageProperties = oOperationContext.IncomingMessageProperties;
                RemoteEndpointMessageProperty oRemoteEndpointMessageProperty = (RemoteEndpointMessageProperty)oMessageProperties[RemoteEndpointMessageProperty.Name];
                string szAddress = oRemoteEndpointMessageProperty.Address;
                int nPort = oRemoteEndpointMessageProperty.Port;

                SqlParameter[] param = {                                  
                                        new SqlParameter("nvUserName",nvUserName), 
                                        new SqlParameter("nvPassword",nvPassword),
                                        new SqlParameter("nvAddress",szAddress),
                                        new SqlParameter("iPort",nPort)
                                        };
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUser_Login_SLCT", param);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //User user = new User()
                    //{
                    //    iUserId = int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()),
                    //    iUserType = int.Parse(ds.Tables[0].Rows[0]["iUserType"].ToString()),
                    //    nvGuide = ds.Tables[0].Rows[0]["nvGuide"].ToString()
                    //};



                    User user = ObjectGenerator<User>.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                    return user;
                    //  return new User() { iUserId = int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()), iUserType = int.Parse(ds.Tables[0].Rows[0]["iUserType"].ToString()), nvGuide = ds.Tables[0].Rows[0]["nvGuide"].ToString() };
                }
                return new User { iUserId = -1 };
            }
            catch (Exception ex)
            {
                Log.ExceptionLog(ex.Message, "Login");
                return null;
            }
        }

       

        public static bool DeleteUser(int iUserId)
        {
            try
            {
                DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUser_DLT", new List<SqlParameter>() {
                new SqlParameter("iUserId",iUserId)
                });
                return true;
            }
            catch (Exception ex)
            {
                Log.ExceptionLog(ex.Message, "DeleteUser");
                return false;
            }
        }

        

        //public static User Login(string nvUserName, string nvUserPassword)
        //{
        //    try
        //    {
        //        OperationContext oOperationContext = OperationContext.Current;
        //        MessageProperties oMessageProperties = oOperationContext.IncomingMessageProperties;
        //        RemoteEndpointMessageProperty oRemoteEndpointMessageProperty = (RemoteEndpointMessageProperty)oMessageProperties[RemoteEndpointMessageProperty.Name];
        //        string szAddress = oRemoteEndpointMessageProperty.Address;
        //        int nPort = oRemoteEndpointMessageProperty.Port;

        //        SqlParameter[] param = {                                  
        //                                new SqlParameter("nvUserName",nvUserName), 
        //                                new SqlParameter("nvPassword",nvUserPassword),
        //                                new SqlParameter("nvAddress",szAddress),
        //                                new SqlParameter("iPort",nPort)
        //                                };
        //        DataSet ds = SqlDataAccess.ExecuteDatasetSP("TUser_Login_SLCT", param);
        //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            User user = new User() { iUserId = int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()), iUserType = int.Parse(ds.Tables[0].Rows[0]["iUserType"].ToString()) };
        //            return user;
        //      //      return new User() { iUserId = int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()), iUserType = int.Parse(ds.Tables[0].Rows[0]["iUserType"].ToString()) };
        //        }
        //        return new User { iUserId = -1 };
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ExceptionLog(ex.Message, "Login");
        //        return null;
        //    }
        //}

        #endregion
    }

}