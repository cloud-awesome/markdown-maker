# markdown-maker

[![Nuget](https://img.shields.io/nuget/v/CloudAwesome.MarkdownMaker)](https://www.nuget.org/packages/CloudAwesome.MarkdownMaker/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=markdown-maker&metric=coverage)](https://sonarcloud.io/dashboard?id=markdown-maker)
[![Build Status](https://dev.azure.com/cloud-awesome/CloudAwesome.Xrm/_apis/build/status/CloudAwesome.MarkdownMaker/publish-release-markdown?branchName=main)](https://dev.azure.com/cloud-awesome/CloudAwesome.Xrm/_build/latest?definitionId=9&branchName=main)

Lightweight library to assist creation of .md files (in GitHub and DocFx styles)

```csharp
    var outputFilePath = "C:\\output.md";
    var document = new MdDocument(outputFilePath);
    
    var docfxMetadata = 
        "---" + Environment.NewLine +
        "uid: this_is_a_tester" + Environment.NewLine +
        "---";
    var docFxHeader = new MdPlainText(docfxMetadata);
    
    var firstHeader = new MdHeader("The header", 1);
    
    var table = 
        new MdTable()
            .AddColumn(new MdPlainText("First Column"))
            .AddColumn(new MdPlainText("Second Column"))
            .AddColumn(new MdPlainText("Third Column"));

    table
        .AddRow(new MdTableRow()
            .AddCell(new MdPlainText("1"))
            .AddCell(new MdPlainText("2"))
            .AddCell(new MdPlainText("3")))
        .AddRow(new MdTableRow()
            .AddCell(new MdPlainText("4"))
            .AddCell(new MdPlainText("5"))
            .AddCell(new MdPlainText("6")));

    var quote = new MdQuote()
        .AddLine(new MdPlainText("All the world’s a stage, and all the men and women merely players."))
        .AddLine(new MdPlainText("They have their exits and their entrances;"))
        .AddLine(new MdPlainText("And one man in his time plays many parts."));

    var bulletList = new MdList(MdListType.Unordered)
        .AddItem(new MdPlainText("First point"))
        .AddItem(new MdPlainText("Second point"))
        .AddItem(new MdPlainText("Third point"))
        .AddItem(new MdPlainText("Fourth point"));
    
    var numberedList = new MdList(MdListType.Ordered)
        .AddItem(new MdPlainText("First point"))
        .AddItem(new MdPlainText("Second point"))
        .AddItem(new MdPlainText("Third point"))
        .AddItem(new MdPlainText("Fourth point"));

    document
        .Add(docFxHeader)
        .Add(firstHeader)
        .Add(new MdParagraph("This is a paragraph of interesting text..."))
        .Add(new MdHorizontalLine())
        .Add(table)
        .Add(quote)
        .Add(bulletList)
        .Add(numberedList)
        .Save();
```