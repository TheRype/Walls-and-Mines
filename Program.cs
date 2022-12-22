class Program
{
    static void Main(string[] args)
    {
        Console.SetCursorPosition(14, 1);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Press any key to start the game");
        Console.ResetColor();

        Random rnd = new Random();
        int pX, pY;   // locate randomly - after initializing walls
        int testZx, testZy;
        ConsoleKeyInfo cki;               // required for readkey
        int Xx = 13, Xy = 19;    // position of X
        int Yx = 14, Yy = 10;    // position of Y
        int xdir = 1; // X       // direction of A:   1:rigth   -1:left
        int ydir = 1;
        int pdir = 1;            // 1:right -1:left 2:up -2:down 
        int score = 0;
        int energy = 200;
        int mine = 0;
        bool success = true;
        int rndX;
        int rndY;

        // --- Static screen parts
        Console.SetCursorPosition(3, 3);
        Console.WriteLine("#####################################################");
        for (int i = 0; i < 21; i++)
        {
            Console.SetCursorPosition(3, 3 + i + 1);
            Console.WriteLine("#                                                   #");
        }
        Console.SetCursorPosition(3, 25);
        Console.WriteLine("#####################################################");
        Console.SetCursorPosition(57, 3); Console.WriteLine("Time   :");

        Console.SetCursorPosition(57, 5); Console.WriteLine("Score  :");

        Console.SetCursorPosition(57, 7); Console.WriteLine("Energy :");

        Console.SetCursorPosition(57, 9); Console.WriteLine("Mine   :");

        char[,] gameField = new char[3 + 53, 3 + 23]; // +3 because of where they start

        for (int i = 3; i < gameField.GetLength(0); i++)
        {
            for (int j = 3; j < gameField.GetLength(1); j++)
            {
                if (i == 3 || j == 3 || i == 55 || j == 25) gameField[i, j] = '#';
                else gameField[i, j] = ' ';
            }
        }

        int a = 5;
        int b = 5;
        char[] walls = { 'u', 'd', 'l', 'r' }; // wall generation logic
        bool okay;
        int startIndex;
        int count = 0;
        int index;
        int speed;
        bool aliveZ = true;

        for (int i = 0; i < 40; i++)
        {
            startIndex = rnd.Next(4);
            okay = false;
            while (!okay)
            {
                for (int j = 0; j < 4; j++)
                {
                    index = (startIndex + j) % 4;

                    if (rnd.NextDouble() < 0.5)
                    {
                        switch (walls[index])
                        {
                            case 'u':
                                for (int k = 0; k < 4; k++)
                                {
                                    gameField[a + k, b] = '#';
                                    Console.SetCursorPosition(a + k, b);
                                    Console.Write("#");
                                }
                                break;
                            case 'd':
                                for (int k = 0; k < 4; k++)
                                {
                                    gameField[a + k, b + 3] = '#';
                                    Console.SetCursorPosition(a + k, b + 3);
                                    Console.Write("#");
                                }
                                break;
                            case 'l':
                                for (int k = 0; k < 4; k++)
                                {
                                    gameField[a, b + k] = '#';
                                    Console.SetCursorPosition(a, b + k);
                                    Console.Write("#");
                                }
                                break;
                            case 'r':
                                for (int k = 0; k < 4; k++)
                                {
                                    gameField[a + 3, b + k] = '#';
                                    Console.SetCursorPosition(a + 3, b + k);
                                    Console.Write("#");
                                }
                                break;
                        }
                        count++;
                    }
                }
                if (count != 4 && count != 0) okay = true;
                else
                {
                    for (int k = 0; k < 4; k++)
                    {
                        gameField[a + k, b] = ' ';
                        Console.SetCursorPosition(a + k, b);
                        Console.Write(" ");
                        gameField[a + k, b + 3] = ' ';
                        Console.SetCursorPosition(a + k, b + 3);
                        Console.Write(" ");
                        gameField[a, b + k] = ' ';
                        Console.SetCursorPosition(a, b + k);
                        Console.Write(" ");
                        gameField[a + 3, b + k] = ' ';
                        Console.SetCursorPosition(a + 3, b + k); // delete the wrong one (4 walls condition from previous loop)
                        Console.Write(" ");
                    }
                }
                count = 0;
            }
            a += 5;
            if (i == 9 || i == 19 || i == 29) { b += 5; a = 5; }
        }

        do
        {
            rndX = rnd.Next(4, 55);
            rndY = rnd.Next(4, 25);
        } while (gameField[rndX, rndY] != ' ');
        Console.SetCursorPosition(rndX, rndY);
        Console.Write("P");
        pX = rndX;
        pY = rndY;
        gameField[pX, pY] = 'P'; // SO numbers don't overlap
        Console.SetCursorPosition(pX, pY);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("P"); // placing player 
        do
        {
            rndX = rnd.Next(4, 55);
            rndY = rnd.Next(4, 25);
        } while (gameField[rndX, rndY] != ' ');
        testZx = rndX;
        testZy = rndY;
        gameField[testZx, testZy] = 'Z'; // SO numbers don't overlap
        Console.SetCursorPosition(testZx, testZy);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Z"); // placing enemy for the first time before game starts

        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < 20; i++)
        {
            do
            {
                rndX = rnd.Next(4, 55);
                rndY = rnd.Next(4, 25);
            } while (gameField[rndX, rndY] != ' ');
            Console.SetCursorPosition(rndX, rndY);
            double rand = rnd.NextDouble();
            if (rand < 0.6) { gameField[rndX, rndY] = '1'; Console.Write("1"); }
            else if (rand < 0.9) { gameField[rndX, rndY] = '2'; Console.Write("2"); }
            else { gameField[rndX, rndY] = '3'; Console.Write("3"); }
        }
        Console.SetCursorPosition(0, 0);
        Console.ResetColor();

        Console.ReadKey(true);
        var watch = System.Diagnostics.Stopwatch.StartNew();
        Console.SetCursorPosition(14, 1);
        Console.Write("                               "); // deletes 'press any key to start'

        // --- Main game loop
        while (true)
        {
            if (Console.KeyAvailable)
            {       // true: there is a key in keyboard buffer
                cki = Console.ReadKey(true);       // true: do not write character 

                if (cki.Key == ConsoleKey.Spacebar && mine != 0)
                {
                    if (pdir == -1 && gameField[pX + 1, pY] != '#') { Console.SetCursorPosition(pX + 1, pY); gameField[pX + 1, pY] = '+'; }
                    else if (pdir == 1 && gameField[pX - 1, pY] != '#') { Console.SetCursorPosition(pX - 1, pY); gameField[pX - 1, pY] = '+'; }
                    else if (pdir == 2 && gameField[pX, pY + 1] != '#') { Console.SetCursorPosition(pX, pY + 1); gameField[pX, pY + 1] = '+'; }
                    else if (pdir == -2 && gameField[pX, pY - 1] != '#') { Console.SetCursorPosition(pX, pY - 1); gameField[pX, pY - 1] = '+'; }
                    else { success = false; }
                    if (success)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("+");
                        Console.ResetColor();
                        mine--;
                    }
                    success = true;
                }
                if (cki.Key == ConsoleKey.RightArrow)
                {   // key and boundary control

                    if (gameField[pX + 1, pY] != '#')
                    {
                        Console.SetCursorPosition(pX, pY);           // delete X (old position)
                        Console.WriteLine(" ");
                        gameField[pX, pY] = ' '; // so new random things will see its old place as null and be able to be placed there
                        pX++;
                        energy--;
                    }
                    pdir = 1;
                }
                if (cki.Key == ConsoleKey.LeftArrow)
                {
                    if (gameField[pX - 1, pY] != '#')
                    {
                        Console.SetCursorPosition(pX, pY);
                        Console.WriteLine(" ");
                        gameField[pX, pY] = ' ';
                        pX--;
                        energy--;
                    }
                    pdir = -1;
                }
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (gameField[pX, pY - 1] != '#')
                    {
                        Console.SetCursorPosition(pX, pY);
                        Console.WriteLine(" ");
                        gameField[pX, pY] = ' ';
                        pY--;
                        energy--;
                    }
                    pdir = 2;
                }
                if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (gameField[pX, pY + 1] != '#')
                    {
                        Console.SetCursorPosition(pX, pY);
                        Console.WriteLine(" ");
                        gameField[pX, pY] = ' ';
                        pY++;
                        energy--;
                    }
                    pdir = -2;
                }
                if (aliveZ) // enemy
                {
                    Console.SetCursorPosition(testZx, testZy);           // delete Z (old position)
                    Console.WriteLine(" ");
                    gameField[testZx, testZy] = ' ';
                    if (gameField[testZx - 1, testZy] != '#' && gameField[testZx + 1, testZy] != '#')
                    {                                                     // prioritizing x
                        if (gameField[testZx - 1, testZy] != '#' && testZx > pX)
                            testZx--;
                        else if (gameField[testZx + 1, testZy] != '#' && testZx < pX)
                            testZx++;
                    }
                    else
                    {
                        if (gameField[testZx, testZy - 1] != '#' && testZy > pY)
                            testZy--;
                        else if (gameField[testZx, testZy + 1] != '#' && testZy < pY)
                            testZy++;
                    }
                }
                if (cki.Key == ConsoleKey.Escape) break; // Esc ends the game
            }

            if (energy < 0) energy = 0; // energy can't be minus

            if (energy == 0) speed = 400; //
            else speed = 200;
            if (mine < 0) mine = 0;
            if (xdir == 1 && Xx >= 54) xdir = -1;    // change direction at boundaries
            if (xdir == -1 && Xx <= 4) xdir = 1;
            if (ydir == 1 && Yy <= 4) ydir = -1;    // change direction at boundaries
            if (ydir == -1 && Yy >= 24) ydir = 1;

            Console.SetCursorPosition(Xx, Xy);    // delete old X
            Console.WriteLine(" ");
            Console.SetCursorPosition(Yx, Yy);    // delete old Y
            Console.WriteLine(" ");

            Xx += xdir;
            Yy -= ydir;

            Console.SetCursorPosition(Xx, Xy);    // refresh X (current position)
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("X");

            Console.SetCursorPosition(Yx, Yy);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Y");

            Console.ResetColor();
            bool gameOver = false;

            if (gameField[pX, pY] == '+')
            {
                Console.SetCursorPosition(pX, pY);
                Console.Write(" ");                  // mine disappears after game is over
                Console.SetCursorPosition(57, 15);
                gameOver = true;
            }

            if (gameField[testZx, testZy] == '+')
            {
                gameField[testZx, testZy] = ' ';
                Console.SetCursorPosition(testZx, testZy);
                Console.Write(" ");
                aliveZ = false;
                score += 300;
                testZx = 0; testZy = 0;
            }

            if (testZx == pX && testZy == pY) gameOver = true; // enemy caught player

            if (Xx == pX && Xy == pY)
            {
                Console.SetCursorPosition(57, 12);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("P and X collided"); // temp
                Console.ResetColor();
            }
            if (Yx == pX && Yy == pY)
            {
                Console.SetCursorPosition(57, 13);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("P and Y collided"); // temp
                Console.ResetColor();
            }

            if (aliveZ || gameOver)
            {
                Console.SetCursorPosition(testZx, testZy);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Z");
            }

            if (gameOver)
            {
                Console.SetCursorPosition(57, 15);
                Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.White;
                Console.Write("GAME OVER!"); Console.SetCursorPosition(0, 25);
                Console.ResetColor();
                break;
            }

            Console.SetCursorPosition(pX, pY);    // refresh P (current position)
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("P");
            Console.ResetColor();

            if (gameField[pX, pY] == '1')
            {
                score += 10;
                gameField[pX, pY] = ' ';
            }
            else if (gameField[pX, pY] == '2')
            {
                score += 30;
                energy += 50;
                gameField[pX, pY] = ' ';
            }
            else if (gameField[pX, pY] == '3')
            {
                score += 90;
                energy += 200;
                mine++;
                gameField[pX, pY] = ' ';
            }

            if (gameField[testZx, testZy] == '1') // logically deleting numbers from the game
                gameField[testZx, testZy] = ' ';
            else if (gameField[testZx, testZy] == '2')
                gameField[testZx, testZy] = ' ';
            else if (gameField[testZx, testZy] == '3')
                gameField[testZx, testZy] = ' ';


            Console.SetCursorPosition(57, 11);
            Console.Write("pX: " + pX + "  pY: " + pY + " "); // temp - printing players location

            Console.SetCursorPosition(66, 3);
            Console.WriteLine((int)watch.Elapsed.TotalSeconds);

            Console.SetCursorPosition(66, 5);
            Console.WriteLine(score);

            Console.SetCursorPosition(66, 7);
            Console.WriteLine(energy + "     ");

            Console.SetCursorPosition(66, 9);
            Console.WriteLine(mine + "    ");

            Thread.Sleep(speed);  // to be changed
        }
        Console.ReadLine();

    }
}