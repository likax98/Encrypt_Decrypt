using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt_Decrypt
{
    static class ProcessExpection
    {
        private static string _password;
        private static Exception _ex;

        public static string ThrowMessage(string password)
        {
            try
            {
                _password = password;
            }
            catch (FormatException ex)
            {
                _ex = ex;
            }
            catch (OverflowException ex)
            {
                _ex = ex;
            }
            catch (Exception ex)
            {
                _ex = ex;
            }

            return (_ex != default) ? GenerateMessage() : default;
        }

        public static bool HasError()
        {
            return (_ex != default);
        }

        private static string GenerateMessage()
        {
            return $"Error Type: {_ex.GetType().Name}\nMessage: {_ex.Message}";
        }
    }
}
