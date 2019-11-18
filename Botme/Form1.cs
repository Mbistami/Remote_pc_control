using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
using System.Drawing.Imaging;

namespace Botme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GuerrillaMail Mail = new GuerrillaMail();
        GuerrillaMail.Email reply = new GuerrillaMail.Email();
        int i = 0;

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void sender()
        {
            
            MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                bmp.Save("Screenshot" + i + ".png");  // saves the image
            }
            mail.From = new MailAddress("botme.boter@gmail.com");
            mail.To.Add(gunaTextBox1.Text);
            mail.Subject = "Bot state";



            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment("Screenshot" + i + ".png");
            attachment.ContentDisposition.Inline = false;
            mail.Body = string.Format("Hey" + @"<img src=""cid:{0}"" />", attachment.ContentId);
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("botme.boter@gmail.com", "a642705MOUSSABTM");
            smtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;
            mail.Attachments.Add(attachment);
            smtpServer.Send(mail);
            //File.Delete("Screenshot.png");
            MessageBox.Show("Sent");
            i++;
            for (int j = 0; j < i; j++)
            {
                File.Delete("Screenshot" + j + ".png");
                MessageBox.Show("sss");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gunaLabel3.Text = Mail.GetMyEmail().ToString();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(gunaLabel3.Text);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Mail.GetLastEmail() != null)
            {
                var check = Mail.GetLastEmail().mail_subject.ToLower();
                var uqid = Mail.GetLastEmail().mail_id.ToString();
                if (check.Equals("botmehello"))
                {
                    Mail.DeleteSingleEmail(uqid);
                    MessageBox.Show("Done");
                    this.sender();
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            
        }
    }
}
