namespace CloudAwesome.MarkdownMaker.Converters;

public static class ListToMdListConverter 
{
    public static MdList ToMdList(this List<string> list, MdListType listType)
    {
        var result = new MdList(listType);

        foreach (var listItem in list)
        {
            result.AddItem(listItem);
        }
        
        return result;
    }
}