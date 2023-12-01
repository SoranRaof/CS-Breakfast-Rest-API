using ErrorOr;

namespace Breakfast.ServiceErrors;

public static class Errors
{
        public static class Breakfast
        {
            public static Error InvalidName => Error.Validation(
                code: "Breakfast.InvalidName",
                description: $"Name must be between {Models.BallinBreakfast.MinNameLength} and {Models.BallinBreakfast.MaxNameLength} characters long"
                );
            public static Error InvalidDescription => Error.Validation(
                code: "Breakfast.InvalidName",
                description: $"Description must be between {Models.BallinBreakfast.MinDescriptionLength} and {Models.BallinBreakfast.MaxDescriptionLength} characters long"
                );
            public static Error NotFound => Error.NotFound(
                code: "Breakfast.NotFound",
                description: "Breakfast not found"
                );
        }
    }