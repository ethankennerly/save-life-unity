using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class PoorLifeMain : IDisposable
    {
        private readonly IPoorLifeMainAuthoring _authoring;
        private readonly IDatabaseLogger _logger;
        private readonly List<step_log> _commands;
        private readonly MainSystem _mainSystem;
        private float _elapsedTime;

        public PoorLifeMain(IPoorLifeMainAuthoring authoring)
        {
            _authoring = authoring;
            _logger = new DummyDatabaseLogger();
            _commands = new List<step_log>();
            _mainSystem = new MainSystem(authoring);
            _authoring.AgeUpButton.onClick.RemoveListener(OnAgeUpButtonClicked);
            _authoring.AgeUpButton.onClick.AddListener(OnAgeUpButtonClicked);
        }

        public void Update(float deltaTime)
        {
            _elapsedTime += deltaTime;
            LogCommands();
            _mainSystem.Update(deltaTime, _commands);
            _commands.Clear();
        }

        public void Dispose()
        {
            _logger.Flush();
            _logger.Dispose();
        }

        private void OnAgeUpButtonClicked()
        {
            step_log log = new step_log
            {
                log_step = LogStep.AgeUpClicked
            };
            _commands.Add(log);
        }

        private void LogCommands()
        {
            if (_commands.Count == 0)
            {
                return;
            }

            LogWait();

            foreach (step_log command in _commands)
            {
                _logger.Log(command);
            }
        }

        private void LogWait()
        {
            const int MillisecondsPerSecond = 1000;
            int waitMilliseconds = (int)(_elapsedTime * MillisecondsPerSecond);
            _elapsedTime -= ((float)waitMilliseconds / MillisecondsPerSecond);
            step_log waitLog = new step_log
            {
                log_step = LogStep.WaitForMilliseconds,
                parameter = waitMilliseconds
            };
            _logger.Log(waitLog);
        }
    }
}
