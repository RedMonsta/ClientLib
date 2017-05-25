using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OlympFoodClient
{
    public static class SpellChecker
    {
        public static bool CheckNickname(string nickname)
        {
            bool result = true;
            if (nickname.Length >= 3)
            {
                foreach (var symbol in nickname)
                {
                    if (!IsEnglishLetter(symbol) && !Char.IsDigit(symbol) && (symbol != '_')) result = false;
                }
            }
            else result = false;
            return result;
        }

        public static bool CheckPassword(string password)
        {
            bool result = true;
            if (password.Length >= 8)
            {
                foreach (var symbol in password)
                {
                    if (!IsEnglishLetter(symbol) && !Char.IsDigit(symbol) && (symbol != '_')) result = false;
                }
            }
            else result = false;
            return result;
        }

        public static bool CheckPhone(string phone)
        {
            string mask = @"[29|44|25|33]\d{7}";
            if (Regex.IsMatch(phone, mask)) return true;
            else return false;
        }

        public static bool CheckName(string name)
        {
            bool result = true;
            if (name.Length >= 2)
            {
                foreach (var symbol in name)
                {
                    if (!IsEnglishLetter(symbol) && !IsRussianLetter(symbol) && (symbol != ' ')) result = false;
                }
            }
            else result = false;
            return result;
        }

        public static bool CheckAddress(string address)
        {
            bool result = true;
            if (address.Length > 0)
            {
                foreach (var symbol in address)
                {
                    if (!IsEnglishLetter(symbol) && !IsRussianLetter(symbol) && !IsSpecialSymbol(symbol) ) result = false;
                }
            }
            else result = false;
            return result;
        }

        private static bool IsSpecialSymbol(char symbol)
        {
            const string EnglishLetters = @"_ \/.,;";
            if (EnglishLetters.IndexOf(symbol) == -1) return false;
            else return true;
        }

        private static bool IsEnglishLetter(char symbol)
        {
            const string EnglishLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            if (EnglishLetters.IndexOf(symbol) == -1) return false;
            else return true;
        }

        private static bool IsRussianLetter(char symbol)
        {
            const string RussianLetters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (RussianLetters.IndexOf(symbol) == -1) return false;
            else return true;
        }
    }
}
