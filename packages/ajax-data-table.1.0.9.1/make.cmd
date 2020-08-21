copy ..\ajax-data-table\bin\Debug\ajax-data-table.dll lib\net
copy ..\ajax-data-table-test\Scripts\ajax-data-table.js content\Scripts
copy ..\ajax-data-table-test\Content\ajax-data-table.css content\Content
copy ..\ajax-data-table-test\Content\ajax-data-table-icons.png content\Content
copy ..\ajax-data-table-test\Content\ajax-data-table-loader.gif content\Content

nuget pack Package.nuspec