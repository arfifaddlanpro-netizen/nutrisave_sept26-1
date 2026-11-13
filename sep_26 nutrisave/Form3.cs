using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sep_26_nutrisave
{
    public partial class Form3 : Form
    {
        public static List<string> MealRecords = new List<string>();
        public Form3()
        {
            InitializeComponent();
            AdminNavigation.Attach(this, panel6);
            dataGridView2.CellContentClick += dataGridView2_EditClick;
            btnDashboard.Click += btnDashboard_Click;
            btnFoodInventory.Click += btnFoodInventory_Click;
            btnMealNutrition.Click += btnMealNutrition_Click;
            btnMedication.Click += btnMedication_Click;
            btnFoodSharing.Click += btnFoodSharing_Click;
            btnReport.Click += btnReport_Click;
        }

        private void btnSaveMeal_Click(object sender, EventArgs e)

        {
            string mealName = txtMealName.Text;
            string mealType = cmbMealType.Text;
            string mealDate = dtpMealDate.Value.ToShortDateString();
            int calories = (int)numCalories.Value;
            string notes = txtNotes.Text;

            if (mealName == "" || mealType == "")
            {
                MessageBox.Show("Please enter the meal name and meal type.");
                return;
            }

            dataGridView2.Rows.Add(
                mealName,
                mealType,
                mealDate,
                calories,
                notes
            );
            MealRecords.Add(mealName);

            MessageBox.Show("Meal saved successfully!");

            txtMealName.Clear();
            cmbMealType.SelectedIndex = -1;
            numCalories.Value = 0;
            txtNotes.Clear();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["Actions"].Index)

            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txtMealName.Text = row.Cells["MealName"].Value?.ToString();
                cmbMealType.Text = row.Cells["MealType"].Value?.ToString();
                dtpMealDate.Value = DateTime.Parse(row.Cells["MealDate"].Value.ToString());
                numCalories.Value = Convert.ToDecimal(row.Cells["Calories"].Value);
                txtNotes.Text = row.Cells["Notes"].Value?.ToString();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMealName.Clear();
            cmbMealType.SelectedIndex = -1;
            dtpMealDate.Value = DateTime.Today;
            numCalories.Value = 0;
            txtNotes.Clear();
        }
        private void dataGridView2_EditClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // EDIT BUTTON
            if (e.ColumnIndex == dataGridView2.Columns["Actions"].Index)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txtMealName.Text = row.Cells["Meal"].Value?.ToString();
                cmbMealType.Text = row.Cells["Type"].Value?.ToString();
                dtpMealDate.Value = DateTime.Parse(row.Cells["Date"].Value?.ToString());
                numCalories.Value = Convert.ToDecimal(row.Cells["Calories (kcal)"].Value);
                txtNotes.Text = row.Cells["Notes"].Value?.ToString();
            }

            // DELETE BUTTON
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this meal?",
                    "Delete Meal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
            
                private void btnDashboard_Click(object sender, EventArgs e)
        {
                     Form1 dashboard = new Form1();
                     dashboard.Show();
                     this.Hide();
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
    }

        }
    
    
    
        
   
    

