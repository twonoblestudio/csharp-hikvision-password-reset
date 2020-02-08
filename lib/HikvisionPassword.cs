using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace lib
{
    public class HikvisionPassword
    {
        private Regex serial_regex = new Regex(@"^[0-9A-Z]+$", RegexOptions.Compiled);

        public bool ValidateSerialNumber(string serial_number)
        {
            Match serial_match = this.serial_regex.Match(serial_number);

            return serial_match.Success && serial_number.Length >= 27;
        }

        public bool ValidateYear(string year)
        {
            if (year.All(char.IsDigit) == true)
            {
                return int.Parse(year) > 1900 && int.Parse(year) <= DateTime.Now.Year;
            }

            return false;
        }

        public bool ValidateMonth(string month)
        {
            if (month.All(char.IsDigit) == true)
            {
                return int.Parse(month) >= 1 && int.Parse(month) <= 12;
            }

            return false;
        }

        public bool ValidateDay(string year, string month, string day)
        {
            try
            {
                new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GeneratePasswordResetCode(string serial_number, string year, string month, string day)
        {
            string combined = serial_number + year + month.PadLeft(2, '0') + day.PadLeft(2, '0');
            uint magic_number = 0;

            for (int idx = 0; idx < combined.Length; idx++)
            {
                magic_number += ((uint)combined[idx] * (uint)(idx + 1)) ^ (uint)(idx + 1);
            }

            magic_number *= 1751873395;

            UInt32 magic_number_uint32 = Convert.ToUInt32(magic_number); // convert to 32 bit unsigned integer
            string magic_number_str = Convert.ToString(magic_number_uint32);
            
            string serial_code = string.Empty;

            for (int idx = 0; idx < magic_number_str.Length; idx++)
            {
                int char_code = (int)magic_number_str[idx];

                if (char_code < 51)
                {
                    serial_code += Convert.ToChar(char_code + 33);
                }
                else if (char_code < 53)
                {
                    serial_code += Convert.ToChar(char_code + 62);
                }
                else if (char_code < 55)
                {
                    serial_code += Convert.ToChar(char_code + 47);
                }
                else if (char_code < 57)
                {
                    serial_code += Convert.ToChar(char_code + 66);
                }
                else
                {
                    serial_code += Convert.ToChar(char_code);
                }
            }

            return serial_code;
        }
    }
}
