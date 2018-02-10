using System;

namespace cimob.Extensions
{
    /// <summary>
    /// Exceção lançada na validação do tamanho do ficheiro.
    /// Esta é lançada se o tamanho for inferior ou superior ao expectável
    /// </summary>
    [Serializable]
    internal class FileSizeException : Exception
    {
        public FileSizeException() {}

        public FileSizeException(string message) : base(message) {}
    }
}