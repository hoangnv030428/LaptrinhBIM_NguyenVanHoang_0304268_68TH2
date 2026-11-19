using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
namespace ClassLibrary1
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit
        .Attributes.TransactionMode.Manual)]
    public class FloorSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            // Chỉ cho phép chọn nếu đối tượng thuộc class Floor
            if (elem is Floor)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false; // Không dùng đến khi chọn Element
        }
    }

    // 2. Lệnh thực thi Pick Object
    [Transaction(TransactionMode.Manual)]
    public class SelectFloorCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                // Gọi lệnh PickObject, truyền bộ lọc FloorSelectionFilter vào
                Reference pickedRef = uidoc.Selection.PickObject(ObjectType.Element, new FloorSelectionFilter(), "Vui lòng chọn một đối tượng Sàn (Floor)");

                if (pickedRef != null)
                {
                    // Lấy đối tượng Floor từ Reference vừa chọn
                    Element selectedFloor = doc.GetElement(pickedRef);

                    Autodesk.Revit.UI.TaskDialog.Show("Thành công", $"Bạn đã chọn sàn: {selectedFloor.Name}\nID: {selectedFloor.Id}");
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                // Bắt lỗi khi người dùng nhấn ESC để thoát lệnh chọn
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                Autodesk.Revit.UI.TaskDialog.Show("Lỗi", ex.Message);
                return Result.Failed;
            }
        }
    }
}