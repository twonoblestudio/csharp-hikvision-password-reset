﻿using lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class Utility : Form
    {
        private HikvisionPassword hikvisionPassword = new HikvisionPassword();

        public Utility()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            pwdResetCodeTextBox.Text = string.Empty;

            errorProvider.Clear();

            bool hasError = false;

            string serial_number = serialNumberTextBox.Text;

            if (hikvisionPassword.ValidateSerialNumber(serial_number) == false)
            {
                errorProvider.SetError(serialNumberTextBox, "Invalid serial number");

                hasError = true;
            }

            DateTime dateTime = dateTimePicker.Value;
            string year = Convert.ToString(dateTime.Year);
            string month = Convert.ToString(dateTime.Month);
            string day = Convert.ToString(dateTime.Day);

            if (hikvisionPassword.ValidateYear(year) == false)
            {
                errorProvider.SetError(dateTimePicker, "Invalid year");

                hasError = true;
            }
            else if (hikvisionPassword.ValidateMonth(month) == false)
            {
                errorProvider.SetError(dateTimePicker, "Invalid month");

                hasError = true;
            }
            else if (hikvisionPassword.ValidateDay(year, month, day) == false)
            {
                errorProvider.SetError(dateTimePicker, "Invalid day");

                hasError = true;
            }

            if (hasError == false)
            {
                pwdResetCodeTextBox.Text = hikvisionPassword.GeneratePasswordResetCode(serial_number, year, month, day);
            }
        }

        private void pwdResetCodeTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox input = sender as TextBox;

            input.SelectAll();
        }
    }
}
