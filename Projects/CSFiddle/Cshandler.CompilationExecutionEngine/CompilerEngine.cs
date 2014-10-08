using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace Cshandler.CompilationExecutionEngine
{
    /// <summary>
    /// The compiler engine.
    /// </summary>
    public class CompilerEngine : IDisposable
    {
        private string code;

        private string mainBlockStatements;

        private string assemblyFile;

        private string tempFolder = "C:\temp";

        private string result = string.Empty;

        public string ErrorDescription { get; set; }

        public CompilerEngine(string code, string mainBlockStatements)
        {
            this.code = code;
            this.mainBlockStatements = mainBlockStatements;
        }

        /// <summary>
        /// The end to end compile and run.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string EndToEndCompileAndRun()
        {
            string mainBlock = @"public class Program
                            {
                                public static void Main(string[] args)
                                {
                                    $
                                }

                                private static string output = """";

                                public static string GetResult()
                                {
                                    return output;
                                }

                                public static void Print(string statement)
                                {
                                  output = System.String.Concat(output, ""<br>"","">"", statement);
                                }
                            }";

            mainBlock = mainBlock.Replace("$", this.mainBlockStatements);

            this.code = string.Concat(this.code, Environment.NewLine, mainBlock);

            var tree = SyntaxTree.ParseText(this.code);

            /*
            assemblyFile = string.Format("{0}.dll", Guid.NewGuid());
            var compilation = Compilation.Create(
                assemblyFile,
                options: new CompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                syntaxTrees: new[] { tree },
                references: new[] { new MetadataFileReference(typeof(object).Assembly.Location) });
            */

            // compiler section starts
            this.assemblyFile = string.Format("{0}.exe", Guid.NewGuid());
            Compilation compilation = Compilation
            .Create(this.assemblyFile)
            .AddSyntaxTrees(tree)
            .AddReferences(new[]
                {
                    new MetadataFileReference(typeof(Console).Assembly.Location), 
                    new MetadataFileReference(typeof(object).Assembly.Location), 
                    new MetadataFileReference(typeof(IEnumerable<>).Assembly.Location),
                    new MetadataFileReference(typeof(IQueryable).Assembly.Location),
                    new MetadataFileReference(this.GetType().Assembly.Location)
                }.ToList());

            // .AddReferences(new MetadataFileReference(typeof(object).Assembly.Location)
            IEnumerable<Diagnostic> errorsAndWarnings = compilation.GetDiagnostics().ToList();

            if (errorsAndWarnings.Any())
            {
                StringBuilder errors = new StringBuilder();

                foreach (var errorsAndWarning in errorsAndWarnings)
                {
                    errors.AppendFormat("Error: {0}{1}", errorsAndWarning.Info.GetMessage(CultureInfo.InvariantCulture), "<br\\>");
                    //// errors.AppendFormat("Location: {0}{1}", errorsAndWarning.Location, Environment.NewLine);
                    errors.AppendFormat("{0}{1}", errorsAndWarning.Location.SourceTree.GetText().ToString(errorsAndWarning.Location.SourceSpan), "<br\\>");
                    //// errors.AppendFormat("Line: {0}{1}", errorsAndWarning.Location.GetLineSpan(usePreprocessorDirectives: true), Environment.NewLine);
                }

                this.result = string.Concat(this.result, Environment.NewLine, errors.ToString());
                return this.result;
            }
            
            // Compiler section ends
            Assembly compiledAssembly;
            using (var stream = new MemoryStream())
            {
                EmitResult compileResult = compilation.Emit(stream);
                compiledAssembly = Assembly.Load(stream.GetBuffer());
            }

            
            Type mainContainerType = compiledAssembly.GetType("Program");
            var instance = Activator.CreateInstance(mainContainerType);
            MethodInfo evaluate = mainContainerType.GetMethod("Main");
            MethodInfo getResult = mainContainerType.GetMethod("GetResult");

            try
            {
                evaluate.Invoke(instance, new object[] { null });
                result = getResult.Invoke(instance, null).ToString();
            }
            catch (Exception ex)
            {
                var actualException = ex.InnerException ?? ex;
                var error = string.Format("Exception: {0}", actualException.Message);
                this.result = string.Concat(this.result, Environment.NewLine, ">", error);
                return this.result;
            }

            return result;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            File.Delete(Path.Combine(this.tempFolder, this.assemblyFile));
        }
    }
}
