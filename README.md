# markdown-maker

[![Nuget](https://img.shields.io/nuget/v/CloudAwesome.MarkdownMaker)](https://www.nuget.org/packages/CloudAwesome.MarkdownMaker/)

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=markdown-maker&metric=coverage)](https://sonarcloud.io/dashboard?id=markdown-maker)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=markdown-maker&metric=alert_status)](https://sonarcloud.io/dashboard?id=markdown-maker)
[![Build Status](https://dev.azure.com/cloud-awesome/CloudAwesome.Xrm/_apis/build/status/CloudAwesome.MarkdownMaker/publish-release-markdown?branchName=main)](https://dev.azure.com/cloud-awesome/CloudAwesome.Xrm/_build/latest?definitionId=9&branchName=main)

Lightweight library to assist creation of .md files (in GitHub and DocFx styles)

## Example usage

```csharp
    
    // Everything starts with an MdDocument
    // Nothing is written to file system until calling the .Save() method
    var outputFilePath = "C:\\output.md";
    var document = new MdDocument(outputFilePath);
    
    var firstHeader = new MdHeader("The header", 1);
    
    var table = new MdTable()
        // Define the table's columns. Others could be added later
        .AddColumn("First Column")
        .AddColumn("Second Column")
        .AddColumn("Third Column");

    table
        .AddRow(new MdTableRow()
            // Use AddCell ...
            .AddCell("1")
            .AddCell("2")
            .AddCell("3"))
        .AddRow(new MdTableRow
        {
            // ... or use a Cell object
            Cells =
            {
                new MdPlainText("4"),
                new MdPlainText("5"),
                new MdPlainText("6")
            }
        });

    var quote = new MdQuote()
        .AddLine("All the world’s a stage, and all the men and women merely players.")
        .AddLine("They have their exits and their entrances;")
        .AddLine("And one man in his time plays many parts.");

    var bulletList = new MdList(MdListType.Unordered)
        .AddItem("First point")
        .AddItem("Second point")
        .AddItem("Third point")
        .AddItem("Fourth point");
    
    var numberedList = new MdList(MdListType.Ordered)
        .AddItem("First point")
        .AddItem("Second point")
        .AddItem("Third point")
        .AddItem("Fourth point");

    var todoList = new MdList(MdListType.Todo)
        .AddItem("Build something")
        .AddItem("Test it")
        .AddItem("Push it");

    // Include DocFX specific metadata. 
    // TODO - First class support is coming in the future!        
    var docfxMetadata = 
        "---" + Environment.NewLine +
        "uid: this_is_a_tester" + Environment.NewLine +
        "---";
    var docFxHeader = new MdPlainText(docfxMetadata);
    
    document
        .Add(docFxHeader)
        .Add(firstHeader)
        // Include markdown inline
        .Add(new MdParagraph("This is a paragraph of interesting text..."))
        .Add(new MdHorizontalLine())
        .Add(table)
        .Add(quote)
        .Add(bulletList)
        .Add(numberedList)
        // Validate and save to file system
        .Save();
        
    // See CloudAwesome.MarkdownMaker.Tests for more examples...
```



Any [bug reports or feature requests](https://github.com/Cloud-Awesome/markdown-maker/issues/new/choose) are greatly appreciated!
