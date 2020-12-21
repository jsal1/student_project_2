using System;
using System.Windows.Forms;
using System.IO;

namespace course4
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OPEN_Click(object sender, EventArgs e)
        {
            OpenFileDialog OF = new OpenFileDialog();
            OF.Filter = "algol files (*.algol)|*.algol";
            OF.Multiselect = false;
            OF.InitialDirectory = Application.StartupPath;
            if (OF.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RTB.Text = File.ReadAllText(OF.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error load: " + ex.Message);
                }
            }
        }

        private void SAVE_Click(object sender, EventArgs e)
        {
            SaveFileDialog SF = new SaveFileDialog();
            SF.Filter = "algol files (*.algol)|*.algol";
            SF.RestoreDirectory = true;
            if (SF.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(SF.FileName, RTB.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error save: " + ex.Message);
                }
            }
        }

        private void ENTER_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                if (RTB.TextLength > 0)
                {
                    var Res = Analiz.LexAnalizText(RTB.Text);
                    i++;
                    SNX.AnalyzeText(Res);
                    i++;
                    MessageBox.Show("Лексический и синтаксический анализы\nУспешно\nСейчас откроются таблицы", "УСПЕХ");
                    TableFormOpen();
                }
                else MessageBox.Show("Выполнять ничего");
            }
            catch(ERR er)
            {
                string err=null;
                if (i == 0)
                {
                    err = "Ни один из анализов не выполнен";
                }
                else if (i==1)
                {
                    err = "Лексический анализ успешно";
                }
                else if (i == 2)
                {
                    err = "Лексический и синтаксический анализ успешно";
                }
                MessageBox.Show("Прервано!\n"+er+"\n P.S.:"+err,"ERROR");
            }
            
        }

        private void O_Click(object sender, EventArgs e)
        {
            MessageBox.Show("По вопросам\nsidorovtoha@gmail.com");
        }

        void TableFormOpen()
        {            
            TableForm TB = new TableForm();
            TB.ShowDialog();
        }
    }
}
