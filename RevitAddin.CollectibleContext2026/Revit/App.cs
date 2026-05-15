using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.CollectibleContext2026.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("RevitAddin.CollectibleContext2026");

            ribbonPanel.RowStackedItems(
                ribbonPanel.CreatePushButton<Commands.Command>()
                    .SetLargeImage("Resources/Revit.ico"),
                ribbonPanel.CreatePushButton<Commands.CommandLibraryReference>()
                    .SetLargeImage("Resources/Revit.ico")
                );

            LoadContext.Load();
            LibraryReference.Show();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}