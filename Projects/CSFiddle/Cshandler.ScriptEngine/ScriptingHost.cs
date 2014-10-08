using System;
using System.Collections.Generic;
using System.Linq;

using Roslyn.Scripting;

namespace Cshandler.ScriptEngine
{
    /// <summary>
    /// The scripting host class contains the scritp engine and session execute methods.
    /// </summary>
    public class ScriptingHost
    {
        /// <summary>
        /// The script engine instance reference
        /// </summary>
        private Roslyn.Scripting.CSharp.ScriptEngine engine;

        /// <summary>
        /// The session created via script engine
        /// </summary>
        private Session session;

        /// <summary>
        /// The report of host object.
        /// </summary>
        private Report report;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptingHost"/> class.
        /// </summary>
        /// <param name="report">
        /// Includes the report object. The report will work as Host object in this case.
        /// </param>
        public ScriptingHost(Report report)
        {
            this.report = report;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptingHost"/> class.
        /// Methods in the Host object can be called directly from the environment 
        /// </summary>
        public ScriptingHost()
        {
            this.Initialize();
        }

        /// <summary>
        /// The execute method.
        ///  Pass the code to the engine, nothing much here 
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Execute(string code)
        {
            return this.session.Execute(code);
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <typeparam name="T"> Type of callee
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>. Type of callee
        /// </returns>
        public T Execute<T>(string code)
        {
            return this.session.Execute<T>(code);
        }

        /// <summary>
        /// The initialize method of script host class.
        /// </summary>
        private void Initialize()
        {
            // Create the script engine 
            // Script engine constructor parameters go changed 
            this.engine = new Roslyn.Scripting.CSharp.ScriptEngine();

            // Let us use engine's Addreference for adding the required 
            // assemblies 
            new[]
                {
                    typeof(Console).Assembly, typeof(ScriptingHost).Assembly, typeof(IEnumerable<>).Assembly,
                    typeof(IQueryable).Assembly, this.GetType().Assembly
                }.ToList()
                 .ForEach(asm => this.engine.AddReference(asm));
            new[] { "System", "System.Linq", "System.Collections", "System.Collections.Generic" }.ToList()
                                                                                                 .ForEach(
                                                                                                     ns =>
                                                                                                     this.engine
                                                                                                         .ImportNamespace(ns));
            /* 
             * Now, you need to create a session using engine's CreateSession method, 
             * which can be seeded with a host object 
             */
            if (this.report != null)
            {
                this.session = this.engine.CreateSession(this.report);
            }
            else
            {
                this.session = this.engine.CreateSession(this);
            }
        }
    }
}

