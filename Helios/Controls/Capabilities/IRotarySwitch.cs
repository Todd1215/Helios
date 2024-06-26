﻿// Copyright 2020 Ammo Goettsch
// 
// Helios is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Helios is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

namespace GadrocsWorkshop.Helios.Controls.Capabilities
{
    public interface IRotarySwitch : IPulsedControl
    {
        #region Properties

        /// <summary>
        /// the current position, in the range MinPosition..MaxPosition (inclusive)
        /// and which can be changed by adding or subtracting via Pulse(...)
        /// </summary>
        int CurrentPosition { get; }

        /// <summary>
        /// minimum position number of the switch
        /// </summary>
        int MinPosition { get; }

        /// <summary>
        /// maximum position number of the switch
        /// </summary>
        int MaxPosition { get; }

        /// <summary>
        /// true if this switch wraps around
        /// </summary>
        bool IsContinuous { get; }

        #endregion
    }
}