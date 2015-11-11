using System;
using EloBuddy.SDK.Events;
using SimpleSivir.Model;

namespace SimpleSivir
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            Sivir.Initiliaze();
        }
    }
}