using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project01_BatchRename
{
    public class OperationDataTemplates : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            StringOperation _tmp = item as StringOperation;
            FrameworkElement element = container as FrameworkElement;
            if (_tmp != null && element != null)
            {
                if (_tmp.NameOfOperation == "Replace")
                {
                    return element.FindResource("ReplaceTemplate") as DataTemplate;
                }
                else
                     if (_tmp.NameOfOperation == "GUID Generate")
                {
                    return element.FindResource("GUIDTemplate") as DataTemplate;
                }
                else
                        if (_tmp.NameOfOperation == "New Case")
                {
                    return element.FindResource("NewCaseTemplate") as DataTemplate;
                }
                else
                            if (_tmp.NameOfOperation == "Normalize")
                {
                    return element.FindResource("NormalizeTemplate") as DataTemplate;
                }
                else
                {
                    return element.FindResource("MoveTemplate") as DataTemplate;
                }
            }
            return null;
        }
    }
}
