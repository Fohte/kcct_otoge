using System;

namespace Otoge
{
  public class InvalidMapException : Exception
  {
    public InvalidMapException() : base() { }
    public InvalidMapException(string message) : base(message) { }
    public InvalidMapException(string message, Exception inner) : base(message, inner) { }
  }
}