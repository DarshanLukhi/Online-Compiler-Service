using System;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Json;


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

            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
            label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key =  "PFTYMbM7l4YkcJGtH9tcKfBrx0fyEaGD",s = "";
            button1.Enabled = false;
            textBox2.Text = "";
            HTTP.CompilerServiceClient client = new HTTP.CompilerServiceClient("BasicHttpBinding_ICompilerService");

            if (radioButton2.Checked)
                if (comboBox1.Text == "C")
                    s = client.CompileC(textBox1.Text, key);
                else if (comboBox1.Text == "C++")
                    s = client.CompileCPP(textBox1.Text, key);
                else if (comboBox1.Text == "Python 3.6")
                    s = client.CompilePython(textBox1.Text, key);
                else
                    s = client.CompileJava(textBox1.Text, key);
            else
                if (comboBox1.Text == "C")     
                    s = client.CompileCWithInput(textBox1.Text,textBox3.Text, key);
                else if (comboBox1.Text == "C++")
                    s = client.CompileCPPWithInput(textBox1.Text, textBox3.Text, key);
                else if (comboBox1.Text == "Python 3.6")
                    s = client.CompilePythonWithInput(textBox1.Text, textBox3.Text, key);
                else 
                    s = client.CompileJavaWithInput(textBox1.Text, textBox3.Text, key);

            button1.Enabled = true ;
            JsonValue json = JsonValue.Parse(s);
            
            string status = json["status"];
            string output = json["output"];
            string error = json["error"];
            if (status == "AC")
            {
                label3.Text = "OUTPUT";
                label6.Text = "STATUS : ";
                label7.Text = status;
                textBox2.Text = output;
            }
            else
            {
                label3.Text = "ERROR";
                label6.Text = "STATUS : ";
                label7.Text = status;
                textBox2.Text = error;
            }
            
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
