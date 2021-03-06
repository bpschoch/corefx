﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Xml;
using OLEDB.Test.ModuleCore;
using XmlCoreTest.Common;

namespace XmlWriterAPI.Test
{
    public partial class TCOmitXmlDecl : XmlFactoryWriterTestCaseBase
    {
        // Type is XmlWriterAPI.Test.TCOmitXmlDecl
        // Test Case
        public override void AddChildren()
        {
            if (WriterType == WriterType.CustomWriter)
            {
                return;
            }
            // for function XmlDecl_1
            {
                this.AddChild(new CVariation(XmlDecl_1) { Attribute = new Variation("Check when false") { id = 1, Pri = 1 } });
            }


            // for function XmlDecl_2
            {
                this.AddChild(new CVariation(XmlDecl_2) { Attribute = new Variation("Check when true") { id = 2, Pri = 1 } });
            }


            // for function XmlDecl_3
            {
                this.AddChild(new CVariation(XmlDecl_3) { Attribute = new Variation("Set to true, write standalone attribute. Should not write XmlDecl") { id = 3, Pri = 1 } });
            }


            // for function XmlDecl_4
            {
                this.AddChild(new CVariation(XmlDecl_4) { Attribute = new Variation("Set to false, write document fragment. Should not write XmlDecl") { id = 4, Pri = 1 } });
            }


            // for function XmlDecl_5
            {
                this.AddChild(new CVariation(XmlDecl_5) { Attribute = new Variation("WritePI with name = 'xml' text = 'version = 1.0' should work if WriteStartDocument is not called") { id = 5, Pri = 1 } });
            }


            // for function XmlDecl_6
            {
                this.AddChild(new CVariation(XmlDecl_6) { Attribute = new Variation("WriteNode(reader) positioned on XmlDecl, OmitXmlDecl = true") { id = 6, Pri = 1 } });
            }


            // for function XmlDecl_7
            {
                this.AddChild(new CVariation(XmlDecl_7) { Attribute = new Variation("WriteNode(reader) positioned on XmlDecl, OmitXmlDecl = false") { id = 7, Pri = 1 } });
            }
        }
    }
}
