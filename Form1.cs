using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment1
{
    public partial class Calculator : Form
    {
        private double assignInput = 0; // Toán tử phía trước của phép tính
        private string operation; // Phép tính
        private Boolean isMathFunction = false;
        private Boolean foundExpression = false; // Biểu thức đã có phép tính
        private double tmpNum = 0;
        List<History> list = new List<History>();

        private void Calculator_Load(object sender, EventArgs e)
        {
            btnClearHistory.Visible = false;
            rtDisplayHistory.SelectionAlignment = HorizontalAlignment.Right;
        }

        public Calculator()
        {
            InitializeComponent();
        }

        // CE: Clear Last Entry (Xóa mục cuối)
        private void btnCE_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            if (lblEquation.Text.Contains("="))
            {
                lblEquation.Text = "";
            }
        }

        // C: Clear All (Xóa tất cả)
        private void btnC_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            lblEquation.Text = "";

            assignInput = 0;
            operation = "";
            foundExpression = false;
            tmpNum = 0;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string text = txtDisplay.Text;
            int len = text.Length;

            if(txtDisplay.Text != "0")
            {
                txtDisplay.Text = text.Remove(len - 1, 1);
            }

            if (txtDisplay.Text == "")
            {
                txtDisplay.Text = "0";
            }              
        }

        // Các button số
        private void Numbers_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if((txtDisplay.Text == "0") || (foundExpression))
            {
                txtDisplay.Clear();
                txtDisplay.Text = b.Text;
                foundExpression = false;
            } 
            else if (b.Text == ".") 
            {
                if (!txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text += b.Text;
                }
            }
            else
            {
                txtDisplay.Text += b.Text;
            }
        }

        // Các button phép tính
        private void operations_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            foundExpression = true;
            operation = b.Text;

            // Nếu assignInput bằng 0 thì gán số vừa nhập cho assignInput
            if (assignInput == 0) 
            {
                assignInput = Double.Parse(txtDisplay.Text);
            }

            lblEquation.Text = assignInput.ToString() + " " + operation + " ";
        }
        private void btnEquals_Click(object sender, EventArgs e)
        {
            string equation = lblEquation.Text, result = txtDisplay.Text;
            if (String.IsNullOrEmpty(operation))
            {
                if (!lblEquation.Text.Contains("=") && !isMathFunction)
                {
                    lblEquation.Text = Double.Parse(txtDisplay.Text) + " = ";
                    equation = lblEquation.Text;
                } 
                if (isMathFunction)
                {
                    equation += " = ";
                }
            } 
            else
            {           
                double firstNumber = 0, secondNumber = 0;

                if (!lblEquation.Text.Contains("="))
                {
                    firstNumber = assignInput;
                    secondNumber = Double.Parse(txtDisplay.Text);
                    tmpNum = secondNumber;
                }
                else
                {
                    firstNumber = Double.Parse(txtDisplay.Text);
                    secondNumber = tmpNum;
                }

                // Hiển thị biểu thức
                lblEquation.Text = firstNumber.ToString() + " " + operation + " " + secondNumber.ToString() + " = ";
                equation = firstNumber.ToString() + "    " + operation + "    " + secondNumber.ToString() + " = ";

                // Tính toán và hiển thị kết quả
                switch (operation)
                {
                    case "+":
                        txtDisplay.Text = (firstNumber + secondNumber).ToString();
                        break;
                    case "-":
                        txtDisplay.Text = (firstNumber - secondNumber).ToString();
                        break;
                    case "x":
                        txtDisplay.Text = (firstNumber * secondNumber).ToString();
                        break;
                    case "÷":
                        txtDisplay.Text = (firstNumber / secondNumber).ToString();
                        break;
                }
                result = txtDisplay.Text;
            }

            // ========== Histoty ==========
            btnClearHistory.Visible = true;

            list.Insert(0, new History(equation, result));

            rtDisplayHistory.Clear();
            foreach (History history in list)
            {
                rtDisplayHistory.SelectionFont = new Font(rtDisplayHistory.SelectionFont.FontFamily, 12, FontStyle.Regular);
                rtDisplayHistory.AppendText(history.Equation + "\n");
                rtDisplayHistory.SelectionFont = new Font(rtDisplayHistory.SelectionFont.FontFamily, 16, FontStyle.Bold);
                rtDisplayHistory.AppendText(history.Result + "\n\n");
            }

            assignInput = Double.Parse(txtDisplay.Text);
            isMathFunction = false;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            rtDisplayHistory.Clear();
            ActiveControl = null;
            btnClearHistory.Visible = false;
            list.Clear();
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            double n = Double.Parse(txtDisplay.Text) ;
            lblEquation.Text = n + "%";
            txtDisplay.Text = (n / 100).ToString();
            operation = "";
            isMathFunction = true;
        }

        private void btnFrac_Click(object sender, EventArgs e)
        {
            double n = Double.Parse(txtDisplay.Text);
            lblEquation.Text = "1/(" + n + ")";
            txtDisplay.Text = (1 / n).ToString();
            operation = "";
            isMathFunction = true;
        }

        private void btnSqr_Click(object sender, EventArgs e)
        {
            double n = Double.Parse(txtDisplay.Text);
            lblEquation.Text = "sqr(" + n + ")";
            txtDisplay.Text = (n * n).ToString();
            operation = "";
            isMathFunction = true;
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            double n = Double.Parse(txtDisplay.Text);
            lblEquation.Text = "√(" + n + ")";
            txtDisplay.Text = (Math.Sqrt(n)).ToString();
            operation = "";
            isMathFunction = true;
        }

        private void btnChangeSign_Click(object sender, EventArgs e)
        {
            double n = Double.Parse(txtDisplay.Text);
            lblEquation.Text = "negate(" + n + ")";
            txtDisplay.Text = (0-n).ToString();
            operation = "";
            isMathFunction = true;
        }
    }
}
