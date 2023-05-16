using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mBank.Forms
{
    public partial class FormDepos : Form
    {
        private Form activeForm;

        public FormDepos()
        {
            InitializeComponent();
        }

        private void FormDepos_Load(object sender, EventArgs e)
        {
            LoadTheme();
            FormDepos formDepos = new FormDepos();
            activeForm = formDepos;
            OpenChildForm(new Forms.Deposit.FormDepMain(), sender);
            btnGl.Enabled = false;
        }


        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            //ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPaneDep.Controls.Add(childForm);
            this.panelDesktopPaneDep.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnEnabled()
        {
            btnGl.Enabled = true;
            btnHist.Enabled = true;
            btnOper.Enabled = true;
            btnAnalis.Enabled = true;
        }

        private void btnGl_Click(object sender, EventArgs e)
        {
            btnEnabled();
            OpenChildForm(new Forms.Deposit.FormDepMain(), sender);
            btnGl.Enabled = false;
        }

        private void btnOper_Click(object sender, EventArgs e)
        {
            btnEnabled();
            OpenChildForm(new Forms.Deposit.FormDepOper(), sender);
            btnOper.Enabled = false;
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            btnEnabled();
            OpenChildForm(new Forms.Deposit.FormDepHistory(), sender);
            btnHist.Enabled = false;
        }

        private void btnAnalis_Click(object sender, EventArgs e)
        {
            btnEnabled();
            OpenChildForm(new Forms.Deposit.FormDepAnalysis(), sender);
            btnAnalis.Enabled = false;
        }
    }
}
