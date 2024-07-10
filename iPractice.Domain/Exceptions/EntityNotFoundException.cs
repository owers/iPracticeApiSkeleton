namespace iPractice.Domain.Exceptions;

/// <summary>
/// Represents an exception for an entity not found.
/// </summary>
public class EntityNotFoundException(string entityName, long id)
    : Exception($"Entity was not found. {new { entityName, id }}");