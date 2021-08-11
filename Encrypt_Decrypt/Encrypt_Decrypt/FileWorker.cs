using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Encrypt_Decrypt
{
    class FileWorker
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        public int ReaderStreamLength { get; }

        public FileWorker(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath ?? throw new ArgumentNullException(nameof(inputFilePath));
            _outputFilePath = outputFilePath ?? throw new ArgumentNullException(nameof(outputFilePath));

            StreamReader reader = new(_inputFilePath, Encoding.Default);
            ReaderStreamLength = (int)reader.BaseStream.Length;
        }

        public List<char> FileSymbols()
        {
            using StreamReader reader = new(_inputFilePath, Encoding.Default);
            List<char> symbols = new();
            while (!reader.EndOfStream)
            {
                symbols.Add((char)reader.Read());
            }
            return symbols;
        }

        public void WriteFile(List<char> characters)
        {
            using StreamWriter writer = new(_outputFilePath);
            foreach (var character in characters)
            {
                writer.Write(character);
            }
        }
    }
}
