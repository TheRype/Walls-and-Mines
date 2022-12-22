class Program2
{
    static void Main2(string[] args) // out of index error for gameField while moving
    {
        char[,] gameField = new char[3 + 53, 3 + 23]; // +3 because of where they start
        
        for(int i = 0; i < gameField.GetLength(0); i++)
            for(int j = 0; j < gameField.GetLength(1); j++)
            {
                if (i == 3 || j == 3 || i == 55 || j == 25) gameField[i, j] = '#';
                else gameField[i, j] = ' ';
            }

        for (int i = 0; i < gameField.GetLength(0); i++)
            for (int j = 0; j < gameField.GetLength(1); j++)
            {
                Console.Write(gameField[i, j]);
                if (j == 25) Console.WriteLine();
            }

        Console.WriteLine("origin: " + gameField[3,3]);
        Console.WriteLine(gameField[55,25]);

        Console.ReadLine();

    }
}
