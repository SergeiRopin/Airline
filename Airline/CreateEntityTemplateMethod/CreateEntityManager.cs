using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.TemplateMethod
{
    /// <summary>
    /// Create entity manager
    /// </summary>
    /// <exception cref="EntityCreationCanceledException"/>
    /// <exception cref="InvalidEntityInputException"/>
    abstract class CreateEntityManager<T> where T : class
    {
        protected abstract bool AskQuestion();
        protected abstract string ReadAnswer();
        protected abstract bool IsValid(string value);
        protected abstract T CreateEntity(string value);
        protected abstract bool AskEditQuestion();
        protected abstract string ReadEditAnswer(T actualEntity);

        /// <summary>
        /// Algorithm of creation an entity
        /// </summary>        
        public T Create()
        {
            if (!AskQuestion())
            {
                throw new EntityCreationCanceledException("Input process has been canceled.");
            }
            var response = ReadAnswer();
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return CreateEntity(response);
        }

        public T Edit(T actualEntity)
        {
            if (!AskEditQuestion())
            {
                throw new EntityCreationCanceledException("Edit process has been canceled.");
            }
            var response = ReadEditAnswer(actualEntity);
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return CreateEntity(response);
        }

        protected string AddSeparator(string answer) => $"{answer}|";
    }
}
