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
using System.Diagnostics;
using System.Collections;

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
        List<string> ls = new List<string>();
        int i = 0;

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void Fullsender()
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
            i++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gunaLabel3.Text = Mail.GetMyEmail().ToString();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        public void KillPross(string mail_Body)
        {
            Process[] processes = Process.GetProcessesByName(mail_Body);
            foreach (var process in processes)
            {
                process.Kill();
            }
        }

        public void prosses()
        {
            ls.Clear();
            var processss = from proc in System.Diagnostics.Process.GetProcesses() orderby proc.ProcessName ascending select proc;
            foreach (var item in processss)
            {
                ls.Add(item.ProcessName);
            }
            MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("Botme.boter@gmail.com");
            mail.To.Add(gunaTextBox1.Text);
            mail.Subject = "Active Prosses";
            mail.Body = string.Format("Here the list of your active prosses:");
            foreach (var item in ls)
            {
                mail.Body = mail.Body + Environment.NewLine + item;
            }
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("botme.boter@gmail.com", "a642705MOUSSABTM");
            smtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);
        }

        public void MainM()
        {
            MailMessage mail = new MailMessage();
            System.Net.Mail.SmtpClient smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("Botme.boter@gmail.com");
            mail.To.Add(gunaTextBox1.Text);
            mail.Subject = "BotMe Main Menu";
            mail.Body = string.Format("Hello User I'm connected to your " + Environment.MachineName + ",Here what I can do Get Active Prosses;Shutdown Pross;Screenshot actual Screen;Screenshot on Active Pross;Send you a file;get floder content;");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("botme.boter@gmail.com", "a642705MOUSSABTM");
            smtpServer.EnableSsl = true;
            mail.IsBodyHtml = true;
            smtpServer.Send(mail);
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
                    this.MainM();
                }
                else if (check.Equals("screenshot actual screen"))
                {
                    Mail.DeleteSingleEmail(uqid);
                    this.Fullsender();
                }
                else if (check.Equals("active prosses"))
                {
                    Mail.DeleteSingleEmail(uqid);
                    this.prosses();
                }
                else if(check.Equals("shutdown pross"))
                {
                    string s = Mail.GetEmail(Convert.ToInt32(uqid)).mail_body;
                    string[] st;
                    s = s.Remove(0, 5);
                    st = s.Split('<');
                    this.KillPross(st[0]);
                    Mail.DeleteSingleEmail(uqid);
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            Process.Start("Cleaner.exe");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.Start("Cleaner.exe");
        }
    }
}
