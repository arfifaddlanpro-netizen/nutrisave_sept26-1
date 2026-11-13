using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace sep_26_nutrisave
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            AdminNavigation.Attach(this, panel6);

            ShowMatchingRecipes();

            btnViewRecipe1.Click += btnViewRecipe1_Click;
            btnViewRecipe2.Click += btnViewRecipe2_Click;
            btnViewRecipe3.Click += btnViewRecipe3_Click;
            txtSearchRecipe.TextChanged += (s, e) => ShowMatchingRecipes();
            cmbCategory.SelectedIndexChanged += (s, e) => ShowMatchingRecipes();
            cmbCookingTime.SelectedIndexChanged += (s, e) => ShowMatchingRecipes();
            chkUsesExpiringFood.CheckedChanged += (s, e) => ShowMatchingRecipes();

        }
        public class Recipe
        {
            public string Name { get; set; }
            public string Ingredients { get; set; }
            public string Category { get; set; }
            public int CookingTime { get; set; }
            public Image Image { get; set; }
        }
        private List<Recipe> recipes = new List<Recipe>
{
    new Recipe
    {
        Name = "Egg Fried Rice",
        Ingredients = "Rice, Egg",
        Category = "Lunch",
        CookingTime = 15,
        Image = Image.FromFile("Images/eggfriedrice.jpg")
    },

    new Recipe
    {
        Name = "Chicken Rice",
        Ingredients = "Rice, Chicken",
        Category = "Lunch",
        CookingTime = 30,
        Image = Image.FromFile("Images/chickenrice.jpg")
    },

    new Recipe
    {
        Name = "Egg Toast",
        Ingredients = "Bread, Egg",
        Category = "Breakfast",
        CookingTime = 10,
        Image = Image.FromFile("Images/eggtoast.jpg")
    },

    new Recipe
    {
        Name = "Chicken Sandwich",
        Ingredients = "Bread, Chicken",
        Category = "Lunch",
        CookingTime = 15
    },

    new Recipe
    {
        Name = "Banana Toast",
        Ingredients = "Bread, Banana",
        Category = "Breakfast",
        CookingTime = 10
    }
};

        private void ShowMatchingRecipes()
        {
            List<Recipe> matchingRecipes = new List<Recipe>();

            foreach (Recipe recipe in recipes)
            {
                string[] ingredients = recipe.Ingredients.Split(',');
                bool hasAllIngredients = true;

                foreach (string ingredient in ingredients)
                {
                    bool found = Form2.FoodItems.Any(food =>
                        food.FoodName.Trim().Equals(ingredient.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (!found)
                    {
                        hasAllIngredients = false;
                        break;
                    }
                }

                if (hasAllIngredients)
                {
                    matchingRecipes.Add(recipe);
                }
            }

            DisplayRecipe(0, matchingRecipes);
            DisplayRecipe(1, matchingRecipes);
            DisplayRecipe(2, matchingRecipes);
        }

        private void DisplayRecipe(int index, List<Recipe> matchingRecipes)
        {
            Panel[] panels = { pnlRecipe1, pnlRecipe2, pnlRecipe3 };
            PictureBox[] pictures = { picRecipe1, picRecipe2, picRecipe3 };
            Label[] names = { lblRecipeName1, lblRecipeName2, lblRecipeName3 };
            Label[] ingredients = { lblIngredients, lblIngredients2, lblIngredients3 };

            if (index < matchingRecipes.Count)
            {
                Recipe recipe = matchingRecipes[index];

                panels[index].Visible = true;
                names[index].Text = recipe.Name;
                ingredients[index].Text = "Ingredients: " + recipe.Ingredients;
                pictures[index].Image = recipe.Image;
            }
            else
            {
                panels[index].Visible = false;
            }
        }
        private void btnViewRecipe1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                lblRecipeName1.Text + "\n\n" + lblIngredients.Text,
                "Recipe Details"
            );
        }

        private void btnViewRecipe2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                lblRecipeName2.Text + "\n\n" + lblIngredients2.Text,
                "Recipe Details"
            );
        }

        private void btnViewRecipe3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                lblRecipeName3.Text + "\n\n" + lblIngredients3.Text,
                "Recipe Details"
            );
        }

        private void picRecipe1_Click(object sender, EventArgs e)
        {

        }

        private void picRecipe3_Click(object sender, EventArgs e)
        {

        }
    }
}
