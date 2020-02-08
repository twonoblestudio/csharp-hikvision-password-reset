using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using lib;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            HikvisionPassword hikvisionPassword = new HikvisionPassword();

            string serial_number = String.Empty;
            bool serial_valid = false;

            while (serial_valid == false)
            {
                Console.Write("Serial number: ");

                serial_number = Console.ReadLine();
                serial_valid = hikvisionPassword.ValidateSerialNumber(serial_number);

                if (serial_valid == false)
                {
                    Console.WriteLine("Invalid serial number");
                }
            }

            string year = String.Empty;
            bool year_valid = false;

            while (year_valid == false)
            {
                Console.Write("Year: ");

                year = Console.ReadLine();
                year_valid = hikvisionPassword.ValidateYear(year);

                if (year_valid == false)
                {
                    Console.WriteLine("Invalid year");
                }
            }

            string month = String.Empty;
            bool mont_valid = false;

            while (mont_valid == false)
            {
                Console.Write("Month: ");

                month = Console.ReadLine();
                mont_valid = hikvisionPassword.ValidateMonth(month);

                if (mont_valid == false)
                {
                    Console.WriteLine("Invalid month");
                }
            }

            string day = String.Empty;
            bool day_valid = false;

            while (day_valid == false)
            {
                Console.Write("Day: ");

                day = Console.ReadLine();
                day_valid = hikvisionPassword.ValidateDay(year, month, day);

                if (day_valid == false)
                {
                    Console.WriteLine("Invalid day");
                }
            }

            Console.WriteLine(hikvisionPassword.GeneratePasswordResetCode(serial_number, year, month, day));

            Console.ReadKey();
        }
    }
}
