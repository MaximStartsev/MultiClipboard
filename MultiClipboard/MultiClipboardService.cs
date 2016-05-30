using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MultiClipboard
{
    public partial class MultiClipboardService : ServiceBase
    {
        public MultiClipboardService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debug.WriteLine("Start");
            var native = new NativeKeyHooks();
            native.KeyPressed += native_KeyPressed;
        }

        void native_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Debug.WriteLine(e.Key);
        }

        protected override void OnStop()
        {
            Debug.WriteLine("Stop");
        }
    }
}
