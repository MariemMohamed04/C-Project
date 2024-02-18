using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Game
    {
        Client player1;
        Client player2;

        public Game(Client plyr1, Client plyr2)
        {
            this.player1 = plyr1;
            this.player2 = plyr2;
        }

        public void StopGame(Client player)
        {
            if (player1.ClientID == player.ClientID)
                Console.WriteLine("Player 1 lost, Player 2 won");
            else if (player2.ClientID == player.ClientID)
                Console.WriteLine("Player 1 won, Player 2 lost");
        }
    }
}
