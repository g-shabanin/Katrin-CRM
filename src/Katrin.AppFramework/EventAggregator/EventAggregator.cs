using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Katrin.AppFramework
{
    /*
        * 
        * License: 
        * 
        *     Microsoft Public License (MS-PL)
        *     
        *     This license governs use of the accompanying software. If you use the software, you
        *     accept this license. If you do not accept the license, do not use the software.
        *     
        *     1. Definitions
        *     The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
        *     same meaning here as under U.S. copyright law.
        *     A "contribution" is the original software, or any additions or changes to the software.
        *     A "contributor" is any person that distributes its contribution under this license.
        *     "Licensed patents" are a contributor's patent claims that read directly on its contribution.
        *     
        *     2. Grant of Rights
        *     (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
        *     (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
        *     
        *     3. Conditions and Limitations
        *     (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
        *     (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
        *     (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
        *     (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
        *     (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
        * 
        * Little bit of history:
        *     EventAggregator origins based on work from StatLight's EventAggregator. Which 
        *     is based on original work by Jermey Miller's EventAggregator in StoryTeller 
        *     with some concepts pulled from Rob Eisenberg in caliburnmicro.
        * 
        * TODO:
        *     - Possibly provide well defined initial thread marshalling actions (depending on platform (WinForm, WPF, Silverlight, WP7???)
        *     - Document the public API better.
        *		
        * Thanks to:
        *     - Jermey Miller - initial implementation
        *     - Rob Eisenberg - pulled some ideas from the caliburn micro event aggregator
        *     - Jake Ginnivan - https://github.com/JakeGinnivan - thanks for the pull requests
        * 
        */

    /// <summary>
    /// Specifies a class that would like to receive particular messages.
    /// </summary>
    /// <typeparam name="TMessage">The type of message object to subscribe to.</typeparam>
#if WINDOWS_PHONE
    public interface IListener<TMessage>
#else
    public interface IListener<in TMessage>
#endif
    {
        /// <summary>
        /// This will be called every time a TMessage is published through the event aggregator
        /// </summary>
        void Handle(TMessage message);
    }

    /// <summary>
    /// Provides a way to add and remove a listener object from the EventAggregator
    /// </summary>
    public interface IEventSubscriptionManager
    {
        /// <summary>
        /// Adds the given listener object to the EventAggregator.
        /// </summary>
        /// <param name="listener">Object that should be implementing IListener(of T's), this overload is used when your listeners to multiple message types</param>
        /// <param name="holdStrongReference">determines if the EventAggregator should hold a weak or strong reference to the listener object. If null it will use the Config level option unless overriden by the parameter.</param>
        /// <returns>Returns the current IEventSubscriptionManager to allow for easy fluent additions.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        IEventSubscriptionManager AddListener(object listener, bool? holdStrongReference = null);

        /// <summary>
        /// Adds the given listener object to the EventAggregator.
        /// </summary>
        /// <typeparam name="T">Listener Message type</typeparam>
        /// <param name="listener"></param>
        /// <param name="holdStrongReference">determines if the EventAggregator should hold a weak or strong reference to the listener object. If null it will use the Config level option unless overriden by the parameter.</param>
        /// <returns>Returns the current IEventSubscriptionManager to allow for easy fluent additions.</returns>
        IEventSubscriptionManager AddListener<T>(IListener<T> listener, bool? holdStrongReference = null);

        /// <summary>
        /// Removes the listener object from the EventAggregator
        /// </summary>
        /// <param name="listener">The object to be removed</param>
        /// <returns>Returnes the current IEventSubscriptionManager for fluent removals.</returns>
        IEventSubscriptionManager RemoveListener(object listener);
    }

    public interface IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        void SendMessage<TMessage>(TMessage message, Action<Action> marshal = null);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        void SendMessage<TMessage>(Action<Action> marshal = null)
            where TMessage : new();
    }

    public interface IEventAggregator : IEventPublisher, IEventSubscriptionManager { }

    public class EventAggregator : IEventAggregator
    {
        private readonly ListenerWrapperCollection _listeners;
        private readonly Config _config;

        public EventAggregator()
            : this(new Config())
        {
        }

        public EventAggregator(Config config)
        {
            _config = config;
            _listeners = new ListenerWrapperCollection();
        }

        /// <summary>
        /// This will send the message to each IListener that is subscribing to TMessage.
        /// </summary>
        /// <typeparam name="TMessage">The type of message being sent</typeparam>
        /// <param name="message">The message instance</param>
        /// <param name="marshal">You can optionally override how the message publication action is marshalled</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void SendMessage<TMessage>(TMessage message, Action<Action> marshal = null)
        {
            if (marshal == null)
                marshal = _config.DefaultThreadMarshaler;

            Call<IListener<TMessage>>(message, marshal);
        }

        /// <summary>
        /// This will create a new default instance of TMessage and send the message to each IListener that is subscribing to TMessage.
        /// </summary>
        /// <typeparam name="TMessage">The type of message being sent</typeparam>
        /// <param name="marshal">You can optionally override how the message publication action is marshalled</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public void SendMessage<TMessage>(Action<Action> marshal = null)
            where TMessage : new()
        {
            SendMessage(new TMessage(), marshal);
        }

        private void Call<TListener>(object message, Action<Action> marshaller)
            where TListener : class
        {
            int listenerCalledCount = 0;
            marshaller(() =>
            {
                foreach (ListenerWrapper o in _listeners.Where(o => o.Handles<TListener>()))
                {
                    bool wasThisOneCalled;
                    o.TryHandle<TListener>(message, out wasThisOneCalled);
                    if (wasThisOneCalled)
                        listenerCalledCount++;
                }
            });

            var wasAnyListenerCalled = listenerCalledCount > 0;

            if (!wasAnyListenerCalled)
            {
                _config.OnMessageNotPublishedBecauseZeroListeners(message);
            }
        }

        public IEventSubscriptionManager AddListener(object listener)
        {
            return AddListener(listener, null);
        }

        public IEventSubscriptionManager AddListener(object listener, bool? holdStrongReference)
        {
            bool holdRef = _config.HoldReferences;
            if (holdStrongReference.HasValue)
                holdRef = holdStrongReference.Value;
            _listeners.AddListener(listener, holdRef);

            return this;
        }

        public IEventSubscriptionManager AddListener<T>(IListener<T> listener, bool? holdStrongReference)
        {
            AddListener((object)listener, holdStrongReference);

            return this;
        }

        public IEventSubscriptionManager RemoveListener(object listener)
        {
            _listeners.RemoveListener(listener);
            return this;
        }

        /// <summary>
        /// Wrapper collection of ListenerWrappers to manage things like 
        /// threadsafe manipulation to the collection, and convenience 
        /// methods to configure the collection
        /// </summary>
        class ListenerWrapperCollection : IEnumerable<ListenerWrapper>
        {
            private readonly List<ListenerWrapper> _listeners = new List<ListenerWrapper>();
            private readonly object _sync = new object();

            public void RemoveListener(object listener)
            {
                ListenerWrapper listenerWrapper;
                lock (_sync)
                    if (TryGetListenerWrapperByListener(listener, out listenerWrapper))
                        _listeners.Remove(listenerWrapper);
            }

            private void RemoveListenerWrapper(ListenerWrapper listenerWrapper)
            {
                lock (_sync)
                    _listeners.Remove(listenerWrapper);
            }

            public IEnumerator<ListenerWrapper> GetEnumerator()
            {
                lock (_sync)
                    return _listeners.ToList().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private bool ContainsListener(object listener)
            {
                ListenerWrapper listenerWrapper;
                return TryGetListenerWrapperByListener(listener, out listenerWrapper);
            }

            private bool TryGetListenerWrapperByListener(object listener, out ListenerWrapper listenerWrapper)
            {
                lock (_sync)
                    listenerWrapper = _listeners.SingleOrDefault(x => x.ListenerInstance == listener);

                return listenerWrapper != null;
            }

            public void AddListener(object listener, bool holdStrongReference)
            {
                lock (_sync)
                {

                    if (ContainsListener(listener))
                        return;

                    var listenerWrapper = new ListenerWrapper(listener, RemoveListenerWrapper, holdStrongReference);

                    _listeners.Add(listenerWrapper);
                }
            }
        }

        #region IReference

        private interface IReference
        {
            object Target { get; }
        }

        private class WeakReferenceImpl : IReference
        {
            private readonly WeakReference _reference;

            public WeakReferenceImpl(object listener)
            {
                _reference = new WeakReference(listener);
            }

            public object Target
            {
                get { return _reference.Target; }
            }
        }

        private class StrongReferenceImpl : IReference
        {
            private readonly object _target;

            public StrongReferenceImpl(object target)
            {
                _target = target;
            }

            public object Target
            {
                get { return _target; }
            }
        }
        #endregion

        class ListenerWrapper
        {
            private const string HandleMethodName = "Handle";
            private readonly Dictionary<Type, MethodInfo> _supportedListeners = new Dictionary<Type, MethodInfo>();
            private readonly Action<ListenerWrapper> _onRemoveCallback;
            private readonly IReference _reference;

            private static IEnumerable<Type> GetBaseInterfaceType(Type type)
            {
                if (type == null)
                    return new Type[0];

#if NETFX_CORE
                List<Type> interfaces = type.GetTypeInfo().ImplementedInterfaces.ToList();
#else
                List<Type> interfaces = type.GetInterfaces().ToList();
#endif

                foreach (var @interface in interfaces.ToArray())
                {
                    interfaces.AddRange(GetBaseInterfaceType(@interface));
                }

#if NETFX_CORE
                if (type.GetTypeInfo().IsInterface)
#else
                if (type.IsInterface)
#endif
                {
                    interfaces.Add(type);
                }

                return interfaces.Distinct();
            }

            public ListenerWrapper(object listener, Action<ListenerWrapper> onRemoveCallback, bool holdReferences)
            {
                _onRemoveCallback = onRemoveCallback;

                if (holdReferences)
                    _reference = new StrongReferenceImpl(listener);
                else
                    _reference = new WeakReferenceImpl(listener);

                var listenerInterfaces = GetBaseInterfaceType(listener.GetType())
                    .Where(w => DirectlyClosesGeneric(w, typeof(IListener<>)));

                foreach (var listenerInterface in listenerInterfaces)
                {
#if NETFX_CORE
                    var typeInfo = listenerInterface.GetTypeInfo();
                    var handleMethod = typeInfo.GetDeclaredMethod(HandleMethodName);
                    var messageType = typeInfo.GenericTypeArguments.First();
#else
                    var handleMethod = listenerInterface.GetMethod(HandleMethodName);
                    var messageType = listenerInterface.GetGenericArguments().First();
#endif
                    _supportedListeners.Add(messageType, handleMethod);
                }
            }

            public object ListenerInstance { get { return _reference.Target; } }

            public bool Handles<TListener>()
                where TListener : class
            {
#if NETFX_CORE
                var messageType = typeof(TListener).GetTypeInfo().GenericTypeArguments.First();
#else
                var messageType = typeof(TListener).GetGenericArguments().First();
#endif
                if (_supportedListeners.ContainsKey(messageType))
                    return true;
                return false;
            }

            public void TryHandle<TListener>(object message, out bool wasHandled)
                where TListener : class
            {
                var target = _reference.Target;
                wasHandled = false;
                if (target == null)
                {
                    _onRemoveCallback(this);
                    return;
                }

#if NETFX_CORE
                var messageType = typeof(TListener).GetTypeInfo().GenericTypeArguments.First();
#else
                var messageType = typeof(TListener).GetGenericArguments().First();
#endif

                if (!_supportedListeners.ContainsKey(messageType))
                    return;

                _supportedListeners[messageType].Invoke(target, new[] { message });
                wasHandled = true;
            }

            private static bool DirectlyClosesGeneric(Type type, Type openType)
            {
                if (type == null)
                    return false;

#if NETFX_CORE
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == openType)
#else
                if (type.IsGenericType && type.GetGenericTypeDefinition() == openType)
#endif
                {
                    return true;
                }

                return false;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class Config
        {
            private Action<object> _onMessageNotPublishedBecauseZeroListeners = msg => { /* TODO: possibly Trace message?*/ };
            public Action<object> OnMessageNotPublishedBecauseZeroListeners
            {
                get { return _onMessageNotPublishedBecauseZeroListeners; }
                set { _onMessageNotPublishedBecauseZeroListeners = value; }
            }

            private Action<Action> _defaultThreadMarshaler = action => action();
            public Action<Action> DefaultThreadMarshaler
            {
                get { return _defaultThreadMarshaler; }
                set { _defaultThreadMarshaler = value; }
            }

            /// <summary>
            /// If true instructs the EventAggregator to hold onto a reference to all listener objects. You will then have to explicitly remove them from the EventAggrator.
            /// If false then a WeakReference is used and the garbage collector can remove the listener when not in scope any longer.
            /// </summary>
            public bool HoldReferences { get; set; }
        }
    }
}