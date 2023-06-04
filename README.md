# YetAnotherConsoleTables [![NuGet](https://img.shields.io/nuget/v/YetAnotherConsoleTables.svg)](https://www.nuget.org/packages/YetAnotherConsoleTables/) [![Made in Ukraine](https://img.shields.io/badge/made_in-ukraine-ffd700.svg?labelColor=0057b7)](https://stand-with-ukraine.pp.ua)
Advanced library to output your POCO collections in a table view in a console (supports multi-line data, attributes settings, output customization).

**Contents**
- [Getting Started](#getting-started-basic)
- [Advanced Features](#advanced-features)
  - [Attributes](#attributes)
    - [TableMemberAttribute](#tablememberattribute)
    - [TableIgnoreAttribute](#tableignoreattribute)
    - [TableMemberConverterAttribute](#tablememberconverterattribute)
    - [Combined Example](#combined-example)
  - [Multi-line Data](#multi-line-data)
  - [Output Customization](#output-customization)

# Getting started (basic)
Install the package:
```
PM> Install-Package YetAnotherConsoleTables
```
Write a code:
```
using YetAnotherConsoleTables;

class Something
{
  private static Random rnd = new Random();

  public int Property1 { get; set; } = rnd.Next(99, 10001);
  public string Field1 = "My String";
}

class Program
{
  var data = Enumerable.Range(0, 5).Select(x => new Something()).ToList();
  ConsoleTable.From(data).Write();
}
```
Output:
```
-------------------------
| Property1 | Field1    |
-------------------------
| 4299      | My String |
-------------------------
| 475       | My String |
-------------------------
| 4142      | My String |
-------------------------
| 239       | My String |
-------------------------
| 6547      | My String |
-------------------------
```

# Advanced Features
## Attributes
### TableMemberAttribute
Sets properties of the table member, such as display name, default value, order, and min width.
### TableIgnoreAttribute
Instructs the library to ignore marked public field or property.
### TableMemberConverterAttribute
Instructs the library to use the specified `TableMemberConverter` when converting the member to string. You can create your own TableMemberConverter by inheriting `TableMemberConverter` or `TableMemberConverter<T>`.

For .NET 7 and upper, the generic version of the attribute is available - `TableMemberConverterAttribute<TConverter>`. The non-generic version is deprecated for these platforms and will be removed because the generic attribute provides more compile-time guarantees on the type parameter.

If you need to pass constructor arguments to your `TableMemberConverter`, you can specify them using the `ConstructorArgs` property of the `TableMemberConverterAttribute`. This allows you to create re-usable, configurable, common converters.
### Combined Example
```
using YetAnotherConsoleTables.Attributes;

class MyConverter : TableMemberConverter<int>
{
  public override string Convert(int value) => $"MyConverter:{value}";
}

class MyConverterWithParam : TableMemberConverter<int>
{
  private readonly string _prefix;

  public MyConverterWithParam(string prefix) => _prefix = prefix;

  public override string Convert(int value) => $"{_prefix}{value}";
}

class Something
{
  private static Random rnd = new Random();

  [TableMember(DisplayName = "My Integer Property")]
  [TableMemberConverter(typeof(MyConverter))]
  public int Property1 { get; set; } = rnd.Next(99, 10001);

  [TableIgnore]
  public string Field1 = "My String";

  [TableMember(Order = 1, DefaultValue = "Null Value")]
  public string Property2 { get; set; } = null;

  [TableMember(MinWidth = 20)]
  [TableMemberConverter(typeof(MyConverterWithParam), ConstructorArgs = new[] { "MyConv1:" })]
  public int Property3 { get; set; } = rnd.Next(100, 999);
}
```
Output:
```
-----------------------------------------------------------
| Property2  | My Integer Property | Property3            |
-----------------------------------------------------------
| Null Value | MyConverter:4292    | MyConv1:512          |
-----------------------------------------------------------
| Null Value | MyConverter:4697    | MyConv1:696          |
-----------------------------------------------------------
| Null Value | MyConverter:1672    | MyConv1:234          |
-----------------------------------------------------------
| Null Value | MyConverter:6317    | MyConv1:754          |
-----------------------------------------------------------
| Null Value | MyConverter:4804    | MyConv1:562          |
-----------------------------------------------------------
```

## Multi-line Data
The library supports multi-line strings in data and the `TableDisplayName` attribute.
```
class Something
{
  private static Random rnd = new Random();

  [TableDisplayName("My\r\nInteger\r\nProperty")]
  public int Property1 { get; set; } = rnd.Next(99, 10001);
  public string Field1;

  public Something()
  {
    Field1 = 
      string.Join(Environment.NewLine, Enumerable.Range(1, rnd.Next(0, 4)).Select(x => new string('A', rnd.Next(1, 5))));
  }
}
```
Output:
```
---------------------
| My       | Field1 |
| Integer  |        |
| Property |        |
---------------------
| 8542     | AAAA   |
---------------------
| 7476     | A      |
|          | AA     |
|          | AAAA   |
---------------------
| 5718     |        |
---------------------
| 9074     | A      |
|          | AA     |
|          | AA     |
---------------------
| 4717     | AAA    |
|          | AAAA   |
---------------------
```

## Output Customization
You can customize the table view using the `Write` method overloads which accept the `ConsoleTableFormat` parameter. You can use one of four library-defined styles or create your own.

**Default Style**: `Write()` or `Write(ConsoleTableFormat.Default)`
```
-------------------------
| Property1 | Field1    |
-------------------------
| 2094      | My String |
-------------------------
| 5183      | My String |
-------------------------
| 5589      | My String |
-------------------------
```
**Plus Style**: `Write(ConsoleTableFormat.Plus)`
```
+-----------+-----------+
| Property1 | Field1    |
+-----------+-----------+
| 9748      | My String |
+-----------+-----------+
| 5487      | My String |
+-----------+-----------+
| 7850      | My String |
+-----------+-----------+
```
**Header Style**: `Write(ConsoleTableFormat.Header)`
```
|===========|===========|
| Property1 | Field1    |
|===========|===========|
| 4118      | My String |
|-----------|-----------|
| 5309      | My String |
|-----------|-----------|
| 4051      | My String |
|-----------|-----------|
```
**Github Markdown**: `Write(ConsoleTableFormat.GithubMarkdown)`
```
| Property1 | Property2 |
|-----------|-----------|
| AA        | 30        |
| AB        | 35        |
| BB        | 40        |
```
**Defined Custom Style**
```
class MyFormat : ConsoleTableFormat
{
  public MyFormat() : base(columnDelimiter: ':',
    intersection: '+', borders: ConsoleTableFormat.Borders.HeaderDelimiter | ConsoleTableFormat.Borders.RowDelimiter)
  {

  }
}
```
```
 Property1 : Field1    
-----------+-----------
 461       : My String 
-----------+-----------
 7180      : My String 
-----------+-----------
 6146      : My String 
```
