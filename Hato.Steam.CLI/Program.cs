using System;
using Hato.Steam;

namespace Hato.Steam.CLI // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PackageInfo m = new PackageInfo("packageinfo.vdf");

            foreach (int s in m.AppID)
            {
                Console.WriteLine(s);
            }
        }
    }
}