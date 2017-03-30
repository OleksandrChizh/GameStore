using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class CreatingGameDtoValidationAttribute : ArgumentValidationAttribute
    {
        private const int LanguageKeyLength = 2;
        private const int NameLength = 70;
        private const int DescriptionLength = 200;

        private ArgumentExceptionDetails _details;

        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            _details = details;

            var game = details.Value as CreatingGameDto;
            ValidateNames(game.LanguagesNames);
            ValidateDescriptions(game.LanguagesDescriptions);
            ValidatePublishingDate(game.PublishingDate);

            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(game, new ValidationContext(game), result, true))
            {
                var error = result.First();
                _details.Message = $"Member: {error.MemberNames.First()}, message: {error.ErrorMessage}";
            }
        }

        private void ValidatePublishingDate(DateTime publishingDate)
        {
            if (publishingDate > DateTime.UtcNow)
            {
                _details.Message = "Publishing date must be in past";
                throw new InvalidArgumentException(_details);
            }
        }

        private void ValidateNames(Dictionary<string, string> names)
        {
            ValidateDictionary(names, LanguageKeyLength, NameLength);
        }

        private void ValidateDescriptions(Dictionary<string, string> descriptions)
        {
            ValidateDictionary(descriptions, LanguageKeyLength, DescriptionLength);
        }

        private void ValidateDictionary(
            Dictionary<string, string> dictionary, 
            int keyLength,
            int valueLength)
        {
            if (dictionary == null)
            {
                _details.Message = "Dictionary must be not nullable";
                throw new InvalidArgumentException();
            }

            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if (pair.Key.Length > keyLength)
                {
                    _details.Message = "Key length is not available";
                    throw new InvalidArgumentException(_details);
                }

                if (pair.Value.Length > valueLength)
                {
                    _details.Message = "Value length is not available";
                    throw new InvalidArgumentException(_details);
                }
            }
        }
    }
}
