using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace CompilerWebService
{

    public class CompilerService : ICompilerService
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string CompileCPP(string value)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\"+name+".cpp",value);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C g++ -w "+name+".cpp -o "+name+".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                process.Start();

                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if(error != "")
                {
                    
                    return error.Replace(name+".cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C "+name+".exe && del "+name+".exe");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return "Time Limit Exceeded";
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if(error != "")
                        return error;
                   
                    return "No Output";
                }
                
                
            }
        }
        public string CompileCPPWithInput(string code,string input)
        {
            using (Process process = new Process())
            {
                string output, error;
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
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                process.Start();

                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {
                    return error.Replace(name+".cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < "+name+".txt && del " + name + ".exe");
                    process.Start();

                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return "Time Limit Exceeded";

                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if (error != "")
                        return error;
                    
                    return "No Output";
                }
            }
        }
        public string CompileC(string value)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".c", value);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C gcc -w -lm "+name+".c -o " + name + ".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {

                    return error.Replace(name+".c:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe && del " + name + ".exe");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return "Time Limit Exceeded";
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if (error != "")
                        return error;
                    
                    return "No Output";
                }


            }
        }
        public string CompileCWithInput(string code, string input)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".c", code);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".txt", input);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C g++ -w -lm "+name+".c -o " + name + ".exe");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                process.Start();

                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {

                    return error.Replace(name+".cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < "+name+".txt && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if (error != "")
                        return error;
                    
                    return "No Output";
                }
            }
        }

        public string CompilePython(string value)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + ".py", value);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C python "+name+".py");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                reader = process.StandardOutput;
                output = reader.ReadToEnd();
                
                if (error != "")
                {
                    return error.Replace("  File \""+name+".py\",", "");
                }
                else if(output != "")
                {
                    return output;
                }
                
                return "No Output";
            }
        }
        public string CompilePythonWithInput(string code, string input)
        {
            using (Process process = new Process())
            {
                string output, error;
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
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                Debug.WriteLine("a");
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";

                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (error != "")
                {
                    return error.Replace("  File \""+name+".py\",", "");
                }
                else if (output != "")
                {
                    return output;
                }
                
                return "No Output";

            }
        }

        public string CompileJava(string value)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
                Directory.CreateDirectory(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name);
                File.WriteAllText(@"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name + "\\Main.java", value);
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = String.Format("/C javac Main.java");
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData\"+name;
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {
                    return error;
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C Java -cp . Main");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return "Time Limit Exceeded";
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if (error != "")
                        return error;

                    return "No Output";
                }
            }
        }

        public string CompileJavaWithInput(string code, string input)
        {
            using (Process process = new Process())
            {
                string output, error;
                string name = CompilerService.RandomString(8);
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
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData\" + name;
                process.Start();
                bool TLE = process.WaitForExit(5000);
                if (!TLE)
                    return "Time Limit Exceeded";
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if (error != "")
                {
                    return error;
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C Java -cp . Main < input.txt");
                    process.Start();
                    TLE = process.WaitForExit(5000);
                    if (!TLE)
                        return "Time Limit Exceeded";
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else if (error != "")
                        return error;

                    return "No Output";
                }
            }
        }
    }

}