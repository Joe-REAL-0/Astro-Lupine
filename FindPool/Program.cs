using System;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {
            var asm = Assembly.LoadFrom(@"D:\SteamLibrary\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.dll");
            foreach (var t in asm.GetTypes())
            {
                if (t.Name.Contains("Pool", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Type found: " + t.FullName);
                }
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            foreach (var t in ex.Types)
            {
                if (t != null && t.Name.Contains("Pool", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Type found in ex: " + t.FullName);
                }
            }
        }
    }
}
