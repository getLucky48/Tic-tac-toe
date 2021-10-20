using System;

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
            FriendShip

        }

        /// <summary>
        /// Отрисовка поля
        /// </summary>
        static void render(Cell[,] matrix)
        {

            for(int i = 0; i < 3; i++)
            {

                for(int j = 0; j < 3; j++)
                {

                    if (matrix[i, j] == Cell.None) { Console.Write(" "); }
                    if (matrix[i, j] == Cell.Cross) { Console.Write("X"); }
                    if (matrix[i, j] == Cell.Zero) { Console.Write("O"); }

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

            //todo

            return Winner.FriendShip;

        }

        static void Main(string[] args)
        {

            Cell[,] matrix = new Cell[3, 3] {

                {Cell.Zero, Cell.None, Cell.None },
                {Cell.Zero, Cell.Zero, Cell.None },
                {Cell.Zero, Cell.None, Cell.None }

            };

            check(matrix);

            render(matrix);

        }
    
    }

}

