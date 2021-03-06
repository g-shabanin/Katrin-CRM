using Katrin.AppFramework.UIElements;
using Katrin.AppFramework.Utils;
using DevExpress.XtraEditors.Controls;

namespace Katrin.Shell.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="EditorButtonCollection"/> for use as an <see cref="IUIElementAdapter"/>.
	/// </summary>
	public class EditorButtonCollectionUIAdapter : UIElementAdapter<EditorButton>
	{
		private readonly EditorButtonCollection buttonCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditorButtonCollectionUIAdapter"/> class.
		/// </summary>
		/// <param name="collection"></param>
		public EditorButtonCollectionUIAdapter(EditorButtonCollection collection)
		{
			Guard.ArgumentNotNull(collection, "EditorButtonCollection");
			buttonCollection = collection;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
		/// </summary>
		protected override EditorButton Add(EditorButton uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "EditorButton");

			buttonCollection.Add(uiElement);
			return uiElement;
		}

		/// <summary>
		/// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
		/// </summary>
		protected override void Remove(EditorButton uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "EditorButton");

			int index = buttonCollection.IndexOf(uiElement);
			if (index > -1) 
				buttonCollection.RemoveAt(index);
		}

		/// <summary>
		/// When overridden in a derived class, returns the correct index for the item being added. By default,
		/// it will return the length of the buttonCollection.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected virtual int GetInsertingIndex(object uiElement)
		{
			return buttonCollection.Count;
		}
	}
}
