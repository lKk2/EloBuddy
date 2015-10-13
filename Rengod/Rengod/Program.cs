using EloBuddy.SDK.Events;
using Rengod.Core;

namespace Rengod
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Rengar.Init;
        }
    }
}