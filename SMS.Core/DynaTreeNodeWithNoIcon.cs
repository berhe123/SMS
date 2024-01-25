using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Core
{
    public class DynaTreeNodeWithNoIcon
    {
        public DynaTreeNodeWithNoIcon() { }
        public DynaTreeNodeWithNoIcon(string title)
        {
            this.title = title;
        }
        /// <summary>
        /// (required) Displayed name of the node (html is allowed here)
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// May be used with activate(), select(), find(), ...
        /// </summary>
        public string key { get; set; } 
        /// <summary>
        /// Use a folder icon. Also the node is expandable but not selectable.
        /// </summary>
        public bool isFolder { get; set; }
        /// <summary>
        /// Call onLazyRead(), when the node is expanded for the first time to allow for delayed creation of children.
        /// </summary>
        public bool isLazy { get; set; }
        /// <summary>
        /// Show this popup text.
        /// </summary>
        public string tooltip { get; set; }
        /// <summary>
        /// Added to the generated <a> tag.
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// Use a custom image (filename relative to tree.options.imagePath). 'null' for default icon, 'false' for no icon.
        /// </summary>
        public bool icon { get; set; }
        /// <summary>
        /// Class name added to the node's span tag.
        /// </summary>
        public string addClass { get; set; }
        /// <summary>
        /// Use <span> instead of <a> tag for this node
        /// </summary>
        public bool noLink { get; set; }
        /// <summary>
        /// Initial active status.
        /// </summary>
        public bool activate { get; set; }
        /// <summary>
        /// Initial focused status.
        /// </summary>
        public bool focus { get; set; }
        /// <summary>
        /// Initial expanded status.
        /// </summary>
        public bool expand { get; set; }
        /// <summary>
        /// Initial selected status.
        /// </summary>
        public bool select { get; set; }
        /// <summary>
        /// Suppress checkbox display for this node.
        /// </summary>
        public bool hideCheckbox { get; set; }
        /// <summary>
        /// Prevent selection.
        /// </summary>
        public bool unselectable { get; set; }
        /// <summary>
        /// Array of child nodes.
        /// </summary>
        public IEnumerable<DynaTreeNodeWithNoIcon> children { get; set; }
        // NOTE: we can also add custom attributes here.
        // This may then also be used in the onActivate(), onSelect() or onLazyTree() callbacks.
    }
}