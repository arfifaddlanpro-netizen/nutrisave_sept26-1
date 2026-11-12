using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sep_26_nutrisave
{
    public partial class Form4 : Form
    {
        public static List<string> Medications = new List<string>();
        public Form4()
        {
            InitializeComponent();
            dataGridView2.CellContentClick += dataGridView2_EditClick;
            btnFoodInventory.Click += btnFoodInventory_Click;
            btnMealNutrition.Click += btnMealNutrition_Click;
            btnMedication.Click += btnMedication_Click;
            btnFoodSharing.Click += btnFoodSharing_Click;
            btnReport.Click += btnReport_Click;
            dtpMedicationDate.Format = DateTimePickerFormat.Custom;
            dtpMedicationDate.CustomFormat = "dd/MM/yyyy hh:mm tt";

            cmbFrequency.Items.Add("Once daily");
            cmbFrequency.Items.Add("Twice daily");
            cmbFrequency.Items.Add("Three times daily");
            cmbFrequency.Items.Add("Every 4 hours");
            cmbFrequency.Items.Add("Every 6 hours");
            cmbFrequency.Items.Add("Every 8 hours");
            cmbFrequency.Items.Add("As needed");
        }


        private void btnSaveMedication_Click(object sender, EventArgs e)
        {
            string medicationName = txtMedicationName.Text;
            string dosage = txtDosage.Text;
            string time = dtpMedicationTime.Value.ToShortTimeString();

            if (medicationName == "" || dosage == "" || cmbFrequency.Text == "")
            {
                MessageBox.Show("Please enter the medication name, dosage and frequency.");
                return;
            }

           

            DateTime scheduledDateTime =
    dtpMedicationDate.Value.Date + dtpMedicationTime.Value.TimeOfDay;

            string status;

            if (scheduledDateTime > DateTime.Now)
            {
                status = "Pending";
            }
            else
            {
                status = "Overdue";
            }

            dataGridView2.Rows.Add(
                medicationName,
                dosage,
                time,
                status
            );
            Medications.Add(medicationName);
            MessageBox.Show("Medication saved successfully!");
        }
        private void dataGridView2_EditClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this medication?",
                    "Delete Medication",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
            }

            if (e.ColumnIndex == dataGridView2.Columns["Actions"].Index)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txtMedicationName.Text = row.Cells["Medication"].Value?.ToString();
                txtDosage.Text = row.Cells["Dosage"].Value?.ToString();

                DateTime time;

                if (DateTime.TryParse(row.Cells["Time"].Value?.ToString(), out time))
                {
                    dtpMedicationDate.Value = time;
                }
            }
        }
        private void dataGridView2_EditClick2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this medication?",
                    "Delete Medication",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
            }

            if (e.ColumnIndex == dataGridView2.Columns["Actions"].Index)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txtMedicationName.Text = row.Cells["Medication"].Value?.ToString();
                txtDosage.Text = row.Cells["Dosage"].Value?.ToString();

                DateTime time;

                if (DateTime.TryParse(row.Cells["Time"].Value?.ToString(), out time))
                {
                    dtpMedicationDate.Value = time;
                }
            }
        }
        private void btnFoodInventory_Click(object sender, EventArgs e)
        {
            Form2 foodInventory = new Form2();
            foodInventory.Show();
            this.Hide();
        }
        private void btnMealNutrition_Click(object sender, EventArgs e)
        {
            Form3 meal = new Form3();
            meal.Show();
            this.Hide();
        }
        private void btnMedication_Click(object sender, EventArgs e)
        {
            Form4 medication = new Form4();
            medication.Show();
            this.Hide();
        }
        private void btnFoodSharing_Click(object sender, EventArgs e)
        {
            Form5 foodSharing = new Form5();
            foodSharing.Show();
            this.Hide();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            Form6 reports = new Form6();
            reports.Show();
            this.Hide();
        }

        private void dtpMedicationDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
   
    

