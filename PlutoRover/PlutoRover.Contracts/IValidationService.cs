using PlutoRover.Models.Exceptions;

namespace PlutoRover.Contracts
{
    public interface IValidationService
    {
        /// <summary>
        /// Validates command.
        /// </summary>
        /// <param name="command">Input command.</param>
        /// <exception cref="ValidationException">Validation fails.</exception>
        void ValidateCommand(string command);
    }
}
