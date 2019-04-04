using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C g++ -w first.cpp -o "+name+".exe");
                
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";
                
                process.Start();
                
                StreamReader reader = process.StandardError;
                error = reader.ReadToEnd();
                if(error != "")
                {
                    
                    return error.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C "+name+".exe && del "+name+".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    reader = process.StandardError;
                    error = reader.ReadToEnd();
                    if (error == "" && output != "")
                        return output;
                    else
                        return error;
                    process.WaitForExit();
                    return output;
                }
                
                
            }
        }
        public string CompileCPPWithInput(string code,string input)
        {
            using (Process process = new Process())
            {
                string output;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C g++ -w first.cpp -o " + name + ".exe 2>&1");

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";

                process.Start();

                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (output != "")
                {

                    return output.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < input.txt && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }
            }
        }
        public string CompileC(string value)
        {
            using (Process process = new Process())
            {
                string output;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C g++ -w first.c -lm -o " + name + ".exe 2>&1");

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";

                process.Start();

                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (output != "")
                {

                    return output.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }


            }
        }
        public string CompileCWithInput(string code, string input)
        {
            using (Process process = new Process())
            {
                string output;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C g++ -w first.c -lm -o " + name + ".exe 2>&1");

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";

                process.Start();

                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (output != "")
                {

                    return output.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < input.txt && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }
            }
        }

        public string CompilePython(string value)
        {
            using (Process process = new Process())
            {
                string output;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C Python first.py 2>&1");

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";

                process.Start();

                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (output != "")
                {

                    return output.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }


            }
        }
        public string CompilePythonWithInput(string code, string input)
        {
            using (Process process = new Process())
            {
                string output;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                string name = CompilerService.RandomString(8);
                process.StartInfo.Arguments = String.Format("/C g++ -w first.c -lm -o " + name + ".exe 2>&1");

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = @"D:\WORKSPACE\Project\CompilerService\CompileCodeData";

                process.Start();

                StreamReader reader = process.StandardOutput;
                output = reader.ReadToEnd();
                if (output != "")
                {

                    return output.Replace("first.cpp:", "");
                }
                else
                {
                    process.StartInfo.Arguments = String.Format("/C " + name + ".exe < input.txt && del " + name + ".exe");
                    process.Start();
                    reader = process.StandardOutput;
                    output = reader.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }
            }
        }

    }

}