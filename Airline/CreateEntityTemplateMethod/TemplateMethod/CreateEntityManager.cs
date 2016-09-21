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
        protected abstract bool AskQuestionToCreate();
        protected abstract string ReadAnswerToCreate();
        protected abstract bool IsValid(string value);
        protected abstract T CreateEntity(string value);
        protected abstract bool AskQuestionToEdit();
        protected abstract string ReadAnswerToEdit(T actualEntity);
        protected abstract T EditEntity(string value, T entity);
        
        /// <summary>
        /// Algorithm of creation an entity
        /// </summary>        
        public T Create()
        {
            if (!AskQuestionToCreate())
                throw new EntityCreationCanceledException("Input process has been canceled.");
            var response = ReadAnswerToCreate();
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return CreateEntity(response);
        }

        public T Edit(T actualEntity)
        {
            if (!AskQuestionToEdit())
                throw new EntityCreationCanceledException("Edit process has been canceled.");
            var response = ReadAnswerToEdit(actualEntity);
            if (!IsValid(response))
                throw new InvalidEntityInputException("Input value is incorrect!");

            return EditEntity(response, actualEntity);
        }

        protected string AddSeparator(string answer) => $"{answer}|";
    }
}
