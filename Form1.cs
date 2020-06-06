using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficLights
{
    public partial class TrafficLights : Form
    {
        private Timer timerColorSwitch = null;
        private Timer timerBlink = null;
        private PictureBox picB = null;
        private Color color = Color.Gray;
        private int seconds = 0;

        public TrafficLights()
        {
            InitializeComponent();
            InitiaizeTrafficLights();
            InitializeTimerColorSwitch();
            InitializeTimerBlink();
        }

        private void InitiaizeTrafficLights()
        {

            // set shape
            RedLight.Region = Round(RedLight);
            YellowLight.Region = Round(YellowLight);
            GreenLight.Region = Round(GreenLight);
            // Set Backcolor
            RedLight.BackColor = Color.Gray;
            YellowLight.BackColor = Color.Gray;
            GreenLight.BackColor = Color.Gray;

            
        }

        private void InitializeTimerColorSwitch()
        {
            timerColorSwitch = new Timer
            {
                Interval = 1000
            };
            timerColorSwitch.Tick += new EventHandler(TimerColorSwitch_Tick);
            timerColorSwitch.Start();

        }

        private void InitializeTimerBlink()
        {
            timerBlink = new Timer
            {
                Interval = 200
            };
            timerBlink.Tick += new EventHandler(TimerBlink_Tick);
        }

        private void TimerColorSwitch_Tick(object sender, EventArgs e)
        {
            switch (seconds)
            {
                case 0:
                    RedLight.BackColor = Color.Red;
                    break;
                case 3:
                    YellowLight.BackColor = Color.Yellow;

                    picB = RedLight;
                    color = Color.Red;
                    timerBlink.Start();
                    break;
                case 5:
                    timerBlink.Stop();
                    YellowLight.BackColor = Color.Gray;
                    GreenLight.BackColor = Color.Green;
                    break;
                case 6:
                    picB = GreenLight;
                    color = Color.Green;
                    timerBlink.Start();
                    break;
                case 8:
                    YellowLight.BackColor = Color.Yellow;
                    GreenLight.BackColor = Color.Gray;
                    timerBlink.Stop();
                    break;
                case 10:
                    YellowLight.BackColor = Color.Gray;
                    RedLight.BackColor = Color.Red;
                    seconds = -1;
                    break;
            }
            seconds++;
        }

        private void TimerBlink_Tick(object sender, EventArgs e)
        {
            if (picB.BackColor == color)
            {
                picB.BackColor = Color.Gray;
            }
            else
            {
                picB.BackColor = color;
            }
        }

        private Region Round(PictureBox picB)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picB.Width, picB.Height);
            return new Region(gp);
        }
    }
}
