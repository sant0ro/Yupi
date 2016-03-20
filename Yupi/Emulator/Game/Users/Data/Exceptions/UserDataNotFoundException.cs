using System;

namespace Yupi.Emulator.Game.Users.Data.Exceptions
{
    /// <summary>
    ///     Class UserDataNotFoundException.
    /// </summary>
	[Serializable]
    internal class UserDataNotFoundException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserDataNotFoundException" /> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public UserDataNotFoundException(string reason) : base(reason)
        {
        }
    }
}