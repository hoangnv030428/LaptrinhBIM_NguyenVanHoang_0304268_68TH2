using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
namespace ClassLibrary1
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit
        .Attributes.TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData
        commandData, ref string message, ElementSet elements)
        {
            MessageBox.Show("chill ");
            for (int i = 0; i < 6; i++)
            {
                MessageBox.Show(i.ToString());
            }
            MessageBox.Show("check");
            return Result.Succeeded;
        }
    }
}