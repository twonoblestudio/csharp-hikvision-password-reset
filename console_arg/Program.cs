using System;
using System.IO;
using System.Reflection;
using System.Linq;
using lib;

namespace console_arg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("This tool will generate a password reset code which you may use to reset a forgotten admin password for a Hikvision camera");

            Console.WriteLine();

            HikvisionPassword hikvisionPassword = new HikvisionPassword();

            string exe_filename = Path.GetFileName(Assembly.GetEntryAssembly().Location);

            if (args.Length == 0)
            {
                Console.WriteLine(string.Format("usage: {0} [-h] [--serial SERIAL] [--year YEAR] [--month MONTH] [--day DAY]", exe_filename));
            }
            else
            {
                string serial_number = string.Empty;
                string year = string.Empty;
                string month = string.Empty;
                string day = string.Empty;

                try
                {
                    string[] arg_names = {
                        "serial", "s",
                        "year", "y",
                        "month", "m",
                        "day", "d"
                    };

                    for (int i = 0; i < args.Length; i++)
                    {
                        string arg_name = args[i];

                        if (arg_name.StartsWith("--") == true || arg_name.StartsWith("-") == true)
                        {
                            arg_name = arg_name.ToLower().TrimStart('-');
                            string arg_value = args[i + 1];

                            if (arg_names.Contains(arg_name) == false)
                            {
                                continue;
                            }

                            if (arg_name == "serial" || arg_name == "s")
                            {
                                if (hikvisionPassword.ValidateSerialNumber(arg_value) == false)
                                {
                                    Console.WriteLine("Invalid serial number.");
                                }
                                else
                                {
                                    serial_number = arg_value;
                                }
                            }

                            if (arg_name == "year" || arg_name == "y")
                            {
                                if (hikvisionPassword.ValidateYear(arg_value) == false)
                                {
                                    Console.WriteLine("Invalid year.");
                                } else
                                {
                                    year = arg_value;
                                }
                            }

                            if (arg_name == "month" || arg_name == "m")
                            {
                                if (hikvisionPassword.ValidateMonth(arg_value) == false)
                                {
                                    Console.WriteLine("Invalid month.");
                                }
                                else
                                {
                                    month = arg_value;
                                }
                            }

                            if (arg_name == "day" || arg_name == "d")
                            {
                                if (hikvisionPassword.ValidateDay(arg_value) == false)
                                {
                                    Console.WriteLine("Invalid day.");
                                }
                                else
                                {
                                    day = arg_value;
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }

                string reset_code = hikvisionPassword.GeneratePasswordResetCode(serial_number, year, month, day);

                Console.WriteLine(string.Format("Your password reset code is '{0}'", reset_code));
            }

            Console.ReadKey();
        }
    }
}
