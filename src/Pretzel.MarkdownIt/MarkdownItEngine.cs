using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using EdgeJs;
using Pretzel.Logic.Extensibility;

namespace Pretzel.MarkdownIt {
    [Export(typeof(ILightweightMarkupEngine))]
    public class MarkdownItEngine : ILightweightMarkupEngine {
        private static readonly Func<object, Task<object>> _markdownItFunc = Edge.Func(@"
var hljs  = require('highlight.js');
var md = require('markdown-it')({
  html: true
});
md.use(require('markdown-it-emoji'), {shortcuts:{}});
md.use(require('markdown-it-footnote'));
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
}