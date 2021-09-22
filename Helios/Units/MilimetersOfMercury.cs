//  Copyright 2014 Craig Courtney
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

namespace GadrocsWorkshop.Helios.Units
{
    using System;

    public class MilimetersOfMercury : PressureUnit
    {
        private Object _hashObject = new Object();

        public override string ShortName
        {
            get { return "mmHg"; }
        }

        public override string LongName
        {
            get { return "Millimeters of mercury"; }
        }

        public override double AreaConversionFactor
        {
            get
            {
                return .0002733918126905263d;
            }
        }

        public override double MassConversionFactor
        {
            get
            {
                return 0.49109778d;
            }
        }

        public override int GetHashCode()
        {
            return _hashObject.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(InchofMercuryUnit);
        }
    }
}
