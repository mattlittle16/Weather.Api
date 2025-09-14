namespace Core.ResponseModels;

public record struct FriendlyExceptionResponse (string Title, int Status, string Details, string Instance);