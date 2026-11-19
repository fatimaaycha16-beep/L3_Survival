using System;
using System.IO;

namespace L3_Survival;

public static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            using (var game = new Game1())
                game.Run();
        }
        catch (Exception e)
        {
            // On affiche l'erreur dans la console et on attend
            Console.WriteLine("ERREUR : " + e.Message);
            Console.WriteLine("Appuie sur Entrée pour fermer...");
            Console.ReadLine(); 
        }
    }
}