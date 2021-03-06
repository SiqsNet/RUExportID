using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
// using System.Xml;
// using System.Text.RegularExpressions;

namespace RULocalExport
{
    public partial class Form1 : Form
    {
        // variables
        public string pathXML = "RU_InternetID.xml";
        public string pathTXT = "RU_InternetID.txt";

        // forms
        public Form1()
        {
            InitializeComponent();
        }

        // load forms
        private void Form1_Load(object sender, EventArgs e)
        {




            // delete local file
            File.Delete(pathXML);
            File.Delete(pathTXT);

            // variable registry key
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Usoris\Remote Utilities Host\Host\Parameters");

            if (key != null)
            {

                Object o = key.GetValue("InternetId", RegistryValueKind.Binary);
                byte[] b = (byte[])o;
                string vOut = System.Text.Encoding.UTF8.GetString(b);

                if (o != null)

                {

                    if (!File.Exists(pathXML))
                    {
                        using (StreamWriter sw = File.CreateText(pathXML))
                        {
                            sw.WriteLine(vOut);
                        }
                    }

                    else

                    {
                        // do not do anything
                        // nic nie rób
                       
                    }

                }

            }

            xmlexport();

        }
        public void xmlexport()
        {

            if (File.Exists(pathXML))
            {
                string text = File.ReadAllText(pathXML);

                int num = 1;

                char[] separators = new char[] { '<', '>' };

                string[] subs = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (string sub in subs)
                {
                    num = num + 1;

                    // The 6th instance of a string is RU InternetID
                    // 6 wystąpienie stringa to RU InternetID

                    if (num == 6)
                    {

                        if (!File.Exists(pathTXT))
                        {
                            using (StreamWriter sw = File.CreateText(pathTXT))
                            {
                                sw.WriteLine($"{sub}");
                            }
                        }

                        else

                        {
                            // do not do anything
                            // nic nie rób
                         
                        }

                    }

                    // If there is an OpenProgram file, leave it open
                    // Zostaw otwarty jezeli jest plik OpenProgram.txt

                    if (!File.Exists("OpenProgram.txt"))
                    {
                        Application.Exit();
                    }
      
                }
            }

        }



        private void btnclick(object sender, EventArgs e)
        {

            if (File.Exists(pathTXT))
            {
                string remoteID = File.ReadAllText(pathTXT);
                MessageBox.Show(remoteID);
            }  

        }

    }
}
