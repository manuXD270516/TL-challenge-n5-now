using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.exceptions
{
    public class ValidationException : Exception
    {

        public const string errorMessage = "One or more errors have ocurred";

        public List<string> _errors { get; }

        public ValidationException() : base(errorMessage)
        {
            _errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            _errors = failures.Select(failure => failure.ErrorMessage)
                .ToList();
        }
    }
}
