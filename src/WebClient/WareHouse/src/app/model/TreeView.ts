export interface TreeView {
    id:         string;
    code:         string;
    name:         string;
    address:      null;
    parentId:     null | string;
    path:         null | string;
    description:  null;
    inactive:     boolean;
    children:     TreeView[];
    active:       boolean;
    data:         null;
    expanded:     boolean;
    extraClasses: null;
    focus:        boolean;
    folder:       boolean;
    hideCheckbox: boolean;
    icon:         null;
    key:          string;
    lazy:         boolean;
    refKey:       null;
    selected:     boolean;
    title:        string;
    tooltip:      string;
    unselectable: boolean;
    level:        number;
}