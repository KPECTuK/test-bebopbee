using System;
using System.Collections.Specialized;
using System.Reflection;
using ThreeInLine.Services.FSM;
using ThreeInLine.Services.FSM.Implementation;
using ThreeInLine.Services.Interaction;
using ThreeInLine.Services.Logging;
using ThreeInLine.Services.Repository;

namespace ThreeInLine.Services.Container
{
	internal class Container : IContainer
	{
		private readonly ListDictionary _singletons;
		private readonly ListDictionary _providers;

		private static Container _instance;

		public static IContainer CompositionRoot => _instance ?? (_instance = new Container());

		private Container()
		{
			// ReSharper disable once UseObjectOrCollectionInitializer
			_singletons = new ListDictionary(); // because of settings call from ctor.s
			_singletons.Add(typeof(IBoard), new Board(this));
			_singletons.Add(typeof(IFocusing), new Focusing(this));
			//! last one because of dependency (initialize and injection are the same phase)
			_singletons.Add(typeof(IMachine), new Machine(this));
			//
			// ReSharper disable once UseObjectOrCollectionInitializer
			_providers = new ListDictionary();
			_providers.Add(typeof(FirstPlayerTurnWait), _singletons[typeof(IMachine)]);
			_providers.Add(typeof(SecondPlayerTurnWait), _singletons[typeof(IMachine)]);
			_providers.Add(typeof(FirstPlayerTurnPerform), _singletons[typeof(IMachine)]);
			_providers.Add(typeof(SecondPlayerTurnPerform), _singletons[typeof(IMachine)]);
		}

		/// <summary>
		/// [Depreciated] Use with caution, because it makes non-obvious context. 
		/// </summary>
		public void RegisterInstance(object instance) 
		{
			// instances are initializing externally and have no container events responders
			var type = instance.GetType();
			if (_singletons.Contains(type))
				throw new ArgumentException("registered already");
			_singletons.Add(type, instance);
		}

		/// <summary>
		/// [Depreciated] Use with caution, because it makes non-obvious context. 
		/// </summary>
		public void RegisterInstance<T>(object instance) 
		{
			// instances are initializing externally and have no container events responders
			var type = typeof(T);
			if (_singletons.Contains(type))
				throw new ArgumentException("registered already");
			_singletons.Add(type, instance);
		}

		/// <summary>
		/// [Depreciated] Use with caution, because it makes non-obvious context. 
		/// </summary>
			public void RegisterProvider<T>(object provider)
		{
			var type = typeof(T);
			if(_providers.Contains(type))
				throw new ArgumentException("registered already");
			_providers.Add(type, provider);
		}

		public T Resolve<T>() where T : class
		{
			if(_singletons.Contains(typeof(T)))
				return _singletons[typeof(T)] as T;
			if(_providers.Contains(typeof(T)))
				// ReSharper disable once PossibleNullReferenceException
				return (_providers[typeof(T)] as IProvider<T>).Get();
			return
				typeof(T)
					.GetConstructor(
						BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
						null,
						new[] { typeof(IContainer) },
						null)?
					.Invoke(new object[] { this }) as T ??
				Activator.CreateInstance<T>();
		}

		public void Release() { }
	}
}
