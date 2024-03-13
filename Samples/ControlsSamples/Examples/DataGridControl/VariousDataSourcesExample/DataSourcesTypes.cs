namespace QSF.Examples.DataGridControl.VariousDataSourcesExample;

public enum DataSourcesTypes
{
    DataTable,
    Collection,
#if WINDOWS || ANDROID
    // Due to limitations ExpandoObject and DynamicObject cannot be compiled AOT on iOS and MacCatalyst.
    // For more information see https://learn.microsoft.com/en-us/dotnet/maui/macios/interpreter?view=net-maui-8.0
    ExpandoObjects,
    DynamicObjects
#endif
}
