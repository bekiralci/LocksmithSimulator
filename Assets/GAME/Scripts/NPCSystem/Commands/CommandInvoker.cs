// Assets/NPCSystem/Commands/CommandInvoker.cs
using System;
using UnityEngine;

namespace NPCSystem
{
    public class CommandInvoker
    {
        private ICommand _command;

        public void SetCommand(ICommand command) => _command = command;

        public void Invoke()
        {
            try
            {
                _command?.Execute();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Command invocation error: {ex}");
            }
        }

        public void ClearCommand() => _command = null;
    }
}
