using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace CompilerWebService
{

    public class CompilerService : ICompilerService
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string IsAuthorized(string key)
        {
            
            string username = "PUBLIC";
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"SELECT * FROM [User] WHERE APIKey=@key";
            SqlCommand cmd = new SqlCommand(query,con);

            cmd.Parameters.AddWithValue("@key",key);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                username = reader.GetString(0);
            }
            con.Close();
            return username;
        }
        
        public static void AddRecord(string id,string username,string lang,string path)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"INSERT INTO Records (CodeId, Username, Language, Link) values(@id,@uname,@lang,@link)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@lang", lang);
            cmd.Parameters.AddWithValue("@link", path);
            con.Open();
            int k = cmd.ExecuteNonQuery();

            
            con.Close();
        }

        public static string SendResponse(string id,string status,string output,string error)
        {
            output = JsonConvert.ToString(output);
            error = JsonConvert.ToString(error);
            
            string response = "{ \"id\":\"" + id + "\",\"status\":\"" + status + "\", \"output\":" + output + ", \"error\":" + error + "}";
            return response;
        }


        





        // service method implementation

        public string CompileCPP(string code, string key)
        {
            
            using (Process process = new Process())
            {
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\"+name+".cpp",code);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C g++ -w "+name+".cpp -o "+name+".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();

                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "C", dir + '\\' + name + ".cpp");


                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace(name + ".cpp:", ""));
                else
                {
                    process.StartInfo.Arguments = String.Format("/C "+name+".exe && del "+name+".exe");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();


                    if (error != "")
                        return CompilerService.SendResponse(name,"RTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }
                
                
            }
        }
        public string CompileCPPWithInput(string code,string input , string key)
        {
            using (Process process = new Process())
            {
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".cpp", code);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".txt", input);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C g++ -w "+name+".cpp -o " + name + ".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "C++", dir + '\\' + name + ".cpp");
                StreamReader reader = process.StandardError;

                
                error = reader.ReadToEnd();
                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace(name + ".cpp:", ""));
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < "+name+".txt && del " + name + ".exe");
                    process.Start();

                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");

                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();

                    if (error != "")
                        return CompilerService.SendResponse(name,"RTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }
            }
        }
        public string CompileC(string code, string key)
        {
            using (Process process = new Process())
            {
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".c", code);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C gcc -w -lm "+name+".c -o " + name + ".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "C", dir + '\\' + name + ".c");

                StreamReader reader = process.StandardError;

                error = reader.ReadToEnd();
                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace(name + ".c:", ""));
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe && del " + name + ".exe");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();

                    if (error != "")
                        return CompilerService.SendResponse(name,"RTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }


            }
        }
        public string CompileCWithInput(string code, string input , string key)
        {

            using (Process process = new Process())
            {
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".c", code);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".txt", input);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C gcc -w -lm "+name+".c -o " + name + ".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "C", dir + '\\' + name + ".c");

                

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace(name + ".c:", ""));
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < "+name+".txt && del " + name + ".exe");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name, "TLE", "", "Time Limit Exceeded");

                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error != "")
                        return CompilerService.SendResponse(name,"RTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }
            }
        }

        public string CompilePython(string code, string key)
        {
            using (Process process = new Process())
            {
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);

               
                
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".py", code);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C python "+name+".py");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;

                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                reader = process.StandardOutput;
                output = reader.ReadToEnd();


                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name,username,"Python",dir +'\\'+name+".py");


                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace("  File \"" + name + ".py\",", ""));
                else if(output != "")   
                    return CompilerService.SendResponse(name,"AC",output,"");
                else
                    return CompilerService.SendResponse(name,"AC", "No Output", "");
            }
        }
        public string CompilePythonWithInput(string code, string input , string key)
        {
            using (Process process = new Process())
            {
                string output, error, username,dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".py", code);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".txt", input);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C python "+name+".py < "+name+".txt");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;

                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "Python", dir + '\\' + name + ".py");


                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                reader = process.StandardOutput;
                output = reader.ReadToEnd();

                if (error != "")
                    return CompilerService.SendResponse(name,"CTE", "", error.Replace("  File \"" + name + ".py\",", ""));
                else if (output != "")
                    return CompilerService.SendResponse(name,"AC", output, "");
                else
                    return CompilerService.SendResponse(name,"AC", "No Output", "");

            }
        }

        public string CompileJava(string code, string key)
        {
            using (Process process = new Process())
            {
                string name = CompilerService.RandomString(8);
                string output, error, username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData\"+name;
                
                Directory.CreateDirectory(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + "\\Main.java", code);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C javac Main.java");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();

                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username, "Java", dir + "\\Main.java");

                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {
                    Debug.WriteLine(CompilerService.SendResponse(name,"CTE", "", error));
                    return CompilerService.SendResponse(name,"CTE", "", error);
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C Java -cp . Main");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();

                    if (error != "")
                        return CompilerService.SendResponse(name,"RTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }
            }
        }

        public string CompileJavaWithInput(string code, string input , string key)
        {
            using (Process process = new Process())
            {
                string name = CompilerService.RandomString(8);
                string output, error,username, dir = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name;

                Directory.CreateDirectory(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + "\\Main.java", code);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + "\\input.txt", input);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C javac Main.java");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = dir;
                process.Start();

                username = CompilerService.IsAuthorized(key);
                CompilerService.AddRecord(name, username,"Java", dir+"\\Main.java");

                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {
                    return CompilerService.SendResponse(name,"RTE", "", error);
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C Java -cp . Main < input.txt");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return CompilerService.SendResponse(name,"TLE", "", "Time Limit Exceeded");
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();

                    if (error != "")
                        return CompilerService.SendResponse(name,"CTE", "", error);
                    else if (output != "")
                        return CompilerService.SendResponse(name,"AC", output, "");
                    else
                        return CompilerService.SendResponse(name,"AC", "No Output", "");
                }
            }
        }

        public string ShowAllCode(string key)
        {
            string username = CompilerService.IsAuthorized(key);
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"SELECT * FROM [Records] WHERE Username = @username";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", username);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);
            Debug.WriteLine(table);
            con.Close();

            string json = JsonConvert.SerializeObject(table);
            return json;
        }

        public string GetCode(string CodeID, string key)
        {
            string code,response  = "{ \"error\":\"" + "Not Found" + "\"}";
            string username = CompilerService.IsAuthorized(key);
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"SELECT * FROM [Records] WHERE CodeID = @CodeID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@CodeID", CodeID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                code = File.ReadAllText(reader.GetString(3));
                code = JsonConvert.ToString(code);
                response = "{ \"id\":\"" + reader.GetString(0) 
                    + "\",\"Language\":\"" + reader.GetString(2) 
                    + "\",\"Code\":" + code 
                    + "}";
            }

            con.Close();
            return response;
        }

        public string DeleteCode(string CodeID, string key)
        {
            string response  = "{ \"statusCode\":\"0\"}";
            string username = CompilerService.IsAuthorized(key);
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string query = @"DELETE FROM [Records] WHERE CodeID = @CodeID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@CodeID", CodeID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if(i == 0)
                response = "{ \"statusCode\":\"0\"}";
            else
                response = "{ \"statusCode\":\"1\"}";
            con.Close();
            return response;
        }

        public string UpdateCode(string CodeID, string code, string key)
        {
            string response = "{ \"statusCode\":\"0\"}";
            string username = CompilerService.IsAuthorized(key);
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompileCodeService;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string query = @"SELECT Link FROM [Records] WHERE CodeID = @CodeID";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@CodeID", CodeID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                File.WriteAllText(reader.GetString(0),code);
                response = "{ \"statusCode\":\"1\"}";

            }
            else
            {
                response = "{ \"statusCode\":\"0\"}";
            }

            con.Close();
            return response;
        }
    }

}