using System;
using System.IO;
using System.Linq;

string modelFolder = @"../EXE202_BE.Data/Models";
string ioutputFolder = @"../EXE202_BE.Repository/Interface";
string routputFolder = @"../EXE202_BE.Repository/Repositories";
string namespacePrefix = "EXE202_BE.Data";
string namespacePrefix2 = "EXE202_BE.Repository";

if (!Directory.Exists(ioutputFolder))
    Directory.CreateDirectory(ioutputFolder);

foreach (var file in Directory.GetFiles(modelFolder, "*.cs"))
{
    var className = Path.GetFileNameWithoutExtension(file);

    if (className.StartsWith("I") || className.Contains("<") || className.Contains("Base"))
        continue;

    var interfaceName = $"I{className}Repository";
    var classRepoName = $"{className}Repository";

    string interfaceCode = $@"
using {namespacePrefix}.Models;

namespace {namespacePrefix2}.Interface;

public interface {interfaceName} : IGenericRepository<{className}>
{{
    // Add custom methods here
}}
";

    string classCode = $@"
using {namespacePrefix}.Models;

namespace {namespacePrefix2}.Repositories;

public class {classRepoName} : GenericRepository<{className}>, {interfaceName}
{{
    public {classRepoName}(AppDbContext context) : base(context)
    {{
    }}
}}
";

    File.WriteAllText(Path.Combine(ioutputFolder, $"{interfaceName}.cs"), interfaceCode.Trim());
    File.WriteAllText(Path.Combine(routputFolder, $"{classRepoName}.cs"), classCode.Trim());

    Console.WriteLine($"✔ Generated {interfaceName} and {classRepoName}");
}

Console.WriteLine("✅ Done generating all repositories!");