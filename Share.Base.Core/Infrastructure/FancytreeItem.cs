using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Infrastructure
{
    public class FancytreeItem
    {
        
        public bool active { get; set; }

        /// <summary>Optional array of child nodes.</summary>
        public IList<FancytreeItem> children { get; set; }

        /// <summary>
        /// All unknown properties from constructor will be copied to `node.data`.
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// Initial expansion state. Use `node.setExpanded()` or `node.isExpanded()` to access.
        /// </summary>
        public bool expanded { get; set; }

        /// <summary>
        /// Class names added to the node markup (separate with space).
        /// </summary>
        public string extraClasses { get; set; }

        /// <summary>
        /// (Initialization only, but will not be stored with the node.)
        /// </summary>
        public bool focus { get; set; }

        /// <summary>
        /// Folders have different default icons and honor the `clickFolderMode` option.
        /// </summary>
        public bool folder { get; set; }

        /// <summary>Remove checkbox for this node.</summary>
        public bool hideCheckbox { get; set; }

        /// <summary>
        /// Class names added to the node icon markup to allow custom icons or glyphs (separate with space, e.g. `ui-icon ui-icon-heart`).
        /// </summary>
        public string icon { get; set; }

        /// <summary>Unique key for this node (auto-generated if omitted)</summary>
        public string key { get; set; }

        /// <summary>
        /// Lazy folders call the `lazyLoad` on first expand to load their children.
        /// </summary>
        public bool lazy { get; set; }

        /// <summary>(Reserved, used by 'clones' extension.)</summary>
        public string refKey { get; set; }

        /// <summary>
        /// Initial selection state. Use `node.setSelected()` or `node.isSelected()` to access.
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// Node text (may contain HTML tags). Use `node.setTitle()` to modify.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Will be added as `title` attribute, thus enabling a tooltip.
        /// </summary>
        public string tooltip { get; set; }
        public string id { get; set; }

        /// <summary>Prevent selection using mouse or keyboard.</summary>
        public bool unselectable { get; set; }

        public int level { get; set; }

        public FancytreeItem() => children = new List<FancytreeItem>();
    }
}