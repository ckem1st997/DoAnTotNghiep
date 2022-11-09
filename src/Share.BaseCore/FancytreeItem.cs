using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore
{
    public class FancytreeItem
    {
        /// <summary>
        /// (Initialization only, but will not be stored with the node.)
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.active">`FancytreeItem.active` on google.com</a></footer>
        public bool active { get; set; }

        /// <summary>Optional array of child nodes.</summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.children">`FancytreeItem.children` on google.com</a></footer>
        public IList<FancytreeItem> children { get; set; }

        /// <summary>
        /// All unknown properties from constructor will be copied to `node.data`.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.data">`FancytreeItem.data` on google.com</a></footer>
        public object data { get; set; }

        /// <summary>
        /// Initial expansion state. Use `node.setExpanded()` or `node.isExpanded()` to access.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.expanded">`FancytreeItem.expanded` on google.com</a></footer>
        public bool expanded { get; set; }

        /// <summary>
        /// Class names added to the node markup (separate with space).
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.extraClasses">`FancytreeItem.extraClasses` on google.com</a></footer>
        public string extraClasses { get; set; }

        /// <summary>
        /// (Initialization only, but will not be stored with the node.)
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.focus">`FancytreeItem.focus` on google.com</a></footer>
        public bool focus { get; set; }

        /// <summary>
        /// Folders have different default icons and honor the `clickFolderMode` option.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.folder">`FancytreeItem.folder` on google.com</a></footer>
        public bool folder { get; set; }

        /// <summary>Remove checkbox for this node.</summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.hideCheckbox">`FancytreeItem.hideCheckbox` on google.com</a></footer>
        public bool hideCheckbox { get; set; }

        /// <summary>
        /// Class names added to the node icon markup to allow custom icons or glyphs (separate with space, e.g. `ui-icon ui-icon-heart`).
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.icon">`FancytreeItem.icon` on google.com</a></footer>
        public string icon { get; set; }

        /// <summary>Unique key for this node (auto-generated if omitted)</summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.key">`FancytreeItem.key` on google.com</a></footer>
        public string key { get; set; }

        /// <summary>
        /// Lazy folders call the `lazyLoad` on first expand to load their children.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.lazy">`FancytreeItem.lazy` on google.com</a></footer>
        public bool lazy { get; set; }

        /// <summary>(Reserved, used by 'clones' extension.)</summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.refKey">`FancytreeItem.refKey` on google.com</a></footer>
        public string refKey { get; set; }

        /// <summary>
        /// Initial selection state. Use `node.setSelected()` or `node.isSelected()` to access.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.selected">`FancytreeItem.selected` on google.com</a></footer>
        public bool selected { get; set; }

        /// <summary>
        /// Node text (may contain HTML tags). Use `node.setTitle()` to modify.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.title">`FancytreeItem.title` on google.com</a></footer>
        public string title { get; set; }

        /// <summary>
        /// Will be added as `title` attribute, thus enabling a tooltip.
        /// </summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.tooltip">`FancytreeItem.tooltip` on google.com</a></footer>
        public string tooltip { get; set; }
        public string id { get; set; }

        /// <summary>Prevent selection using mouse or keyboard.</summary>
        /// <footer><a href="https://www.google.com/search?q=VTQT.Web.Framework.Modelling.FancytreeItem.unselectable">`FancytreeItem.unselectable` on google.com</a></footer>
        public bool unselectable { get; set; }

        public int level { get; set; }

        public FancytreeItem() => this.children = (IList<FancytreeItem>)new List<FancytreeItem>();
    }
}