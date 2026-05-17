using System.Reflection;
using TicketFlow.Api.Domain.Attributes;
using TicketFlow.Api.Domain.Models;

namespace TicketFlow.Api.Application.Services;

public sealed class ReflectionMetadataService
{
    private static readonly Type[] DomainTypes =
    {
        typeof(Customer),
        typeof(SupportAgent),
        typeof(Ticket),
        typeof(IncidentTicket),
        typeof(ServiceRequestTicket),
        typeof(Money),
        typeof(SlaTime)
    };

    public IReadOnlyList<TypeDescription> DescribeDomain()
    {
        return DomainTypes.Select(DescribeType).ToList();
    }

    private static TypeDescription DescribeType(Type type)
    {
        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => new PropertyDescription(
                property.Name,
                GetFriendlyTypeName(property.PropertyType),
                property.CanRead,
                property.CanWrite,
                property.GetCustomAttribute<MetaDescriptionAttribute>()?.Description
            ))
            .ToList();

        var constructors = type
            .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .Select(constructor => string.Join(
                ", ",
                constructor.GetParameters().Select(parameter =>
                    $"{GetFriendlyTypeName(parameter.ParameterType)} {parameter.Name}"
                )
            ))
            .ToList();

        return new TypeDescription(
            type.Name,
            type.IsAbstract ? "abstract class" : type.IsValueType ? "struct" : "class",
            type.GetCustomAttribute<MetaDescriptionAttribute>()?.Description,
            constructors,
            properties
        );
    }

    private static string GetFriendlyTypeName(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }

        var genericName = type.Name[..type.Name.IndexOf('`')];
        var arguments = string.Join(", ", type.GetGenericArguments().Select(GetFriendlyTypeName));
        return $"{genericName}<{arguments}>";
    }
}

public sealed record TypeDescription(
    string Name,
    string Kind,
    string? Description,
    IReadOnlyList<string> Constructors,
    IReadOnlyList<PropertyDescription> Properties
);

public sealed record PropertyDescription(
    string Name,
    string Type,
    bool CanRead,
    bool CanWrite,
    string? Description
);
