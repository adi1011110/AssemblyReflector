namespace AssemblyReflector.Library.Models;

public class ControllerData
{
    public string ControllerName { get; init; }
    public IEnumerable<EndpointData> Endpoints {  get; init; }
}
