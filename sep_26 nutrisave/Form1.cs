namespace sep_26_nutrisave
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            btnAddFoodItems.Click += btnAddFood_Click;
            btnAddMedications.Click += btnAddMedication_Click;
            btnRecordMeals.Click += btnRecordMeal_Click;
            btnFoodInventory.Click += btnFoodInventory_Click;
            btnMeal.Click += btnMeal_Click;
            btnMedication.Click += btnMedication_Click;
            btnFoodSharing.Click += btnFoodSharing_Click;
            btnReport.Click += btnReport_Click;
            btnRecipes.Click += btnRecipes_Click;
            LoadExpiryAlerts();
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }


        private void btnFoodInventory_Click(object sender, EventArgs e)




        {
            Form2 foodInventory = new Form2();
            foodInventory.Show();

        }

        private void btnMeal_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btnMedication_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(); ;
            form4.Show();
        }

        private void btnFoodSharing_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Form1 dashboard = new Form1();
            dashboard.Show();
            this.Hide();
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void btnAddMedication_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void btnRecordMeal_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void LoadExpiryAlerts()
        {
            dataGridView1.Rows.Clear();

            foreach (FoodItem food in Form2.FoodItems)
            {
                DateTime expiryDate;

                if (DateTime.TryParse(food.ExpiryDate, out expiryDate))
                {
                    if (expiryDate.Date <= DateTime.Today.AddDays(7))
                    {
                        string status;

                        if (expiryDate.Date < DateTime.Today)
                            status = "Expired";
                        else
                            status = "Expiring Soon";

                        dataGridView1.Rows.Add(
                            food.FoodName,
                            food.Category,
                            food.ExpiryDate,
                            status
                        );
                    }
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            Form6 reports = new Form6();
            reports.Show();
            this.Hide();
        }
        private void btnRecipes_Click(object sender, EventArgs e)
        {
            Form7 recipe = new Form7();
            recipe.Show();
            this.Hide();
        }


    }
}
    

    

