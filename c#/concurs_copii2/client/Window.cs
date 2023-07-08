using model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Window : Form
    {
        private readonly ClientCtrl ctrl;
        private  IList<Challenge> challengesData;
        private BindingList<Challenge> bindingList;



        public Window(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            reloadDataGridView();
            dataGridViewChallenges.RowPostPaint += dataGridView1_RowPostPaint;
            dataGridViewChallenges.Columns[dataGridViewChallenges.Columns.Count - 1].Visible = false;

            
            ctrl.updateEvent += userUpdate;
            
            dataGridViewChallenges.DataError += dataGridView_DataError;
            dataGridViewCompetitors.DataError += dataGridView_DataError;
            
        }
        
        private void reloadDataGridView()
        {
            dataGridViewChallenges.Rows.Clear();
            List<Challenge> challengesList = ctrl.FindAllChallenges().ToList();
            challengesData = challengesList;
            bindingList = new BindingList<Challenge>(challengesData);
            dataGridViewChallenges.DataSource = bindingList;
            
            
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String tbcn = textBoxChallengeName.Text;
            String tbc = textCategory.Text;
            if (!string.IsNullOrWhiteSpace(tbcn) && !string.IsNullOrWhiteSpace(tbc))
            {

                List<Competitor> list = ctrl.FindCompetitors(tbcn,tbc);


                dataGridViewCompetitors.DataSource = null;
                dataGridViewCompetitors.DataSource = list;
                if (dataGridViewCompetitors.Columns.Count > 0)
                {
                    dataGridViewCompetitors.Columns[dataGridViewCompetitors.Columns.Count - 1].Visible = false;
                }
            }
            else
            {
                dataGridViewCompetitors.DataSource = null;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            String tbn = textBoxName.Text;
            String tba = textBoxAge.Text;
            String tbc1 = textBoxChallenge1.Text;
            String tbc2 = textBoxChallenge2.Text;

            if (!string.IsNullOrEmpty(tbn) && !string.IsNullOrEmpty(tba) && !string.IsNullOrEmpty(tbc1))
            {
                List<int> lista = new List<int>();
                lista.Add(int.Parse(tbc1));
                    
                if (!string.IsNullOrEmpty(tbc2))
                {
                    lista.Add(int.Parse(tbc2));
                        
                }
                ctrl.SaveCompetitor(tbn, int.Parse(tba), lista);
                MessageBox.Show("Success!");
             }
             else
             {
                MessageBox.Show("Empty!");
             }

        }

        public void userUpdate(object sender, UserEventArgs e)
        {
            if (e.UserEventType == UserEvent.NewCompetitor)
            {
                dataGridViewChallenges.BeginInvoke((Action)delegate
                {
                    challengesData.Clear();
                    List<Challenge> challengesList = ctrl.FindAllChallenges().ToList();
                    
                    foreach (Challenge challenge in challengesList)
                    {
                        challengesData.Add(challenge);
                    }
                    
                    
                    bindingList.ResetBindings();
                    dataGridViewChallenges.DataSource = bindingList;
                });
            }
        }

        
        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            ctrl.logout();

            Application.Exit();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Window closing " + e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ctrl.logout();
                
                Application.Exit();
            }
        }
        
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

    }
}
