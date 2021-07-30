﻿using GameSystem.MoveCommandProviders;
using GameSystem.MoveCommands;
using GameSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.MoveCommandProviders
{
    [MoveCommandProvider(Name)]
    public class PlayerMoveTeleportCommandProvider : AbstractMoveCommandProvider
    {
        public const string Name = "TeleportMove";

        public PlayerMoveTeleportCommandProvider() : base(new PlayerMoveTeleportCommand())
        {
        }
    }
}