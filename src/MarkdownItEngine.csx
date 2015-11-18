#r EdgeJs.dll

using EdgeJs;

[Export(typeof(ILightweightMarkupEngine))]
public class MarkdownItEngine : ILightweightMarkupEngine {
    static readonly Func<object, Task<object>> _markdownItFunc = Edge.Func(@"
var hljs  = require('highlight.js');
var md = require('markdown-it')({
  html: true,
  highlight: function (str, lang) {
    if (lang && hljs.getLanguage(lang)) {
      try {
        return hljs.highlight(lang, str).value;
      } catch (__) {}
    }
    try {
      return hljs.highlightAuto(str).value;
    } catch (__) {}
    return ''; // use external default escaping
  }
});
md.use(require('markdown-it-emoji'), {shortcuts:{}});
md.use(require('markdown-it-footnote'));
md.use(require('markdown-it-sup'));
var mdContainer = require('markdown-it-container');
md.use(mdContainer, ""image-right"");
md.use(mdContainer, ""image-left"");
return function (data, callback) {
    var renderedHtml = md.render(data);
    callback(null, renderedHtml);
}");

    public string Convert(string source) {
        return (string) _markdownItFunc(source).Result;
    }
}