﻿using System;
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
        }

        protected override void OnStop()
        {
            Debug.WriteLine("Stop");
        }
    }
}