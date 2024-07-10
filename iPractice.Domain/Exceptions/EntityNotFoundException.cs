namespace iPractice.Domain.Exceptions;

public class EntityNotFoundException(string entityName, long id)
    : Exception($"Entity was not found. {new { entityName, id }}");