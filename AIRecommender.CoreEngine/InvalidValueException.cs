using System;
using System.Runtime.Serialization;

namespace AIRecommender.CoreEngine
{
   // [Serializable]
    public class InvalidValueException : ApplicationException
    {
        public InvalidValueException(string message) : base(message)
        {
        }
    }
}