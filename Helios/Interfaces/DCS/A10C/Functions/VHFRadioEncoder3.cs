﻿//  Copyright 2014 Craig Courtney
//  Copyright 2020 Ammo Goettsch
//    
//  Helios is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Helios is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace GadrocsWorkshop.Helios.Interfaces.DCS.A10C.Functions
{
    using GadrocsWorkshop.Helios.Interfaces.DCS.Common;
    using GadrocsWorkshop.Helios.UDPInterface;
    using System;
    using System.Globalization;

    public class VHFRadioEncoder3 : Axis
    {
        private double _lastData;
        private HeliosValue _windowValue;

        public VHFRadioEncoder3(BaseUDPInterface sourceInterface, string deviceId, string buttonId, string argId, double stepValue, double argMin, double argMax, string device, string name)
            : base(sourceInterface, deviceId, buttonId, argId, stepValue, argMin, argMax, device, name, false, "%.3f")
        {
            // base calls DoBuild, we add ours
            DoBuild();
        }

        // deserialization constructor
        public VHFRadioEncoder3(BaseUDPInterface sourceInterface, System.Runtime.Serialization.StreamingContext context)
            : base(sourceInterface, context)
        {
            // no code
        }

        public override void BuildAfterDeserialization()
        {
            base.BuildAfterDeserialization();
            DoBuild();
        }

        private void DoBuild()
        {
            _windowValue = new HeliosValue(SourceInterface, new BindingValue(0.0d), SerializedDeviceName,
                SerializedFunctionName + " window", "Current value displayed in this encoder.",
                ArgumentMin.ToString(CultureInfo.CurrentCulture) + "-" + ArgumentMax.ToString(CultureInfo.CurrentCulture), BindingValueUnits.Text);
            Values.Add(_windowValue);
            Triggers.Add(_windowValue);
        }

        public override void ProcessNetworkData(string id, string value)
        {
            base.ProcessNetworkData(id, value);
            double parseValue;
            if (double.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out parseValue))
            {
                double newValue = parseValue;

                if (newValue < _lastData)
                {
                    newValue = Math.Floor(newValue * 10);                    
                }
                else if (newValue > _lastData)
                {
                    newValue = Math.Ceiling(newValue * 10);
                    if (newValue == 10) newValue = 0;
                }
                else
                {
                    return;
                }

                _lastData = parseValue;

                _windowValue.SetValue(new BindingValue("." + newValue.ToString(CultureInfo.InvariantCulture)), false);
            }
        }
    }
}
