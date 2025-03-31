using System.Reflection;

namespace AssemblyReflector.Library;

public static class Utilities
{
    public static bool InheritsFromType(Type type, string baseType)
    {
        if(type == null)
        {
            return false;
        }

        while (!string.IsNullOrEmpty(type?.BaseType?.Name))
        {
            if (type.BaseType.Name == baseType)
            {
                return true;
            }
            type = type.BaseType;

            if(type == null)
            {
                return false;
            }
        }

        return false;
    }

    public static bool IsHttpAttribute(Type attributeType)
    {
        string attributeName = attributeType.Name;

        bool result = (attributeName == HttpAttributes.Get) ||
            (attributeName == HttpAttributes.Post) ||
            (attributeName == HttpAttributes.Put) ||
            (attributeName == HttpAttributes.Delete) ||
            (attributeName == HttpAttributes.Patch);

        return result;
    }

    public static string GetHttpAttributeName(MethodInfo method)
    {
        var httpAttribute = method
                            .GetCustomAttributes()
                            .FirstOrDefault(a => IsHttpAttribute(a.GetType()));

        var actionName =  httpAttribute?.GetType().Name;

        return actionName ?? string.Empty;
    }
}
