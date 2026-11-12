using sep_26_nutrisave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sep_26_nutrisave
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            LoadReportData();


            pnlFoodWasteChart.Paint += pnlFoodWasteChart_Paint;
            pnlMedicationChart.Paint += pnlMedicationChart_Paint;
            btnGenerateReport.Click += btnGenerateReport_Click;
            dataGridView3.Visible = false;
        }





        private void LoadReportData()
        {
            lblItemsSaved.Text = Form2.FoodItems.Count.ToString();
            lblMealsRecorded.Text = Form3.MealRecords.Count.ToString();
            lblMedicationDoses.Text = Form4.Medications.Count.ToString();
            lblSurplusMeals.Text = Form5.FoodOffers.Count.ToString();
        }

        private void pnlFoodWasteChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int total = Form2.FoodItems.Count;
            int expired = Form2.FoodItems.Count(item => item.Status == "Expired");
            int good = total - expired;

            using (Font numberFont = new Font("Segoe UI", 20, FontStyle.Bold))
            using (Font smallFont = new Font("Segoe UI", 9))
            {
                // If there are no food items
                if (total == 0)
                {
                    g.DrawString(
                        "0",
                        numberFont,
                        Brushes.Black,
                        40,
                        135
                    );

                    g.DrawString(
                        "No food items recorded",
                        smallFont,
                        Brushes.Gray,
                        40,
                        170
                    );

                    return;
                }

                // =========================
                // DONUT CHART
                // =========================

                int centerX = 150;
                int centerY = 190;
                int diameter = 110;

                float goodAngle = (float)good / total * 360f;
                float expiredAngle = (float)expired / total * 360f;

                using (Brush goodBrush =
                    new SolidBrush(Color.FromArgb(150, 190, 120)))
                using (Brush expiredBrush =
                    new SolidBrush(Color.FromArgb(240, 150, 160)))
                {
                    // Good / usable
                    g.FillPie(
                        goodBrush,
                        centerX - diameter / 2,
                        centerY - diameter / 2,
                        diameter,
                        diameter,
                        0,
                        goodAngle
                    );

                    // Expired
                    g.FillPie(
                        expiredBrush,
                        centerX - diameter / 2,
                        centerY - diameter / 2,
                        diameter,
                        diameter,
                        goodAngle,
                        expiredAngle
                    );
                }

                // White centre
                using (Brush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(
                        whiteBrush,
                        centerX - 32,
                        centerY - 32,
                        64,
                        64
                    );
                }

                // Total number in centre
                string totalText = total.ToString();

                SizeF textSize = g.MeasureString(
                    totalText,
                    numberFont
                );

                g.DrawString(
                    totalText,
                    numberFont,
                    Brushes.Black,
                    centerX - textSize.Width / 2,
                    centerY - textSize.Height / 2
                );

                // =========================
                // LEGEND
                // =========================

                // Green square
                using (Brush goodBrush =
                    new SolidBrush(Color.FromArgb(150, 190, 120)))
                {
                    g.FillRectangle(
                        goodBrush,
                        270,
                        155,
                        14,
                        14
                    );
                }

                g.DrawString(
                    "Good / Usable (" + good + ")",
                    smallFont,
                    Brushes.Black,
                    295,
                    152
                );

                // Pink square
                using (Brush expiredBrush =
                    new SolidBrush(Color.FromArgb(240, 150, 160)))
                {
                    g.FillRectangle(
                        expiredBrush,
                        270,
                        190,
                        14,
                        14
                    );
                }

                g.DrawString(
                    "Expired (" + expired + ")",
                    smallFont,
                    Brushes.Black,
                    295,
                    187
                );
            }
        }



        private void pnlMedicationChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int medicationCount = Form4.Medications.Count;

            // Main number
            using (Font numberFont = new Font("Segoe UI", 30, FontStyle.Bold))
            {
                g.DrawString(
                    medicationCount.ToString(),
                    numberFont,
                    Brushes.Black,
                    45,
                    125
                );
            }

            // Description
            using (Font infoFont = new Font("Segoe UI", 12))
            {
                g.DrawString(
                    "doses scheduled",
                    infoFont,
                    Brushes.Gray,
                    45,
                    180
                );
            }

            // Scheduled
            using (Font smallFont = new Font("Segoe UI", 12))
            {
                g.DrawString(
                    "Scheduled: " + medicationCount,
                    smallFont,
                    Brushes.Gray,
                    300,
                    130
                );

                g.DrawString(
                    "Recorded: 0",
                    smallFont,
                    Brushes.Gray,
                    300,
                    165
                );
            }

            // Progress bar background
            using (Brush backgroundBrush = new SolidBrush(Color.LightGray))
            {
                g.FillRectangle(
                    backgroundBrush,
                    300,
                    195,
                    250,
                    18
                );
            }
        }
  



        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            // Update report numbers
            lblItemsSaved.Text = Form2.FoodItems.Count.ToString();
            lblMealsRecorded.Text = Form3.MealRecords.Count.ToString();
            lblMedicationDoses.Text = Form4.Medications.Count.ToString();
            lblSurplusMeals.Text = Form5.FoodOffers.Count.ToString();

            // Refresh the charts
            pnlFoodWasteChart.Invalidate();
            pnlMedicationChart.Invalidate();

            // Confirmation
            MessageBox.Show(
                "Report generated successfully!",
                "NutriSave+",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        
    }
}











