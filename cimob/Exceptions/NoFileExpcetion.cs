using System;

namespace cimob.Extensions
{
    /// <summary>
    /// Exceção lançada na validação do ficheiro.
    /// Esta é lançada se a função não recebe ficheiro nenhum
    /// </summary>
    [Serializable]
    internal class NoFileExpcetion : Exception
    {
        public NoFileExpcetion() {}

        public NoFileExpcetion(string message) : base(message) {}
    }
}