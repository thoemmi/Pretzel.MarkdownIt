using NUnit.Framework;

namespace Pretzel.MarkdownIt.Tests
{
    [TestFixture]
    public class MarkdownItEngineTests
    {
        [Test]
        public void can_render_markdown() {
            const string source = "**bold** *italic*";
            var sut = new MarkdownItEngine();
            var actual = sut.Convert(source).Trim('\n');
            Assert.AreEqual("<p><strong>bold</strong> <em>italic</em></p>", actual);
        }

        [Test]
        public void can_render_fenced_code() {
            var source = @"
```cs
static void Main()
{
    Console.WriteLine(""Hello World!"");
}
```
";
            var expected = @"<pre><code class=""language-cs"">static void Main()
{
    Console.WriteLine(&quot;Hello World!&quot;);
}
</code></pre>
".Replace("\r\n", "\n");
            var sut = new MarkdownItEngine();
            var actual = sut.Convert(source);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_render_table() {
            const string source = @"
Col 1 | Col 2
------|------
A     |B
";
            var expected = @"<table>
<thead>
<tr>
<th>Col 1</th>
<th>Col 2</th>
</tr>
</thead>
<tbody>
<tr>
<td>A</td>
<td>B</td>
</tr>
</tbody>
</table>
".Replace("\r\n", "\n");
            var sut = new MarkdownItEngine();
            var actual = sut.Convert(source);
            Assert.AreEqual(expected, actual);
        }
    }
}
