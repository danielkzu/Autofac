﻿// This software is part of the Autofac IoC container
// Copyright (c) 2007 - 2008 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Autofac
{
    /// <summary>
    /// A parameter that can supply values to sites that exactly
    /// match a specified type.
    /// </summary>
    public class TypedParameter : ConstantParameter
    {
        /// <summary>
        /// The type against which sites are matched.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Create a typed parameter for the specified type.
        /// </summary>
        /// <param name="type">The exact type to match.</param>
        /// <param name="value">The parameter value.</param>
        public TypedParameter(Type type, object value)
            : base(value, pi => pi.ParameterType == type)
        {
            Type = Enforce.ArgumentNotNull(type, "type");
        }
    }
}