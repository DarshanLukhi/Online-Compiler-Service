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
        string CompileCPP(string value);
        [OperationContract]
        string CompileCPPWithInput(string code,string input);

        // Compiler Service For C Language
        [OperationContract]
        string CompileC(string value);
        [OperationContract]
        string CompileCWithInput(string code, string input);

        // Compiler Service For Python Language
        [OperationContract]
        string CompilePython(string value);
        [OperationContract]
        string CompilePythonWithInput(string code, string input);

        // Compiler Service For Java Language 
        [OperationContract]
        string CompileJava(string value);
        [OperationContract]
        string CompileJavaWithInput(string code, string input);
    }
}
