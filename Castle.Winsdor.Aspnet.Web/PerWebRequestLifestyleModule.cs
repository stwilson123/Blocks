// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace Castle.Winsdor.Aspnet.Web
{
	public class PerWebRequestLifestyleModule 
	{
		private const string Key = "castle.per-web-request-lifestyle-cache";

		private static bool initialized = true;

		public static Func<int, IDictionary> FuncHttpCache;

		private static int noInput = 0;
	 
//		public void Init(HttpApplication context)
//		{
//			initialized = true;
//			context.EndRequest += EndRequest;
//		}
 
		public static void EndRequest(Object sender, EventArgs e)
		{
			var scope = GetOrCreateScope(createIfNotPresent: false);
			scope?.Dispose();
		}

		internal static ILifetimeScope AttachScope()
		{
			EnsureInitialized();

			var httpContextItems = FuncHttpCache?.Invoke(noInput);
			if (httpContextItems == null)
			{
				throw new InvalidOperationException("HttpContext.Current is null. PerWebRequestLifestyle can only be used in ASP.Net");
			}
//			var context = HttpContext.Current;
//			if (context == null)
//			{
//				throw new InvalidOperationException("HttpContext.Current is null. PerWebRequestLifestyle can only be used in ASP.Net");
//			}

			return GetOrCreateScope(createIfNotPresent: true);
		}

		internal static ILifetimeScope DetachScope()
		{
			var scope = GetOrCreateScope(createIfNotPresent: true);
			if (scope != null)
			{
				FuncHttpCache?.Invoke(noInput).Remove(Key);
				//HttpContext.Current.Items.Remove(Key);
			}

			return scope;
		}

		private static void EnsureInitialized()
		{
			if (initialized)
			{
				return;
			}

			throw new ComponentResolutionException($"PerWebRequestLifestyleModule was not initialised. Please refer to https://github.com/castleproject/Windsor/blob/master/docs/systemweb-facility.md for more info.");
		}

		private static ILifetimeScope GetOrCreateScope(bool createIfNotPresent)
		{
			var context = FuncHttpCache?.Invoke(noInput);
			if (context == null)
			{
				return null;
			}
//			var context = HttpContext.Current;
//			if (context == null)
//			{
//				return null;
//			}

			var candidates = (ILifetimeScope)context[Key];
			if (candidates == null && createIfNotPresent)
			{
				candidates = new DefaultLifetimeScope(new ScopeCache());
				context[Key] = candidates;
			}

			return candidates;
		}

		public void Dispose()
		{
		}
	}

	public interface IHttpModule
	{
	}
}
