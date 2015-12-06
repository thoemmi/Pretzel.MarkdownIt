# Pretzel.MarkdownIt

This is a plugin for [Pretzel](https://github.com/Code52/pretzel), a static site generation tool following (more or less) the same conventions as [Jekyll](https://github.com/mojombo/jekyll). It replaces the default markdown engine of Pretzel with an engine supporting not only CommonMark but extras such as tables, footnotes, etc.

[![Build status](https://ci.appveyor.com/api/projects/status/al781gft07q9gsdp?svg=true)](https://ci.appveyor.com/project/thoemmi/pretzel-markdownit)

## Motivation

Pretzel uses [CommonMark.NET](https://github.com/Knagis/CommonMark.NET/) for rendering markdown files. This library follows the CommonMark specification, but does not support extensions such as tables.

Therefore [Jérémie Bertrand](https://github.com/laedit) added an extensibility point for Pretzel to support other engines for rendering, and [created a plugin](https://github.com/laedit/Pretzel.MarkdownDeep) using [MarkdownDeep](http://www.toptensoftware.com/markdowndeep/). This library does support tables, however it has an error regarding [empty lines in code blocks](https://github.com/toptensoftware/markdowndeep/issues/62), and seems somehow abandoned.

Then I remembered that [Harry Pierson posted](http://devhawk.net/blog/2015/9/2) how to use the Javascript markdown parser [markdown-it](https://github.com/markdown-it/markdown-it) with the help of [edge.js](http://tjanczuk.github.io/edge/) in .NET.

So I took Jérémie's plugin as a template and threw in Harry's code. Now I have a Pretzel plugin creating nicely rendered markdown.

### Installation

Download the [latest release](https://github.com/thoemmi/Pretzel.MarkdownIt/releases) and copy its content to the `_plugins` folder of your Pretzel site.
