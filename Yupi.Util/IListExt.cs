﻿using System;
using System.Collections.Generic;

namespace Yupi.Util
{
	public static class IListExt
	{
		public static int RemoveAll<T>(this IList<T> list, Predicate<T> match)
		{
			if (list == null || match == null) {
				return 0;
			}

			int count = 0;

			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (match(list[i]))
				{
					++count;
					list.RemoveAt(i);
				}
			}

			return count;
		}        
	}
}
