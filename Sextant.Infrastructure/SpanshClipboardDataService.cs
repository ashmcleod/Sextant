﻿// Copyright (c) Stickymaddness All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Sextant.Domain;
using System.Windows.Forms;
using System.Collections.Generic;
using Sextant.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using FileHelpers;
using System.Text.RegularExpressions;

namespace Sextant.Infrastructure
{
    public class SpanshClipboardDataService : IUserDataService
    {
        private static ILogger _logger;

        public SpanshClipboardDataService(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<StarSystem> GetExpeditionData()
        {
            try
            {
                string clipboardData = GetClipboard();

                List<StarSystem> systems = new List<StarSystem>();
                var engine = new FileHelperEngine<SpanshLine>();
                var records = engine.ReadString(clipboardData);

                StarSystem currentSystem = null;
                foreach (var line in records)
                {
                    if (currentSystem == null || currentSystem.Name != line.SystemName)
                    {
                        if (currentSystem != null) systems.Add(currentSystem);
                        currentSystem = new StarSystem(line.SystemName);
                    }
                    var classification = GetClassification(line.Subtype);
                    currentSystem.AddCelestial(line.BodyName, classification);
               
                }
                systems.Add(currentSystem);
                //StarSystem currentSystem = null;

                //if (!lines.First().Contains(Header))
                //    return null;

                //foreach (var line in lines.Skip(2))
                //{
                //    if (string.IsNullOrWhiteSpace(line))
                //        continue;

                //    if (!line.StartsWith("\t"))
                //    {
                //        var systemName = line.Substring(11);

                //        currentSystem = new StarSystem(systemName);
                //        systems.Add(currentSystem);
                //        continue;
                //    }

                //    var length = line.IndexOf('(') - 13;
                //    var planet = line.Substring(12, length);

                //    var index = line.IndexOf(')') + 2;
                //    var classification = GetClassification(line.Substring(index));

                //    currentSystem.AddCelestial(string.Join(" ", currentSystem.Name, planet), classification);
                //}
                _logger.Information($"Spansh loaded: {systems.Count()}");
                return systems;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception parsing expedition data");
                return null;
            }
        }

        private string GetClassification(string code)
        {
            switch (code)
            {
                case "TWW": return "Water World";
                case "HMC": return "High metal content world";
                case "ELW": return "Earth world";
                default: return "Unknown";
            }
        }

        protected virtual string GetClipboard()
        {
            string clipboardText = string.Empty;

            try
            {
                Thread thread = new Thread(() => clipboardText = Clipboard.GetText(TextDataFormat.Text));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception attempting to get clipboard");
            }
            return clipboardText;
        }
    }
}
