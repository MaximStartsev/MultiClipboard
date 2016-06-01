using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace MultiClipboard
{
    public partial class MultiClipboardService : ServiceBase
    {
        readonly GlobalKeyboardHook _gkh = new GlobalKeyboardHook();
        private readonly List<Keys> _pressedKeys = new List<Keys>();
        private readonly Keys[] _controlKeys = new[] { Keys.Control, Keys.LControlKey, Keys.RControlKey, Keys.ControlKey };

        private readonly Keys[] _numberKeys = new[]
        {
            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9
        };
        private readonly Dictionary<Keys, String> _clipboard;
        public MultiClipboardService()
        {
            InitializeComponent();
            _clipboard = new Dictionary<Keys, string>()
            {
                {Keys.D0, String.Empty},
                {Keys.D1, String.Empty},
                {Keys.D2, String.Empty},
                {Keys.D3, String.Empty},
                {Keys.D4, String.Empty},
                {Keys.D5, String.Empty},
                {Keys.D6, String.Empty},
                {Keys.D7, String.Empty},
                {Keys.D8, String.Empty},
                {Keys.D9, String.Empty},
            };
        }

        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Start");
            _gkh.HookedKeys.AddRange(new[]{Keys.C, Keys.X, Keys.V});
            _gkh.HookedKeys.AddRange(_controlKeys);
            _gkh.HookedKeys.AddRange(_numberKeys);
            _gkh.KeyDown += gkh_KeyDown;
            _gkh.KeyUp += gkh_KeyUp;
        }
       
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            if (_pressedKeys.Contains(e.KeyCode))
                _pressedKeys.Remove(e.KeyCode);
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_pressedKeys.Contains(e.KeyCode))
            {
                _pressedKeys.Add(e.KeyCode);
            }
            if (_pressedKeys.Count==3 && _pressedKeys.Any(k=>_controlKeys.Contains(k)) && _pressedKeys.Any(k=>_numberKeys.Contains(k)))
            {
                if (_pressedKeys.Contains(Keys.X) || _pressedKeys.Contains(Keys.C))
                {
                    TryCopy();
                }
                else if (_pressedKeys.Contains(Keys.V))
                {
                    TryPaste();
                }
                e.Handled = true;
            }
        }

        private void TryCopy()
        {
            try
            {
                var number = _pressedKeys.First(k => _numberKeys.Contains(k));
                var text = Clipboard.GetText();
                if (!String.IsNullOrEmpty(text))
                    _clipboard[number] = text;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            } 
        }

        private void TryPaste()
        {
            //todo: сделать, чтобы текст вставлялся сразу, а не после двух комбинаций клавиш.
            try
            {
                var number = _pressedKeys.First(k => _numberKeys.Contains(k));
                if (!String.IsNullOrEmpty(_clipboard[number]))
                {
                    var value = _clipboard[number];
                    Debug.WriteLine("paste: "+value);
                    Clipboard.SetText(value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            } 
        }
        protected override void OnStop()
        {
            Debug.WriteLine("Stop");
        }
    }
}
