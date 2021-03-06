using Katrin.AppFramework.UIElements;
using Katrin.AppFramework.Utils;
using DevExpress.XtraEditors;

namespace Katrin.Shell.UIElements
{
	/// <summary>
	/// A <see cref="IUIElementAdapterFactory"/> that produces adapters for NavigatorCustomButtons-related UI Elements.
	/// </summary>
	public class NavigatorCustomButtonUIAdapterFactory : IUIElementAdapterFactory
	{
		/// <summary>
		/// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
		/// </summary>
		/// <param name="uiElement">uiElement for which to return a <see cref="IUIElementAdapter"/></param>
		/// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element</returns>
		public IUIElementAdapter GetAdapter(object uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");

			if (uiElement is NavigatorCustomButtons)
				return new NavigatorCustomButtonUIAdapter((NavigatorCustomButtons)uiElement);
            
			throw ExceptionFactory.CreateInvalidAdapterElementType(uiElement.GetType(), GetType());
		}

		/// <summary>
		/// Indicates if the specified ui element is supported by the adapter factory.
		/// </summary>
		/// <param name="uiElement">UI Element to evaluate</param>
		/// <returns>Returns true for supported elements, otherwise returns false.</returns>
		public bool Supports(object uiElement)
		{
			return uiElement is NavigatorCustomButtons;
		}
	}
}
