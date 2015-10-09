using EloBuddy.SDK.Events;

namespace SwagZilean
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Brain.Init;
        }
    }
}