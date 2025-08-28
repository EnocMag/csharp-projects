// See https://aka.ms/new-console-template for more information
using ExtensionMethods;

Console.WriteLine("Hello, World!");
var forte = new Car();

forte.SetBrand("KIA")
    .SetColor("RED")
    .SetYear(2020);

Console.WriteLine($"El auto es de la marca {forte.Brand}");

