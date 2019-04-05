using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using CompilerWebService;

namespace Host
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost sh;
        private void Form1_Load(object sender, EventArgs e)
        {
            Uri tcpa = new Uri("net.tcp://localhost:8000/CompilerService");

            sh = new ServiceHost(typeof(CompilerService), tcpa);
            NetTcpBinding tcpb = new NetTcpBinding();

            ServiceMetadataBehavior mBehavior = new ServiceMetadataBehavior();

            sh.Description.Behaviors.Add(mBehavior);
            sh.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
            sh.AddServiceEndpoint(typeof(ICompilerService),tcpb,tcpa);

            sh.Open();
            label1.Text = "Service Running";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
