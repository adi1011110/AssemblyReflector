using AssemblyReflector.Library.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AssemblyReflector.Library.Service;

public class AssemblyReflectorService : IAssemblyReflectorService
{
    public string Reflect(AssemblyConfig assemblyConfig)
    {
        try
        {
            string sampleDllPath = "";
            if (assemblyConfig.IsAssemblyIncluded)
            {
                sampleDllPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                assemblyConfig.AssemblyPath);
            }
            else
            {
                sampleDllPath = assemblyConfig.AssemblyPath;
            }

            var assembly = Assembly.LoadFrom(sampleDllPath);
            var result = _ExtractAssemblyData(assembly);
            return result;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Sorry, couldn't process the assembly. \n {ex.Message}");
        }

        return "";
    }

    private string _ExtractAssemblyData(Assembly assembly)
    {
        string baseControllerName = "BaseController";

        var controllerTypes = assembly
            .GetTypes()
            .Where(t => t.IsPublic &&
                t.IsClass &&
                (!t.IsAbstract) &&
                Utilities.InheritsFromType(t, baseControllerName) &&
                t.Name.EndsWith("Controller"))
            .ToList();

        ReflectionData reflectionData = new();

        controllerTypes.ForEach(t =>
        {
            string controllerName = t.Name;
            string controllerNameFiltered = controllerName.EndsWith("Controller")
            ? controllerName.Substring(0, controllerName.Length - "Controller".Length)
            : controllerName;

            var endpointTypes = t.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(m => m.GetCustomAttributes()
                                              .Any(a => Utilities.IsHttpAttribute(a.GetType())))
                                 .ToList();

            var endpointNames = endpointTypes.Select(e => e.Name).ToList();

            var endpointData = endpointTypes
            .Select(e => new EndpointData
            {
                HttpMethod = e.GetCustomAttributes()
                 .FirstOrDefault(a =>
                    Utilities.IsHttpAttribute(a.GetType()))?.GetType().Name.Replace("Attribute", ""),
                Name = e.Name,
                Params = e.GetParameters().Select(p => p.ParameterType + " " + p.Name)
            });

            var controllerData = new ControllerData
            {
                ControllerName = controllerName,
                Endpoints = endpointData
            };

            reflectionData.ControllersData.Add(controllerData);

        });

        return JsonSerializer.Serialize(reflectionData);
    }
}