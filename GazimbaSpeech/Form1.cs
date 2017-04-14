using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace GazimbaSpeech
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            label1.Text = "What do you want to do today?";

            synth.SpeakAsync(label1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            if (textBox1.Text == "Hello" || textBox1.Text == "Hi")
            {
                synth.SpeakAsync("Hi!");
                label1.Text = "Hi!";
            }
            else if (textBox1.Text.Contains("Play") || textBox1.Text.Contains("play"))
            {
                label1.Text = "Playing " + textBox1.Text.Substring(5);
                synth.SpeakAsync(label1.Text);
                axWindowsMediaPlayer1.URL = s + @"\" + textBox1.Text.Substring(5) + ".mp3";
                axWindowsMediaPlayer1.Visible = false;
            }
            else if (textBox1.Text.Contains("Video") || textBox1.Text.Contains("video"))
            {
                label1.Text = "Playing " + textBox1.Text.Substring(6);
                synth.SpeakAsync(label1.Text);
                axWindowsMediaPlayer1.URL = s + @"\" + textBox1.Text.Substring(6) + ".mp4";
                axWindowsMediaPlayer1.Visible = true;
            }
            else if (textBox1.Text == "Stop" || textBox1.Text == "stop")
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                label1.Text = "Stopped";
                synth.SpeakAsync(label1.Text);
            }
            else if (textBox1.Text == "Exit" || textBox1.Text == "exit" || textBox1.Text == "Quit" || textBox1.Text == "quit")
            {
                label1.Text = "Bye bye!";
                synth.SpeakAsync(label1.Text);
                System.Threading.Thread.Sleep(1200);
                Application.Exit();
            }
            else if (textBox1.Text == "Pause" || textBox1.Text == "pause")
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                label1.Text = "Paused";
                synth.SpeakAsync(label1.Text);
            }
            else if (textBox1.Text == "Resume" || textBox1.Text == "pause")
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                label1.Text = "Playing";
                synth.SpeakAsync(label1.Text);
            }
            else
            {
                synth.SpeakAsync("I'm not so smart for this time, so I don't know what to do!");
                label1.Text = "I'm not so smart for this time, so I don't know what to do!";
            }
            
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = GazimbaSpeech.Properties.Resources.closeButtonHover;
            pictureBox2.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = GazimbaSpeech.Properties.Resources.closeButton;
            pictureBox2.Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }
}
