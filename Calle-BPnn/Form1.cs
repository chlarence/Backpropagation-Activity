using Backprop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calle_BPnn
{
    public partial class Form1 : Form
    {
        NeuralNet nn;
        public Form1()
        {
            InitializeComponent();
            this.Width = 400;
            this.Height = 280;  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nn = new NeuralNet(4, 125, 1);
        
            if (nn != null)
            {
                button1.Enabled = false;

                button2.Enabled = true;
                button3.Enabled = true;

                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
              
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            double[,] weights = new double[4, 1];
            double learningRate = 0.1;

            for (int epoch = 0; epoch < 100; epoch++)
            {
                for (int i = 0; i < 16; i++)
                {
                    double[] inputs = new double[4];
                    for (int j = 0; j < 4; j++)
                    {
                        inputs[j] = (i & (1 << j)) > 0 ? 1.0 : 0.0;
                    }

                    double desiredOutput = i == 15 ? 1.0 : 0.0;

                    double output = CalculateOutput(inputs, weights);

                    // Update weights using the perceptron learning rule
                    for (int j = 0; j < 4; j++)
                    {
                        weights[j, 0] += learningRate * (desiredOutput - output) * inputs[j];
                    }
                }
            }

            label3.Text = "Successfully trained using perceptron learning!";
        }

        // Assume you have a method like this for calculating the output
        private double CalculateOutput(double[] inputs, double[,] weights)
        {
            double sum = 0.0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i, 0];
            }
            return sum > 0.5 ? 1.0 : 0.0; // Threshold for binary output
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double input1 = Convert.ToDouble(textBox1.Text);
                double input2 = Convert.ToDouble(textBox2.Text);
                double input3 = Convert.ToDouble(textBox3.Text);
                double input4 = Convert.ToDouble(textBox4.Text);

                Validate(input1);
                Validate(input2);
                Validate(input3);
                Validate(input4);

                // Simple AND logic with sigmoid activation
                double result = Sigmoid(input1 + input2 + input3 + input4);

                textBox5.Text = result.ToString("0.000000000000");

                label3.Text = "Successfully ran logic AND with sigmoid activation!";
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter valid numbers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        private void Validate(double input)
        {
            if (input != 0 && input != 1)
            {
                throw new ArgumentException("Invalid input. Please enter either 0 or 1.");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = " ";
            timer1.Stop();
        }
    }
}
