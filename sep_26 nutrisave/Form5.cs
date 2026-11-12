using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sep_26_nutrisave
{
    public partial class Form5 : Form
    {
        public static List<string> FoodOffers = new List<string>();
        public Form5()
        {
            InitializeComponent();
            dataGridView2.CellContentClick += dataGridView2_EditClick;
            btnAvailableFood.Click += btnAvailableFood_Click;
            btnClaimRequests.Click += btnClaimRequests_Click;
            dataGridViewClaimRequests.CellContentClick += dataGridViewClaimRequests_Click;
            btnDashboard.Click += btnDashboard_Click;
            btnFoodInventory.Click += btnFoodInventory_Click;
            btnMealNutrition.Click += btnMealNutrition_Click;
            btnMedication.Click += btnMedication_Click;
            btnFoodSharing.Click += btnFoodSharing_Click;
            btnReport.Click += btnReport_Click;
            UpdateSummary();
        }


        private void dataGridView2_EditClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            // DELETE
            if (e.ColumnIndex == dataGridView2.Columns["Delete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this food offer?",
                    "Delete Food Offer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
                UpdateSummary();
            }

            // ACTIONS
            if (e.ColumnIndex == dataGridView2.Columns["Actions"].Index)
            {
                string status = dataGridView2.Rows[e.RowIndex]
              .Cells["Status"].Value?.ToString();

                if (status == "Available")
                {
                    DialogResult claimResult = MessageBox.Show(
                        "Do you want to claim this food?",
                        "Claim Food",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );



                    {
                        if (status == "Available")
                        {
                            DialogResult claimDialogResult = MessageBox.Show(
                                "Do you want to claim this food?",
                                "Claim Food",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question
                            );

                            if (claimDialogResult == DialogResult.Yes)
                            {
                                string foodName = dataGridView2.Rows[e.RowIndex]
                                    .Cells["FoodName"].Value?.ToString();

                                string quantity = dataGridView2.Rows[e.RowIndex]
                                    .Cells["Quantity"].Value?.ToString();

                                string expiryDate = dataGridView2.Rows[e.RowIndex]
                                    .Cells["ExpiryDate"].Value?.ToString();

                                // Change original food status
                                dataGridView2.Rows[e.RowIndex]
                                    .Cells["Status"].Value = "Claimed";

                                // Add claim request to top table
                                dataGridViewClaimRequests.Rows.Add(
                                    foodName,
                                    quantity,
                                    expiryDate,
                                    "Pending"
                                );

                                MessageBox.Show("Claim request submitted!");

                                UpdateSummary();
                            }

                            return;
                        }
                    }

                    return;
                }
                DialogResult result = MessageBox.Show(
                    "YES = Approve\nNO = Reject\nCANCEL = Complete",
                    "Food Offer Status",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows[e.RowIndex].Cells["Status"].Value = "Available";
                    UpdateSummary();
                }
                else if (result == DialogResult.No)
                {
                    dataGridView2.Rows[e.RowIndex].Cells["Status"].Value = "Rejected";
                }
                else if (result == DialogResult.Cancel)
                {
                    dataGridView2.Rows[e.RowIndex].Cells["Status"].Value = "Completed";
                }
            }


            // CLAIM
            if (e.ColumnIndex == dataGridView2.Columns["Claim"].Index)
            {
                string status = dataGridView2.Rows[e.RowIndex]
                    .Cells["Status"].Value?.ToString();

                if (status != "Available") ;

            }

        } // ← THIS stays as your existing method-closing bracket

        private void btnSubmitFood_Click(object sender, EventArgs e)
        {
            string foodName = txtFoodName.Text;
            string category = cmbCategory.Text;
            int quantity = (int)numQuantity.Value;
            string expiryDate = dtpExpiryDate.Value.ToShortDateString();

            if (foodName == "" || category == "")
            {
                MessageBox.Show("Please enter the food name and category.");
                return;
            }

            string status = "Pending";

            dataGridView2.Rows.Add(
                foodName,
                quantity,
                expiryDate,
                status
            );
            FoodOffers.Add(foodName);
            UpdateSummary();

            MessageBox.Show("Food offer submitted successfully!");

            txtFoodName.Clear();
            cmbCategory.SelectedIndex = -1;
            numQuantity.Value = 0;
            dtpExpiryDate.Value = DateTime.Today;
        }
        private void UpdateSummary()
        {
            int foodOffers = 0;
            int pending = 0;
            int claims = 0;
            int mealsShared = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow)
                    continue;

                foodOffers++;

                string status = row.Cells["Status"].Value?.ToString();

                if (status == "Pending")
                    pending++;

                if (status == "Completed")
                    claims++;

                if (status == "Approved" || status == "Completed")
                    mealsShared++;
            }

            lblFoodOffers.Text = foodOffers.ToString();
            lblPending.Text = pending.ToString();
            lblClaims.Text = claims.ToString();
            lblMealsShared.Text = mealsShared.ToString();
        }

        private void btnMyOffers_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow)
                    continue;

                row.Visible = true;
            }
        }

        private void btnMyClaims_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string status = row.Cells["Status"].Value?.ToString();

                row.Visible = status == "Completed";
            }
        }

        private void btnAvailableFood_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string status = row.Cells["Status"].Value?.ToString();

                row.Visible = status == "Available";
            }
        }
        private void btnClaimRequests_Click(object sender, EventArgs e)
        {
            bool foundRequest = false;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string status = row.Cells["Status"].Value?.ToString();

                if (status == "Claimed")
                {
                    foundRequest = true;

                    DialogResult result = MessageBox.Show(
                        "Someone has claimed: " +
                        row.Cells["FoodName"].Value?.ToString() +
                        "\n\nDo you want to approve this claim?",
                        "Claim Request",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        row.Cells["Status"].Value = "Completed";

                        MessageBox.Show(
                            "Claim approved! Food sharing completed."
                        );

                        UpdateSummary();
                    }
                }
            }

            if (!foundRequest)
            {
                MessageBox.Show(
                    "There are no claim requests at the moment.",
                    "Claim Requests",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        private void dataGridViewClaimRequests_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridViewClaimRequests.Columns["RequestActions"].Index)
            {
                DataGridViewRow row = dataGridViewClaimRequests.Rows[e.RowIndex];

                string status = row.Cells["RequestStatus"].Value?.ToString();

                if (status == "Pending")
                {
                    DialogResult result = MessageBox.Show(
                        "Do you want to approve this claim?",
                        "Claim Request",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    string foodName = row.Cells["RequestFood"].Value?.ToString();

                    if (result == DialogResult.Yes)
                    {
                        row.Cells["RequestStatus"].Value = "Approved";

                        foreach (DataGridViewRow foodRow in dataGridView2.Rows)
                        {
                            if (foodRow.IsNewRow)
                                continue;

                            if (foodRow.Cells["FoodName"].Value?.ToString() == foodName)
                            {
                                foodRow.Cells["Status"].Value = "Completed";
                                break;
                            }
                        }

                        MessageBox.Show("Claim approved! Food sharing completed.");
                    }
                    else
                    {
                        row.Cells["RequestStatus"].Value = "Rejected";

                        foreach (DataGridViewRow foodRow in dataGridView2.Rows)
                        {
                            if (foodRow.IsNewRow)
                                continue;

                            if (foodRow.Cells["FoodName"].Value?.ToString() == foodName)
                            {
                                foodRow.Cells["Status"].Value = "Available";
                                break;
                            }
                        }

                        MessageBox.Show("Claim request rejected.");
                    }

                    UpdateSummary();
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
    

    
    
