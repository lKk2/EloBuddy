using System;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Veigar
{

    #region do not open this region

    /*
    i told ya


                             `-:+//:::/+o++o+++/-.`                        
                        `-:+sydmmmdyssyhhdmmmddddhyso/oo/:.`              
                   `.:+o+hhhhdmmmmdddmddddmmmmddmmdddmhhhhhy+-`           
                 -:ohdhhdhyydmmdhdmmddddmmmmdhhddmddmmdmmdyyyys+-         
             `-+syshysydmhyyhhhhddddddhhhhhhdhdmmddmmmmhyyhhdhyyhy/`      
           `-+syyohysyddhoysosydhdhyyddmdddddhmmmddmmmdhhddmmmmmmmmhy/`   
         `-oyosyyhhsshhhsoosyhdmdyydmmmdmmmmmdmNmmmmmmhdmmmmmmmmmmNmmmy-  
        ./shyyyhhhysyhhysyddmmmdydmmmddmmmmNmmmmmmmmmdmmmmmmmmmmmmmmmmNy` 
      `-+syyhyyyhyhyhddhdddhhhdmddmmmmmmmmmmmmmmmmmmmdmmmmmmmmmmNNmNNmNd+ 
      :oshyyyyyyyhhddmmddhhddmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmNNNmNNms 
     .oyyhysyyhhhddmmmmmdmmmmmmmmmmmmmmmmmmmmmmmdddddmmmmmmmmmmmmNNNNNNd+ 
     +yyyysshhdddmdmmmmmNmmmmmmmmmmmmmmmmmmmmddhhhyyyyhhddmmmmmmmmNmmmNmy 
    .yhhhhyddddmmmmmmmmNmNmmmmmmmmmmmmmmddhhysoooo++o+ossyhhdmmmmNNmmNmNd`
    oddddddmdmmmmmmmmNmmmmmmmmmmmmmmmmddhyo+//://///++++ooosyhdmmNNmNNNNm-
    dmmmmmmmmmmmmmmNNNNNNmmmmmmdddmmdhsso+/:::/://///+++++++oyhdmmNNNNNNm:
    mmmmmmmmmmmmmmmNmNNNmmmmmdhhhhhddo++//:::::://///++++++osyhhddmNmNNNm:
    mmmmmmmmmmNmmmNNmmmmmmmddhs+++oyy+///:::::::////+++++ooosyhhhhdmNNNNm:
    dmmmmmmmNmmmmmNNmmmmmmmdddds+//+os/+::--::://+++++++++oossyyyhhdmNNNm`
    ymmmmmNNmmmmmmmmdddddddhs+oos+:::/+/:---:://////+///+++oossyyyhhmmNNm`
    +mmmmmNNNNmmmmmmmddhy+++/::-::------:---::///////::////+oossyyhhmmNNd 
    -dNmmNmmmmmmmmmmmdhs+:::-------------::://+++++///://++oossyyyhhdmNNh 
     ymNmmNNmmmmmmmmho+::------..--::/++ossssssoooo++ooossyyyhhhhhhhhmNms 
     -hNNmNmNmmmmmds/::--------::/oyyhdmmmmmddhysoossyhddmmdmmmmmdmmddmd:  KAPPA !
     `/shmNmmmmmmmo/:----:/+oosyhhdddmmmmmmmmmdhsooyhdmmmmmmmmmmmmmmmdmy  
     :///sdNmmmmmh------:+oyhddmddmmmmmmmmmmmmdho//ohmmmmNNNmmmmmmmmddds  
     ohdyssdmmmmdo------:+oyhddddddddddddddhyyyo/-.-+mmmmmmmmmmmmmmdhhy+  
     :ss+/+shmmmdo-----..--:/++osyyhhhhdddhyo++/:-..-yddddddddddddhhyyo-  
     ./+//oddyhdh+:----.......--:/+oosssso+////::-..-+hdhhhhhhhhyyyysy+.  
     `:::+yoo:-/+:::----...`````..--::::---:::-:--.`./shhyssssssoosssyo.  
      `-::+:/-..--::::----.....```........-:::----.``-/syyso++oo+osyydo.  
       `-:-..:-.-::::::-:-----.........--://::----.`.-:oyhysooooosshhdo`  
        `.......-:::::::::::::::::::::/++o+:.....-...:/ossyyyyssyyyddmo`  
         ````..-::::::::::::////////+osso++++so++////ooyhhhhhhhhhhhddd+   
          .-...-::::::::::///+++++oosss+///+sydddddhhhddmddhhhdhhddddd/   
          `/////:/::::::://///++oossso+::::://+osyhdmmmmmdddhhhhhdddmy:   
            `..../+//::::///////+oo+///:////++ooosyhddmmddddhhhhhhhdm+.   
                 `o+///////////::////++oooooooossyhhhddddddddhysyhhdd:    
                  ./+++/+++/////:::/+syyyhhyyhhdddddmmmmmmmdhysyhddm+`    
                   `-+oooo++++++++/+++/::///+++ossyyhhhdhhhyyyhhddmy.     
                     `/ssssosssssssso/:---::/+osyyyyhhhhhyyyddddmdy`      
                       .shyyyyyhhhhys+/:://+oosyhhddddhhhhhhddmmh+        
                        `+yhhhhhhhyyo+++/+oossssyyhhddhhhhhddmmy/         
                          .:shhhyyssoo+++++++ooooossyyyysyhdmd+.          
                            `-ohhhyyssso++//::://+ooosssyhdm+`            
                               `+shhhhyhysssooossyyyyhhhdmd-              
                                  ./shhddddddddddddddmmmmy-               
                                     `-/syhhyhyhhhhyyo+/.`         

    */

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "veigar") return;
            Brain.Init();
        }
    }
}