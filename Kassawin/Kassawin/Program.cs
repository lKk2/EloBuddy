using EloBuddy.SDK.Events;

namespace Kassawin
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Brain.Init;
        }
    }
}