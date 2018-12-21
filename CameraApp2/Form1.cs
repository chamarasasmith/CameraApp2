using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CameraApp2
{
    public partial class Form1 : Form
    {
        FilterInfoCollection webcam;
        VideoCaptureDevice cam;
        public Form1()
        {
            InitializeComponent();

            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in webcam)
            {
                comboBox1.Items.Add(item.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_newFrame);
            cam.Start();
        }

        private void cam_newFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "@C:\\";
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName+".jpg");
            }
        }
    }
}
