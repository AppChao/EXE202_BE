using System;
using System.IO;
using System.Linq;

string modelFolder = @"../EXE202_BE.Data/Models";
string ioutputFolder = @"../EXE202_BE.Service/Interfaces";
string routputFolder = @"../EXE202_BE.Service/Services";
string namespacePrefix = "EXE202_BE.Data";
string namespacePrefix2 = "EXE202_BE.Service";

if (!Directory.Exists(ioutputFolder))
    Directory.CreateDirectory(ioutputFolder);

foreach (var file in Directory.GetFiles(modelFolder, "*.cs"))
{
    var className = Path.GetFileNameWithoutExtension(file);

    if (className.StartsWith("I") || className.Contains("<") || className.Contains("Base"))
        continue;

    var interfaceName = $"I{className}Service";
    var classRepoName = $"{className}Service";

    string interfaceCode = $@"
using {namespacePrefix}.Models;

namespace {namespacePrefix2}.Interface;

public interface {interfaceName}
{{
    // Add custom methods here
}}
";

    string classCode = $@"
using {namespacePrefix}.Models;

namespace {namespacePrefix2}.Services;

public class {classRepoName} : {interfaceName}
{{
    public {classRepoName}()
    {{
    }}
}}
";

    File.WriteAllText(Path.Combine(ioutputFolder, $"{interfaceName}.cs"), interfaceCode.Trim());
    File.WriteAllText(Path.Combine(routputFolder, $"{classRepoName}.cs"), classCode.Trim());

    Console.WriteLine($"✔ Generated {interfaceName} and {classRepoName}");
}

Console.WriteLine("✅ Done generating all repositories!");