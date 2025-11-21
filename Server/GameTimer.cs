using ChessLogic;
using System;
// XÓA dòng 'using System.Timers;' để tránh xung đột,
// chúng ta sẽ gọi trực tiếp tên đầy đủ bên dưới.

namespace MyTcpServer
{
    public class GameTimer
    {
        // SỬA ĐỔI: Gọi đích danh System.Timers.Timer
        private readonly System.Timers.Timer _timer;

        public int WhiteTimeSeconds { get; private set; }
        public int BlackTimeSeconds { get; private set; }

        private Player _activePlayer;

        public event Action<Player> OnTimeExpired;

        public GameTimer(int minutesPerSide)
        {
            WhiteTimeSeconds = minutesPerSide * 60;
            BlackTimeSeconds = minutesPerSide * 60;

            // SỬA ĐỔI: Gọi đích danh
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            _timer.AutoReset = true;
        }

        public void Start(Player activePlayer)
        {
            _activePlayer = activePlayer;
            _timer.Start();
        }

        public void SwitchTurn()
        {
            _activePlayer = _activePlayer.Opponent();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        // Lưu ý: ElapsedEventArgs nằm trong System.Timers
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_activePlayer == Player.White)
            {
                WhiteTimeSeconds--;
                if (WhiteTimeSeconds <= 0)
                {
                    HandleTimeOut(Player.White);
                }
            }
            else
            {
                BlackTimeSeconds--;
                if (BlackTimeSeconds <= 0)
                {
                    HandleTimeOut(Player.Black);
                }
            }
        }

        private void HandleTimeOut(Player loser)
        {
            _timer.Stop();
            OnTimeExpired?.Invoke(loser);
        }
    }
}