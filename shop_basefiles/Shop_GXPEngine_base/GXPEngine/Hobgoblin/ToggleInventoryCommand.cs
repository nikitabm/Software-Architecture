﻿using System;
using GXPEngine;
using Hobgoblin.Interfaces;

namespace Hobgoblin.Commands.PlayerCommands
{
    public class ToggleInventoryCommand : ICommand
    {
        private MyGame _game;

        public ToggleInventoryCommand(MyGame pGame)
        {
            _game = pGame;
        }

        public void Execute()
        {
            if (_game != null)
            {
                _game.ToggleInventory();
            }
        }
    }
}
