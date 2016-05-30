using System;
using System.Windows.Forms;

namespace ServiceTest
{
    public partial class Form1 : Form
    {
        private readonly TestWrapper _service;
        public Form1()
        {
            InitializeComponent();
            _service = new TestWrapper();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           _service.TestStart(new string[0]);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _service.TestStop();
        }
    }
}
