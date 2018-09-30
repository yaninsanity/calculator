//Zeren Yan CIS345
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A4
{
    public partial class Calculator : Form
    {
        //instantiate
        decimal result = '0';
        decimal tmpResult = '0';
        bool dotClick = false;
        int decimalDigits = 2;
        bool operatorClicked = false;
        
        
       
        string tmpOperatorion = "";
        public Calculator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //assign the font 
            foreach (Control tmpControl in this.Controls)
            {
                if (tmpControl is Button)
                {
                    tmpControl.Font = new Font("Microsoft Sans Serif", 25);
                       
                }

            }
            this.nagativeButton.Font = new Font("Microsoft Sans Serif", 17);
            this.backSpaceButton.Font = new Font("Microsoft Sans Serif", 11);

            //load ComboBox
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            for (int i = 0; i < 9; i++)
            {

                decimalComboBox.Items.Add($"{i.ToString()}");
            }
           
            decimalComboBox.Items.Add("12");
        }
        

       

        private void operator_Click(object sender, EventArgs e)
        {
            dotClick = false;
            operatorClicked = true;
           

            Button tmpOperator=(Button) sender;
            switch (tmpOperator.Name)
            {
                
                //assign the  opreator 
                case "plusButton":
                    {
                        tmpOperatorion = "+";
                        calculateStatusLabel.Text = result.ToString() + "+";
                        break;
                    }
                case "minusButton":
                    {
                        tmpOperatorion = "-";
                        calculateStatusLabel.Text = result.ToString() + "-";
                        break;
                    }
                case "MultiplyButton":
                    {
                        tmpOperatorion = "×";
                        calculateStatusLabel.Text = result.ToString() + "×";
                        break;
                    }
                case "DivideButton":
                    {
                        tmpOperatorion = "÷";
                        calculateStatusLabel.Text = result.ToString() + "÷";
                        break;
                    }
                
                    //calculate by opreator
                case "equalButton":
                    {
                        switch (tmpOperatorion)
                        {
                            case "+": { result += tmpResult; break; }
                            case "-": { result = tmpResult - result ; break; }
                            case "×": { result *= tmpResult; break; }                              
                            case "÷": {
                                    try { result = tmpResult/result; }


                                    //protected program from crash
                                    catch (DivideByZeroException)
                                    {     }

                                    break;
                                }
                            
                        }

                        //NaN if number devide 0
                        if (result == 0 && tmpOperatorion == "÷")
                        {
                            resultTextBox.Text = "NaN/Infinity";
                            calculateStatusLabel.Text = "NaN/Infinity";
                            memLabel.Text = "Mem: NaN/Infinity";
                        }
                        else
                        {
                            if (tmpOperatorion != "")
                            {
                                //round digit
                                result = Math.Round(result, decimalDigits);
                                calculateStatusLabel.Text = calculateStatusLabel.Text += $"={result}";
                                resultTextBox.Text = $"{result}";
                                // assign the meomory 
                                memLabel.Text = $"Mem: {result}";
                            }
                           

                        }

                        tmpResult = 0;
                        tmpOperatorion = "";

                        break;
                    }
            }
            

         
        }

        private void decimalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //pass degit value from combobox to varible
            decimalDigits = Convert.ToInt16(decimalComboBox.SelectedItem.ToString());
        }

        private void calculateStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void numberButton_Click(object sender, EventArgs e)
        {
            //innitialzation.
            if (resultTextBox.Text == "0")
            { resultTextBox.Clear(); }
            //reset label
            if (calculateStatusLabel.Text == "0" || calculateStatusLabel.Text.Contains("=") || calculateStatusLabel.Text.Contains("NaN/Infinity"))
            { calculateStatusLabel.Text = ""; }
            
            Button tmpButton = (Button)sender;
            if (operatorClicked == true) { resultTextBox.Clear(); tmpResult = result; }
            operatorClicked = false;

            if (dotClick == false || tmpButton.Name != "dotButton")
            {
                resultTextBox.Text += tmpButton.Text;
                calculateStatusLabel.Text += tmpButton.Text;
            }

            if (tmpButton.Name == "dotButton")
            {
                dotClick = true;
            }
            try { result = Convert.ToDecimal(resultTextBox.Text); }
            catch (OverflowException)
            { MessageBox.Show("The number is too long!", "Error"); }
            catch (FormatException)
            { MessageBox.Show("The number input's format is wrong!", "Error"); }
        }

        private void format_Click(object sender, EventArgs e)
        {
            Button tmpButton = (Button)sender;
            switch (tmpButton.Name)
            {
                case "nagativeButton":
                    {
                        result = 0 - result;

                        if (result >= 0)
                            resultTextBox.Text = result.ToString();
                        else
                        {
                            resultTextBox.Text = "-" + Math.Abs(result).ToString();
                            calculateStatusLabel.Text = "-" + Math.Abs(result).ToString();
                        }
                        break;
                    }
                case "cleanButton":
                    {
                        resultTextBox.Text = "0";
                        calculateStatusLabel.Text = "0";
                        result = 0;
                        tmpResult = 0;
                        break;
                    }
                case "backSpaceButton":
                    {
                        if (result.ToString().Length > 1)
                        {
                            if (result.ToString().Contains(".") && (result.ToString().Remove(resultTextBox.TextLength - 2).Contains(".")) == false)
                            { resultTextBox.Text = result.ToString().Remove(resultTextBox.TextLength - 2); } 
                            else
                            { resultTextBox.Text = result.ToString().Remove(resultTextBox.TextLength - 1); }
                      
                       

                            result = Convert.ToDecimal(resultTextBox.Text);
                        }
                        else
                        { result = 0;
                            resultTextBox.Text = result.ToString();
                            
                        }
                        calculateStatusLabel.Text = resultTextBox.Text;
                        break;
                    }
            }
        }
    }
}
