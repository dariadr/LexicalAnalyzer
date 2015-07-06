using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string st="";
            String[] s = textBox1.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Length; i++)
            {
                st = s[i];
                while (st != "")
                {
                    if ((new Regex(@"\s*|\t*").Match(st)).Success)
                        st=st.Remove(0, new Regex(@"\s*|\t*").Match(st).Length);
                    if (st == "") break;
                    if ((new Regex(@"^while[^A-Za-z\d]").Match(st)).Success || st=="while")
                    {
                        st=st.Remove(0, 5);
                        textBox2.AppendText("KW(while) " + Environment.NewLine);
                        continue;
                    }
                    if ((new Regex(@"^do[^A-Za-z0-9]").Match(st)).Success || st == "do")
                    {
                        st=st.Remove(0, 2);
                        textBox2.AppendText("KW(do)" + Environment.NewLine);
                        continue;
                    }

                    if ((new Regex(@"^or[^A-Za-z0-9]").Match(st)).Success || new Regex(@"^or").Match(st).Length == st.Length)
                    {
                        st=st.Remove(0, new Regex(@"^or").Match(st).Length);
                        textBox2.AppendText("or" + " или" + Environment.NewLine);
                        continue;
                    }
                    if ((new Regex(@"^and[^A-Za-z0-9]").Match(st)).Success || new Regex(@"^and").Match(st).Length == st.Length)
                    {
                        st=st.Remove(0, new Regex(@"^and").Match(st).Length);
                        textBox2.AppendText("and" + " и" + Environment.NewLine);
                        continue;
                    }
                    if ((new Regex(@":=").Match(st)).Success)
                    {
                        st=st.Remove(0, 2);
                        textBox2.AppendText(":=" + " присвоение" + Environment.NewLine);
                        continue;
                    }
                    if ((new Regex(@"^\(+").Match(st)).Success)
                    {
                        textBox2.AppendText(new Regex(@"^\(+").Match(st) + " скобка(и)" + Environment.NewLine);
                        st=st.Remove(0, new Regex(@"^\(+").Match(st).Length);
                        continue;
                    }
                    if ((new Regex(@"^\)+").Match(st)).Success)
                    {
                        textBox2.AppendText(new Regex(@"^\)+").Match(st) + " скобка(и)" + Environment.NewLine);
                        st=st.Remove(0, new Regex(@"^\)+").Match(st).Length);
                        continue;
                    }
                    if ((new Regex(@"^[<>=]").Match(st)).Success)
                    {
                        textBox2.AppendText(new Regex(@"^[<>=]|[[<>]=]").Match(st) + "оператор сравнения" + Environment.NewLine);
                        st=st.Remove(0, new Regex(@"^[<>=]|[[<>]=]").Match(st).Length);
                        continue;
                    }
                    if ((new Regex(@"^;").Match(st)).Success)
                    {
                        textBox2.AppendText(new Regex(@"^;").Match(st) + "конец действия" + Environment.NewLine);
                        st = st.Remove(0, new Regex(@"^;").Match(st).Length);
                        continue;
                    }
                    if ((new Regex(@"^[A-Za-z_][A-Za-z_0-9]*[^A-Za-z0-9]").Match(st)).Success || new Regex(@"^[A-Za-z_][A-Za-z_0-9]*").Match(st).Length == st.Length)
                    {
                        textBox2.AppendText(new Regex(@"^\(*[A-Za-z_][A-Za-z_0-9]*").Match(st) + " идентификатор" + Environment.NewLine);
                        st = st.Remove(0, new Regex(@"^\(*[A-Za-z_][A-Za-z_0-9]*").Match(st).Length);
                        continue;
                    }
                    if ((new Regex(@"^\d+[^A-Za-z0-9]").Match(st)).Success || new Regex(@"^\d+").Match(st).Length == st.Length)
                    {
                        textBox2.AppendText(new Regex(@"^[0-9]+").Match(st) + " число" + Environment.NewLine);
                        st = st.Remove(0, new Regex(@"^[0-9]+").Match(st).Length);
                        continue;
                    }
                    textBox2.AppendText("ошибка компиляции" + Environment.NewLine);
                    return;
                }
            }
              
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string s;
                System.IO.StreamReader str = new System.IO.StreamReader(openFileDialog1.FileName);
                while ((s = str.ReadLine())!=null)
                    textBox1.AppendText(s + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
