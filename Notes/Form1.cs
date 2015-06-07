using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Notes
{
    public partial class Notes : Form
    {
        /// <summary>
        /// Store the login property.
        /// </summary>
        private string login    =   "admin";
        /// <summary>
        /// Store the password property.
        /// </summary>
        private string password =   "admin";

        private Panel panel_l;
        private Panel panel_n;

        private TextBox textBox_l;
        private TextBox textBox_p;
        private TextBox textBox_n;

        private Button button_l;
        private Button button_n;
        private Button button_e;
        private Button button_c;
        
        #region Methods

        /// <summary>
        /// Checks for a correct login and password.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void check(Object o, EventArgs e)
        {
            string temp_l = this.textBox_l.Text;
            if (temp_l == this.login)
            {
                string temp_p = textBox_p.Text;
                if (temp_p == this.password) this.createNote();
                else MessageBox.Show("Invalid password");
            }
            else MessageBox.Show("Invalid username");
        }

        /// <summary>
        /// Key handlig for more comfortable usage of app.
        /// To move from Login to Password field just click Enter.
        /// To log in, press Enter while focusing Password field.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e">Key that is pressed</param>
        private void keyPressed(object o, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.textBox_l.Focused) this.textBox_p.Focus();
                else if (this.textBox_p.Focused) this.check(e, EventArgs.Empty);
            }
        }
        
        /// <summary>
        /// Enable Ctrl+A for Select All in Note field.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e">Key that are down.</param>
        private void keyDown(object o, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.textBox_n.SelectAll();
            }
        }

        /// <summary>
        /// Save a Note to a new file or adds to an exisitng one.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void saveFile(object o, EventArgs e)
        {            
            string[] save_n = {login + " " + DateTime.Now, textBox_n.Text};
            try
            {
                using (StreamWriter add_n = new StreamWriter(@"Note.txt", true))
                {
                    add_n.WriteLine(save_n[0]);
                    add_n.WriteLine(save_n[1]);
                }
            }
            catch (IOException ex)
            {
                File.WriteAllLines(@"Note.txt", save_n);
            }
        }

        /// <summary>
        /// Dispose a watermark over Login field.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void enterTextBoxHandler_l(object o, EventArgs e)
        {
            if (textBox_l.Text == "Login")
            {
                this.textBox_l.Text = "";
                this.textBox_l.ForeColor = System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// Show watermark over Login field.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void leaveTextBoxHandler_l(object o, EventArgs e)
        {
            if (this.textBox_l.TextLength == 0)
            {
                this.textBox_l.Text = "Login";
                this.textBox_l.ForeColor = System.Drawing.Color.Gray;
            }
        }

        /// <summary>
        /// Dispose watermark over Password field hides it.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void enterTextBoxHandler_p(object o, EventArgs e)
        {
            if (this.textBox_p.Text == "Password")
            {
                this.textBox_p.Text = "";
                this.textBox_p.ForeColor = System.Drawing.Color.Black;
                this.textBox_p.PasswordChar = '*';
            }
        }

        /// <summary>
        /// Show watermark over Password field and reveals it.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void leaveTextBoxHandler_p(object o, EventArgs e)
        {
            if (this.textBox_p.TextLength == 0)
            {
                this.textBox_p.Text = "Password";
                this.textBox_p.ForeColor = System.Drawing.Color.Gray;
                this.textBox_p.PasswordChar = '\0';
            }
        }

        /// <summary>
        /// Exit the program.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void exit(object o, EventArgs e)
        {
            Application.Exit();
        }
        
        /// <summary>
        /// Creates a Logging panel.
        /// </summary>
        private void createLogin()
        {
            this.panel_l = new Panel();
            this.textBox_l = new TextBox();
            this.textBox_p = new TextBox();
            this.button_l = new Button();
            this.button_e = new Button();
            this.button_c = new Button();

            this.panel_l.Controls.Add(textBox_l);
            this.panel_l.Controls.Add(textBox_p);
            this.panel_l.Controls.Add(button_l);
            this.panel_l.Controls.Add(button_e);
            this.panel_l.Controls.Add(button_c);
            this.panel_l.Location = new Point(50, 20);
            this.panel_l.Height = 170;

            this.textBox_l.Width = 200;
            this.textBox_l.Text = "";
            this.textBox_l.TextAlign = HorizontalAlignment.Center;
            this.textBox_l.KeyPress += keyPressed;
            this.textBox_l.Enter += enterTextBoxHandler_l;
            this.textBox_l.Leave += leaveTextBoxHandler_l;

            this.textBox_p.Width = 200;
            this.textBox_p.Text = "Password";
            this.textBox_p.ForeColor = System.Drawing.Color.Gray;
            this.textBox_p.TextAlign = HorizontalAlignment.Center;
            this.textBox_p.Location = new Point(0, 30);
            this.textBox_p.KeyPress += keyPressed;
            this.textBox_p.Enter += enterTextBoxHandler_p;
            this.textBox_p.Leave += leaveTextBoxHandler_p;

            this.button_l.Width = 95;
            this.button_l.Text = "Log in";
            this.button_l.Location = new Point(0, 60);
            this.button_l.Click += check;

            this.button_e.Width = 95;
            this.button_e.Text = "Exit";
            this.button_e.Location = new Point(105, 60);
            this.button_e.Click += exit;

            this.button_c.Width = 200;
            this.button_c.Text = "Change color";
            this.button_c.Location = new Point(0, 90);
            this.button_c.Click += dialog;

            this.Controls.Add(panel_l);
        }

        /// <summary>
        /// Open Color Dialog for Color pick.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void dialog(Object o, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            diag.ShowDialog();
            this.BackColor = diag.Color;
        }

        /// <summary>
        /// Creates a Note panel.
        /// </summary>
        private void createNote()
        {
            this.Controls.Remove(panel_l);

            this.panel_n = new Panel();
            this.textBox_n = new TextBox();
            this.button_n = new Button();
            this.button_e = new Button();

            this.panel_n.Controls.Add(textBox_n);
            this.panel_n.Controls.Add(button_n);
            this.panel_n.Controls.Add(button_e);
            this.panel_n.Controls.Add(button_c);
            this.panel_n.Location = new Point(50, 20);
            this.panel_n.Height = 170;

            this.textBox_n.Size = new Size(200, 50);
            this.textBox_n.Multiline = true;
            this.textBox_n.KeyDown += keyDown;

            this.button_n.Width = 95;
            this.button_n.Text = "Save me!";
            this.button_n.Location = new Point(0, 60);
            this.button_n.Click += saveFile;

            this.button_e.Width = 95;
            this.button_e.Text = "Exit";
            this.button_e.Location = new Point(105, 60);
            this.button_e.Click += exit;

            this.button_c.Width = 200;
            this.button_c.Text = "Change color";
            this.button_c.Location = new Point(0, 90);
            this.button_c.Click += dialog;

            this.Controls.Add(panel_n);

            this.textBox_n.Focus();
        }

        #endregion

        public Notes()
        {
            this.Text = "Note";
            this.Size = new Size(300, 190);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.createLogin();
         }



    }
}
