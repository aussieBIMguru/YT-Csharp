// Autodesk
using Autodesk.Revit.UI.Selection;
using View = Autodesk.Revit.DB.View;

// Associate to the utility namespace
namespace guRoo.Utilities
{
    public static class Selection_Utils
    {
        public class ISF_ByCategory : ISelectionFilter
        {
            private ElementId _categoryId;
            
            public ISF_ByCategory(BuiltInCategory category)
            {
                _categoryId = new ElementId(category);
            }
            
            public bool AllowElement(Element elem)
            {
                if (elem.Category is Category category)
                {
                    return category.Id == _categoryId;
                }
                else
                {
                    return false;
                }
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }

        public class ISF_HiddenInView : ISelectionFilter
        {
            private View _view;

            public ISF_HiddenInView(View view)
            {
                _view = view;
            }

            public bool AllowElement(Element elem)
            {
                return elem.IsHidden(_view);
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }
    }
}
