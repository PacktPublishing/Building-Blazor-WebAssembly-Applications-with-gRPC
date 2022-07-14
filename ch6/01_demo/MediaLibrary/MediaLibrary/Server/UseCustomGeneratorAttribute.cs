namespace MediaLibrary.Server;
[AttributeUsage(AttributeTargets.Class)]
public class UseCustomGeneratorAttribute : Attribute
{
    public bool GenerateConstructor { get; set; }

    public UseCustomGeneratorAttribute(bool generateConstructor)
    {
        GenerateConstructor = generateConstructor;
    }
}
