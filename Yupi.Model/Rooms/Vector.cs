﻿using System;
using FluentNHibernate.Data;

namespace Yupi.Model.Rooms
{
	public class Vector : Entity {
		public virtual int X { get; set; }
		public virtual int Y { get; set; }
		public virtual double Z { get; set; } // TODO Why double?

		public Vector() {
			this.X = 0;
			this.Y = 0;
			this.Z = 0;
		}

		public Vector(int x, int y, double z) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
	}
}

