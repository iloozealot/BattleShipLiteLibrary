
using BattleShipLiteLibrary;
using BattleShipLiteLibrary.Models;

WelcomeMessage();

PlayerInfoModel activePlayer = CreatePlayer("Player 1");
PlayerInfoModel opponent = CreatePlayer("Player 2");
PlayerInfoModel winner = null;

do
{
    DisplayShotGrid(activePlayer);

    RecordPlayerShot(activePlayer, opponent);

    bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

    if (doesGameContinue == true)
    {
        //PlayerInfoModel tempHolder = opponent;
        //opponent = activePlayer;
        //activePlayer = tempHolder;

        (activePlayer, opponent) = (opponent, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }
    
} while (winner == null);

IdentifyWinner(winner);

void IdentifyWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulations to { winner.UsersName } for winning!");
    Console.WriteLine($"{ winner.UsersName } took { GameLogic.GetShotCount(winner) } shots.");
}

void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
{
    bool isValidShot = false;
    string row = "";
    int column = 0;
    do
    {
       string shot = AskForShot(activePlayer);
        try
        {
            (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
            isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
        }
        catch (Exception ex)
        {

            Console.WriteLine("Error: " + ex.Message);
            isValidShot = false;
        }

        if (isValidShot == false)
        {
            Console.WriteLine("Invalid Shot Location. Please try again.");
        }

    } while (isValidShot == false);

    bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

    GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
    
    DisplayShotResults(row, column, isAHit);
}

static void DisplayShotResults(string row, int column, bool isAHit)
{
    if (isAHit)
    {
        Console.WriteLine($"{ row }{ column } is a Hit!"); 
    }
    else
    {
        Console.WriteLine($"{ row }{ column } is a miss.");
    }

    Console.WriteLine();
}

static string AskForShot(PlayerInfoModel player)
{
    Console.Write($"{ player.UsersName }, please enter your shot selection: ");
    string output = Console.ReadLine();

    return output;
}

void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShotGrid[0].SpotLetter;

    foreach (var gridSpot in activePlayer.ShotGrid)
    {
        if (gridSpot.SpotLetter != currentRow)
        {
            Console.WriteLine();
            currentRow = gridSpot.SpotLetter;
        }

        if (gridSpot.Status == GridSpotStatus.Empty)
        {
            Console.Write($"{gridSpot.SpotLetter}{gridSpot.SpotNumber}");
        }
        else if (gridSpot.Status == GridSpotStatus.Hit)
        {
            Console.Write("X ");
        }
        else if (gridSpot.Status == GridSpotStatus.Miss)
        {
            Console.Write("O ");
        }
        else
        {
            Console.Write("? ");
        }
    }
    Console.WriteLine();
    Console.WriteLine();
}

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to BattleShip Lite");
    Console.WriteLine("This application was created by: Justin Spencer\n");

}

static PlayerInfoModel CreatePlayer(string playerTitle)
{
    PlayerInfoModel output = new PlayerInfoModel();

    Console.WriteLine($"Player information for { playerTitle }");

    output.UsersName = AskForUsersName();

    GameLogic.InitializeGrid(output);

    PlaceShips(output);

    Console.Clear();

    return output;

}

static string AskForUsersName()
{
    Console.Write("What is your first name: ");
    string output = Console.ReadLine();

    return output;
}

static void PlaceShips(PlayerInfoModel model)
{
    do
    {
        Console.Write($"Where would you like to place ship number {model.ShipLocations.Count + 1}: ");
        string location = Console.ReadLine();

        bool isValidLocation = false;

        try
        {
            isValidLocation = GameLogic.PlaceShip(model, location);
        }
        catch (Exception ex)
        {

            Console.WriteLine("Error: " + ex.Message);
        }

        if (isValidLocation == false)
        {
            Console.WriteLine("That was not a valid location. Please try again.");
        }

    } while (model.ShipLocations.Count < 5);
}

Console.ReadLine();