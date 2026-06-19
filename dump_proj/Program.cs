using System;
using System.Reflection;
public class Program {
    public static void Main() {
        try {
            var asm = Assembly.Load(System.IO.File.ReadAllBytes(@"D:\SteamLibrary\steamapps\common\Slay the Spire 2\mods\BaseLib\BaseLib.dll"));
            asm.GetTypes();
        } catch(ReflectionTypeLoadException e) {
            foreach(var t in e.Types) {
                if (t != null && t.Name.Contains("Pool")) Console.WriteLine("Type: " + t.FullName);
            }
        }
    }
}
