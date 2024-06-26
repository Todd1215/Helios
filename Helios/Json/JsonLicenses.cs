﻿// Copyright 2021 Ammo Goettsch
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

using Newtonsoft.Json.Schema;

namespace GadrocsWorkshop.Helios.Json
{
    internal class JsonLicenses
    {
        internal static void LoadLicenses()
        {
            // if you have a license for JSON.Net Schema, it needs to be installed here during development to 
            // generate schemas
            string license = ConfigManager.SettingsManager.LoadSetting("Licenses", "JSONNetSchema", null);
            if (license != null)
            {
                License.RegisterLicense(license);
            }
        }
    }
}