using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiClipboard;

namespace ServiceTest
{
    public partial class Form1 : Form
    {
        private MultiClipboardService _service;
        public Form1()
        {
            InitializeComponent();
            _service = new MultiClipboardService();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //_service.
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _service.Stop();
        }
    }
}
