namespace AssemblyReflector.Library.Models;

public class ReflectionData
{
    public IList<ControllerData> ControllersData { get; set; } 
        = new List<ControllerData>();
}
