using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project01_BatchRename
{
    public class StringArguments
    {
        public string Origin { get; set; } //source of data to operate
    }


    public class ReplaceArguments : StringArguments
    {
        public string oldPattern { get; set; } //string to to replaced
        public string newPattern { get; set; } //string to replace oldPattern
    }

    public class NewCaseArguments : StringArguments
    {
        public int typeOfNewCase { get; set; } //1 - all, low 2 - all up, 3 - normal
    }

    public class NormalizeArguments : StringArguments
    {

    }

    public class MoveCharactersArguments : StringArguments
    {
        public int numbersOfChar { get; set; } //numbers of chars to move
        public int typeOfMove { get; set; } //1 - move to last, 2 - move to first
    }

    public class GUIDGenerateArguments : StringArguments
    {

    }

    public abstract class StringOperation
    {
        public StringArguments Arguments { get; set; }
        public abstract string NameOfOperation { get; }
        public abstract string DescriptionOfOperation { get; }

        public abstract string Operate();
        public abstract StringOperation Clone();
    }

    public class Replace : StringOperation
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

        public override StringOperation Clone()
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
    }

    public class NewCase : StringOperation
    {
        public override string NameOfOperation => "New Case";

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as NewCaseArguments;
                if (args.typeOfNewCase == 1)
                    return $"New Case: AllCap";
                else
                    if (args.typeOfNewCase == 2)
                    return $"New Case: AllLow";
                else
                    return $"New Case: Normal";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Arguments as NewCaseArguments;
            return new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
                    Origin = oldArgs.Origin,
                    typeOfNewCase = oldArgs.typeOfNewCase
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as NewCaseArguments;
            string _tmp = args.Origin;
            switch (args.typeOfNewCase)
            {
                case 1:
                    _tmp = _tmp.ToUpper();
                    break;
                case 2:
                    _tmp = _tmp.ToLower();
                    break;
                case 3:
                    _tmp = _tmp.ToLower();
                    string firstChar = _tmp.Substring(0, 1).ToUpper();
                    _tmp = _tmp.Remove(0, 1).Insert(0, firstChar);
                    break;
            }
            return _tmp;
        }
    }

    public class Normalize : StringOperation
    {
        public override string NameOfOperation => "Normalize";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Full String Normalize";
            }
        }

        public override StringOperation Clone()
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
            return Global.NormalizeString(argr.Origin);
        }
    }
    public class MoveCharacters : StringOperation
    {
        public override string NameOfOperation => "Move";

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as MoveCharactersArguments;
                if (args.typeOfMove == 1)
                    return $"Move {args.numbersOfChar} chars to end";
                else
                    return $"Move {args.numbersOfChar} chars to begin";
            }
        }

        public override StringOperation Clone()
        {
            var oldArgs = Arguments as MoveCharactersArguments;
            return new MoveCharacters()
            {
                Arguments = new MoveCharactersArguments()
                {
                    Origin = oldArgs.Origin,
                    typeOfMove = oldArgs.typeOfMove,
                    numbersOfChar = oldArgs.numbersOfChar
                }
            };
        }

        public override string Operate()
        {
            throw new NotImplementedException();
        }
    }

    public class GUIDGenerate : StringOperation
    {
        public override string NameOfOperation => "GUID Generate";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Generate new GUID Name";
            }
        }

        public override StringOperation Clone()
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
    }
}
