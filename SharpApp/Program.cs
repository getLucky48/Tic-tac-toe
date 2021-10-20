using System;
using System.Threading;

namespace SharpApp
{

    class Program
    {

        /// <summary>
        /// Состояние ячейки
        /// </summary>
        enum Cell
        {

            Cross,
            Zero,
            None

        }

        enum Winner
        {

            Computer,
            User,
            FriendShip,
            None

        }

        /// <summary>
        /// Отрисовка поля
        /// </summary>
        static void render(Cell[,] matrix, int posX, int posY)
        {

            Console.Clear();

            for(int i = 0; i < 3; i++)
            {

                for(int j = 0; j < 3; j++)
                {

                    if(i == posY && j == posX)
                    {

                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;

                    }

                    if (matrix[i, j] == Cell.None) { Console.Write(" "); }
                    if (matrix[i, j] == Cell.Cross) { Console.Write("X"); }
                    if (matrix[i, j] == Cell.Zero) { Console.Write("O"); }

                    Console.ResetColor();

                }

                Console.WriteLine();

            }

        }

        /// <summary>
        /// Проверка победителя
        /// </summary>
        static Winner check(Cell[,] matrix)
        {

            #region проверка победной комбинации по горизонтали
            for (int i = 0; i < 3; i++)
            {

                bool isWin = true;

                int score = 0;

                for(int j = 0; j < 3; j++)
                {

                    if(matrix[i,j] == Cell.None) { isWin = false; }
                    else { score += (int)matrix[i, j]; }

                }

                if (isWin) { 
                
                    if(score == 3 * (int)Cell.Zero) { return Winner.Computer; }
                    if(score == 3 * (int)Cell.Cross) { return Winner.User; }
                
                }

            }
            #endregion

            #region проверка победной комбинации по вертикали
            for (int i = 0; i < 3; i++)
            {

                bool isWin = true;

                int score = 0;

                for (int j = 0; j < 3; j++)
                {

                    if (matrix[j, i] == Cell.None) { isWin = false; }
                    else { score += (int)matrix[j, i]; }

                }

                if (isWin)
                {

                    if (score == 3 * (int)Cell.Zero) { return Winner.Computer; }
                    if (score == 3 * (int)Cell.Cross) { return Winner.User; }

                }

            }
            #endregion

            #region проверка победной комбинации по диагонали (от 0;0 до 2;2)
            bool isWinGlob = true;
            int scoreGlob = 0;

            for (int i = 0; i < 3; i++)
            {

                if (matrix[i, i] == Cell.None) { isWinGlob = false; }
                else { scoreGlob += (int)matrix[i, i]; }

            }

            if (isWinGlob)
            {

                if (scoreGlob == 3 * (int)Cell.Zero) { return Winner.Computer; }
                if (scoreGlob == 3 * (int)Cell.Cross) { return Winner.User; }

            }
            #endregion

            #region проверка победной комбинации по диагонали (от 0;2 до 2;0)
            isWinGlob = true;
            scoreGlob = 0;

            for (int i = 0; i < 3; i++)
            {

                if (matrix[i, 3 - i - 1] == Cell.None) { isWinGlob = false; }
                else { scoreGlob += (int)matrix[i, 3 - i - 1]; }

            }

            if (isWinGlob)
            {

                if (scoreGlob == 3 * (int)Cell.Zero) { return Winner.Computer; }
                if (scoreGlob == 3 * (int)Cell.Cross) { return Winner.User; }

            }
            #endregion

            #region проверка заполнения всех полей

            int freeFields = 9;

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    if (matrix[i, j] == Cell.None) { freeFields--; }

                }

            }

            if (freeFields != 0) { return Winner.None; }

            #endregion

            return Winner.FriendShip;

        }

        static void Main(string[] args)
        {

            Cell[,] matrix = new Cell[3, 3] {

                {Cell.None, Cell.None, Cell.None },
                {Cell.None, Cell.None, Cell.None },
                {Cell.None, Cell.None, Cell.None }

            };

            int currentPosX = 1;
            int currentPosY = 1;

            bool switcher = false;

            while (true)
            {

                render(matrix, currentPosX, currentPosY);

                ConsoleKeyInfo input = Console.ReadKey();
                ConsoleKey key = input.Key;

                if (key == ConsoleKey.LeftArrow)
                {

                    if (currentPosX > 0) { currentPosX--; }

                }

                if (key == ConsoleKey.RightArrow)
                {

                    if (currentPosX < 2) { currentPosX++; }

                }

                if (key == ConsoleKey.UpArrow)
                {

                    if (currentPosY > 0) { currentPosY--; }

                }

                if (key == ConsoleKey.DownArrow)
                {

                    if (currentPosY < 2) { currentPosY++; }

                }

                if(key == ConsoleKey.Enter)
                {

                    if (matrix[currentPosY, currentPosX] == Cell.None)
                    {

                        if (switcher) { matrix[currentPosY, currentPosX] = Cell.Zero; }
                        else { matrix[currentPosY, currentPosX] = Cell.Cross; }

                        switcher = !switcher;

                        Winner winner = check(matrix);

                        if(winner != Winner.None)
                        {

                            Console.WriteLine($"{winner} win!");

                            return;

                        }

                    }

                }

            }

        }
    
    }

}

