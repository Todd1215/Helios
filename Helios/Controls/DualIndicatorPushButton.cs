﻿//  Copyright 2014 Craig Courtney
//  Copyright 2022 Helios Contributors
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

namespace GadrocsWorkshop.Helios.Controls
{
    using GadrocsWorkshop.Helios.ComponentModel;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml;

    [HeliosControl("Helios.Base.DualIndicatorPushButton", "Dual Indicator Push Button", "Push Button Indicators", typeof(DualIndicatorPushButtonRenderer))]
    public class DualIndicatorPushButton : IndicatorPushButton
    {
        private Color _additionalOnTextColor = Color.FromArgb(0xF0,0x34, 0xAE,0x00);
        private Color _additionalOffTextColor = Color.FromArgb(0x20, 0x34, 0xAE, 0x00);
        private bool _on = false;
        private string _additionalIndicatorText = "";
        private TextFormat _additionalTextFormat = new TextFormat();

        private HeliosAction _toggleAction;
        private HeliosValue _value;

        public DualIndicatorPushButton()
            : base("Dual Indicator Pushbutton", new Size(75, 75))
        {
            _additionalTextFormat.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Format_PropertyChanged);
            _additionalTextFormat.VerticalAlignment = TextVerticalAlignment.Bottom;
            _additionalTextFormat.HorizontalAlignment = TextHorizontalAlignment.Center;
            _additionalTextFormat.PaddingBottom = 0.2;
            _additionalTextFormat.FontSize = 18;
            _additionalIndicatorText = "SAFE";
            _on = false;

            OnTextColor = Color.FromArgb(0xF0,0xF6,0x7A,0x00);
            TextColor = Color.FromArgb(0x20, 0xF6, 0x7A, 0x00);
            TextFormat.VerticalAlignment = TextVerticalAlignment.Top;
            TextFormat.HorizontalAlignment = TextHorizontalAlignment.Center;
            TextFormat.PaddingTop = 0.2;
            TextFormat.FontSize = 18;
            Text = "ARM";
            Indicator = false;

            _value = new HeliosValue(this, new BindingValue(false), "", "additional indicator", "Current On/Off State for this buttons additional indicator.", "True if the additional indicator is on, otherwise false.", BindingValueUnits.Boolean);
            _value.Execute += new HeliosActionHandler(Indicator_Execute);
            Values.Add(_value);
            Actions.Add(_value);

            _toggleAction = new HeliosAction(this, "", "", "toggle additional indicator", "Toggles this additional indicator between on and off.");
            _toggleAction.Execute += new HeliosActionHandler(ToggleAction_Execute);
            Actions.Add(_toggleAction);
        }

        #region Properties

        public bool AdditionalIndicator
        {
            get
            {
                return _on;
            }
            set
            {
                if (!_on.Equals(value))
                {
                    bool oldValue = _on;

                    _on = value;
                    _value.SetValue(new BindingValue(_on), BypassTriggers);

                    OnPropertyChanged("Additional Indicator", oldValue, value, false);
                    OnDisplayUpdate();
                }
            }
        }


        public Color AdditionalOnTextColor
        {
            get
            {
                return _additionalOnTextColor;
            }
            set
            {
                if (!_additionalOnTextColor.Equals(value))
                {
                    Color oldValue = _additionalOnTextColor;
                    _additionalOnTextColor = value;
                    OnPropertyChanged("AdditionalOnTextColor", oldValue, value, true);
                    Refresh();
                }
            }
        }
        public Color AdditionalOffTextColor
        {
            get
            {
                return _additionalOffTextColor;
            }
            set
            {
                if (!_additionalOffTextColor.Equals(value))
                {
                    Color oldValue = _additionalOffTextColor;
                    _additionalOffTextColor = value;
                    OnPropertyChanged("AdditionalOffTextColor", oldValue, value, true);
                    Refresh();
                }
            }
        }
        public string AdditionalText
        {
            get
            {
                return _additionalIndicatorText;
            }
            set
            {
                if (!_additionalIndicatorText.Equals(value))
                {
                    string oldValue = _additionalIndicatorText;
                    _additionalIndicatorText = value;
                    OnPropertyChanged("AdditionalText", oldValue, value, true);
                    Refresh();
                }
            }
        }
        public TextFormat AdditionalTextFormat
        {
            get { return _additionalTextFormat; }
        }

        #endregion
        #region Execution
        void Format_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PropertyNotificationEventArgs origArgs = e as PropertyNotificationEventArgs;
            if (origArgs != null)
            {
                OnPropertyChanged("AdditionalTextFormat", origArgs);
            }
            OnDisplayUpdate();
        }

        void ToggleAction_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            AdditionalIndicator= !AdditionalIndicator;
            EndTriggerBypass(e.BypassCascadingTriggers);
        }

        void Indicator_Execute(object action, HeliosActionEventArgs e)
        {
            BeginTriggerBypass(e.BypassCascadingTriggers);
            AdditionalIndicator = e.Value.BoolValue;
            EndTriggerBypass(e.BypassCascadingTriggers);
        }
        #endregion

        #region Overrides
        protected override void OnPropertyChanged(PropertyNotificationEventArgs args)
        {
            OnDisplayUpdate();
            base.OnPropertyChanged(args);
        }

        public override void ScaleChildren(double scaleX, double scaleY)
        {
            if (GlobalOptions.HasScaleAllText)
            {
                _additionalTextFormat.FontSize *= Math.Max(scaleX, scaleY);
            }
            base.ScaleChildren(scaleX, scaleY);
        }


        public override void Reset()
        {
            base.Reset();

            BeginTriggerBypass(true);
            AdditionalIndicator = false;
            EndTriggerBypass(true);
        }

        public override void MouseDown(Point location)
        {
            if (DesignMode)
            {
                AdditionalIndicator = !AdditionalIndicator;
            }
            base.MouseDown(location);
        }

        public override void WriteXml(XmlWriter writer)
        {
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));
            base.WriteXml(writer);
            if (Text != null && Text.Length > 0)
            {
                writer.WriteStartElement("AdditionalText");

                writer.WriteStartElement("Font");
                AdditionalTextFormat.WriteXml(writer);
                writer.WriteEndElement();

                writer.WriteElementString("Text", AdditionalText);
                writer.WriteElementString("OnTextColor", colorConverter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, AdditionalOnTextColor));
                writer.WriteElementString("OffTextColor", colorConverter.ConvertToString(null, System.Globalization.CultureInfo.InvariantCulture, AdditionalOffTextColor));

                writer.WriteEndElement();
            }
        }

        public override void ReadXml(XmlReader reader)
        {
            TypeConverter colorConverter = TypeDescriptor.GetConverter(typeof(Color));
            base.ReadXml(reader);
            if (reader.Name.Equals("AdditionalText"))
            {
                reader.ReadStartElement("AdditionalText");
                if (reader.Name.Equals("Font"))
                {
                    reader.ReadStartElement("Font");
                    AdditionalTextFormat.ReadXml(reader);
                    reader.ReadEndElement();
                    AdditionalText = reader.ReadElementString("Text");
                }
                if (reader.Name.Equals("OnTextColor"))
                {
                    AdditionalOnTextColor = (Color)colorConverter.ConvertFromString(null, System.Globalization.CultureInfo.InvariantCulture, reader.ReadElementString("OnTextColor"));
                }
                if (reader.Name.Equals("OffTextColor"))
                {
                    AdditionalOffTextColor = (Color)colorConverter.ConvertFromString(null, System.Globalization.CultureInfo.InvariantCulture, reader.ReadElementString("OffTextColor"));
                }
                reader.ReadEndElement();
            }
        }
        #endregion
    }
}