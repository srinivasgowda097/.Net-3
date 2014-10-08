using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.Framework.Patterns.COR.Abstract
{
    /// <summary>
    /// The responsibility base.
    /// </summary>
    /// <typeparam name="T">Type for responsible classes
    /// </typeparam>
    internal class CorBase<T, TReqeust , TReturnType> where T : class
    {
        /// <summary>
        /// The successor in the chain.
        /// </summary>
        private readonly T successor;

        protected TReqeust request;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorBase{T}"/> class.
        /// </summary>
        /// <param name="nextSuccessor">
        /// The next successor.
        /// </param>
        public CorBase(T nextSuccessor)
        {
            this.successor = nextSuccessor;
        }

        public TReturnType MapCommand<TRequest>()
        {
            return default(TReturnType);
        }

        /// <summary>
        /// Inners the execute.
        /// </summary>
        /// <returns>Executable command</returns>
        protected Func<object, TReturnType> InnerExecute;

        /// <summary>
        /// Determines whether the specified request is responsible.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the specified request is responsible; otherwise, <c>false</c>.
        /// </returns>
        protected Func<T, bool> IsResponsible;
    }
}
