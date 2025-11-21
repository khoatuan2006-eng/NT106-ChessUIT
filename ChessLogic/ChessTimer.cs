using System;
using System.Timers; // Giữ dòng này để dùng ElapsedEventArgs

namespace ChessLogic
{
    public class ChessTimer
    {
        // --- SỬA: Gọi đầy đủ System.Timers.Timer ---
        private readonly System.Timers.Timer _timer;

        public int WhiteRemaining { get; private set; }
        public int BlackRemaining { get; private set; }
        public Player ActivePlayer { get; private set; }

        public event Action<Player>? TimeExpired;
        public event Action<int, int>? Tick;

        public ChessTimer(int minutes)
        {
            WhiteRemaining = minutes * 60;
            BlackRemaining = minutes * 60;

            // --- SỬA: Gọi đầy đủ System.Timers.Timer ---
            _timer = new System.Timers.Timer(1000); // 1 giây
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimedEvent;
        }

        public void Start(Player player)
        {
            ActivePlayer = player;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void SwitchTurn()
        {
            if (ActivePlayer == Player.White)
                ActivePlayer = Player.Black;
            else
                ActivePlayer = Player.White;
        }

        public void Sync(int white, int black)
        {
            WhiteRemaining = white;
            BlackRemaining = black;
            Tick?.Invoke(WhiteRemaining, BlackRemaining);
        }

        // --- SỬA: Gọi đầy đủ System.Timers.ElapsedEventArgs (nếu cần) hoặc để nguyên nếu đã using ---
        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if (ActivePlayer == Player.White)
            {
                WhiteRemaining--;
                if (WhiteRemaining <= 0)
                {
                    _timer.Stop();
                    TimeExpired?.Invoke(Player.White);
                }
            }
            else
            {
                BlackRemaining--;
                if (BlackRemaining <= 0)
                {
                    _timer.Stop();
                    TimeExpired?.Invoke(Player.Black);
                }
            }
            Tick?.Invoke(WhiteRemaining, BlackRemaining);
        }
    }
}