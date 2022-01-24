using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MorseCommunicator.Client.Operators
{
    public class MorseCodeOperator
    {
        private readonly Node rootNode;
        private class Node
        {
            public Node DotNode { get; set; }
            public Node DashNode { get; set; }
            public char Character { get; set; }
        }

        public MorseCodeOperator()
        {
            rootNode = new Node();
            for(int level=1;level <7; level ++)
            {
                foreach(char character in MorseCodeSignals.CodedCharacterSet.Keys)
                {
                    string code = MorseCodeSignals.CodedCharacterSet[character];
                    if(code.Length == level)
                    {
                        Node currentNode = rootNode;
                        foreach(char signal in code)
                        {
                            if(signal == MorseCodeSignals.DotSign)
                            {
                                if (currentNode.DotNode == null)
                                {
                                    currentNode.DotNode = new Node { Character = character };
                                }
                                currentNode = currentNode.DotNode;
                            }
                            if (signal == MorseCodeSignals.DashSign)
                            {
                                if (currentNode.DashNode == null)
                                {
                                    currentNode.DashNode = new Node { Character = character };
                                }
                                currentNode = currentNode.DashNode;
                            }
                        }
                    }
                }
                
            }
        }
        public string EncodeCallReply(string callingStationCallSign, string calledStationCallSign)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(FormatSignal(MorseCodeSignals.StartingSignal));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(EncodeWord(calledStationCallSign));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(EncodeWord("DE"));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(EncodeWord(callingStationCallSign));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.Cross));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.InvitationToTransmit));

            string callSignal = stringBuilder.ToString();
            return callSignal;
        }

        public string EncodeWait()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(FormatSignal(MorseCodeSignals.StartingSignal));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.Wait));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.Cross));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.InvitationToTransmit));

            string callSignal = stringBuilder.ToString();
            return callSignal;
        }

        public string EncodeEndOfWork()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(FormatSignal(MorseCodeSignals.StartingSignal));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(MorseCodeSignals.EndOfWork);
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.Cross));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.InvitationToTransmit));

            string callSignal = stringBuilder.ToString();
            return callSignal;
        }

        public string EncodeTelegram(string telegram)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append(FormatSignal(MorseCodeSignals.StartingSignal));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            string encodedText = EncodeText(telegram);
            stringBuilder.Append(encodedText);
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.Cross));
            stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
            stringBuilder.Append(FormatSignal(MorseCodeSignals.InvitationToTransmit));

            string encodedTelegram = stringBuilder.ToString();
            return encodedTelegram;
        }

        public string DecodeTelegram(string encodedTelegram)
        {
            string telegram = DecodeText(encodedTelegram);
            return telegram;
        }

        private string FormatSignal(string signal)
        {
            StringBuilder stringBuilder = new();

            for (int i = 0; i < signal.Length; i++)
            {
                char sign = signal[i];
                stringBuilder.Append(sign);
                if (i < (signal.Length - 1))
                {
                    stringBuilder.Append(MorseCodeSignals.SpaceBetweenSignals);
                }
            }

            string formattedSignal = stringBuilder.ToString();
            return formattedSignal;
        }

        private string UnformatSignal(string formattedSignal)
        {
            StringBuilder stringBuilder = new();

            for (int i = 0; i < formattedSignal.Length; i++)
            {
                char sign = formattedSignal[i];
                if (sign.ToString() != MorseCodeSignals.SpaceBetweenSignals)
                {
                    stringBuilder.Append(sign);
                }
            }

            string unformattedSignal = stringBuilder.ToString();
            return unformattedSignal;
        }

        private string EncodeWord(string word)
        {
            StringBuilder stringBuilder = new();

            string cleanWord = word.ToUpper()
                                   .Replace('*', 'x')
                                   .Replace("%", "0/0")
                                   .Replace("‰", "0/00")
                                   .Replace("′", "'")
                                   .Replace("″", "''");
            for (int i = 0; i < cleanWord.Length; i++)
            {
                char letter = cleanWord[i];
                string signal = MorseCodeSignals.CodedCharacterSet.GetValueOrDefault(letter);
                if (signal != null)
                {
                    string formattedSignal = FormatSignal(signal);
                    stringBuilder.Append(formattedSignal);
                    if (i < (cleanWord.Length - 1))
                    {
                        stringBuilder.Append(MorseCodeSignals.SpaceBetweenLetters);
                    }
                }
            }

            string encodedWord = stringBuilder.ToString();
            return encodedWord;
        }

        private string DecodeWord(string encodedWord)
        {
            StringBuilder stringBuilder = new();

            string[] encodedSignals = encodedWord.Split(MorseCodeSignals.SpaceBetweenLetters);

            for (int i = 0; i < encodedSignals.Length; i++)
            {
                string formattedSignal = encodedSignals[i];
                string unformattedSignal = UnformatSignal(formattedSignal);

                if (unformattedSignal == MorseCodeSignals.Understood)
                {
                    stringBuilder.Append("UNDERSTOOD");
                }
                else if (unformattedSignal == MorseCodeSignals.Error)
                {
                    stringBuilder.Append("ERROR");
                }
                else if (unformattedSignal == MorseCodeSignals.Wait)
                {
                    stringBuilder.Append("WAIT");
                }
                else if (unformattedSignal == MorseCodeSignals.EndOfWork)
                {
                    stringBuilder.Append("END OF WORK");
                }
                else if (unformattedSignal == MorseCodeSignals.StartingSignal)
                {
                    stringBuilder.Append("STARTING SIGNAL");
                }
                else
                {
                    Node currentNode = rootNode;
                    foreach (char sign in unformattedSignal)
                    {
                        if (sign == MorseCodeSignals.DotSign)
                        {
                            currentNode = currentNode.DotNode;
                        }
                        else if (sign == MorseCodeSignals.DashSign)
                        {
                            currentNode = currentNode.DashNode;
                        }
                    }
                    char letter = currentNode.Character;

                    if (letter == '+')
                    {
                        stringBuilder.Append("+(CROSS)");
                    }
                    else if (letter == 'K')
                    {
                        stringBuilder.Append("K(INVITATION TO TRANSMIT)");
                    }
                    else
                    {
                        stringBuilder.Append(letter);
                    }
                }
            }

            string decodedWord = stringBuilder.ToString();
            return decodedWord;
        }

        private string EncodeText(string text)
        {
            StringBuilder stringBuilder = new();

            string[] words = text.Split(' ')
                                 .Where(word => word.Length > 0)
                                 .ToArray();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                string encodedWord = EncodeWord(word);
                stringBuilder.Append(encodedWord);
                if (i < (words.Length - 1))
                {
                    stringBuilder.Append(MorseCodeSignals.SpaceBetweenWords);
                }
            }

            string encodedtext = stringBuilder.ToString();
            return encodedtext;
        }

        private string DecodeText(string encodedText)
        {
            StringBuilder stringBuilder = new();

            string[] encodedWords = encodedText.Split(MorseCodeSignals.SpaceBetweenWords)
                                               .Where(word => word.Length>0)
                                               .ToArray();
            for (int i = 0; i < encodedWords.Length; i++)
            {
                string encodedWord = encodedWords[i];
                string decodedWord = DecodeWord(encodedWord);
                stringBuilder.Append(decodedWord);
                if (i < (encodedWords.Length - 1))
                {
                    stringBuilder.Append(' ');
                }
            }

            string decodedtext = stringBuilder.ToString();
            return decodedtext;
        }
    }
}
