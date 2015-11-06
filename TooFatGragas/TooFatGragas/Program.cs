using System;
using EloBuddy.SDK.Events;
using TooFatGragas.Model;

namespace TooFatGragas
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingOnOnLoadingComplete;
        }

        private static void LoadingOnOnLoadingComplete(EventArgs args)
        {
            new Gragas().Init();
        }
    }
}