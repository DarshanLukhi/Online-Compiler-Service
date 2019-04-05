using System;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            radioButton2.Checked = true;
            textBox3.ReadOnly = true;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox2.Text = "";

            if (radioButton2.Checked)
            {
                if (comboBox1.Text == "C")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileC(textBox1.Text);

                }
                else if (comboBox1.Text == "C++")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileCPP(textBox1.Text);
                }
                else if (comboBox1.Text == "Python 3.6")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();

                    textBox2.Text = client.CompilePython(textBox1.Text);
                }
                else
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileJava(textBox1.Text);
                }
            }
            else
            {
                if (comboBox1.Text == "C")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileCWithInput(textBox1.Text,textBox3.Text);

                }
                else if (comboBox1.Text == "C++")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileCPPWithInput(textBox1.Text, textBox3.Text);
                }
                else if (comboBox1.Text == "Python 3.6")
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompilePythonWithInput(textBox1.Text, textBox3.Text);
                    
                }
                else
                {
                    TCP.CompilerServiceClient client = new TCP.CompilerServiceClient();
                    textBox2.Text = client.CompileJavaWithInput(textBox1.Text, textBox3.Text);
                }
            }
            button1.Enabled = true ;
        }

     

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox3.ReadOnly = false;
            }
            else
            {
                textBox3.Text = "";
                textBox3.ReadOnly = true;
                
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                textBox3.Text = "";
                textBox3.ReadOnly = true;
                
            }
            
        }
    }
}
