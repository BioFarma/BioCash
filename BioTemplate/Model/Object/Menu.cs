using System.Collections.Generic;

namespace BioTemplate.Model.Object
{
    public class Menu : BioTemplate.Controller.Helper.ITreeNode<Menu>
    {
        //Current Node Information
        protected int _menuId;
        protected string _menuName;
        protected string _navUrl;
        protected string _iconClass;
	//Pointer into Parent Node (ParentId will be the _menuId of this _parent object)
        protected Menu _parent;
	//Pointer into Children Node
        protected IList<Menu> _children;

	/// <summary>
	/// Using ITreeNode Helper Function, which is a helper function to convert flat array into tree node
	/// We have to implement the ITreeNode interface (ITreeNode<ClassObject>) into the object class (using inheritance)
	/// We also have to provide at least three properties in the object class with pointed name and type, as because it was available in the Interface
	/// so we have to implement it. The three properties are :
	/// Id : Int
	/// Parent : Class Object
	/// Children : IList<ClassObject>
	/// </summary>

        public int Id
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }

        public string NavUrl
        {
            get { return _navUrl; }
            set { _navUrl = value; }
        }

        public string IconClass
        {
            get { return _iconClass; }
            set { _iconClass = value; }
        }

	public Menu Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

	public IList<Menu> Children
	{
	    get { return _children; }
	    set { _children = value; }
	}

        public Menu() { }

        public Menu(int menuId, string menuName, Menu parentId, string navUrl)
        {
            _menuId = menuId;
            _menuName = menuName;
            _parent = parentId;
            _navUrl = navUrl;
        }

        public Menu(int menuId, string menuName, Menu parentId, string navUrl, string iconClass)
        {
            _menuId = menuId;
            _menuName = menuName;
            _parent = parentId;
            _navUrl = navUrl;
            _iconClass = iconClass;
        }
    }
}