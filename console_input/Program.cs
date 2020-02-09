using lib;
using System;

namespace console_input
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            Console.WriteLine("This tool will generate a password reset code which you may use to reset a forgotten admin password for a Hikvision camera");

            HikvisionPassword hikvisionPassword = new HikvisionPassword();

            string serial_number = String.Empty;
            bool serial_valid = false;

            while (serial_valid == false)
            {
                Console.Write("Enter camera serial number: ");

                serial_number = Console.ReadLine();
                serial_valid = hikvisionPassword.ValidateSerialNumber(serial_number);

                if (serial_valid == false)
                {
                    Console.WriteLine("Invalid serial number. Press Ctrl+C to abort.");
                }
            }

            string year = String.Empty;
            bool year_valid = false;

            while (year_valid == false)
            {
                Console.Write("Enter year displayed on screen: ");

                year = Console.ReadLine();
                year_valid = hikvisionPassword.ValidateYear(year);

                if (year_valid == false)
                {
                    Console.WriteLine("Invalid year. Press Ctrl+C to abort.");
                }
            }

            string month = String.Empty;
            bool mont_valid = false;

            while (mont_valid == false)
            {
                Console.Write("Enter month displayed on screen: ");

                month = Console.ReadLine();
                mont_valid = hikvisionPassword.ValidateMonth(month);

                if (mont_valid == false)
                {
                    Console.WriteLine("Invalid month. Press Ctrl+C to abort.");
                }
            }

            string day = String.Empty;
            bool day_valid = false;

            while (day_valid == false)
            {
                Console.Write("Enter day displayed on screen: ");

                day = Console.ReadLine();
                day_valid = hikvisionPassword.ValidateDay(day);

                if (day_valid == false)
                {
                    Console.WriteLine("Invalid day. Press Ctrl+C to abort.");
                }
            }

            string reset_code = hikvisionPassword.GeneratePasswordResetCode(serial_number, year, month, day);

            Console.WriteLine(string.Format("Your password reset code is '{0}'", reset_code));

            Console.ReadKey();
        }
    }
}
