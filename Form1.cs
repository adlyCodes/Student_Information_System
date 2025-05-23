using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace Student_Information_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Read grades and credits from TextBoxes
                int[] grades = new int[]
                {
            int.Parse(txtGrade1.Text),
            int.Parse(txtGrade2.Text),
            int.Parse(txtGrade3.Text),
            int.Parse(txtGrade4.Text),
            int.Parse(txtGrade5.Text),
            int.Parse(txtGrade6.Text)
                };

                int[] credits = new int[]
                {
            int.Parse(txtCredit1.Text),
            int.Parse(txtCredit2.Text),
            int.Parse(txtCredit3.Text),
            int.Parse(txtCredit4.Text),
            int.Parse(txtCredit5.Text),
            int.Parse(txtCredit6.Text)
                };

                float[] points = new float[grades.Length];

                float totalPoints = 0, totalCredits = 0;

                // Convert grades to points and calculate GPA
                for (int i = 0; i < grades.Length; i++)
                {
                    if (grades[i] <= 49)
                        points[i] = 0.0f;
                    else if (grades[i] <= 52)
                        points[i] = 3.0f;
                    else if (grades[i] <= 55)
                        points[i] = 4.0f;
                    else if (grades[i] <= 59)
                        points[i] = 5.0f;
                    else if (grades[i] <= 64)
                        points[i] = 6.0f;
                    else if (grades[i] <= 69)
                        points[i] = 7.0f;
                    else if (grades[i] <= 74)
                        points[i] = 8.0f;
                    else if (grades[i] <= 79)
                        points[i] = 9.0f;
                    else if (grades[i] <= 84)
                        points[i] = 10.0f;
                    else if (grades[i] <= 89)
                        points[i] = 11.0f;
                    else if (grades[i] <= 94)
                        points[i] = 11.5f;
                    else
                        points[i] = 12.0f;

                    totalPoints += points[i] * credits[i];
                    totalCredits += credits[i];
                }

                // Compute GPA
                float gpa = totalPoints / totalCredits / 3;
                string letter = GetLetterGrade(gpa);

                // Show GPA and letter grade in labels
                lblGPA.Text = $"GPA: {gpa:F2}";
                lblLetterGrade.Text = $"Grade: {letter}";
                lblCreditHours.Text = $"Credit Hours: {totalCredits}";
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid numbers in all fields.");
            }
        }
        private string GetLetterGrade(float gpa)
        {
            if (gpa >= 3.6f) return "A (Excellent)";
            else if (gpa >= 3.0f) return "B (Very Good)";
            else if (gpa >= 2.6f) return "C (Good)";
            else if (gpa >= 2.0f) return "D (Pass)";
            else return "F (Fail)";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            // Collect courses from TextBoxes
            List<string> courses = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtCourse1.Text)) courses.Add(txtCourse1.Text);
            if (!string.IsNullOrWhiteSpace(txtCourse2.Text)) courses.Add(txtCourse2.Text);
            if (!string.IsNullOrWhiteSpace(txtCourse3.Text)) courses.Add(txtCourse3.Text);
            if (!string.IsNullOrWhiteSpace(txtCourse4.Text)) courses.Add(txtCourse4.Text);
            if (!string.IsNullOrWhiteSpace(txtCourse5.Text)) courses.Add(txtCourse5.Text);
            if (!string.IsNullOrWhiteSpace(txtCourse6.Text)) courses.Add(txtCourse6.Text);

            if (courses.Count == 0)
            {
                MessageBox.Show("Please enter at least one course name.");
                return;
            }

            // Days (excluding Friday)
            List<string> days = new List<string> { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday" };

            // Time slots
            List<string> timeSlots = new List<string>
    {
        "08:00 AM - 09:30 AM",
        "10:00 AM - 11:30 AM",
        "12:00 PM - 01:30 PM",
        "02:00 PM - 03:30 PM"
    };

            // Shuffle helper
            Random rand = new Random();
            string scheduleOutput = "Weekly Schedule:\n\n";

            List<string> usedSlots = new List<string>(); // Track used day+slot combinations

            foreach (string course in courses)
            {
                string day, slot, combined;

                // Ensure unique day+slot for each course
                do
                {
                    day = days[rand.Next(days.Count)];
                    slot = timeSlots[rand.Next(timeSlots.Count)];
                    combined = $"{day} - {slot}";
                } while (usedSlots.Contains(combined));

                usedSlots.Add(combined);
                scheduleOutput += $"{course}: {combined}\n";
            }

            // Display in Label
            lblScheduleResult.Text = scheduleOutput;
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            // Combine GPA and Schedule results from labels
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Generated on: " + DateTime.Now.ToString());

            reportBuilder.AppendLine("----- GPA Report -----");
            reportBuilder.AppendLine(lblGPA.Text);
            reportBuilder.AppendLine(lblLetterGrade.Text);
            reportBuilder.AppendLine(lblCreditHours.Text);
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("----- Weekly Schedule -----");
            reportBuilder.AppendLine(lblScheduleResult.Text);

            // File save dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save Report";
            saveDialog.Filter = "Text Files (*.txt)|*.txt";
            saveDialog.FileName = "StudentReport.txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveDialog.FileName, reportBuilder.ToString());
                    MessageBox.Show("Report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save file: " + ex.Message);
                }
            }
        }
    }
}
