﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Xml;
using OLEDB.Test.ModuleCore;
using XmlCoreTest.Common;
using XmlReaderTest.Common;

namespace XmlReaderTest.ReaderSettingsTest
{
    public partial class TCReaderSettings : TCXMLReaderBaseGeneral
    {
        // Type is XmlReaderTest.ReaderSettingsTest.TCReaderSettings
        // Test Case
        public override void DOTEST()
        {
            base.DOTEST();
            CurVariation = new CVariation(this);
        }
    }
}
