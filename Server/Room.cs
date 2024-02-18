using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Room
    {
        Client player1;
        Client player2;
        Dictionary<string, Client> spectators;
        Game game;
        int numberOfPlayers;
        string roomID;

        int NumberOfPlayers { get { return numberOfPlayers; } }
        string RoomID { get { return roomID; } }

        public Room(Client player, string ID)
        {
            this.player1 = player;
            this.roomID = ID;
            this.numberOfPlayers = 1;
        }

        public void AddPlayer(Client player)
        {
            if (this.player1 == null)
                this.player1 = player;
            else
                this.player2 = player;
            this.numberOfPlayers++;
            if (numberOfPlayers == 2)
            {
                StartGame();
            }
        }

        public void RemovePlayer(Client player)
        {
            if (this.player1.ClientID == player.ClientID)
            {
                EndGame(player1);
                this.player1 = null; //////
            }
            else if (this.player2.ClientID == player.ClientID)
            {
                EndGame(player2);
                this.player2 = null; //////
            }
        }
        public void AddSpectator(Client specClient) //boolean or void return type
        {
            this.spectators.Add(specClient.ClientID, specClient);
        }

        public void RemoveSpectator(Client specClient)
        {
            Client ClientSearched;
            if (spectators.TryGetValue(specClient.ClientID, out ClientSearched))
            {
                spectators.Remove(specClient.ClientID);
            }
        }

        private void StartGame()
        {
            game = new Game(player1, player2);
        }

        private void EndGame(Client player)
        {
            if(numberOfPlayers == 2)
            {
                game.StopGame(player);
                game = null;
            }
        }
    }
}
