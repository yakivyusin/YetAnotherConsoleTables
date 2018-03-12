# YetAnotherConsoleTables
Advanced library for output your POCO collections in a table view in a console (supports multi-line data, attributes settings, output customization).

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
  var data = Enumerable.Range(0, 5).Select(x => new Something2()).ToList();
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
Library defines `TableDisplayName` and `TableIgnore` attributes for custom columns name setuping and ignoring a property/field respectively.
```
using YetAnotherConsoleTables.Attributes;

class Something
{
  private static Random rnd = new Random();

  [TableDisplayName("My Integer Property")]
  public int Property1 { get; set; } = rnd.Next(99, 10001);
  [TableIgnore]
  public string Field1 = "My String";
}
```
Output:
```
-----------------------
| My Integer Property |
-----------------------
| 4292                |
-----------------------
| 4697                |
-----------------------
| 1672                |
-----------------------
| 6317                |
-----------------------
| 4804                |
-----------------------
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
    rowDelimiter: '-',
    headerDelimiter: '-',
    intersection: '+')
  {

  }
}
```
```
+-----------+-----------+
: Property1 : Field1    :
+-----------+-----------+
: 461       : My String :
+-----------+-----------+
: 7180      : My String :
+-----------+-----------+
: 6146      : My String :
+-----------+-----------+
```
