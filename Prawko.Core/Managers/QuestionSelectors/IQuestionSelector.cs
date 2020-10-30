using Prawko.Core.Managers.Models;
using System.Collections.Generic;

namespace Prawko.Core.Managers.QuestionSelectors
{
    public interface IQuestionSelector
    {
        IEnumerable<Question> SelectFrom(IEnumerable<Question> questions);
    }
}
