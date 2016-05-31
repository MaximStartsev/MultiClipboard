using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiClipboard
{
    public partial class MultiClipboardService : ServiceBase
    {
        GlobalKeyboardHook _gkh = new GlobalKeyboardHook();
        private List<Keys> _pressedKeys = new List<Keys>();
        public MultiClipboardService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Start");
            _gkh.HookedKeys.AddRange(new[]
            {
                Keys.Control, Keys.C, Keys.X, Keys.V, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6,
                Keys.D7, Keys.D8, Keys.D9
            });

            _gkh.KeyDown += gkh_KeyDown;
            _gkh.KeyUp += gkh_KeyUp;
        }
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            if (_pressedKeys.Contains(e.KeyCode))
                _pressedKeys.Remove(e.KeyCode);
            e.Handled = true;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if(!_pressedKeys.Contains(e.KeyCode))
                _pressedKeys.Add(e.KeyCode);
            if (_pressedKeys.Contains(Keys.Control))
            {
                if (_pressedKeys.Contains(Keys.X))
                {
                    TryCut();
                }
                else if (_pressedKeys.Contains(Keys.C))
                {
                    TryCopy();
                }
                else if (_pressedKeys.Contains(Keys.V))
                {
                    TryPaste();
                }
            }
            e.Handled = true;
        }

        private void TryCopy()
        {
            try
            {
                Debug.WriteLine(Clipboard.GetText());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
          
        }

        private void TryCut()
        {
            
        }

        private void TryPaste()
        {
            
        }
        protected override void OnStop()
        {
            Debug.WriteLine("Stop");
        }
    }
}
