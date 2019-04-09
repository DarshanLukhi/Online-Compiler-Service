using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CompilerWebService
{
    
    [ServiceContract]
    public interface ICompilerService
    {
        // Compiler Service For C++ Language
        [OperationContract]
        string CompileCPP(string code, string key);
        [OperationContract]
        string CompileCPPWithInput(string code,string input, string key);

        // Compiler Service For C Language
        [OperationContract]
        string CompileC(string code, string key);
        [OperationContract]
        string CompileCWithInput(string code, string input, string key);

        // Compiler Service For Python Language
        [OperationContract]
        string CompilePython(string code, string key);
        [OperationContract]
        string CompilePythonWithInput(string code, string input, string key);

        // Compiler Service For Java Language 
        [OperationContract]
        string CompileJava(string code, string key);
        [OperationContract]
        string CompileJavaWithInput(string code, string input, string key);

        // Service Method For Retrive All Record
        [OperationContract]
        string ShowAllCode(string key);

        [OperationContract]
        string GetCode(string CodeID,string key);
        [OperationContract]
        string DeleteCode(string CodeID, string key);
        [OperationContract]
        string UpdateCode(string CodeID,string code, string key);
    }
}
