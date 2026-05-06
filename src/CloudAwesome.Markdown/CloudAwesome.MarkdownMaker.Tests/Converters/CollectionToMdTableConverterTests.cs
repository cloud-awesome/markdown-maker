using CloudAwesome.MarkdownMaker.Converters;
using FluentAssertions;

namespace CloudAwesome.MarkdownMaker.Tests.Converters;

[TestFixture]
public class CollectionToMdTableConverterTests
{
	[Test]
	public void CollectionOfObjects_Returns_Valid_Markdown()
	{
		var collection = new[]
		{
			new TestObject { Name = "Test", Year = 2021, Description = "This is a test", Price = 100.00 },
			new TestObject { Name = "Test2", Year = 2022, Description = "This is a test2", Price = 200.00 },
			new TestObject { Name = "Test3", Year = 2023, Description = "This is a test3", Price = 300.00 },
			new TestObject { Name = "Test4", Year = 2024, Description = "This is a test4", Price = 400.00 }
		};
		
		var expectedResult = 
			$"| Name | Year | Description | Price | {Environment.NewLine}" +
			$"|---|---|---|---|{Environment.NewLine}" +
			$"| Test | 2021 | This is a test | 100.00 | {Environment.NewLine}" +
			$"| Test2 | 2022 | This is a test2 | 200.00 | {Environment.NewLine}" +
			$"| Test3 | 2023 | This is a test3 | 300.00 | {Environment.NewLine}" +
			$"| Test4 | 2024 | This is a test4 | 400.00 | {Environment.NewLine}" +
			$"{Environment.NewLine}";
		
		var result = collection.ToMdTable();
		result.Should().NotBeNull();
		
		var markdown = result.Markdown;
		
		markdown.Should().NotBeNullOrEmpty();
		markdown.Should().Be(expectedResult);
	}

	[Test] public void ToMdTable_Only_Includes_Properties_With_Values()
	{
		var collection = new[]
		{
			new TestObject { Name = "Test", Year = 2021, Price = 100.00 },
			new TestObject { Name = "Test2", Year = 2022, Price = 200.00 },
			new TestObject { Name = "Test3", Year = 2023, Price = 300.00 },
			new TestObject { Name = "Test4", Year = 2024, Price = 400.00 }
		};
		
		var expectedResult = 
			$"| Name | Year | Price | {Environment.NewLine}" +
			$"|---|---|---|{Environment.NewLine}" +
			$"| Test | 2021 | 100.00 | {Environment.NewLine}" +
			$"| Test2 | 2022 | 200.00 | {Environment.NewLine}" +
			$"| Test3 | 2023 | 300.00 | {Environment.NewLine}" +
			$"| Test4 | 2024 | 400.00 | {Environment.NewLine}" +
			$"{Environment.NewLine}";
		
		var result = collection.ToMdTable();
		result.Should().NotBeNull();
		
		var markdown = result.Markdown;
		
		markdown.Should().NotBeNullOrEmpty();
		markdown.Should().Be(expectedResult);
	}
	
	[Test]
	public void Object_Property_With_Null_Value_Is_Included_As_Empty_Column_If_IncludeNullProperties_Is_True()
	{
		var collection = new[]
		{
			new TestObject { Name = "Test", Year = 2021, Price = 100.00 },
			new TestObject { Name = "Test2", Year = 2022, Price = 200.00 },
			new TestObject { Name = "Test3", Year = 2023, Price = 300.00 },
			new TestObject { Name = "Test4", Year = 2024, Price = 400.00 }
		};
		
		var expectedResult = 
			$"| Name | Year | Description | Price | {Environment.NewLine}" +
			$"|---|---|---|---|{Environment.NewLine}" +
			$"| Test | 2021 | (empty) | 100.00 | {Environment.NewLine}" +
			$"| Test2 | 2022 | (empty) | 200.00 | {Environment.NewLine}" +
			$"| Test3 | 2023 | (empty) | 300.00 | {Environment.NewLine}" +
			$"| Test4 | 2024 | (empty) | 400.00 | {Environment.NewLine}" +
			$"{Environment.NewLine}";
		
		var result = collection.ToMdTable(ignorePropertiesWithOnlyNullValues: false);
		result.Should().NotBeNull();
		
		var markdown = result.Markdown;
		
		markdown.Should().NotBeNullOrEmpty();
		markdown.Should().Be(expectedResult);
	}

	public class TestObject
	{
		public string Name { get; set; }
		public int Year { get; set; }
		public string? Description { get; set; }
		public double? Price { get; set; }
	}
}