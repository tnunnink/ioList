using System;
using System.Linq.Expressions;

namespace ioList.Entities;

public class TagFilter
{
    public TagFilter()
    {
    }

    public TagFilter(string propertyName, FilterOperator filterOperator, string value)
    {
        PropertyName = propertyName;
        FilterOperator = filterOperator;
        Value = value;
    }

    public string PropertyName { get; set; }
    public FilterOperator FilterOperator { get; set; }
    public string Value { get; set; }
    

    public Expression<Func<DeviceTag, bool>> ToPredicate()
    {
        var parameter = Expression.Parameter(typeof(DeviceTag), "p");
        var property = Expression.Property(parameter, PropertyName);
        var constant = Expression.Constant(Value);

        Expression expression = FilterOperator switch
        {
            FilterOperator.Is => Expression.Equal(property, constant),
            FilterOperator.IsNot => Expression.NotEqual(property, constant),
            FilterOperator.Contains => Expression.Call(property, "Contains", Type.EmptyTypes, constant),
            _ => throw new NotSupportedException($"Operator {FilterOperator} is not supported.")
        };

        return Expression.Lambda<Func<DeviceTag, bool>>(expression, parameter);
    }
}