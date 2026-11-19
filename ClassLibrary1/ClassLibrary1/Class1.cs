using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ClassLibrary1
{
    [Transaction(TransactionMode.Manual)]
    public class SelectColumnCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            Autodesk.Revit.UI.TaskDialog.Show("Revit", "Số lượng đối tượng được chọn: " + selectedIds.Count.ToString());

            ICollection<ElementId> selectedColumnIds = new List<ElementId>();
            foreach (ElementId id in selectedIds)
            {
                Element elem = doc.GetElement(id);
                if (elem is FamilyInstance && elem.Category != null)
                {
                    int catId = elem.Category.Id.IntegerValue;
                    if (catId == (int)BuiltInCategory.OST_StructuralColumns ||
                        catId == (int)BuiltInCategory.OST_Columns)
                    {
                        selectedColumnIds.Add(id);
                    }
                }
            }

            uidoc.Selection.SetElementIds(selectedColumnIds);

            if (0 != selectedColumnIds.Count)
            {
                Autodesk.Revit.UI.TaskDialog.Show("Revit", selectedColumnIds.Count.ToString() + " Đối tượng Cột đã được chọn!");
            }
            else
            {
                Autodesk.Revit.UI.TaskDialog.Show("Revit", "Không có đối tượng cột nào được chọn!");
            }

            return Result.Succeeded;
        }
    }
}