using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PropertyChanged;

namespace Project01_BatchRename
{
    public class StringArguments : INotifyPropertyChanged
    {
        public string Origin { get; set; } //source of data to operate

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class ReplaceArguments : StringArguments
    {
        public string oldPattern { get; set; } //string to to replaced
        public string newPattern { get; set; } //string to replace oldPattern
    }

    public class NewCaseArguments : StringArguments
    {
        public int isUpper { get; set; } //1 - all, low 2 - all up, 3 - normal
        public int isLower { get; set; } //1 - all, low 2 - all up, 3 - normal
        public int isSentence { get; set; } //1 - all, low 2 - all up, 3 - normal
    }

    public class NormalizeArguments : StringArguments
    {

    }

    public class MoveCharactersArguments : StringArguments
    {
        public int numbersOfChar { get; set; } //numbers of chars to move
        public int isToLast { get; set; } //1 - move to last, 2 - move to first
        public int isToFirst { get; set; } //1 - move to first, 2 - move to last
        public int is_ISBN_Style { get; set; } 
    }

    public class GUIDGenerateArguments : StringArguments
    {

    }

    public abstract class StringOperations
    {
        public StringArguments Arguments { get; set; }
        public abstract string NameOfOperation { get; }
        public abstract string DescriptionOfOperation { get; }

        public abstract string Operate();
        public abstract StringOperations Clone();
        public abstract void refreshChange();
    }

    public class Replace : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "Replace";
        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as ReplaceArguments;
                return $"Replace pattern: {args.oldPattern} with pattern: {args.newPattern}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
        {
            var oldArgs = Arguments as ReplaceArguments;
            return new Replace()
            {
                Arguments = new ReplaceArguments()
                {
                    Origin = oldArgs.Origin,
                    oldPattern = oldArgs.oldPattern,
                    newPattern = oldArgs.newPattern
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as ReplaceArguments;
            string pattern = args.oldPattern;
            string src = args.Origin;
            string newPattern = args.newPattern;
            string tmp = "";
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            var myBuilder = new StringBuilder();
            int startPos = 0;

            foreach (Match m in Regex.Matches(src, pattern, options))
            {
                tmp = src.Substring(startPos, m.Index - startPos);
                myBuilder.Append(tmp + newPattern);
                startPos = m.Index + m.Value.Length;
            }
            tmp = src.Substring(startPos);
            myBuilder.Append(tmp);

            return myBuilder.ToString();
        }

        public override void refreshChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("oldPattern"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("newPattern"));
        }
    }

    public class NewCase : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "NewCase";

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as NewCaseArguments;
                if (args.isUpper == 1)
                    return $"New Case: AllCap";
                else
                    if (args.isLower == 2)
                    return $"New Case: AllLow";
                else
                    return $"New Case: Normal";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
        {
            var oldArgs = Arguments as NewCaseArguments;
            return new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
                    Origin = oldArgs.Origin,
                    isLower = oldArgs.isLower,
                    isSentence = oldArgs.isSentence,
                    isUpper = oldArgs.isUpper
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as NewCaseArguments;
            string _tmp = args.Origin;
            if (args.isUpper == 1)
                _tmp = _tmp.ToUpper();
            else
                if (args.isLower == 1)
                _tmp = _tmp.ToLower();
            else
                    if (args.isSentence == 1)
            {
                _tmp = _tmp.ToLower();
                string firstChar = _tmp.Substring(0, 1).ToUpper();
                _tmp = _tmp.Remove(0, 1).Insert(0, firstChar);
            }

            return _tmp;
        }

        public override void refreshChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isUpper"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isLower"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isSentence"));
        }
    }

    public class Normalize : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "Normalize";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Full String Normalize";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
        {
            var oldArgs = Arguments as NormalizeArguments;
            return new Normalize()
            {
                Arguments = new NormalizeArguments()
                {
                    Origin = oldArgs.Origin,
                }
            };
        }

        public override string Operate()
        {
            var argr = Arguments as NormalizeArguments;
            return Global.NormalizeString(argr.Origin.ToLower());
        }

        public override void refreshChange()
        {
            return;
        }
    }
    public class MoveCharacters : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "Move ISBN";

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as MoveCharactersArguments;
                if (args.isToLast == 1)
                    return $"Move {args.numbersOfChar} chars to end";
                else
                    return $"Move {args.numbersOfChar} chars to begin";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
        {
            var oldArgs = Arguments as MoveCharactersArguments;
            return new MoveCharacters()
            {
                Arguments = new MoveCharactersArguments()
                {
                    Origin = oldArgs.Origin,
                    isToFirst = oldArgs.isToFirst,
                    isToLast = oldArgs.isToLast,
                    numbersOfChar = oldArgs.numbersOfChar
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as MoveCharactersArguments;
            string result = "";

            string src = args.Origin;
            string extension = "";
            int dotPos = src.LastIndexOf('.');
            if (dotPos >= 0)
            {
                extension = src.Substring(dotPos);
                src = src.Remove(dotPos);
            }

            if (args.numbersOfChar < 0 || args.numbersOfChar > src.Length)
            {
                // báo lỗi MessageBox.Show("Number of characters to move is outside valid range!", "Error");
                return null;
            }

            if (args.isToFirst == 1)
            {
                string tmp = src.Substring(src.Length - args.numbersOfChar);
                src = src.Remove(src.Length - args.numbersOfChar);
                result = tmp + src + extension;
            }
            if (args.isToLast == 1)
            {
                string tmp = src.Substring(0, args.numbersOfChar);
                //src = src.Substring(args.numbersOfChar);
                src = src.Remove(0, args.numbersOfChar);
                result = src + tmp + extension;
            }

            return result;
        }

        public override void refreshChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("numbersOfChar"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isToLast"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isToFirst"));
        }
    }

    public class GUIDGenerate : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "GUID Generate";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Generate new GUID Name";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
        {
            var oldArgs = Arguments as GUIDGenerateArguments;
            return new GUIDGenerate()
            {
                Arguments = new GUIDGenerateArguments()
                {
                    Origin = oldArgs.Origin
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as GUIDGenerateArguments;
            return Guid.NewGuid().ToString();
        }

        public override void refreshChange()
        {
            return;
        }
    }
}
