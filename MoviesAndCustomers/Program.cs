using MoviesAndCustomers.src.FileReader;
using MoviesAndCustomers.src.NameBreakdown;


Console.WriteLine("Hey everyone\n");
Console.WriteLine("If you would like to convert .tab to XML, type 1\nTo use the function NameBreakDown, type 2");

string? console = Console.ReadLine();

if(console == "1")
{
    FileConverter.TabReader();
}
if(console == "2")
{
    NameReader.NameBreakdown();
}

