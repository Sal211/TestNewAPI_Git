using System.Data;
using System.Data.SqlClient;
using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.Models.Connection;
using testNewAPI.Repository.Contract;

namespace testNewAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private async Task<bool> PostDataStudent(Dictionary<string, object> DataStudent, string query)
        {
            var con = new ClsConnection();
            if (con._Errcode == 0)
            {
                try
                {
                    con._cmd = new SqlCommand(query, con._con);
                    AddParameters(con._cmd, DataStudent);
                    return await con._cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch (Exception ex)
                {              
                    Console.Write(ex.Message, "An error occurred while Post or update student data.");
                    throw new ArgumentException("Internal server error.");                 
                }
                finally
                {
                    await con._con.CloseAsync();
                }
            }
            else
            {
                Console.Write("Database connection error.");
                 throw new ArgumentException("Database connection error.");
            }
        }
        public async Task<List<StudentDto>> GetMainStudentData(string query)
        {
            var con = new ClsConnection();
            DataTable dt = new DataTable();
            List<StudentDto> studentData = new List<StudentDto>();
            if (con._Errcode == 0)
            {
                try
                {
                    con._Ad = new SqlDataAdapter(query, con._con);
                    con._Ad.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        var objStudent = new StudentDto()
                        {
                            Age = int.Parse(row["Age"].ToString()),
                            Name = row["Name"].ToString(),
                            ID = int.Parse(row["ID"].ToString()),
                        };
                        studentData.Add(objStudent);
                    }
                }
                catch (Exception ex)
                {
                  Console.Write(ex.Message);
                    throw new ArgumentException("Internal server error.");
                }
                finally
                {
                    await con._con.CloseAsync();
                }

            }
            else
            {
                Console.Write("Database connection error.");
                throw new ArgumentException("Database connection error.");
            }
            return studentData;
        }
        private void AddParameters(SqlCommand command, Dictionary<string, object> DataStudent)
        {
            foreach (var student in DataStudent)
            {
                command.Parameters.AddWithValue(student.Key, student.Value);
            }
        }
        public async Task<bool> CreateStudentAsync(ClsStudent newStudent)
        {
            string query = "EXEC sp_CrudStudent @Name = @Name, @Age = @Age, @type = @type , @CreateDate = @CreateDate , @Inactive = @Inactive";
            Dictionary<string, object> DataStudent = new Dictionary<string, object>
            {
                { "@Name", newStudent.Name },
                { "@Age", newStudent.Age },
                { "@type", 'C' },
                { "@CreateDate" , DateTime.Now },
                { "@Inactive" , newStudent.Inactive },
            };
            return await PostDataStudent(DataStudent, query);
        }

        public async Task<bool> DeleteStudentAsync(int Id)
        {
             var con = new ClsConnection();
            if (con._Errcode == 0)
            {
                try
                {
                    string query = $"EXEC sp_CrudStudent @ID = {Id}, @type = D";
                    con._cmd = new SqlCommand(query, con._con);
                    return await con._cmd.ExecuteNonQueryAsync() > 0 ? true : false ;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message, "An error occurred while deleting student data.");
                    throw new ArgumentException("Internal server error.");
                }
                finally
                {
                    await con._con.CloseAsync();
                }
            }
            else
            {
                Console.Write("Database connection error.");
                throw new ArgumentException("Database connection error.");
            }
        }

        public async Task<List<StudentDto>> GetAllStudentAsync()
        {
            string query = "EXEC sp_CrudStudent";
            return await GetMainStudentData(query);
        }

        public async Task<List<StudentDto>> GetStudentByIdAsync(int Id)
        {
            string query = $"EXEC sp_CrudStudent @ID = {Id}, @type = G";
            return await GetMainStudentData(query);
        }

        public async Task<bool> SearchStudentAsync(int Id)
        {
            ClsConnection con = new ClsConnection();
            if (con._Errcode == 0)
            {
                try
                {
                    string query = $"EXEC sp_CrudStudent @id = {Id}, @type = F";
                    con._cmd = new SqlCommand(query, con._con);
                    int count = (int)await con._cmd.ExecuteScalarAsync();
                    return count > 0 ? true : false;
                }
                catch (Exception ex)
                {
                   Console.Write($"Exception: {ex.Message}");
                    return false;
                }
                finally
                {
                    await con._con.CloseAsync();
                }
            }
            else
            {
                Console.Write("Database connection error.");
                throw new ArgumentException("Database connection error.");
            }
        }

        public Task<bool> UpdateStudentAsync(ClsStudent updateStudent)
        {
            string query = "EXEC sp_CrudStudent @ID = @ID, @Name = @Name, @Age = @Age, @type = @type";
            Dictionary<string, object> DataStudent = new Dictionary<string, object>
            {
                { "@ID", updateStudent.ID },
                { "@Name", updateStudent.Name },
                { "@Age", updateStudent.Age },
                { "@type", "U" }
            };
            return PostDataStudent(DataStudent, query);
        }
    }
}
