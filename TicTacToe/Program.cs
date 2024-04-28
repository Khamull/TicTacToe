namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                TicTacToeBoard board = new TicTacToeBoard();
                board.PlayGame();
                Console.WriteLine("Deseja jogar novamente? (s/n)");
            } while (Console.ReadLine().Trim().ToLower() == "s");

            Console.WriteLine("Obrigado por jogar! Até mais.");
            Console.ReadKey();
        }
    }
    class TicTacToeBoard
    {
        private char[,] board;
        private char currentPlayer;
        public TicTacToeBoard()
        {
            // Inicializa o tabuleiro vazio
            board = new char[3, 3];
            currentPlayer = 'X';
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Preenche o tabuleiro com espaços em branco inicialmente
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        public void DisplayBoard()
        {
            // Limpa a tela antes de exibir o tabuleiro
            Console.Clear();

            // Exibe as linhas do tabuleiro
            for (int row = 0; row < 3; row++)
            {
                Console.WriteLine(" {0} | {1} | {2} ", board[row, 0], board[row, 1], board[row, 2]);
                if (row < 2)
                    Console.WriteLine("---|---|---");
            }
        }
        public char GetCurrentPlayer()
        {
            return currentPlayer;
        }
        public bool MakeMove(int row, int col)
        {
            // Verifica se a posição está vazia
            if (row < 0 || row >= 3 || col < 0 || col >= 3 || board[row, col] != ' ')
            {
                Console.WriteLine("Movimento inválido. Tente novamente.");
                return false;
            }

            // Define o movimento do jogador atual
            board[row, col] = currentPlayer;

            // Troca o jogador
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';

            return true;
        }

        public bool IsVictoriousCell(int row, int col)
        {
            // Verifica se a célula atual faz parte de uma linha, coluna ou diagonal vitoriosa
            return IsRowVictorious(row) || IsColumnVictorious(col) || IsDiagonalVictorious();
        }

        public bool IsRowVictorious(int row)
        {
            // Verifica se a linha tem todas as células iguais e não vazias
            return board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2] && board[row, 0] != ' ';
        }

        public bool IsColumnVictorious(int col)
        {
            // Verifica se a coluna tem todas as células iguais e não vazias
            return board[0, col] == board[1, col] && board[1, col] == board[2, col] && board[0, col] != ' ';
        }

        public bool IsDiagonalVictorious()
        {
            // Diagonal Primaria (Topo Esquerdo - Fundo Direito)
            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;

            // Diagonal Secundaria (Topo direito para Fundo Esquerdo)
            if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }
        public bool IsBoardFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                        return false; // Se qualquer celula for vazia
                }
            }
            return true; // Se não tivermos mais celulas vazias
        }
        public bool IsGameFinished()
        {
            char winner = GetCurrentPlayer() == 'X' ? 'O' : 'X';
            // Verifica se houve um vencedor
            for (int i = 0; i < 3; i++)
            {
                if (IsRowVictorious(i) || IsColumnVictorious(i))
                {
                    Console.WriteLine($"O jogador {winner} venceu!");
                    return true;
                }
            }
            if (IsDiagonalVictorious())
            {
                Console.WriteLine($"O jogador {winner} venceu!");
                return true;
            }

            // Verifica se o tabuleiro está cheio (empate)
            if (IsBoardFull())
            {
                Console.WriteLine("O jogo empatou!");
                return true;
            }

            return false;
        }
        public void PlayGame()
        {
            while (true)
            {
                // Exibe o tabuleiro
                DisplayBoard();

                // Verifica se o jogo terminou
                if (IsGameFinished())
                    break;

                // Obtém a entrada do jogador
                Console.WriteLine($"Vez do jogador {GetCurrentPlayer()}. Digite a linha (0-2) e a coluna (0-2) separadas por espaço:");
                string[] input = Console.ReadLine().Split(' ');

                if (input.Length != 2)
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite novamente.");
                    continue;
                }

                if (!int.TryParse(input[0], out int row) || !int.TryParse(input[1], out int col))
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite números válidos.");
                    continue;
                }

                // Tenta fazer o movimento
                if (!MakeMove(row, col))
                    continue;
            }
        }
    }
}
