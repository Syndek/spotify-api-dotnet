﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Spotify.Web.RequestObjects
{
    internal readonly struct NamedArray<TElement> : IReadOnlyList<TElement>
    {
        private readonly IReadOnlyList<TElement> elements;

        internal NamedArray(String name, IReadOnlyList<TElement> elements)
        {
            this.Name = name;
            this.elements = elements;
        }

        public TElement this[Int32 index] => this.elements[index];

        public String Name { get; }
        public Int32 Count => this.elements.Count;

        public IEnumerator<TElement> GetEnumerator() => this.elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.elements.GetEnumerator();
    }
}