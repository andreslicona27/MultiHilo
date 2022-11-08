using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Ejercicio3
{
    /// <summary>
    /// manifiesto 
    /// ponerlo en 64 bits
    /// meter array for each con lambdas 
    /// arreglar si el pid no existe 
    /// trim de star with
    /// </summary>
    /// 

    public partial class Form1 : Form
    {
        public string FixName(string name, int amountOfChar)
        {
            string name2 = name;
            if (name.Length >= amountOfChar)
            {
                name2 = name.Substring(0, amountOfChar) + "...";
            }
            return name2;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            string format = "{0,5}{1,20}{2,30}\r\n";

            textBox1.Text = String.Format(format, "PID", "NAME", "MAIN WINDOW");
            Array.ForEach(processes, (p) =>
            {
                textBox1.Text += String.Format(format, p.Id, FixName(p.ProcessName, 10), FixName(p.MainWindowTitle, 15));
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result;
            try
            {
                int pid = int.Parse(textBox2.Text);
                Process p = Process.GetProcessById(pid);
                ProcessModuleCollection m = p.Modules;
                ProcessThreadCollection s = p.Threads;

                textBox1.Text = String.Format("PID: {0}\r\nName: {1}\r\nMain window: {2}\r\n",
                    p.Id, p.ProcessName, p.MainWindowTitle);

                for (int i = 0; i < m.Count; i++)
                {
                    textBox1.Text += String.Format("Module Name: {0}\r\nFile Name:{1}\r\n\r\n", m[i].ModuleName, m[i].FileName);
                }
                for (int i = 0; i < s.Count; i++)
                {
                    textBox1.Text += String.Format("ID: {0}\r\nStart Time:{1}\r\n\r\n", s[i].Id, s[i].StartTime);
                }
            }
            catch (InvalidOperationException)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
            catch (ArgumentNullException)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
            catch (ArgumentException)
            {
                result = MessageBox.Show("That PID is not running or does not exists", "Information");
            }
            catch (FormatException)
            {
                result = MessageBox.Show("Give me the name of the process", "Information");
            }
            catch (Win32Exception)
            {
                result = MessageBox.Show("You don't have the necessary permissions", "Information");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult result;
            try
            {
                int pid = int.Parse(textBox2.Text);
                Process.GetProcessById(pid).CloseMainWindow();

            }
            catch (InvalidOperationException)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
            catch (ArgumentException)
            {
                result = MessageBox.Show("That PID is not running or does not exists", "Information");
            }
            catch (FormatException)
            {
                result = MessageBox.Show("Give me something to close", "Information");
            }
            catch (Win32Exception)
            {
                result = MessageBox.Show("You don't have the necessary permissions", "Information");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result;
            try
            {
                int pid = int.Parse(textBox2.Text);
                Process.GetProcessById(pid).Kill();
            }
            catch (InvalidOperationException)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
            catch (ArgumentException)
            {
                result = MessageBox.Show("That PID is not running or does not exists", "Information");
            }
            catch (FormatException)
            {
                result = MessageBox.Show("Give me something to kill", "Information");
            }
            catch (Win32Exception)
            {
                result = MessageBox.Show("You don't have the necessary permissions", "Information");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            Process p;
            Process p2;
            DialogResult result;
            try
            {
                p = Process.Start(textBox2.Text);
            }
            catch (InvalidOperationException)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
            catch (Win32Exception)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result;
            Process[] processes = Process.GetProcesses();
            string format = "{0,5}{1,20}{2,30}\r\n";


            textBox1.Text = String.Format(format, "PID", "NAME", "MAIN WINDOW");
            try
            {
                foreach (Process p in processes)
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        if (p.ProcessName.StartsWith(textBox2.Text.Trim()))
                        {
                            textBox1.Text += String.Format(format, p.Id, FixName(p.ProcessName, 10), FixName(p.MainWindowTitle, 15));
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                result = MessageBox.Show("You don't have the necessary permissions", "Information");
            }
            catch (FormatException)
            {
                result = MessageBox.Show("There has been a problem with the format", "Information");
            }
            catch (Win32Exception)
            {
                result = MessageBox.Show("There has been a problem", "Information");
            }
        }
    }
}