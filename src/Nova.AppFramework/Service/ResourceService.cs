using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Katrin.AppFramework
{
    /// <summary>
    /// Denotes the interface to provide image and string resource.
    /// </summary>
    public interface IResource
    {
        string GetString(string name);
        Image GetImage(string name);
        CultureInfo CurrentCulture { set; }
    }

    /// <summary>
    /// Defines the interface to manage resources.
    /// </summary>
    public interface IResourceService
    {
        void ChangeLanguage(string language);
        string GetString(string name);
        Image GetImage(string name);
        event EventHandler LanguageChanged;
    }

    /// <summary>
    /// Default implementation of <see cref="IResourceService"/>, it uses MEF to construct the <see cref="IResource"/>.
    /// </summary>
    public class ResourceService : IResourceService
    {
        [ImportMany]
        public IResource[] Resources { get; set; }

        public void ChangeLanguage(string language)
        {
            CultureInfo culture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Resources.Apply(item => item.CurrentCulture = culture);

            IEventAggregator eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Publish(new LanguageChangedMessage());

            if (LanguageChanged != null)
            {
                LanguageChanged(this, null);
            }
        }

        public string GetString(string name)
        {
            string str = null;

            foreach (var resource in Resources)
            {
                str = resource.GetString(name);
                if (str != null)
                {
                    break;
                }
            }
            return str;
        }

        public Image GetImage(string name)
        {
            Image image = null;
            foreach (var resource in Resources)
            {
                image = resource.GetImage(name);
                if (image != null)
                {
                    break;
                }
            }
            return image;
        }

        public event EventHandler LanguageChanged;
    }

    /// <summary>
    /// Denotes the message.
    /// </summary>
    public interface IMessage
    {

    }

    /// <summary>
    /// Concrete <see cref="IMessage"/> published when language changed.
    /// </summary>
    public class LanguageChangedMessage : IMessage
    {

    }
}
