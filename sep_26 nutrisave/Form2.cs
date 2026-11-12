using sep_26_nutrisave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sep_26_nutrisave
{


    public partial class Form2 : Form
    {
        public static List<FoodItem> FoodItems = new List<FoodItem>();



        public Form2()
        {
            InitializeComponent();


            dataGridView2.CellContentClick += dataGridView2_EditClick;
            btnDashboard.Click += btnDashboard_Click;
            btnFoodInventory.Click += btnFoodInventory_Click;
            btnMealNutrition.Click += btnMealNutrition_Click;
            btnMedication.Click += btnMedication_Click;
            btnFoodSharing.Click += btnFoodSharing_Click;
            btnReport.Click += btnReport_Click;


            UpdateSummary();
        }






        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all food items from the grid
            dataGridView2.Rows.Clear();

            txtFoodName.Clear();
            cmbCategory.SelectedIndex = -1;
            numQuantity.Value = 1;
            cmbUnit.SelectedIndex = -1;
            dtpExpiryDate.Value = DateTime.Today;
            cmbStorage.SelectedIndex = -1;

        }
        private void dataGridview2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string foodName = txtFoodName.Text;
            string category = cmbCategory.Text;
            int quantity = (int)numQuantity.Value;
            string unit = cmbUnit.Text;
            string expiryDate = dtpExpiryDate.Value.ToShortDateString();
            string storage = cmbStorage.Text;

            if (foodName == "" || category == "")
            {
                MessageBox.Show("Please enter the food name and category.");
                return;
            }
            string status;

            if (dtpExpiryDate.Value.Date < DateTime.Today)
            {
                status = "Expired";
            }
            else if ((dtpExpiryDate.Value.Date - DateTime.Today).TotalDays <= 3)
            {
                status = "Expiring";
            }
            else
            {
                status = "Good";
            }

            dataGridView2.Rows.Add(
                foodName,
                category,
                quantity,
                unit,
                expiryDate,
                storage,
                status
            );

            FoodItems.Add(new FoodItem
            {
                FoodName = foodName,
                Category = category,
                Quantity = quantity,
                ExpiryDate = expiryDate,
                Status = status
            });
            MessageBox.Show("FoodItems count: " + FoodItems.Count);


            UpdateSummary();

                MessageBox.Show("Food item added successfully!");

                txtFoodName.Clear();
                cmbCategory.SelectedIndex = -1;
                numQuantity.Value = 0;
                cmbUnit.SelectedIndex = -1;
                dtpExpiryDate.Value = DateTime.Today;
                cmbStorage.SelectedIndex = -1;
            }
        
        private void dataGridView2_EditClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // EDIT
            if (e.ColumnIndex == dataGridView2.Columns["Actions"].Index)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                txtFoodName.Text = row.Cells["FoodName"].Value?.ToString();
                cmbCategory.Text = row.Cells["Category"].Value?.ToString();
                numQuantity.Value = Convert.ToDecimal(row.Cells["Quantity"].Value);
                cmbUnit.Text = row.Cells["Unit"].Value?.ToString();
                dtpExpiryDate.Value = DateTime.Parse(row.Cells["ExpiryDate"].Value?.ToString());
                cmbStorage.Text = row.Cells["StorageLocation"].Value?.ToString();
            }

            // DELETE
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this food item?",
                    "Delete Food",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                    UpdateSummary();
                }
            }
        }

        private void UpdateSummary()
        {
            // Total Food Items
            lblTotalFood.Text = dataGridView2.Rows
                .Cast<DataGridViewRow>()
                .Count(row => !row.IsNewRow)
                .ToString();

            // Expiring Soon
            int expiringSoon = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                DateTime expiryDate;

                if (DateTime.TryParse(row.Cells["ExpiryDate"].Value?.ToString(), out expiryDate))
                {
                    if (expiryDate.Date >= DateTime.Today &&
                        expiryDate.Date <= DateTime.Today.AddDays(7))
                    {
                        expiringSoon++;
                    }
                }
            }

            lblExpiringSoon.Text = expiringSoon.ToString();

            // Food Categories
            HashSet<string> categories = new HashSet<string>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                string category = row.Cells["Category"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(category))
                {
                    categories.Add(category);
                }
            }

            lblFood.Text = categories.Count.ToString();

            // Low Stock
            int lowStock = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                int quantity;

                if (int.TryParse(row.Cells["Quantity"].Value?.ToString(), out quantity))
                {
                    if (quantity <= 2)
                    {
                        lowStock++;
                    }
                }
            }



            lblStock.Text = lowStock.ToString();
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
        
          
             
         











