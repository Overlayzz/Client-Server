using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mBank.Forms
{
    public partial class MsgBox : Form
    {
        public bool clickBtnOK;
        public bool clickBtnClose;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MovingForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public MsgBox()
        {
            InitializeComponent();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            clickBtnClose = false;
            clickBtnOK = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clickBtnOK = false;
            clickBtnClose = true;
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MovingForm();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            MovingForm();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            MovingForm();
        }

        private void lblTextMsg_MouseDown(object sender, MouseEventArgs e)
        {
            MovingForm();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            MovingForm();
        }

        private void MsgBox_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void MsgBox_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void MsgBox_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }
    }
}
