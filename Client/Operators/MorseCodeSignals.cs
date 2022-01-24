using System.Collections.Generic;


namespace MorseCommunicator.Client.Operators
{
    public class MorseCodeSignals
    {
        public static readonly char DotSign = '.';
        public static readonly char DashSign = '-';
        public static readonly char SpaceSign = ' ';

        // Spacing and length of the signals
        public static readonly int DotIntervalLength = 1;
        public static readonly int DashIntervalLength = 3 * DotIntervalLength;
        public static readonly int SpaceBetweenSignalsLength = 1 * DotIntervalLength;
        public static readonly int SpaceBetweenLettersLength = 3 * DotIntervalLength;
        public static readonly int SpaceBetweenWordsLength = 7 * DotIntervalLength;

        public static readonly string SpaceBetweenSignals = new(SpaceSign, SpaceBetweenSignalsLength);
        public static readonly string SpaceBetweenLetters = new(SpaceSign, SpaceBetweenLettersLength);
        public static readonly string SpaceBetweenWords = new(SpaceSign, SpaceBetweenWordsLength);

        public static readonly Dictionary<char, string> CodedCharacterSet = new()
        {
            // Letters
            {'A', ".-"},       {'I', ".."},      {'R', ".-."},
            {'B', "-..."},     {'J', ".---"},    {'S', "..."},
            {'C', "-.-."},     {'K', "-.-"},     {'T', "-"},
            {'D', "-.."},      {'L', ".-.."},    {'U', "..-"},
            {'E', "."},        {'M', "--"},      {'V', "...-"},
            {'É', "..-.."},    {'N', "-."},      {'W', ".--"},
            {'F', "..-."},     {'O', "---"},     {'X', "-..-"},
            {'G', "--."},      {'P', ".--."},    {'Y', "-.--"},
            {'H', "...."},     {'Q', "--.-"},    {'Z', "--.."},

            // Figures
            {'1', ".----"},    {'6', "-...."},
            {'2', "..---"},    {'7', "--..."},
            {'3', "...--"},    {'8', "---.."},
            {'4', "....-"},    {'9', "----."},
            {'5', "....."},    {'0', "-----"},

            // Punctuation marks and miscellaneous signs
            {'.', ".-.-.-"},
            {',', "--..--"},
            {':', "---..."},
            {'?', "..--.."},
            {'\'', ".----."},
            {'-', "-....-"},
            {'/', "-..-."},
            {'(', "-.--."},
            {')', "-.--.-"},
            {'"', ".-..-."},
            {'=', "-...-"},
            {'+', ".-.-."},
            {'@', ".--.-." }
        };

        public static readonly string Understood = "...-.";
        public static readonly string Error = "........";
        public static readonly string Cross = ".-.-.";
        public static readonly string InvitationToTransmit = "-.-";
        public static readonly string Wait = ".-...";
        public static readonly string EndOfWork = "...-.-";
        public static readonly string StartingSignal = "-.-.-";
    }
}
