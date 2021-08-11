using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypt_Decrypt
{
    class FileEncryptor
    {
        private string _inputFilePath;
        private string _outputFilePath;
        private List<char> _passwordCharArr;
        private List<int> _progressionNums;
        private readonly int _evenNumber;
        private readonly int _oddNumber;

        public FileEncryptor(string inputFilePath, string outputFilePath)
        {
            CheckPaths(inputFilePath, outputFilePath);
            _passwordCharArr = new();
            _progressionNums = new();
            _oddNumber = 3;
            _evenNumber = 4;
        }

        public void Encrypt(string password)
        {
            FileWorker file = new(_inputFilePath, _outputFilePath);
            _passwordCharArr = StringToCharArr(password);
            List<char> fileChars = file.FileSymbols();
            List<char> decryptedChars = new();
            int counter = 0;

            CalculateProgression(length: fileChars.Count);

            if (password.Length < fileChars.Count)
            {
                int substraction = fileChars.Count - password.Length;
                for (int i = 0; i < substraction; i++) 
                { 
                    password += password[i];
                    _passwordCharArr.Add(password[i]);
                }
            }

            foreach (var fileChar in fileChars)
            {
                char decryptedChar = (char)Calculate(isSum: true, fileChar, counter);
                decryptedChars.Add(decryptedChar);

                counter++;
            }
            int randLength = RandomLength(_progressionNums[0]);
            decryptedChars.AddRange(AddChars(randLength));

            file.WriteFile(decryptedChars);
        }

        public void Decrypt(string password, string inputFilePath, string outputFilePath)
        {
            CheckPaths(inputFilePath, outputFilePath);
            FileWorker file = new(_inputFilePath, _outputFilePath);
            List<char> encryptedChars = new();
            List<char> fileChars = file.FileSymbols();
            int counter = 0;

            int randLength = RandomLength(_progressionNums[0]);

            if (password.Length < _passwordCharArr.Count)
            {
                for (int i = password.Length; i < _passwordCharArr.Count; i++)
                {
                    password += _passwordCharArr[i];
                }
            }

            _passwordCharArr = StringToCharArr(password);
            fileChars.RemoveRange(fileChars.Count - randLength, randLength);

            foreach (var fileChar in fileChars)
            {
                char encryptedChar = (char)Calculate(isSum: false, fileChar, counter);
                encryptedChars.Add(encryptedChar);

                counter++;
            }

            file.WriteFile(encryptedChars);
        }

        private List<char> StringToCharArr(string word)
        {
            List<char> symbols = new();
            for (int i = 0; i < word.Length; i++)
            {
                symbols.Add(word[i]);
            }
            return symbols;
        }

        private int GenerateRandom(int min = 5, int max = 350)
        {
            Random random = new();
            return random.Next(min, max);
        }

        private IEnumerable<char> AddChars(int length)
        {
            for (int i = 0; i < length; i++)
            {
                yield return (char)GenerateRandom();
            }
        }

        private int RandomLength(int number) => (number + 12) % 2 == 0 ? _evenNumber : _oddNumber;

        private void CalculateProgression(int length)
        {
            int randNum = length;
            int difference = RandomLength(randNum);

            for (int i = 1; i <= length; i++)
            {
                randNum += difference;
                _progressionNums.Add(randNum);
            }
        }
        private int Calculate(bool isSum, char fileChar, int counter)
        {
            int sum = _passwordCharArr[counter] + _progressionNums[counter];
            return isSum ? (fileChar + sum) : (fileChar - sum);
        }

        private void CheckPaths(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath ?? throw new ArgumentNullException(nameof(inputFilePath));
            _outputFilePath = outputFilePath ?? throw new ArgumentNullException(nameof(outputFilePath));
        }
    }
}
