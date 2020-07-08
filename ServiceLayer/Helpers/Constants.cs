
namespace ServiceLayer.Helpers
{
    /// <summary>
    /// Class that contains constants used in ServiceLayer <see cref="ServiceLayer"/>
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Defines password hash size in bytes
        /// </summary>
        public const int PASSWORD_HASH_SIZE = 16;

        /// <summary>
        /// Defines salt size in bytes
        /// </summary>
        public const int SALT_SIZE = 16;

        /// <summary>
        /// Integet value that indicates that error happened and other value couldn't be returned
        /// </summary>
        public const int ERROR_INDICATOR = -1;

        /// <summary>
        /// Maximum number of messages that student can send technical service per day
        /// </summary>
        public const int MAX_NUMBER_OF_MESSAGES_PER_DAY = 10;
    }
}
