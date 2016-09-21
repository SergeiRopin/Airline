using Airline.TemplateMethod;
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
        protected abstract bool AskQuestionCreateEntity();
        protected abstract string ReadAnswerCreateEntity();
        protected abstract bool IsValid(string value);
        protected abstract T CreateEntity(string value);

        protected abstract bool AskQuestionEditEntity();
        protected abstract string ReadAnswerEditEntity(T actualEntity);
        protected abstract T EditEntity(string value, T entity);
        
        /// <summary>
        /// Algorithm of creation an entity
        /// </summary>        
        public T Create()
        {
            if (!AskQuestionCreateEntity())
                throw new EntityCreationCanceledException("Input process has been canceled.");
            var response = ReadAnswerCreateEntity();
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return CreateEntity(response);
        }

        public T Edit(T actualEntity)
        {
            if (!AskQuestionEditEntity())
                throw new EntityCreationCanceledException("Edit process has been canceled.");
            var response = ReadAnswerEditEntity(actualEntity);
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return EditEntity(response, actualEntity);
        }

        protected string AddSeparator(string answer) => $"{answer}|";
    }
}
