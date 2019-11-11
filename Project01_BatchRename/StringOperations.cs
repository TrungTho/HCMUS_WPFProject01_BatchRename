using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
=======
>>>>>>> almost-finish
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
<<<<<<< HEAD
using PropertyChanged;

namespace Project01_BatchRename
{
    public class StringArguments : INotifyPropertyChanged
    {
        public string Origin { get; set; } //source of data to operate

        public event PropertyChangedEventHandler PropertyChanged;
    }

   
=======

namespace Project01_BatchRename
{
    public class StringArguments
    {
        public string Origin { get; set; } //source of data to operate
    }


>>>>>>> almost-finish
    public class ReplaceArguments : StringArguments
    {
        public string oldPattern { get; set; } //string to to replaced
        public string newPattern { get; set; } //string to replace oldPattern
    }

    public class NewCaseArguments : StringArguments
    {
<<<<<<< HEAD
        public int isUpper { get; set; } //1 - all, low 2 - all up, 3 - normal
        public int isLower { get; set; } //1 - all, low 2 - all up, 3 - normal
        public int isSentence { get; set; } //1 - all, low 2 - all up, 3 - normal
=======
        public int typeOfNewCase { get; set; } //1 - all, low 2 - all up, 3 - normal
>>>>>>> almost-finish
    }

    public class NormalizeArguments : StringArguments
    {

    }
<<<<<<< HEAD
       
    public class MoveCharactersArguments : StringArguments
    {
        public int numbersOfChar { get; set; } //numbers of chars to move
        public int isToLast { get; set; } //1 - move to last, 2 - move to first
        public int isToFirst { get; set; } //1 - move to last, 2 - move to first
=======

    public class MoveCharactersArguments : StringArguments
    {
        public int numbersOfChar { get; set; } //numbers of chars to move
        public int typeOfMove { get; set; } //1 - move to last, 2 - move to first
>>>>>>> almost-finish
    }

    public class GUIDGenerateArguments : StringArguments
    {

    }

<<<<<<< HEAD
    public abstract class StringOperations
=======
    public abstract class StringOperation
>>>>>>> almost-finish
    {
        public StringArguments Arguments { get; set; }
        public abstract string NameOfOperation { get; }
        public abstract string DescriptionOfOperation { get; }

        public abstract string Operate();
<<<<<<< HEAD
        public abstract StringOperations Clone();
        public abstract void refreshChange();
    }

    public class Replace : StringOperations, INotifyPropertyChanged
=======
        public abstract StringOperation Clone();
    }

    public class Replace : StringOperation
>>>>>>> almost-finish
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

<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
=======
        public override StringOperation Clone()
>>>>>>> almost-finish
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
<<<<<<< HEAD

        public override void refreshChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("oldPattern"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("newPattern"));
        }
    }

    public class NewCase : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "NewCase";
=======
    }

    public class NewCase : StringOperation
    {
        public override string NameOfOperation => "New Case";
>>>>>>> almost-finish

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as NewCaseArguments;
<<<<<<< HEAD
                if (args.isUpper == 1)
                    return $"New Case: AllCap";
                else
                    if (args.isLower == 2)
=======
                if (args.typeOfNewCase == 1)
                    return $"New Case: AllCap";
                else
                    if (args.typeOfNewCase == 2)
>>>>>>> almost-finish
                    return $"New Case: AllLow";
                else
                    return $"New Case: Normal";
            }
        }

<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
=======
        public override StringOperation Clone()
>>>>>>> almost-finish
        {
            var oldArgs = Arguments as NewCaseArguments;
            return new NewCase()
            {
                Arguments = new NewCaseArguments()
                {
                    Origin = oldArgs.Origin,
<<<<<<< HEAD
                    isLower = oldArgs.isLower,
                    isSentence = oldArgs.isSentence,
                    isUpper = oldArgs.isUpper
=======
                    typeOfNewCase = oldArgs.typeOfNewCase
>>>>>>> almost-finish
                }
            };
        }

        public override string Operate()
        {
            var args = Arguments as NewCaseArguments;
            string _tmp = args.Origin;
<<<<<<< HEAD
            //switch (args.typeOfNewCase)
            //{
            //    case 1:
            //        _tmp = _tmp.ToUpper();
            //        break;
            //    case 2:
            //        _tmp = _tmp.ToLower();
            //        break;
            //    case 3:
            //        _tmp = _tmp.ToLower();
            //        string firstChar = _tmp.Substring(0, 1).ToUpper();
            //        _tmp = _tmp.Remove(0, 1).Insert(0, firstChar); 
            //        break;
            //}
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
=======
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
>>>>>>> almost-finish
    {
        public override string NameOfOperation => "Normalize";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Full String Normalize";
            }
        }

<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
=======
        public override StringOperation Clone()
>>>>>>> almost-finish
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
<<<<<<< HEAD

        public override void refreshChange()
        {
            return;
        }
    }
    public class MoveCharacters : StringOperations, INotifyPropertyChanged
    {
        public override string NameOfOperation => "Move ISBN";
=======
    }
    public class MoveCharacters : StringOperation
    {
        public override string NameOfOperation => "Move";
>>>>>>> almost-finish

        public override string DescriptionOfOperation
        {
            get
            {
                var args = Arguments as MoveCharactersArguments;
<<<<<<< HEAD
                if (args.isToLast == 1)
=======
                if (args.typeOfMove == 1)
>>>>>>> almost-finish
                    return $"Move {args.numbersOfChar} chars to end";
                else
                    return $"Move {args.numbersOfChar} chars to begin";
            }
        }

<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
=======
        public override StringOperation Clone()
>>>>>>> almost-finish
        {
            var oldArgs = Arguments as MoveCharactersArguments;
            return new MoveCharacters()
            {
                Arguments = new MoveCharactersArguments()
                {
                    Origin = oldArgs.Origin,
<<<<<<< HEAD
                    isToFirst=oldArgs.isToFirst,
                    isToLast=oldArgs.isToLast,
                    numbersOfChar=oldArgs.numbersOfChar
=======
                    typeOfMove = oldArgs.typeOfMove,
                    numbersOfChar = oldArgs.numbersOfChar
>>>>>>> almost-finish
                }
            };
        }

        public override string Operate()
        {
            throw new NotImplementedException();
        }
<<<<<<< HEAD

        public override void refreshChange()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("numbersOfChar"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isToLast"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isToFirst"));
        }
    }

    public class GUIDGenerate : StringOperations, INotifyPropertyChanged
=======
    }

    public class GUIDGenerate : StringOperation
>>>>>>> almost-finish
    {
        public override string NameOfOperation => "GUID Generate";

        public override string DescriptionOfOperation
        {
            get
            {
                return "Generate new GUID Name";
            }
        }

<<<<<<< HEAD
        public event PropertyChangedEventHandler PropertyChanged;

        public override StringOperations Clone()
=======
        public override StringOperation Clone()
>>>>>>> almost-finish
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
<<<<<<< HEAD

        public override void refreshChange()
        {
            return;
        }
=======
>>>>>>> almost-finish
    }
}
