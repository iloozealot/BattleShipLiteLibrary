using BattleShipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiteLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = ["A", "B", "C", "D", "E"];

            List<int> numbers = [1, 2, 3, 4, 5];

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {

                }
            }
        }
        
        static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerInfoModel model, string? location)
        {
            throw new NotImplementedException();
        }

        public static bool PlayerStillActive(PlayerInfoModel opponent)
        {
            throw new NotImplementedException();
        }
    }
}
