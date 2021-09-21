using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessGameCore.ContentManager;

namespace ChessGameView.Shcedulers
{

    public class PlayerTimerShceduler : IHostedService, IDisposable
    {
        private readonly int _updateTimeInSeconds = 1;
        private Timer _timer;

        private readonly GameManager _manager;

        public PlayerTimerShceduler(GameManager manager)
        {
            _manager = manager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_updateTimeInSeconds));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _manager.PlayerTimerShceduling();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }

}
