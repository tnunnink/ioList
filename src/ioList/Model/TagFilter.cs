using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ioList.Model;

public partial class TagFilter : ObservableObject
{
    public TagFilter()
    {
    }

    public TagFilter(string propertyName, FilterCondition condition, string value)
    {
        PropertyName = propertyName;
        Condition = condition;
        Value = value;
    }

    [ObservableProperty]
    private bool _enabled = true;
    
    [ObservableProperty]
    private string _propertyName = "Module";
    
    [ObservableProperty]
    private FilterCondition _condition;
    
    [ObservableProperty]
    private string _value;

    public Expression<Func<DeviceTag, bool>> ToPredicate()
    {
        var parameter = Expression.Parameter(typeof(DeviceTag), "p");
        var property = Expression.Property(parameter, PropertyName);
        var constant = Expression.Constant(Value);

        Expression expression = Condition switch
        {
            FilterCondition.Equals => Expression.Equal(property, constant),
            FilterCondition.NotEqual => Expression.NotEqual(property, constant),
            FilterCondition.Contains => Expression.Call(property, "Contains", Type.EmptyTypes, constant),
            FilterCondition.DoesNotContain => Expression.Not(Expression.Call(property, "Contains", Type.EmptyTypes,
                constant)),
            FilterCondition.StartsWith => Expression.Call(property, "StartsWith", Type.EmptyTypes, constant),
            FilterCondition.DoesNotStartWith => Expression.Not(Expression.Call(property, "StartsWith", Type.EmptyTypes,
                constant)),
            FilterCondition.EndsWith => Expression.Call(property, "EndsWith", Type.EmptyTypes, constant),
            FilterCondition.DoesNotEndWith => Expression.Not(Expression.Call(property, "EndsWith", Type.EmptyTypes,
                constant)),
            FilterCondition.IsEmpty => Expression.Call(property, "Equals", Type.EmptyTypes,
                Expression.Constant(string.Empty)),
            FilterCondition.IsNotEmpty => Expression.Not(Expression.Call(property, "Equals", Type.EmptyTypes,
                Expression.Constant(string.Empty))),
            FilterCondition.IsMatch => Expression.Call(typeof(Regex), "IsMatch", Type.EmptyTypes, property, constant),
            _ => throw new NotSupportedException($"Operator {Condition} is not supported.")
        };

        return Expression.Lambda<Func<DeviceTag, bool>>(expression, parameter);
    }
}