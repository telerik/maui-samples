using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Globalization;
using System.Reflection;

namespace QSF.MarkupExtensions;

[ContentProperty(nameof(Default))]
public class OnDotNetExtension : IMarkupExtension
{
    public object Default { get; set; }

    public object Net6 { get; set; }

    public object Net7 { get; set; }

    public IValueConverter Converter { get; set; }

    public object ConverterParameter { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Default == null && this.Net6 == null && this.Net7 == null)
        {
            throw new XamlParseException("OnDotNetExtension requires a non-null value to be specified for at least one dotnet version or Default.");
        }

        var valueProvider = serviceProvider?.GetService<IProvideValueTarget>() ?? throw new ArgumentException();

        BindableProperty bp;
        PropertyInfo pi = null;
        Type propertyType = null;

        if (valueProvider.TargetObject is Setter setter)
        {
            bp = setter.Property;
        }
        else
        {
            bp = valueProvider.TargetProperty as BindableProperty;
            pi = valueProvider.TargetProperty as PropertyInfo;
        }

        propertyType = bp?.ReturnType ?? pi?.PropertyType ?? throw new InvalidOperationException("Cannot determine property to provide the value for.");

        var value = this.GetValue();
        if (value == null && propertyType.IsValueType)
        {
            return Activator.CreateInstance(propertyType);
        }

        var converter = this.Converter;
        if (converter != null)
        {
            return converter.Convert(value, propertyType, this.ConverterParameter, CultureInfo.CurrentUICulture);
        }

        var type = Type.GetType("Microsoft.Maui.Controls.Xaml.TypeConversionExtensions, Microsoft.Maui.Controls");
        var convertTo = type.GetMethod("ConvertTo",
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic, 
                new[] { typeof(Object), typeof(Type), typeof(Func<MemberInfo>), typeof(IServiceProvider), typeof(Exception).MakeByRefType() });

        Exception exception = null;
        var result = convertTo.Invoke(null, new object[] { value, propertyType, () => this.GetMemberInfo(bp, pi), serviceProvider, exception });
        if (exception != null)
        {
            throw exception;
        }

        return result;
    }

    private MemberInfo GetMemberInfo(BindableProperty bp, PropertyInfo pi)
    {
        if (pi != null)
        {
            return pi;
        }

        MemberInfo memberInfo = null;
        try
        {
            memberInfo = bp.DeclaringType.GetRuntimeProperty(bp.PropertyName);
        }
        catch (AmbiguousMatchException e)
        {
            throw new XamlParseException($"Multiple properties with name '{bp.DeclaringType}.{bp.PropertyName}' found.", innerException: e);
        }

        if (memberInfo != null)
        {
            return memberInfo;
        }

        try
        {
            return bp.DeclaringType.GetRuntimeMethod("Get" + bp.PropertyName, new[] { typeof(BindableObject) });
        }
        catch (AmbiguousMatchException e)
        {
            throw new XamlParseException($"Multiple methods with name '{bp.DeclaringType}.Get{bp.PropertyName}' found.", innerException: e);
        }
    }

    private object GetValue()
    {
#if NET6_0
        return this.Net6 ?? this.Default;
#elif NET7_0
        return this.Net7 ?? this.Default;
#else
        return this.Default;
#endif
    }
}
