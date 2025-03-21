using AssemblyReflector.Library.Models;

namespace AssemblyReflector.Library.Service;

public interface IAssemblyReflectorService
{
    string Reflect(AssemblyConfig assemblyConfig);
}
