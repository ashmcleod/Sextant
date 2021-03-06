﻿// Copyright (c) Stickymaddness All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Sextant.Domain;
using Sextant.Infrastructure.Settings;
using Sextant.Infrastructure.VirtualKeyboard;
using System.Threading;

namespace Sextant.Infrastructure
{
    public class GalaxyMapInteractor : IGalaxyMap
    {
        private readonly GalaxyMapInteractorSettings _settings;

        public GalaxyMapInteractor(GalaxyMapInteractorSettings settings)
        {
            _settings = settings;
            KeystrokeService.SetKeyPressDuration(settings.KeyDownDuration);
        }

        private void SendKey(string key, int delay)
        {
            KeystrokeService.SendSingleKeyPress(key);
            Thread.Sleep(delay);
        }

        public void FindSystem(string system)
        {
            SendKey(_settings.GalaxyMapKey, _settings.GalaxyMapOpenDelay);
            SendKey(_settings.NextUIPanelKey, _settings.GalaxyMapUINavigateDelay);
            SendKey(_settings.UISelectKey, _settings.GalaxyMapUINavigateDelay);

            KeystrokeService.SendKeystrokes(system);

            KeystrokeService.SendEnter();
        }
    }
}
 