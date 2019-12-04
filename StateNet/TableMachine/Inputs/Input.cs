﻿using System;

namespace Aptacode.StateNet.TableMachine.Inputs
{
    public struct Input : IEquatable<Input>
    {
        public Input(string name) => Name = name;

        public override bool Equals(object obj) => (obj is Input other) && Equals(other);

        public bool Equals(Input other) => Name.Equals(other.Name);

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name;

        public string Name { get; }
    }
}
