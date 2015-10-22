using System;

namespace DataAccess
{
    public class DataAccessException : ApplicationException
    {
        public DataAccessException()
        {

        }

        public DataAccessException(string message)
        : base(message)
        {
        }

        public DataAccessException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
