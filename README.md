# YetAnotherConsoleTables [![NuGet](https://img.shields.io/nuget/v/YetAnotherConsoleTables.svg)](https://www.nuget.org/packages/YetAnotherConsoleTables/)
Advanced library for output your POCO collections in a table view in a console (supports multi-line data, attributes settings, output customization).

**Contents**
- [Getting Started](#getting-started-basic)
- [Advanced Features](#advanced-features)
  - [Attributes](#attributes)
    - [TableDisplayNameAttribute](#tabledisplaynameattribute)
    - [TableIgnoreAttribute](#tableignoreattribute)
    - [TableMemberConverterAttribute](#tablememberconverterattribute)
    - [TableMemberOrderAttribute](#tablememberorderattribute)
    - [TableDefaultValueAttribute](#tabledefaultvalueattribute)
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
### TableDisplayNameAttribute
Defines custom column name instead of default member name.
### TableIgnoreAttribute
Instructs the library to ignore marked public field or property.
### TableMemberConverterAttribute
Instructs the library to use the specified `TableMemberConverter` when converting the member to string. You can create own TableMemberConverter by inheriting `TableMemberConverter` or `TableMemberConverter<T>`.
### TableMemberOrderAttribute
Sets the order of the member in the table. First, will be output members with this attribute in ascending order, second, other members.
### TableDefaultValueAttribute
Defines default string value that will be used if member value is null or your custom converter return null. 
```
using YetAnotherConsoleTables.Attributes;

class MyConverter : TableMemberConverter<int>
{
  public override string Convert(int value)
  {
    return $"MyConverter:{value}";
  }
}

class Something
{
  private static Random rnd = new Random();

  [TableDisplayName("My Integer Property")]
  [TableMemberConverter(typeof(MyConverter))]
  public int Property1 { get; set; } = rnd.Next(99, 10001);
  [TableIgnore]
  public string Field1 = "My String";
  [TableMemberOrder(1)]
  [TableDefaultValue("Null Value")]
  public string Property2 { get; set; } = null;
}
```
Output:
```
------------------------------------
| Property2  | My Integer Property |
------------------------------------
| Null Value | MyConverter:4292    |
------------------------------------
| Null Value | MyConverter:4697    |
------------------------------------
| Null Value | MyConverter:1672    |
------------------------------------
| Null Value | MyConverter:6317    |
------------------------------------
| Null Value | MyConverter:4804    |
------------------------------------
```

## Multi-line Data
Library supports multi-line strings in data and in `TableDisplayName` attribute.
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
You can customize table view by passing to `Write` method one from three library styles or by passing your own `ConsoleTableFormat` object (or it subclass).

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
**Defined Custom Style**
```
class MyFormat : ConsoleTableFormat
{
  public MyFormat() : base(columnDelimiter: ':',
    intersection: '+', outsideBorders: false)
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
