using Dapper;
using Student_Managment.Models;
using System.Data;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;

namespace Student_Managment.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IConfiguration _config;

        public StudentServices(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnection");
            providerName = "System.Data.SqlClient";
        }
        public String ConnectionString { get; }
        public string providerName { get; }
        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        public string DeleteStudent(int Student_Id)
        {
            string? result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var DeleteResult = dbConnection.Query<Student>("delete_Student",
                        new
                        {
                            Student_Id = Student_Id
                        },
                        commandType: CommandType.StoredProcedure);
                    if (DeleteResult != null && DeleteResult?.FirstOrDefault()?.Responce == "Delete sucessfully")
                    {
                        result = "Record Delete Sucessfully";
                    }
                    else
                    {
                        result = DeleteResult?.FirstOrDefault()?.Responce;
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }
        }
        public string InsertStudent(Student student)
        {
            string? result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var InsertResult = dbConnection.Query<Student>("insert_StudentList",
                        new
                        {
                            FullName = student.FullName,
                            Email = student.Email,
                            city = student.city,
                            Created_By = student.Created_By
                        },
                        commandType: CommandType.StoredProcedure);
                    if (InsertResult != null && InsertResult?.FirstOrDefault()?.Responce == "Save Sucessfully")
                    {
                        result = "Save Sucessfully";
                    }
                    else
                    {
                        result = InsertResult?.FirstOrDefault()?.Responce;
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }
        }
        public List<Student> GetStudentList()
        {
            List<Student> stu = new List<Student>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    stu = dbConnection.Query<Student>("Get_StudentList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return stu;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return stu;
            }
        }
        public string UpdateStudent(Student student)
        {
            string? result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var UpdateResult = dbConnection.Query<Student>("Update_Student",
                        new
                        {
                            FullName = student.FullName,
                            Email = student.Email,
                            city = student.city,
                            Student_Id = student.Student_Id
                        },
                        commandType: CommandType.StoredProcedure);
                    if (UpdateResult != null && UpdateResult?.FirstOrDefault()?.Responce == "Update Sucessfully")
                    {
                        result = "Record Update Sucessfully";
                    }
                    else
                    {
                        result = UpdateResult?.FirstOrDefault()?.Responce;
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }
        }

    }
}
