namespace AssemblyReflector.Library.Models;

public class EndpointData 
{ 
    public string HttpMethod { get; init; }
    public string Name { get; init; }
    public IEnumerable<string> Params { get; init; }
}